using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
    public static class SocketExtension
    {
        /// <summary>
        /// Receive datas from socket and process them
        /// The receive operation and data process operation is separate from each other 
        /// so that each of them could be more advanced to do their job
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public static Task ReadAllAsync(this Socket socket)
        {
            var pipe = new Pipe();

            Task writing = FillPipeAsync(socket, pipe.Writer);
            Task reading = ReadPipeAsync(pipe.Reader);

            return Task.WhenAll(writing, reading);
        }

        /// <summary>
        /// Reads data from socket and write into pipeline
        /// </summary>
        /// <param name="socket">The socket client</param>
        /// <param name="writer">The <see cref="PipeWriter"/> that receive data and processed by <see cref="PipeReader"/></param>
        /// <returns></returns>
        private static async Task FillPipeAsync(Socket socket, PipeWriter writer)
        {
            const int minimumBufferSize = 512;

            while (true)
            {
                // Allocate at least 512 bytes from the PipeWriter
                Memory<byte> memory = writer.GetMemory(minimumBufferSize);
                try
                {
                    int bytesRead = await socket.ReceiveAsync(memory, SocketFlags.None);
                    if (bytesRead == 0)
                        break;

                    writer.Advance(bytesRead);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    break;
                }

                // Make the data avaliable to the PipeReader
                FlushResult result = await writer.FlushAsync();

                if (result.IsCompleted)
                {
                    break;
                }
            }

            // Tell the PipeReader that there's no more data coming
            writer.Complete();
        }

        /// <summary>
        /// Reads data from Pipeline and parses into line
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static async Task ReadPipeAsync(PipeReader reader)
        {
            while (true)
            {
                ReadResult result = await reader.ReadAsync();

                ReadOnlySequence<byte> buffer = result.Buffer;
                SequencePosition? position = null;

                do
                {
                    // Look for a EOL in the buffer
                    position = buffer.PositionOf((byte)'\n');

                    if (position != null)
                    {
                        ProcessLine(buffer.Slice(0, position.Value));

                        // Skip the line + the \n character (basically position)
                        buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
                    }
                }
                while (position != null);

                // Tell the PipeReader how much of the buffer we have consumed
                reader.AdvanceTo(buffer.Start, buffer.End);

                // Stop reading if there's no more data coming
                if (result.IsCompleted)
                {
                    break;
                }
            }

            // Make the PipeReader as complete
            reader.Complete();
        }

        private static void ProcessLine(ReadOnlySequence<byte> readOnlySequence)
        {
            foreach (var item in readOnlySequence)
            {
                var msg = Encoding.ASCII.GetString(item.Span);
                Console.Write(msg);
            }
        }

        #region Infrastructure

        /// <summary>
        /// Log errpr message
        /// </summary>
        /// <param name="ex"></param>
        private static void LogError(Exception ex)
        {
            switch (ex)
            {
                case IOException ioEx:
                    Console.WriteLine($"An exception happened with the IO operation: {ioEx.Message}");
                    break;
                default:
                    Console.WriteLine(ex.Message);
                    break;
            }
        }

        #endregion
    }
}

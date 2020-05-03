using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
    public static class NetworkStreamExtension
    {
        #region Private Methods

        /// <summary>
        /// Process a line that is transfered from a buffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="strat"></param>
        /// <param name="count"></param>
        private static void ProcessLine(byte[] buffer, int strat, int count)
        {
            var msg = Encoding.ASCII.GetString(buffer, strat, count);
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Process line from a segment list
        /// </summary>
        private static void ProcessLine(List<BufferSegment> segments, int segmentOffset, int consumedOffset)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < segments.Count; i++)
            {
                var segment = segments[i];
                // Get the offset from start index
                var offset = i == 0 ? consumedOffset : 0;
                // Get the count of bytes needs to parse
                var count = i == segments.Count - 1 ? segmentOffset - offset + 1 : segment.Count - offset;
                var msg = Encoding.ASCII.GetString(segments[i].Buffer, offset, count);
                sb.Append(msg);
            }
            Console.Write(sb);
        }


        #endregion

        #region Extensions

        /// <summary>
        /// The extension process a single line from network stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task ProcessSingleLineAsync(this NetworkStream stream)
        {
            var buffer = new byte[1024];
            // In a single ReadAsync operation, a single line may not return. So this buffered the length of readed bytes
            var bytesBuffered = 0;
            // The data length that has consumed in buffer
            var bytesConsumed = 0;

            while (true)
            {
                // Read data from stream
                var bytesRead = await stream.ReadAsync(buffer, bytesBuffered, buffer.Length - bytesBuffered);

                // End of read
                if (bytesRead == 0)
                    break;

                // Keep track of the amount of bufferd bytes
                bytesBuffered += bytesRead;

                var linePosition = -1;

                do
                {
                    linePosition = Array.IndexOf(buffer, (byte)'\n', bytesConsumed, bytesBuffered - bytesConsumed);

                    if (linePosition >= 0)
                    {
                        // The length of current line
                        var lineLength = linePosition - bytesConsumed;

                        // Process the received data in current loop
                        ProcessLine(buffer, bytesConsumed, lineLength);

                        // Add the consumed byte's length
                        bytesConsumed += lineLength + 1;
                    }
                }
                while (linePosition >= 0);
            }
        }

        /// <summary>
        /// The extension process multiple line in a single ReadAsync call.
        /// And auto growing the buffer into twice length then buffer length is not enough of saving data.
        /// The buffer allocation is controled by <see cref="ArrayPool<byte>"/> to avoid the heap allocation.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task ProcessMultipleLineWithAutoGrowthBufferAsync(this NetworkStream stream)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(1024);

            var bytesBuffered = 0;
            var bytesConsumed = 0;

            while (true)
            {
                // Get the remained buffer length
                var bytesRemaining = buffer.Length - bytesBuffered;

                // If the remaining buffer is zero, double the buffer space
                if (bytesRemaining == 0)
                {
                    // Rent double length from ArrayPool
                    var newBuffer = ArrayPool<byte>.Shared.Rent(buffer.Length * 2);

                    // Copy the buffered data into new buffer
                    Buffer.BlockCopy(buffer, 0, newBuffer, 0, buffer.Length);

                    // Return the old buffer space to ArrayPool
                    ArrayPool<byte>.Shared.Return(buffer);

                    buffer = newBuffer;
                    bytesRemaining = buffer.Length - bytesBuffered;
                }

                var bytesRead = await stream.ReadAsync(buffer, bytesBuffered, buffer.Length - bytesBuffered);

                if (bytesRead == 0)
                    break;

                bytesBuffered += bytesRead;


                int linePosition = 0;

                do
                {
                    linePosition = Array.IndexOf(buffer, (byte)'\n', bytesConsumed, bytesBuffered - bytesConsumed);

                    if (linePosition >= 0)
                    {
                        // The length of current line
                        var lineLength = linePosition - bytesConsumed;

                        // Process the received data in current loop
                        ProcessLine(buffer, bytesConsumed, lineLength);

                        // Add the consumed byte's length
                        bytesConsumed += lineLength + 1;
                    }
                }
                while (linePosition >= 0);
            }
        }

        /// <summary>
        /// Extend the buffer size by BufferSegment class each time, that avoid the copy of buffer.
        /// In buffer will grow when it is remaining buffer is less than 512 bytes.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task ProcessLineWithAdvancedGrowthBufferAndBufferRecycleAsync(this NetworkStream stream)
        {
            const int minimumBufferSize = 512;

            var segments = new List<BufferSegment>();
            // The index of consumed bytes in the first segment
            var bytesConsumed = 0;
            // The bytes that buffered in current segment
            var bytesBuffered = 0;
            var segment = new BufferSegment { Buffer = ArrayPool<byte>.Shared.Rent(1024) };

            segments.Add(segment);

            while (true)
            {
                if (segment.Remaining < minimumBufferSize)
                {
                    // Remove the current segment if it has not any valid data
                    if (segment.Count == bytesConsumed)
                    {
                        segments.RemoveAt(0);
                        bytesConsumed = 0;
                    }
                    // Allocate a new segment
                    segment = new BufferSegment { Buffer = ArrayPool<byte>.Shared.Rent(1024) };
                    segments.Add(segment);
                    bytesBuffered = 0;
                }
                var bytesRead = 0;
                try
                {
                    bytesRead = await stream.ReadAsync(segment.Buffer, segment.Count, segment.Remaining);
                }
                catch (IOException e)
                {
                    Console.WriteLine($"{e.Message}");
                    return;
                }
                if (bytesRead == 0)
                        break;
                segment.Count += bytesRead;
                bytesBuffered += bytesRead;

                // Process multiple line
                while (true)
                {
                    // Look for line end delimiter
                    var startIndex = segments.Count == 1 ? bytesConsumed : 0;
                    var count = segments.Count == 1 ? segment.Count - bytesConsumed : segment.Count;
                    var linePosition = Array.IndexOf(segment.Buffer, (byte)'\n', startIndex, count);
                    if (linePosition >= 0)
                    {
                        ProcessLine(segments, linePosition, bytesConsumed);

                        bytesConsumed = linePosition + 1;

                        // Drop fully consumed segments from the list so we don't look at them again
                        for (int i = segments.Count - 1; i >= 0; --i)
                        {
                            var consumedSegment = segments[i];

                            // Return all segment unless the last segment
                            if (consumedSegment != segment)
                            {
                                ArrayPool<byte>.Shared.Return(consumedSegment.Buffer);
                                segments.RemoveAt(i);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Buffer segment for line which is very large.
        /// </summary>
        public class BufferSegment
        {
            public byte[] Buffer { get; set; }
            public int Count { get; set; }

            public int Remaining => Buffer.Length - Count;
        }

        #endregion
    }
}

using System.Runtime.InteropServices;

namespace System.Text
{
    public static class EncodingExtension
    {
        public static string GetString(this Encoding encoding, ReadOnlyMemory<byte> memory)
        {
            var array = GetArray(memory);
            return encoding.GetString(array);
        }

        private static ArraySegment<byte> GetArray(ReadOnlyMemory<byte> memory)
        {
            if (!MemoryMarshal.TryGetArray(memory, out var result))
            {
                throw new InvalidOperationException("Buffer backed by array was expected");
            }

            return result;
        }
    }
}

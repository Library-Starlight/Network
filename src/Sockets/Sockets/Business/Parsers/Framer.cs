using System;
using System.IO;

namespace Sockets.Business.Parsers
{
    public static class Framer
    {
        public static byte[] NextToken(Stream input, byte[] delimiter)
        {
            int nextByte;

            if ((nextByte = input.ReadByte()) == -1) return null;

            using var tokenBuffer = new MemoryStream();
            do 
            {
                tokenBuffer.WriteByte((byte)nextByte);
                byte[] currentToken = tokenBuffer.ToArray();
                if (EndsWith(currentToken, delimiter))
                {
                    int tokenLength = currentToken.Length - delimiter.Length;
                    byte[] token = new byte[tokenLength];
                    Array.Copy(currentToken, 0, token, 0, token.Length);
                    return token;
                }
            } while ((nextByte = input.ReadByte()) != -1);

            return tokenBuffer.ToArray();
        }

        private static bool EndsWith(byte[] value, byte[] delimiter)
        {
            if (value.Length != delimiter.Length)
                return false;
            
            for (int i = 1; i <= delimiter.Length; i++)
            {
                if (value[value.Length - i] != delimiter[delimiter.Length - i])
                    return false;
            }
            
            return true;
        }
    }
}

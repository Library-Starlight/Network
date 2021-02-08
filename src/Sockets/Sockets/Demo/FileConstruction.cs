using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sockets.Demo
{
    public class FileConstruction
    {
        /// <summary>
        /// 写文件
        /// </summary>
        public static void WriteFile()
        {
            Console.WriteLine(BitConverter.ToString(System.Text.Encoding.UTF8.GetBytes("哈")));
            Console.WriteLine(System.Web.HttpUtility.UrlPathEncode("哈"));

            // 1a
            var utf8Data = new byte[] { 0xEF, 0xBB, 0xBF, 0x31, 0x61, 0xc8, 0x54 };
            Write("UTF8-w.txt", utf8Data);

            var unicodeData = new byte[] { 0xFF, 0xFE, 0x31, 0x00, 0x61, 0x00, 0xc8, 0x54 };
            Write("Unicode-w.txt", unicodeData);
        }

        private static void Write(string file, byte[] data)
        {
            file = GetPath(file);
            using var stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Dispose();

            using var stream1 = new FileStream(file, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(stream1);
            Console.WriteLine(sr.ReadToEnd());
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        public static void ReadFile()
        {
            Read("utf8.txt");
            Read("unicode.txt");

            Console.WriteLine("\u0031");
        }

        private static void Read(string file)
        {
            Console.WriteLine($"File: {file}");
            using var stream = new FileStream(GetPath(file), FileMode.Open, FileAccess.Read);
            Console.WriteLine(stream.Length);
            var data = new byte[stream.Length];
            while (stream.CanRead)
            {
                var b = stream.ReadByte();
                if (b == -1) break;
                data[stream.Position - 1] = (byte)b;
            }

            Console.WriteLine(BitConverter.ToString(data));
        }

        private static string GetPath(string fileName)
            => $"Demo\\Files\\{fileName}";
    }
}
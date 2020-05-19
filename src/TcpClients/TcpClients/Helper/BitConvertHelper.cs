using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Helper
{
    public class BitConvertHelper
    {
        /// <summary>
        /// 将<see cref="byte[]"/>转换为<see cref="UInt16"/>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="index">起始位置</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>转换结果</returns>
        public static ushort ToUInt16(byte[] data, int index, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
                return BitConverter.ToUInt16(data, index);
            var buffer = new byte[2];
            Array.Copy(data, index, buffer, 0, 2);
            buffer = buffer.Reverse().ToArray();
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// 将<see cref="byte[]"/>转换为<see cref="Int16"/>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="index">起始位置</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>转换结果</returns>
        public static short ToInt16(byte[] data, int index, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
                return BitConverter.ToInt16(data, index);
            var buffer = new byte[4];
            Array.Copy(data, index, buffer, 0, 4);
            buffer = buffer.Reverse().ToArray();
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// 将<see cref="byte[]"/>转换为<see cref="UInt32"/>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="index">起始位置</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>转换结果</returns>
        public static uint ToUInt32(byte[] data, int index, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
                return BitConverter.ToUInt32(data, index);
            var buffer = new byte[4];
            Array.Copy(data, index, buffer, 0, 4);
            buffer = buffer.Reverse().ToArray();
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// 将<see cref="byte[]"/>转换为<see cref="Int32"/>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="index">起始位置</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>转换结果</returns>
        public static int ToInt32(byte[] data, int index, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
                return BitConverter.ToInt32(data, index);
            var buffer = new byte[4];
            Array.Copy(data, index, buffer, 0, 4);
            buffer = buffer.Reverse().ToArray();
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// 将<see cref="byte[]"/>转换为<see cref="Int64"/>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="index">起始位置</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>转换结果</returns>
        public static long ToInt64(byte[] data, int index, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
                return BitConverter.ToInt64(data, index);
            var buffer = new byte[4];
            Array.Copy(data, index, buffer, 0, 4);
            buffer = buffer.Reverse().ToArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 将<see cref="byte[]"/>转换为<see cref="UInt64"/>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="index">起始位置</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>转换结果</returns>
        public static ulong ToUInt64(byte[] data, int index, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
                return BitConverter.ToUInt64(data, index);
            var buffer = new byte[4];
            Array.Copy(data, index, buffer, 0, 4);
            buffer = buffer.Reverse().ToArray();
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// 将<see cref="byte[]"/>转换为<see cref="Single"/>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="index">起始位置</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>转换结果</returns>
        public static float ToSingle(byte[] data, int index, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
                return BitConverter.ToSingle(data, index);
            var buffer = new byte[4];
            Array.Copy(data, index, buffer, 0, 4);
            buffer = buffer.Reverse().ToArray();
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// 获取<see cref="ushort"/>数据编码
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>数据数组</returns>
        public static byte[] GetBytes(ushort value, bool isLittleEndian = false)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian != isLittleEndian)
                data = data.Reverse().ToArray();
            return data;
        }

        /// <summary>
        /// 获取<see cref="uint"/>数据编码
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>数据数组</returns>
        public static byte[] GetBytes(uint value, bool isLittleEndian = false)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian != isLittleEndian)
                data = data.Reverse().ToArray();
            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TcpClients.Helper
{
    public static class CrcCheckHelper
    {
        private static readonly Crc16 _crc16 = new Crc16();

        /// <summary>
        /// CRC校验，根据预期字节数据与预期校验和，计算是否校验成功
        /// </summary>
        /// <param name="data">待校验数据</param>
        /// <param name="start">起始字节</param>
        /// <param name="len">校验长度</param>
        /// <param name="exceptCheckSum">预期校验和</param>
        /// <returns>校验结果</returns>
        public static bool CheckSum(byte[] data, int start, int len, ushort exceptCheckSum)
        {
            var realCheckSum = GetCheckSum(data, start, len);
            return realCheckSum == exceptCheckSum;
        }

        /// <summary>
        /// 计算CRC校验和
        /// </summary>
        /// <param name="data">待计算校验和的数组</param>
        /// <returns>校验和</returns>
        public static ushort GetCheckSum(byte[] data, int start, int len)
        {
            return _crc16.ComputeChecksum(data, start, len);
        }


		public class Crc16
		{
			const ushort polynomial = 0xA001;
			readonly ushort[] table = new ushort[256];
			public ushort ComputeChecksum(byte[] bytes, int start, int len)
			{
				ushort crc = 0;
				var end = len + start;
				for (int i = start; i < end; ++i)
				{
					byte index = (byte)(crc ^ bytes[i]);
					crc = (ushort)((crc >> 8) ^ table[index]);
				}

				return crc;
			}

			public byte[] ComputeChecksumBytes(byte[] bytes, int start, int len)
			{
				ushort crc = ComputeChecksum(bytes, start, len);
				return BitConverter.GetBytes(crc);
			}

			public Crc16()
			{
				ushort value;
				ushort temp;
				for (ushort i = 0; i < table.Length; ++i)
				{
					value = 0;
					temp = i;
					for (byte j = 0; j < 8; ++j)
					{
						if (((value ^ temp) & 0x0001) != 0)
						{
							value = (ushort)((value >> 1) ^ polynomial);
						}
						else
						{
							value >>= 1;
						}

						temp >>= 1;
					}

					table[i] = value;
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TcpClients.Model;

namespace TcpClients.Helper
{
    /// <summary>
    /// 通道数据帮助类
    /// </summary>
    public static class ChannelDataHelper
    {
        #region 私有字段

        /// <summary>
        /// 字典: 将二进制数据转换为通道对应的数据类型的值
        /// </summary>
        private static Dictionary<byte, Func<byte[], int, object>> _converters;

        #endregion

        #region 静态构造函数

        static ChannelDataHelper()
        {
            _converters = new Dictionary<byte, Func<byte[], int, object>>
            {
                // Unsigned char 1byte
                { 0x01, (data, index) => data[index] },
                // Signed char 1byte
                { 0x02, (data, index) => data[index] },
                // Unsigned int 2byte
                { 0x03, (data, index) => BitConvertHelper.ToUInt16(data, index) },
                // Signed int 2byte
                { 0x04, (data, index) => BitConvertHelper.ToInt16(data, index) },
                // Unsigned long 4byte
                { 0x05, (data, index) => BitConvertHelper.ToUInt32(data, index) },
                // Signed long 4byte
                { 0x06, (data, index) => BitConvertHelper.ToInt32(data, index) },
                // Float 4byte
                { 0x07, (data, index) => BitConvertHelper.ToSingle(data, index) },
                // BCD HH 1byte
                { 0x08, (data, index) => BitConverter.ToString(data, index, 1) },
                // BCD MMHH 2byte
                { 0x09, (data, index) => BitConverter.ToString(data, index, 2).Replace("-", "") },
                // BCD YYMMDDHHMM 6byte
                { 0x10, (data, index) => BitConverter.ToString(data, index, 6).Replace("-", "") },
                // string 长度(2byte)+字符内容
                { 0x11, (data, index) =>
                        {
                            var length = BitConvertHelper.ToUInt16(data, index);
                            return Encoding.UTF8.GetString(data, index + 2, length);
                        }
                },
            };
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取所有通道数据
        /// </summary>
        /// <param name="count">通道数</param>
        /// <param name="body">通道数据体</param>
        /// <returns></returns>
        public static Dictionary<string, ChannelData> GetAllChannelData(byte[] body)
        {
            var channelCount = body[0];
            var datas = new Dictionary<string, ChannelData>();
            if (channelCount == 0)
                return datas;

            // 当前的索引值
            var index = 1;

            // 按指定的通道数获取通道数据
            while (channelCount-- > 0)
            {
                var channelData = new ChannelData
                {
                    ChannelType = (ChannelType)body[index++],
                    ChannelSubType = body[index++],
                    Data = _converters[body[index++]](body, index),
                };
                if (channelData.Data is string s)
                    index += s.Length / 2;
                else
                    index += Marshal.SizeOf(channelData.Data);

                datas.Add(channelData.GetKey(), channelData);
            }

            return datas;
        }

        #endregion
    }
}

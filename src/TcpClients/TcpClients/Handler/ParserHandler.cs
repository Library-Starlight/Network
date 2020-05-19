using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TcpClients.Helper;
using TcpClients.Model;
using TcpClients.Tcp;

namespace TcpClients.Handler
{
    public class ParserHandler : IHandler
    {
        #region 控制字符

        /// <summary>
        /// 起始字符
        /// </summary>
        private const byte SOH = 0x01;

        /// <summary>
        /// 数据体起始字符
        /// </summary>
        private const byte STX = 0x02;

        /// <summary>
        /// 数据体结束字符
        /// </summary>
        private const byte ETX = 0x03;

        /// <summary>
        /// 结束字符
        /// </summary>
        private const byte EOT = 0x04;

        /// <summary>
        /// 头部长度
        /// </summary>
        private const int HeaderLength = 7;

        /// <summary>
        /// CRC校验长度
        /// </summary>
        private const int CRCLength = 2;

        #endregion

        #region 私有字段

        /// <summary>
        /// 日志类
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// 是否已开始
        /// </summary>
        private bool _dataStarted = false;

        /// <summary>
        /// 是否已结束
        /// </summary>
        private bool _dataEnded = false;

        /// <summary>
        /// 是否在获取数据体
        /// </summary>
        private bool _bodyStarted = false;

        /// <summary>
        /// 是否获取数据体完成
        /// </summary>
        private bool _bodyEnded = false;

        /// <summary>
        /// 当前头部长度
        /// </summary>
        private int _headerLength = 0;

        /// <summary>
        /// 实际数据体长度
        /// </summary>
        private int _realBodyLength = 0;

        /// <summary>
        /// 当前数据体长度
        /// </summary>
        private int _bodyLength = 0;

        /// <summary>
        /// 当前CRC校验数据长度
        /// </summary>
        private int _crcLength = 0;

        /// <summary>
        /// 数据缓存
        /// </summary>
        private List<byte> _cache = new List<byte>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="logger"></param>
        public ParserHandler(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 解析数据包
        /// </summary>
        /// <param name="data">数据包二进制数据</param>
        /// <returns>通过解析得到的数据包实体</returns>
        private DataBase ParsePacket(byte[] data)
        {
            var dataStr = BitConverter.ToString(data).Replace("-", "");
            _logger?.LogDebug($"收到数据包: {dataStr}");

            // TODO: 进行CRC校验,确认计算是否有误,若有误,算法修改为协议指定的算法
            // CRC校验
            //var crc = BitConvertHelper.ToUInt16(data, data.Length - 3);
            //if (!CrcCheckHelper.CheckSum(data, 0, data.Length - 3, crc))
            //    throw new InvalidOperationException($"CRC校验失败");

            // 目标Id
            var targetId = BitConvertHelper.ToUInt16(data, 1);

            // 发送Id
            var senderId = BitConvertHelper.ToUInt16(data, 3);

            // 命令字
            var commandValue = data[5];
            // 判断是否为预定义的枚举值
            if (!Enum.IsDefined(typeof(CommandType), commandValue) || !Enum.TryParse<CommandType>(commandValue.ToString(), out var command))
                throw new InvalidOperationException($"不支持指令: 0x{commandValue:X2}");

            // 数据长度
            var length = BitConvertHelper.ToUInt16(data, 6);

            // 数据内容
            var body = new byte[length];
            Array.Copy(data, 9, body, 0, body.Length);

            // 解析数据包
            var result = ParsePacket(targetId, senderId, command, body);
            return result;
        }

        /// <summary>
        /// 解析数据包
        /// </summary>
        /// <param name="targetId">目标Id</param>
        /// <param name="senderId">发送Id</param>
        /// <param name="command">指令类型</param>
        /// <param name="body">数据体</param>
        private DataBase ParsePacket(ushort targetId, ushort senderId, CommandType command, byte[] body)
        {
            switch (command)
            {
                case CommandType.TimingData:
                    return ParseTimingData(targetId, senderId, command, body);
            }

            return null;
        }

        /// <summary>
        /// 解析定时数据
        /// </summary>
        /// <param name="targetId">目标Id</param>
        /// <param name="senderId">发送Id</param>
        /// <param name="command">指令类型</param>
        /// <param name="body">数据体</param>
        /// <returns></returns>
        private DeviceData ParseTimingData(ushort targetId, ushort senderId, CommandType command, byte[] body)
        {
            // 设置数据头部
            var deviceData = new DeviceData
            {
                TargetId = targetId,
                SenderId = senderId,
                CommandType = command,
            };

            // 设置通道数
            deviceData.ChannelCount = body[0];

            // 设置通道数据
            deviceData.Datas = ChannelDataHelper.GetAllChannelData(body);

            return deviceData;
        }

        #endregion

        #region 接口方法

        /// <summary>
        /// 匹配数据包
        /// </summary>
        /// <param name="client">通讯客户端</param>
        /// <param name="data">原始数据</param>
        public void Parse(Client client, byte[] data)
        {
            try
            {
                foreach (var b in data)
                {
                    // 等待协议头时,收到无效数据
                    if (!_dataStarted && b != SOH)
                        continue;

                    // 将有效数据添加到缓存列表
                    _cache.Add(b);

                    // 开始
                    if (!_dataStarted)
                        _dataStarted = true;
                    // 数据体开始
                    else if (!_bodyStarted)
                    {
                        _headerLength++;
                        if (_headerLength > HeaderLength)
                        {
                            // 获取指定的长度后,才匹配结束符
                            if (b == STX)
                            {
                                _bodyStarted = true;
                                // 获取数据体长度
                                _bodyLength = (int)BitConvertHelper.ToUInt16(_cache.GetRange(_cache.Count - 3, 2).ToArray(), 0);
                            }
                            else
                            {
                                // 期待一个结束符,但实际不是结束符
                                ResetCache();
                                throw new InvalidOperationException($"数据错误,应以符号0x02结束数据头, 请检查并确保数据包正确无误");
                            }
                        }
                    }
                    // 数据体结束
                    else if (!_bodyEnded)
                    {
                        _realBodyLength++;
                        // 获取指定的长度后,才匹配结束符
                        if (_realBodyLength > _bodyLength)
                        {
                            if (b == ETX)
                            {
                                _bodyEnded = true;
                            }
                            else
                            {
                                // 期待一个结束符,但实际不是结束符
                                ResetCache();
                                throw new InvalidOperationException("数据错误,应以符号0x03结束数据体, 请检查并确保数据包正确无误");
                            }
                        }
                    }
                    // 结束
                    else if (!_dataEnded)
                    {
                        _crcLength++;
                        if (_crcLength > CRCLength)
                        {
                            if (b == EOT)
                            {
                                var entity = ParsePacket(_cache.ToArray());
                                SendResponse(client, entity);
                                ResetCache();
                            }
                            else
                            {
                                // 期待一个结束符,但实际不是结束符
                                ResetCache();
                                throw new InvalidOperationException("数据错误,应以符号0x04结束, 请检查并确保数据包正确无误");
                            }
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("发生意料之外的错误, 请检查并确保数据包正确无误");
                    }
                }
            }
            catch 
            {
                ResetCache();
                throw;
            }
        }

        private void ResetCache()
        {
            _cache.Clear();
            _dataStarted = false;
            _bodyStarted = false;
            _bodyEnded = false;
            _headerLength = 0;
            _realBodyLength = 0;
            _bodyLength = 0;
            _crcLength = 0;
        }

        /// <summary>
        /// 发送指令应答
        /// </summary>
        /// <param name="client"></param>
        /// <param name="entity"></param>
        private void SendResponse(Client client, DataBase entity)
        {
            // TODO: 发送指令应答


            //client.Stream.Write();
        }

        #endregion
    }
}

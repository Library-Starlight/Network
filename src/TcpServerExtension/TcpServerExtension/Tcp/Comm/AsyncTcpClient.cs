using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace System.Net
{
    public class AsyncTcpClient
    {
        #region 私有字段

        /// <summary>
        /// Tcp客户端
        /// </summary>
        private readonly TcpClient _client;

        /// <summary>
        /// 缓存
        /// </summary>
        private byte[] _buffer = new byte[1024];

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="client">Tcp客户端</param>
        public AsyncTcpClient(TcpClient client)
        {
            _client = client;
        }

        #endregion

        #region 保护方法

        //protected void OnReceivedData()

        #endregion

        #region 公共方法

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns></returns>
        public Task ReceiveAsync()
            => Task.Run(async () =>
            {
                using (var stream = _client.GetStream())
                {
                    while (_client.Connected)
                    {
                        var count = await stream.ReadAsync(_buffer, 0, _buffer.Length);
                        if (count == 0)
                        {
                            Console.WriteLine($"客户端通讯断开，地址：{_client.Client.RemoteEndPoint.ToString()}");
                            break;
                        }

                        var receivedData = new byte[count];
                        Array.Copy(_buffer, 0, receivedData, 0, count);
                        
                    }
                }
            });

        #endregion
    }
}

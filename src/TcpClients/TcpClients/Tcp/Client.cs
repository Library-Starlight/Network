using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcpClients.Model;

namespace TcpClients.Tcp
{
    public class Client
    {
        #region 私有字段

        /// <summary>
        /// 数据接收触发信号
        /// </summary>
        private AutoResetEvent _autoReset = new AutoResetEvent(false);

        /// <summary>
        /// 数据队列
        /// </summary>
        private Queue<byte[]> _qData = new Queue<byte[]>();

        /// <summary>
        /// 日志类
        /// </summary>
        private ILogger _logger;

        #endregion

        #region 公共属性

        /// <summary>
        /// 该客户端关联的套接字
        /// </summary>
        public Socket Socket { get; }

        /// <summary>
        /// Socket网络通讯的数据流
        /// </summary>
        public Stream Stream { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="socket"></param>
        public Client(Socket socket, ILogger logger)
        {
            Socket = socket;
            Stream = new NetworkStream(socket);
            _logger = logger;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 处理客户端网络通讯
        /// </summary>
        /// <returns></returns>
        public Task HandleNetworkAsync()
        {
            // 取消任务令牌
            var cancelSource = new CancellationTokenSource();

            var tFill = FillQueueAsync(cancelSource);
            var tRead = ReadQueueAsync(cancelSource.Token);

            return Task.WhenAll(tFill, tRead);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 客户端接收数据并填充队列
        /// </summary>
        /// <param name="socket">Tcp客户端套接字</param>
        /// <param name="writer">写入管道</param>
        /// <returns></returns>
        private async Task FillQueueAsync(CancellationTokenSource cancelSource)
        {
            const int BufferSize = 1024;
            byte[] buffer = new byte[BufferSize];
            
            while (true)
            {
                try
                {
                    var count = await Stream.ReadAsync(buffer, 0, buffer.Length);

                    // 若连接已关闭，退出循环
                    if (count <= 0)
                        break;

                    // 将接收到的数据加入到队列中
                    var data = new byte[count];
                    Array.Copy(buffer, 0, data, 0, data.Length);
                    _qData.Enqueue(data);

                    // 发送通知
                    _autoReset.Set();
                }
                catch (Exception ex)
                {
                    _logger?.LogError($"获取数据失败, {ex.ToString()}");
                    break;
                }
            }

            // 发送取消任务通知
            cancelSource.Cancel();
            _autoReset.Set();
        }

        /// <summary>
        /// 从数据接收队列获取数据并解析
        /// </summary>
        /// <param name="reader">读取管道</param>
        /// <returns></returns>
        private Task ReadQueueAsync(CancellationToken token)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        // 等待接收数据
                        _autoReset.WaitOne();

                        while (_qData.Count > 0)
                        {
                            var data = _qData.Dequeue();
                            if (data == null || data.Length <= 0) continue;

                            Received(data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError($"处理数据失败, {ex.ToString()}");
                }
            });
        }

        /// <summary>
        /// 触发接收数据事件
        /// </summary>
        /// <param name="data"></param>
        private void Received(byte[] data)
        {
            // 将数据直接传入管道
            try
            {
                DataPipeline.Instance.Received(this, data);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"数据解析失败, {ex.ToString()}");
            }
        }

        #endregion
    }
}

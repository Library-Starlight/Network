using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpClients
{
    public class DataPipeline
    {
        #region 单例

        public static DataPipeline Instance = new DataPipeline();

        #endregion

        #region 接收事件

        /// <summary>
        /// 接收委托实例
        /// </summary>
        private EventHandler<ReceivedEventArgs> _receivedData;

        /// <summary>
        /// 接收事件
        /// </summary>
        public event EventHandler<ReceivedEventArgs> ReceivedData
        {
            add => _receivedData += value;
            remove => _receivedData -= value;
        }

        /// <summary>
        /// 触发接收事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnReceivedData(ReceivedEventArgs e)
        {
            var temp = Volatile.Read(ref _receivedData);
            temp?.Invoke(this, e);
        }

        /// <summary>
        /// 接收：将输入转换为事件参数并触发事件
        /// </summary>
        /// <param name="data"></param>
        public void Received(Client client, byte[] data)
        {
            OnReceivedData(new ReceivedEventArgs { Client = client, Data = data });
        }

        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcpClients.Tcp;

namespace TcpClients.Model
{
    public class DataPipeline
    {
        #region 单例

        private static object _objLock = new object();
        private static DataPipeline _instance;


        public static DataPipeline Instance
        {
            get
            {
                if (_instance == null)
                    lock (_objLock)
                        if (_instance == null)
                            _instance = new DataPipeline();
                return _instance;
            }
        }

        private DataPipeline() { }

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
        public void Received(Client client, byte[] data) =>
            OnReceivedData(new ReceivedEventArgs { Client = client, Data = data });

        #endregion

        #region 解析完成事件

        /// <summary>
        /// 解析委托实例
        /// </summary>
        private EventHandler<ParsedEventArgs> _parsedData;

        /// <summary>
        /// 解析事件
        /// </summary>
        public event EventHandler<ParsedEventArgs> ParsedData
        {
            add => _parsedData += value;
            remove => _parsedData -= value;
        }

        /// <summary>
        /// 触发解析事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnParsedData(ParsedEventArgs e)
        {
            var temp = Volatile.Read(ref _parsedData);
            temp?.Invoke(this, e);
        }

        public void Parsed(DataBase data) =>
            OnParsedData(new ParsedEventArgs { Data = data });

        #endregion

        #region 公共方法

        /// <summary>
        /// 注册数据处理方法
        /// </summary>
        /// <param name="handlers">实现处理接口的实例</param>
        public void Register(params IHandler[] handlers)
        {
            foreach (var handler in handlers)
                ReceivedData += (_, e) => handler.Parse(e.Client, e.Data);
        }

        #endregion
    }
}
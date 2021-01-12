using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Net.Sockets
{
    /// <summary>
    /// 异步TcpListener TCP服务器事件参数
    /// </summary>
    public class AsyncEventArgs : EventArgs
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        public string _msg;

        public string Msg { get { return _msg; } }

        /// <summary>
        /// 客户端状态封装类
        /// </summary>
        public TcpClientState _state;

        public TcpClientState State { get { return _state; } }

        /// <summary>
        /// 是否已经处理过了
        /// </summary>
        public bool IsHandled { get; set; }

        public AsyncEventArgs(string msg)
        {
            this._msg = msg;
            IsHandled = false;
        }
        public AsyncEventArgs(TcpClientState state)
        {
            this._state = state;
            IsHandled = false;
        }
        public AsyncEventArgs(string msg, TcpClientState state)
        {
            this._msg = msg;
            this._state = state;
            IsHandled = false;
        }
    }
}

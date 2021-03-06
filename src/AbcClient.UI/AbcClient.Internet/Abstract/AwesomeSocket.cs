﻿using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.Internet.Abstract
{
    /// <summary>
    /// 这是一个高性能、可扩展、高可用、高并发的Tcp套接字抽象
    ///     高效地实现了数据的异步接收、异步发送
    ///     高性能地管道处理极大地简化并减小了内存空间的消耗
    ///     高效率地解析和封装数据包
    ///     高可用性，开放了数据的发送、接收接口，数据的封装及解析接口。
    /// </summary>
    public abstract class AwesomeSocket
    {
        #region 只读字段

        /// <summary>
        /// 事件集合，管理一组“事件/委托”对
        /// </summary>
        private readonly EventSet m_eventSet = new EventSet();

        #endregion

        #region 受保护属性

        /// <summary>
        /// 对子类开放事件集合
        /// </summary>
        protected EventSet EventSet => m_eventSet;

        #endregion

        #region 获取数据包事件处理代码

        /// <summary>
        /// 获取数据包事件的主键
        /// 注：每个对象都有一个自己的哈希码，不同对象的同一事件可以共用相同的哈希码，节约了一点内存
        /// </summary>
        protected static readonly EventKey s_receivedPacketKey = new EventKey();

        /// <summary>
        /// 获取数据包事件
        /// </summary>
        public event EventHandler<ReceivedPacketEventArgs> ReceivedPacket
        {
            add => m_eventSet.Add(s_receivedPacketKey, value);
            remove => m_eventSet.Remove(s_receivedPacketKey, value);
        }

        /// <summary>
        /// 可重写的触发获取数据包事件的方法
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnReceivedPacket(ReceivedPacketEventArgs e) => m_eventSet.Raise(s_receivedPacketKey, this, e);

        /// <summary>
        /// 将输入转换为获取数据包事件的方法
        /// </summary>
        public void TrigerReceivedPacket() => OnReceivedPacket(new ReceivedPacketEventArgs());

        #endregion

        #region 公共属性

        /// <summary>
        /// 套接字绑定的终结点
        /// </summary>
        public IPEndPoint EndPoint { get; }

        /// <summary>
        /// 套接字
        /// </summary>
        public Socket Socket { get; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="endPoint">套接字启动时绑定的端口</param>
        public AwesomeSocket(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        #endregion

        #region 抽象方法

        /// <summary>
        /// 启动套接字
        /// </summary>
        /// <returns></returns>
        public abstract Task StartAsync();

        #endregion

        #region 私有方法

        protected async Task FillPipeAsync(Socket socket, PipeWriter writer)
        {

        }

        protected async Task ReadPipeAsync(PipeReader reader)
        {

        }

        #endregion


        #region 受保护方法

        protected Task HandleDataAsync(Socket socket)
        {
            var pipe = new Pipe();

            var tFill = FillPipeAsync(socket, pipe.Writer);
            var tRead = ReadPipeAsync(pipe.Reader);

            return Task.WhenAll(tFill, tRead);
        }

        #endregion

        #region 公共方法

        public async Task SendAsync()
        {

        }

        #endregion
    }
}

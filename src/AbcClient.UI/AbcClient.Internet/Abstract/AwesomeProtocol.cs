using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.Internet.Abstract
{
    /// <summary>
    /// 协议解析类，定义了解析协议的基本框架。
    /// 具有高可用、高性能、可扩展等敏捷开发特性
    /// </summary>
    public abstract class AwesomeProtocol
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



    }
}

using System.Collections.Generic;
using System.Threading;

namespace System
{
    /// <summary>
    /// 事件主键，定义该类保证了类型安全及代码可维护性
    /// </summary>
    public sealed class EventKey { }

    /// <summary>
    /// 线程安全的事件集合
    /// </summary>
    public sealed class EventSet
    {
        #region 只读字段

        /// <summary>
        /// 该字典维护了<see cref="EventKey"/> -> <see cref="Delegate"/>映射
        /// </summary>
        private readonly Dictionary<EventKey, Delegate> m_events = new Dictionary<EventKey, Delegate>();

        #endregion

        #region 公共方法

        /// <summary>
        /// 以线程安全的方式添加<see cref="EventKey"/> -> <see cref="Delegate"/>映射
        /// 若已存在映射，将委托附加到现有委托的方法链上
        /// </summary>
        /// <param name="eventKey">事件主键</param>
        /// <param name="handler">需添加到事件方法链上的委托</param>
        public void Add(EventKey eventKey, Delegate handler)
        {
            // 进入锁，在添加、移除委托或触发事件时保证线程安全
            Monitor.Enter(m_events);

            // 若已存在相同键的委托，将委托附加到现有委托的方法链上
            m_events.TryGetValue(eventKey, out var d);
            m_events[eventKey] = Delegate.Combine(d, handler);

            // 退出锁
            Monitor.Exit(m_events);
        }

        /// <summary>
        /// 以线程安全的方式移除<see cref="EventKey"/> -> <see cref="Delegate"/>映射上的委托的部分或全部，
        /// 若移除后委托为空，则删除该项
        /// </summary>
        /// <param name="eventKey">事件主键</param>
        /// <param name="handler">需从事件方法链上移除的委托</param>
        public void Remove(EventKey eventKey, Delegate handler)
        {
            // 进入锁，在添加、移除委托或触发事件时保证线程安全
            Monitor.Enter(m_events);

            // 通过键获取委托
            if (m_events.TryGetValue(eventKey, out var d))
            {
                // 将委托从现有委托的方法链上移除
                d = Delegate.Remove(d, handler);

                // 如果还有委托，则设置新的头部地址，否则删除字典里的该项
                if (d != null) m_events[eventKey] = d;
                else m_events.Remove(eventKey);
            }

            // 退出锁
            Monitor.Exit(m_events);
        }

        /// <summary>
        /// 以线程安全的方式引发<see cref="EventKey"/> -> <see cref="Delegate"/>映射的委托上订阅的事件
        /// </summary>
        /// <param name="eventKey">事件主键</param>
        /// <param name="sender">事件触发者</param>
        /// <param name="e">事件参数，
        /// 由于编译时无法构造类型安全的调用，该参数通过需EventArgs基类传输。
        /// 在方法内部<see cref="Delegate.DynamicInvoke"/>方法获取了参数的运行时类型，并触发事件。
        /// 若类型不匹配，<see cref="Delegate.DynamicInvoke"/>方法将抛出异常，设计时应进行良好的测试，避免该情况发生</param>
        public void Raise(EventKey eventKey, Object sender, EventArgs e)
        {
            // 进入锁，在添加、移除委托或触发事件时保证线程安全
            Monitor.Enter(m_events);

            // 获取事件的委托链
            m_events.TryGetValue(eventKey, out var d);

            // 退出锁
            Monitor.Exit(m_events);

            // 以类型安全的方式调用方法，若参数的运行类型与委托的方法签名不匹配，将抛出异常
            d?.DynamicInvoke(new object[] { sender, e });
        }

        #endregion
    }
}

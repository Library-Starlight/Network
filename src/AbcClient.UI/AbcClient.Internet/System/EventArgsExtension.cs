using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 事件参数的扩展方法
    /// </summary>
    internal static class EventArgsExtension
    {
        /// <summary>
        /// 以线程安全地方式触发事件。对扩展方法地使用保证了事件触发代码的可重用性
        /// </summary>
        /// <typeparam name="TEventArgs">事件参数类</typeparam>
        /// <param name="e">事件的参数</param>
        /// <param name="sender">事件的触发者</param>
        /// <param name="eventDelegate">记录订阅了事件的方法链</param>
        public static void Raise<TEventArgs>(this TEventArgs e, Object sender, ref EventHandler<TEventArgs> eventDelegate)
        {
            // 出于线程安全考虑，将对委托字段的引用复制到临时变量中
            var temp = Volatile.Read(ref eventDelegate);

            // 触发登记了事件的方法
            temp?.Invoke(sender, e);
        }
    }
}

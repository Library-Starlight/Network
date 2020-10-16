using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
    /// <summary>
    /// <see cref="DateTime"/>的扩展方法
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 零时刻
        /// </summary>
        private readonly static DateTime _zero = new DateTime(1970, 1, 1);

        #region 北京时间

        /// <summary>
        /// 获取长整型秒数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToSecondInt64(this DateTime time)
        {
            return (long)(time - _zero).TotalSeconds;
        }

        /// <summary>
        /// 获取长整形秒数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static ulong ToSecondUint64(this DateTime time)
        {
            return (ulong)(time - _zero).TotalSeconds;
        }

        /// <summary>
        /// 获取长整型毫秒数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToMillisecondInt64(this DateTime time)
        {
            return (long)(time - _zero).TotalMilliseconds;
        }

        /// <summary>
        /// 获取无符号长整形毫秒数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static ulong ToMillisecondUint64(this DateTime time)
        {
            return (ulong)(time - _zero).TotalMilliseconds;
        }

        #endregion

        #region Utc时间

        /// <summary>
        /// 获取Utc长整型秒数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToUtcSecondInt64(this DateTime time)
            => time.AddHours(-8).ToSecondInt64();

        /// <summary>
        /// 获取Utc无符号长整型秒数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static ulong ToUtcSecondUint64(this DateTime time)
            => time.AddHours(-8).ToSecondUint64();

        /// <summary>
        /// 将Utc整数值转换为时间
        /// </summary>
        /// <param name="value">需要转换为时间的Utc整数值</param>
        /// <returns></returns>
        public static DateTime UtcMillisecondsToTime(this long value)
        {
            return _zero.AddMilliseconds(value);
        }

        /// <summary>
        /// 将Utc整数值转换为北京时间
        /// </summary>
        /// <param name="value">需要转换为时间的Utc整数值</param>
        /// <returns></returns>
        public static DateTime UtcMillisecondsToBeiJingTime(this long value)
            => UtcMillisecondsToTime(value).AddHours(8D);

        #endregion
    }
}

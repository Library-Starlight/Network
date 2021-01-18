using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Log
{
    public class Logger
    {
        #region 常量

        /// <summary>
        /// 日志文件目录
        /// </summary>
        private static string LogFilePath = AppDomain.CurrentDomain.BaseDirectory + $"/../log/{Assembly.GetAssembly(typeof(Logger)).GetName().Name}/";
        //private static string LogFilePath = Assembly.GetExecutingAssembly().Location + $"/../log/{Assembly.GetAssembly(typeof(Logger)).GetName().Name}/";

        #endregion

        #region 私有字段

        /// <summary>
        /// 文件日志API
        /// </summary>
        private readonly FileLogger _logger = new FileLogger(LogFilePath);

        #endregion

        #region 单例

        /// <summary>
        /// 同步锁
        /// </summary>
        private static object _syncLock = new object();

        /// <summary>
        /// 单例
        /// </summary>
        private static Logger _instance = null;

        /// <summary>
        /// 单例
        /// </summary>
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                    lock (_syncLock)
                        if (_instance == null)
                        {
                            // 创建日志存放目录
                            if (!Directory.Exists(LogFilePath))
                                Directory.CreateDirectory(LogFilePath);

                            _instance = new Logger();
                        }

                return _instance;
            }
        }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private Logger() { }

        #endregion

        #region 公共方法

        public Logger(string logPath)
        {
            _logger = new FileLogger(logPath);
        }

        public void LogDebug(string message)
        {
            _logger.Log(message, LogLevel.Debug);
        }

        public void LogWarning(string message)
        {
            _logger.Log(message, LogLevel.Warning);
        }

        public void LogError(Exception ex, [CallerMemberName] string name = null)
        {
            _logger.Log($"{name}, Exception: {ex}", LogLevel.Error);
        }

        public void LogError(string message)
        {
            _logger.Log(message, LogLevel.Error);
        }

        public void LogCallMember([CallerMemberName] string name = null, [CallerLineNumber] int line = 0)
        {
            _logger.Log($"Method: {name}", LogLevel.Debug);
            _logger.Log($"  Line: {line.ToString()}", LogLevel.Debug);
        }

        #endregion
    }
}

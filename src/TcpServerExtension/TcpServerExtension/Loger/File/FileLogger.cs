using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Log
{
    /// <summary>
    /// Logs to a specific file
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly FileManager _fileManager = new FileManager();

        /// <summary>
        /// The update time of new log file
        /// </summary>
        private DateTime _updateTime;

        #region Public Properties

        /// <summary>
        /// The path to write the log file to 
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The path and name of file to write log to
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// If true, logs the current time with each message
        /// </summary>
        public bool LogTime { get; set; } = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filePath">The path to log to </param>
        public FileLogger(string filePath)
        {
            FilePath = filePath;
        }

        #endregion

        #region Logger Methods

        /// <summary>
        /// Handles the log message being passed in
        /// </summary>
        /// <param name="message">The message being log</param>
        /// <param name="level">The level of the log message</param>
        public void Log(string message, LogLevel level)
        {
            // Generate new log file path
            NewLogFile();

            // Get current time
            var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Prepend the time to the log if desired
            var timeLogString = LogTime ? $"[{currentTime}] " : string.Empty;

            // Write the message to the log file
            _fileManager.WriteAllTextToFileAsync($"{timeLogString} {level.ToString()} {message}{Environment.NewLine}", FileName, append: true);
        }

        public void NewLogFile()
        {
            if ((DateTime.Now.Date - _updateTime.Date).TotalDays == 0)
                return;

            _updateTime = DateTime.Now;
            FileName = Path.Combine(FilePath, $"log_{DateTime.Now:yyyyMMdd}.txt");

            // 删除超过7天的日志
            var dir = new DirectoryInfo(FilePath);
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                if ((DateTime.Now.Date - file.CreationTime.Date).TotalDays > 7)
                    file.Delete();
            }
        }

        #endregion
    }
}

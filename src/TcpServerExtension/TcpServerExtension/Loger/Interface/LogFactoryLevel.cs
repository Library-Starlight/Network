namespace Log
{
    /// <summary>
    /// The serverity of the log message
    /// </summary>
    public enum LogFactoryLevel
    {
        /// <summary>
        /// Logs everything
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Lot all information except debug information
        /// </summary>
        Verbose = 2,

        /// <summary>
        /// Logs all informative message, ignoring any debug and verbose messages
        /// </summary>
        Informative = 3,

        /// <summary>
        /// Log only critical erroes and warnings and success, but no general information
        /// </summary>
        Critical = 4,

        /// <summary>
        /// The logger will never output anything
        /// </summary>
        Nothing = 7,
    }
}

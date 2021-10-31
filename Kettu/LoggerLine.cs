namespace Kettu {
    /// <summary>
    /// The data sent to loggers.
    /// </summary>
    public struct LoggerLine {
        /// <summary>
        /// The text that is logged.
        /// </summary>
        public string      LineData;
        /// <summary>
        /// The level of logging.
        /// </summary>
        public LoggerLevel LoggerLevel;

        /// <summary>
        /// How the LoggerLine is turned into a string. 0 is the LoggerLevel, and 1 is the LineData.
        /// <seealso cref="LoggerLevel"/>
        /// <seealso cref="LineData"/>
        /// </summary>
        public static string LogFormat = "{0}: {1}";

        public override string ToString() => string.Format(LogFormat, this.LoggerLevel, this.LineData);
    }
}

namespace Kettu {
    public struct LoggerLine {
        public string      LineData;
        public LoggerLevel LoggerLevel;

        public static string LogFormat = "{0}: {1}";

        public override string ToString() => string.Format(LogFormat, this.LoggerLevel, this.LineData);
    }
}

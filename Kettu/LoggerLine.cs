namespace Kettu {
    public struct LoggerLine {
        public string      LineData;
        public LoggerLevel LoggerLevel;

        public override string ToString() => $"{this.LoggerLevel}: {this.LineData}";
    }
}

namespace Kettu {
    public class LoggerLevel {
        public virtual string Name => null;
        
        public override string ToString() {
            return this.Name ?? this.GetType().Name;
        }
    }

    public class LoggerLevelUnknown : LoggerLevel {
        public override string Name => "Unknown";
        public static LoggerLevelUnknown Instance = new();
    }
    
    public class LoggerLevelLoggerInfo : LoggerLevel {
        public override string Name => "LoggerInfo";
        public static LoggerLevelLoggerInfo Instance = new();
    }

    public class LoggerLevelAll : LoggerLevel {
        public static LoggerLevelAll Instance = new();
    }
}

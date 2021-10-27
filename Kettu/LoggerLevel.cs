namespace Kettu {
    public class LoggerLevel {
        public virtual string Name => null;

        public string Channel = null;

        public static string FullFormat = "{0} [{1}]";
        
        public override string ToString() {
            return this.Channel is null ? $"{this.Name ?? this.GetType().Name}" : string.Format(FullFormat, this.Name ?? this.GetType().Name, this.Channel);
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

    public class LoggerLevelChannelTest : LoggerLevel {
        public override string Name => "ChannelTest";

        public enum Channel {
            Important,
            Unimportant,
            EvenLessImportant,
            Beyley
        }

        public static LoggerLevelChannelTest InstanceImportant         = new(Channel.Important);
        public static LoggerLevelChannelTest InstanceUnimportant       = new(Channel.Unimportant);
        public static LoggerLevelChannelTest InstanceEvenLessImportant = new(Channel.EvenLessImportant);
        public static LoggerLevelChannelTest InstanceBeyley            = new(Channel.Beyley);
        
        private LoggerLevelChannelTest(Channel channel) {
            base.Channel = channel.ToString();
        }
    }
}

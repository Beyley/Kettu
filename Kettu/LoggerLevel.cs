namespace Kettu {
    /// <summary>
    /// A type of information that is logged.
    /// </summary>
    public class LoggerLevel {
        /// <summary>
        /// The name of the logging level.
        /// </summary>
        public virtual string Name => null;

        /// <summary>
        /// A channel that is logged *under* the LoggerLevel.
        /// Think of this as your Info/Warn/Error/Debug channels you would find in other logging systems.
        /// Attached to the instance.
        /// </summary>
        public string Channel = null;

        /// <summary>
        /// The format of how the LoggerLevel is turned into a string. 0 is the name of te logger, and 1 is the channel.
        /// </summary>
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

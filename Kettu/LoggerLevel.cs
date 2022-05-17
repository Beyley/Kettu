namespace Kettu; 

/// <summary>
///     A type of information that is logged.
/// </summary>
public class LoggerLevel {
	/// <summary>
	///     The format of how the LoggerLevel is turned into a string. 0 is the name of te logger, and 1 is the channel.
	/// </summary>
	public static string FullFormat = "{0} [{1}]";

	/// <summary>
	///     A channel that is logged *under* the LoggerLevel.
	///     Think of this as your Info/Warn/Error/Debug channels you would find in other logging systems.
	///     Attached to the instance.
	/// </summary>
	public string Channel;
	/// <summary>
	///     The name of the logging level.
	/// </summary>
	public virtual string Name => null;

	public override string ToString() {
		return this.Channel is null ? $"{this.Name ?? this.GetType().Name}" : string.Format(FullFormat, this.Name ?? this.GetType().Name, this.Channel);
	}
}

public class LoggerLevelUnknown : LoggerLevel {
	public static   LoggerLevelUnknown Instance = new();
	public override string             Name => "Unknown";
}

public class LoggerLevelLoggerInfo : LoggerLevel {
	public static   LoggerLevelLoggerInfo Instance = new();
	public override string                Name => "LoggerInfo";
}

public class LoggerLevelAll : LoggerLevel {
	public static LoggerLevelAll Instance = new();
}

public class LoggerLevelChannelTest : LoggerLevel {
	private new enum Channel {
		Important,
		Unimportant,
		EvenLessImportant,
		Complain //https://www.youtube.com/watch?v=K8mPn7ykV18
	}

	public static LoggerLevelChannelTest InstanceImportant         = new(Channel.Important);
	public static LoggerLevelChannelTest InstanceUnimportant       = new(Channel.Unimportant);
	public static LoggerLevelChannelTest InstanceEvenLessImportant = new(Channel.EvenLessImportant);
	public static LoggerLevelChannelTest InstanceComplain          = new(Channel.Complain);

	private LoggerLevelChannelTest(Channel channel) {
		base.Channel = channel.ToString();
	}
	public override string Name => "ChannelTest";
}
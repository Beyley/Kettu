using System;
using System.Diagnostics;

namespace Kettu; 

/// <summary>
///     The data sent to loggers.
/// </summary>
public struct LoggerLine {
	/// <summary>
	///     The text that is logged.
	/// </summary>
	public string LineData;
	/// <summary>
	///     The level of logging.
	/// </summary>
	public readonly LoggerLevel LoggerLevel;
	/// <summary>
	/// The exact timestamp the line was created
	/// </summary>
	public readonly DateTime TimeStamp;
	/// <summary>
	/// The stacktrace of when the line was created
	/// </summary>
	public readonly StackTrace StackTrace;

	public LoggerLine(string data, LoggerLevel level, StackTrace stackTrace) {
		this.LineData    = data;
		this.LoggerLevel = level;
		this.TimeStamp   = DateTime.UtcNow;
		this.StackTrace  = stackTrace;
	}
		
	/// <summary>
	///     How the LoggerLine is turned into a string. 0 is the LoggerLevel, and 1 is the LineData.
	///     <seealso cref="LoggerLevel" />
	///     <seealso cref="LineData" />
	/// </summary>
	public static string LogFormat = "{0}: {1}";

	public override string ToString() {
		return string.Format(LogFormat, this.LoggerLevel, this.LineData);
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kettu {
	public static class Logger {
		private static readonly Queue<LoggerLine> _LoggerLines = new();

		private static double _UpdateDeltaTime;

		private static double _updateRate = 0.1d;

		private static Timer _Timer;

		/// <summary>
		///     A list of loggers.
		///     <seealso cref="LoggerBase" />
		/// </summary>
		public static List<LoggerBase> Loggers {
			get;
		} = new();
		/// <summary>
		///     The time in seconds before each logger update, defaults to half a second.
		/// </summary>
		public static double UpdateRate {
			get => _updateRate;
			set {
				if (Math.Abs(value - _updateRate) < 0.001d) return;

				if (TimerStarted)
					_Timer.Change(0, (int)(value * 1000));

				_updateRate = value;
			}
		}
		public static bool TimerStarted {
			get;
			private set;
		}

		/// <summary>
		///     Async method that processes queued logger lines.
		///     <seealso cref="UpdateRate" />
		///     <seealso cref="LoggerLine" />
		/// </summary>
		public static async Task Update() {
			await Task.Run(
				delegate {
					lock (_LoggerLines) {
						if (_LoggerLines.Count == 0) return;
						if (Loggers.Count      == 0) return;

						do {
							if (_LoggerLines.Count == 0) break;
							LoggerLine lineToSend = _LoggerLines.Dequeue();

							if (lineToSend.LoggerLevel == null || lineToSend.LineData is null or "") return;

							foreach (LoggerBase logger in Loggers.Where(
										 logger => logger.Level.Contains(lineToSend.LoggerLevel) || logger.Level.Contains(LoggerLevelAll.Instance)
									 ))
								logger.Send(lineToSend);
						} while (_LoggerLines.Count > 0);
					}
				}
			);
		}

		/// <summary>
		///     XNA helper function that processes queued logger lines.
		/// </summary>
		/// <seealso cref="UpdateRate" />
		/// <seealso cref="LoggerLine" />
		/// <seealso cref="Update" />
		/// <param name="elapsedSeconds">The number of elapsed seconds.</param>
		public static async void XnaUpdate(double elapsedSeconds) {
			_UpdateDeltaTime += elapsedSeconds;

			if (!(_UpdateDeltaTime >= UpdateRate))
				return;
			_UpdateDeltaTime = 0d;

			await Update();
		}

		/// <summary>
		///     Starts the internal update cycle.
		///     <seealso cref="Update" />
		///     <seealso cref="Timer" />
		/// </summary>
		public static void StartLogging() {
			if (_Timer is not null && TimerStarted)
				_Timer.Dispose();

			_Timer = new Timer(TimerTick);
			_Timer.Change(0, (int)(UpdateRate * 1000));
			TimerStarted = true;
		}
		private static void TimerTick(object state) {
			Update().Wait();
		}

		/// <summary>
		///     Stops the internal update cycle.
		///     <seealso cref="StartLogging" />
		/// </summary>
		public static void StopLogging() {
			_Timer.Dispose();
			TimerStarted = false;
		}

		/// <summary>
		///     Initializes and adds a logger to the list of loggers.
		/// </summary>
		/// <param name="loggerBase">The logger to add.</param>
		public static void AddLogger(LoggerBase loggerBase) {
			if (!loggerBase.AllowMultiple && Loggers.Any(x => x.GetType() == loggerBase.GetType()))
				return;

			loggerBase.Initialize();
			Loggers.Add(loggerBase);
		}

		/// <summary>
		///     Disposes and removes a logger from the list of loggers.
		/// </summary>
		/// <param name="loggerBase">The logger to remove.</param>
		public static void RemoveLogger(LoggerBase loggerBase) {
			loggerBase.Dispose();
			Loggers.Remove(loggerBase);
		}

		/// <summary>
		///     Disposes and removes every instance of a logger from the list of loggers.
		/// </summary>
		/// <param name="type">The type of logger to remove.</param>
		public static void RemoveLogger(Type type) {
			for (int i = 0; i < Loggers.Count; i++)
				if (Loggers[i].GetType() == type)
					RemoveLogger(Loggers[i]);
		}

		/// <summary>
		///     Sends a LoggerLine to the queue.
		/// </summary>
		/// <param name="line">The LoggerLine to log.</param>
		public static void Log(LoggerLine line) {
			line.LineData = line.LineData.Replace("\r", "");
			line.LineData = line.LineData.Replace("\n", " ");
			lock (_LoggerLines) {
				_LoggerLines.Enqueue(line);
			}
		}

		[Obsolete("You should always provide a LoggerLevel")]
		public static void Log(string data) {
			if (data is null) data = "";

			Log(new LoggerLine { LoggerLevel = LoggerLevelUnknown.Instance, LineData = data });
		}

		/// <summary>
		///     Creates and sends a LoggerLine to the queue.
		/// </summary>
		/// <param name="data">The text you want to log.</param>
		/// <param name="level">The level you want to log at. Can be null, but it defaults to "Unknown".</param>
		public static void Log(string data, LoggerLevel level) {
			if (data is null) data = "";

			Log(new LoggerLine { LoggerLevel = level, LineData = data });
		}
	}
}

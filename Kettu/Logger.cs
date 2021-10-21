using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kettu {
    public static class Logger {
        private static Queue<LoggerLine> _LoggerLines = new();
        private static List<LoggerBase>  _Loggers     = new();

        private static double _UpdateDeltaTime = 0;
        
        public static List<LoggerBase> Loggers => _Loggers;

        private static double _updateRate = 0.1d;
        /// <summary>
        /// The time in seconds before each logger update, defaults to half a second
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

        private static Timer _Timer;
        public static bool TimerStarted {
            get;
            private set;
        }
        
        public static async Task Update() {
            await Task.Run(
                delegate {
                    if (_LoggerLines.Count == 0) return;

                    do {
                        if (_LoggerLines.Count == 0) break;
                        LoggerLine lineToSend = _LoggerLines.Dequeue();

                        if (lineToSend.LoggerLevel == null || lineToSend.LineData is null or "") return;
                        
                        foreach (LoggerBase logger in _Loggers.Where(
                            logger => logger.Level.Contains(lineToSend.LoggerLevel) || logger.Level.Contains(LoggerLevelAll.Instance)
                            )) {
                            logger.Send(lineToSend);
                        }
                    } while (_LoggerLines.Count > 0);
                }
            );
        }
        
        public static async void XnaUpdate(double elapsedSeconds) {
            _UpdateDeltaTime += elapsedSeconds;

            if (!(_UpdateDeltaTime >= UpdateRate))
                return;
            _UpdateDeltaTime = 0d;

            await Update();
        }

        public static void StartLogging() {
            if (_Timer is not null && TimerStarted)
                _Timer.Dispose();

            _Timer = new Timer(TimerTick);
            _Timer.Change(0, (int)(UpdateRate * 1000));
            TimerStarted = true;
        }
        private static void TimerTick(object? state) => Update().Wait();

        public static async Task StopLogging() {
            await _Timer.DisposeAsync();
            TimerStarted = false;
        }

        public static void AddLogger(LoggerBase loggerBase) {
            loggerBase.Initialize();
            _Loggers.Add(loggerBase);
        }

        public static void RemoveLogger(LoggerBase loggerBase) {
            loggerBase.Dispose();
            _Loggers.Remove(loggerBase);
        }

        public static void RemoveLogger(Type type) {
            for (var i = 0; i < _Loggers.Count; i++) 
                if (_Loggers[i].GetType() == type) 
                    RemoveLogger(_Loggers[i]);
        }

        public static void Log(LoggerLine line) {
            line.LineData = line.LineData.Replace("\r", "");
            line.LineData = line.LineData.Replace("\n", " ");
            _LoggerLines.Enqueue(line);
        }

        public static void Log(string data, LoggerLevel level = null) {
            if (data is null) data = "";
            
            level ??= LoggerLevelUnknown.Instance;

            Log(new LoggerLine{LoggerLevel = level, LineData = data});
        }
    }
}

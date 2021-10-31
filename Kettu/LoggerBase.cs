using System;
using System.Collections.Generic;

namespace Kettu {
    /// <summary>
    /// A method of logging LoggerLines to a logger.
    /// </summary>
    public abstract class LoggerBase : IDisposable {
        public LoggerBase(List<LoggerLevel> level = null) {
            level ??= new List<LoggerLevel> {
                LoggerLevelAll.Instance
            };
            
            this.Level = level;
        }
        
        /// <summary>
        /// Called when something is logged. You can use the data however you want.
        /// </summary>
        /// <param name="line">The LoggerLine sent.</param>
        public abstract void Send(LoggerLine line);

        /// <summary>
        /// Initializes the logger. This is called by Logger.AddLogger, so you do not need to call this manually.
        /// </summary>
        public virtual void Initialize() {
            Logger.Log(
                new LoggerLine {
                    LoggerLevel = LoggerLevelLoggerInfo.Instance,
                    LineData    = $"{this.GetType().Name} initialized!"
                }
            );
        }
        
        public List<LoggerLevel> Level;
        
        /// <summary>
        /// Cleans up and removes the object from memory.
        /// </summary>
        public void              Dispose() {}
    }
}

using System;

namespace Igtampe.BasicLogger {

    /// <summary>Severity enum for any events logged by BasicLogger</summary>
    public enum LogSeverity { 
    
        /// <summary>Fatal errors</summary>
        FATAL = 0,

        /// <summary>Errors that are not fatal</summary>
        ERROR = 1,

        /// <summary>Warnings</summary>
        WARNING = 2,

        /// <summary>Information that the user may want to know</summary>
        INFO = 3,

        /// <summary>Debug information that the user may not need</summary>
        DEBUG = 4,

    }

    /// <summary>A standard BasicLogger logger</summary>
    public abstract class Logger {

        
        /// <summary>Minimum severity the logger will log</summary>
        public LogSeverity MinSeverity { get; set; }

        /// <summary>Creates a logger with minimum log severity</summary>
        /// <param name="MinSeverity">Minimum severity a log item needs to be logged</param>
        public Logger(LogSeverity MinSeverity) { 
            this.MinSeverity = MinSeverity;
            Debug("Initialized logger");
        }

        /// <summary>Whether or not this severity is loggable when compared to this logger's minimum loggable severity</summary>
        /// <param name="Severity"></param>
        /// <returns></returns>
        protected bool Loggable(LogSeverity Severity) => Severity < MinSeverity;

        /// <summary>Logs a Fatal error</summary>
        /// <param name="Text"></param>
        public virtual void Fatal(string Text) => Log(LogSeverity.FATAL, Text);

        /// <summary>Logs a non-fatal error</summary>
        /// <param name="Text"></param>
        public virtual void Error(string Text) => Log(LogSeverity.ERROR, Text);

        /// <summary>Logs a warning</summary>
        /// <param name="Text"></param>
        public virtual void Warn(string Text) => Log(LogSeverity.WARNING, Text);

        /// <summary>Logs some information</summary>
        /// <param name="Text"></param>
        public virtual void Info(string Text) => Log(LogSeverity.INFO, Text);

        /// <summary>Logs debug information</summary>
        /// <param name="Text"></param>
        public virtual void Debug(string Text) => Log(LogSeverity.DEBUG, Text);

        /// <summary>Logs an Exception</summary>
        /// <param name="E"></param>
        /// <param name="Severity"></param>
        public virtual void Exception(Exception E, LogSeverity Severity = LogSeverity.ERROR) 
            => Log(Severity, $"{E}\n\n{E.StackTrace}");

        /// <summary>Log something</summary>
        /// <param name="Severity"></param>
        /// <param name="LogItem"></param>
        public void Log(LogSeverity Severity, string LogItem) { if (Loggable(Severity)) WriteLog(Severity, LogItem); }

        /// <summary>Writes specified logitem to wherever this logger is configured to write to</summary>
        /// <param name="Severity"></param>
        /// <param name="LogItem"></param>
        protected abstract void WriteLog(LogSeverity Severity, string LogItem);
    }
}
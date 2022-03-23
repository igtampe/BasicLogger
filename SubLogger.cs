namespace Igtampe.BasicLogger {

    /// <summary>A logger for a specific part of a program which requires some sort of distinction (Like a task), but logs to another parent logger</summary>
    public class SubLogger : Logger {

        /// <summary>Logger this SubLogger delegates to</summary>
        private readonly Logger Parent;

        /// <summary>Name of this SubLogger</summary>
        private readonly string Name;

        /// <summary>Creates a Task Logger </summary>
        /// <param name="Name"></param>
        /// <param name="Parent"></param>
        public SubLogger(string Name, Logger Parent) : base(Parent.MinSeverity) { 
            this.Parent = Parent;
            this.Name = Name;
            Parent.Debug($"Initializing sub-logger {Name}");
        }

        /// <summary>Sends a request to log to the configured parent logger</summary>
        /// <param name="Severity"></param>
        /// <param name="LogItem"></param>
        protected override void WriteLog(LogSeverity Severity, string LogItem) => Parent?.Log(Severity, $"({Name}) {LogItem}");
    }
}

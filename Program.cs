using System;

namespace Igtampe.BasicLogger {
    /// <summary>Test program</summary>
    internal static class Program {
        public static void Main() {
            Console.WriteLine("Hello, World!");
            Logger Logger = new BasicLogger(LogSeverity.DEBUG, "TestOutput.txt");

            Logger.Debug("This is debug information");
            Logger.Info("This is Info information");
            Logger.Warn("This is a Warning");
            Logger.Error("This is an Error");
            Logger.Fatal("This is another fatal exception");

            try { StackedError(2); } catch (Exception E) { Logger.Exception(E); }
            try { StackedError(2); } catch (Exception E) { Logger.Exception(E, LogSeverity.WARNING); }

            Logger SubLogger = new SubLogger("Sub", Logger);
            SubLogger.Debug("This is debug information");
            SubLogger.Info("This is Info information");
            SubLogger.Warn("This is a Warning");
            SubLogger.Error("This is an Error");
            SubLogger.Fatal("This is another fatal exception");

            try { StackedError(2); } catch (Exception E) { SubLogger.Exception(E, LogSeverity.FATAL); }
            try { StackedError(2); } catch (Exception E) { SubLogger.Exception(E, LogSeverity.INFO); }

            Console.WriteLine();
            Console.WriteLine("This concludes the demo");
            Console.ReadLine();

        }
        static int StackedError(int Max) => Max == 0 ? 1 / Max : StackedError(Max - 1);
    }
}

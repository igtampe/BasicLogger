using System;

namespace BasicLogger {
    /// <summary>Test program</summary>
    internal static class Program {
        public static void Main() {
            Console.WriteLine("Hello, World!");
            Igtampe.BasicLogger.Logger Logger = new Igtampe.BasicLogger.BasicLogger(Igtampe.BasicLogger.LogSeverity.DEBUG, "TestOutput.txt");

            Logger.Debug("This is debug information");
            Logger.Info("This is Info information");
            Logger.Warn("This is a Warning");
            Logger.Error("This is an Error");
            Logger.Fatal("This is another fatal exception");

            try { StackedError(2); } catch (Exception E) { Logger.Exception(E); }
            try { StackedError(2); } catch (Exception E) { Logger.Exception(E, Igtampe.BasicLogger.LogSeverity.WARNING); }

            Igtampe.BasicLogger.Logger SubLogger = new Igtampe.BasicLogger.SubLogger("Sub", Logger);
            SubLogger.Debug("This is debug information");
            SubLogger.Info("This is Info information");
            SubLogger.Warn("This is a Warning");
            SubLogger.Error("This is an Error");
            SubLogger.Fatal("This is another fatal exception");

            try { StackedError(2); } catch (Exception E) { SubLogger.Exception(E, Igtampe.BasicLogger.LogSeverity.FATAL); }
            try { StackedError(2); } catch (Exception E) { SubLogger.Exception(E, Igtampe.BasicLogger.LogSeverity.INFO); }

            Console.WriteLine();
            Console.WriteLine("This concludes the demo");
            Console.ReadLine();

        }
        static int StackedError(int Max) => Max == 0 ? 1 / Max : StackedError(Max - 1);
    }
}

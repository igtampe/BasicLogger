using Igtampe.BasicRender;
using System;
using System.Collections.Generic;
using System.IO;

namespace Igtampe.BasicLogger {

    /// <summary>A standard BasicLogger logger</summary>
    public class BasicLogger : Logger {

        private static readonly Dictionary<LogSeverity, (ConsoleColor, ConsoleColor)> SeverityColorDictionary 
            = new Dictionary<LogSeverity, (ConsoleColor, ConsoleColor)>() {
                { LogSeverity.FATAL,  (ConsoleColor.White,ConsoleColor.DarkRed) },
                { LogSeverity.ERROR,  (ConsoleColor.White,ConsoleColor.Red) },
                { LogSeverity.WARNING,(ConsoleColor.Black,ConsoleColor.Yellow) },
                { LogSeverity.INFO,   (ConsoleColor.White,ConsoleColor.Blue) },
                { LogSeverity.DEBUG,  (ConsoleColor.Black,ConsoleColor.Gray) },
        };

        /// <summary>Optional output file</summary>
        protected readonly string OutputFile;

        /// <summary>Optional secondary text output </summary>
        protected readonly TextWriter OutputText;

        /// <summary>Creates a logger with minimum log severity, an optional output file to write this all to, and a custom textwriter output if you're into that sort of thing</summary>
        /// <param name="MinSeverity">Minimum severity a log item needs to be logged</param>
        /// <param name="OutputFile">Output file to write this stuff to (Like a Log file). If null, no log will be written to a log file</param>
        /// <param name="Output">Output stream to write this stuff to (Like the Console.Out). If null, this option will be ignored</param>
        public BasicLogger(LogSeverity MinSeverity, string OutputFile = null, TextWriter Output = null) : base(MinSeverity) {

            this.OutputFile = OutputFile;
            OutputText = Output;

        }

        /// <summary>Writes logItem to the Console, and to the optionally configured log file and text output</summary>
        /// <param name="Severity"></param>
        /// <param name="LogItem"></param>
        protected override void WriteLog(LogSeverity Severity, string LogItem) {
            string LogText = $"[{DateTime.Now}] {LogItem}";
            string OutText = $"[{Enum.GetName(typeof(LogSeverity), Severity)}] {LogText}";
            if (OutputFile != null) { using (var Pen = File.AppendText(OutputFile)) { Pen.WriteLine(OutText); }; }
            if (OutputText != null) { OutputText.WriteLine(OutText); }

            var ColorPair = SeverityColorDictionary[Severity];
            
            string TypePrefix = Enum.GetName(typeof(LogSeverity), Severity);
            TypePrefix += new string(' ', 7 - TypePrefix.Length);

            Draw.Sprite(TypePrefix, ColorPair.Item2, ColorPair.Item1);
            //Draw.Sprite($" {LogText}", Console.BackgroundColor, ColorPair.Item2);
            //We have to manuallg console writeline this because of the overscan stuff.

            ConsoleColor OldFG = Console.ForegroundColor;

            RenderUtils.Color(Console.BackgroundColor, ColorPair.Item2);
            Console.WriteLine($" {LogText}");
            RenderUtils.Color(Console.BackgroundColor, OldFG);

        }
    }
}
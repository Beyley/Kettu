using System;

namespace Kettu {
    /// <summary>
    /// A default logger. Simply prints the LoggerLine to the console.
    /// </summary>
    public class ConsoleLogger : LoggerBase {
        public override void Send(LoggerLine line) {
            Console.WriteLine(line.ToString());
        }

        public override bool AllowMultiple => false;
    }
}

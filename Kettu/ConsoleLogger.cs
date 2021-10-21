using System;

namespace Kettu {
    public class ConsoleLogger : LoggerBase {
    
        public override void Send(LoggerLine line) {
            Console.WriteLine(line.ToString());
        }
    }
}

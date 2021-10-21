using System;

namespace Kettu.Test {
	class Program {
		static void Main(string[] args) {
			Logger.StartLogging();
			
			Logger.AddLogger(new ConsoleLogger());

			Console.ReadLine();

			Logger.Log("Hi!");

			Console.ReadLine();
			
			Logger.Log("Hello world!");

			Console.ReadLine();
			
			Logger.StopLogging();
			
			Logger.Log("Test!");

			Console.ReadLine();
		}
	}
}

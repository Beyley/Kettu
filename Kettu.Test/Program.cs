using System;

namespace Kettu.Test {
	internal class Program {
		private static void Main(string[] args) {
			Logger.StartLogging();

			Logger.AddLogger(new ConsoleLogger());

			Console.ReadLine();

			Logger.Log("Hi!");

			Console.ReadLine();

			Logger.Log("Hello world!");

			Console.ReadLine();

			Logger.Log("Hello world!", LoggerLevelChannelTest.InstanceImportant);

			Console.ReadLine();

			Logger.Log("Hello world!", LoggerLevelChannelTest.InstanceUnimportant);

			Console.ReadLine();

			Logger.Log("Hello world!", LoggerLevelChannelTest.InstanceEvenLessImportant);

			Console.ReadLine();

			Logger.Log("Hello world!");

			Console.ReadLine();

			Logger.StopLogging();

			Logger.Log("Test!");

			Console.ReadLine();
		}
	}
}

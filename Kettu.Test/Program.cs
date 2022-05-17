using System;
using System.IO;
using System.Net;
using Kettu.Binary;

namespace Kettu.Test; 

internal class Program {
	private static void Main(string[] args) {
		Logger.StartLogging();

		FileStream fs = File.Create("test");
		// fs.Close();
		// fs = File.OpenWrite("test");

		Logger.AddLogger(new ConsoleLogger());
		Logger.AddLogger(new BinaryLogger(fs));

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
		
		fs.Close();
			
		fs = File.OpenRead("test");

		while (fs.Position < fs.Length) {
			DeserializedLoggerLevel deserializedLoggerLevel = BinarySerializer.Deserialize(fs);

			Console.WriteLine($"Level: {deserializedLoggerLevel.LevelName} Channel: {deserializedLoggerLevel.LevelChannel} LineData: {deserializedLoggerLevel.LineData} TimeStamp: {deserializedLoggerLevel.TimeStamp}");
			for (int i = 0; i < deserializedLoggerLevel.StackFrames.Length; i++) {
				string frame = deserializedLoggerLevel.StackFrames[i];
				
				Console.WriteLine($"    StackFrame {i}: {frame.TrimEnd()}");
			}
		}
		
		fs.Close();
	}
}
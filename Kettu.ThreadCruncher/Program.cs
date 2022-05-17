namespace Kettu.ThreadCruncher;

public class Program {
	public static uint ThreadCount = 32;

	public static bool Run = true;

	public static async Task Main(string[] args) {
		Logger.AddLogger(new ConsoleLogger());
		Logger.StartLogging();

		if (args.Length == 1)
			ThreadCount = uint.Parse(args[0]);

		for (uint i = 0; i < ThreadCount; i++) {
			Thread t = new(ThreadRun);

			t.Start();
		}

		Console.ReadLine();

		Logger.StopLogging();
	}

	private static void ThreadRun(object obj) {
		Thread t = Thread.CurrentThread;
		while (Run)
			Logger.Log($"Log from thread {t.ManagedThreadId}");
	}
}

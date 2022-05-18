using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Kettu.Binary;

public static class BinarySerializer {
	public static void Serialize(BinaryWriter writer, LoggerLine line) {
		writer.Write(Encoding.UTF8.GetBytes("LOGHEADER"));
		writer.Write(BinaryLogger.BINARY_VERSION);    //int
		writer.Write(line.LoggerLevel.Name);          //str
		writer.Write(line.LoggerLevel.Channel ?? ""); //str
		writer.Write(line.LineData);                  //str
		if (line.StackTrace.FrameCount == 0) {
			writer.Write((int)0);
		}
		else {
			writer.Write(line.StackTrace.FrameCount); //int
			foreach (StackFrame stackFrame in line.StackTrace.GetFrames()!)
				writer.Write(stackFrame.ToString()); //str
		}
		writer.Write(line.TimeStamp.ToBinary());
	}

	public static DeserializedLoggerLevel? Deserialize(Stream stream) {
		DeserializedLoggerLevel deserialized = new();

		long startPos = stream.Position;

		BinaryReader reader = new(stream);

		string str = Encoding.UTF8.GetString(reader.ReadBytes("LOGHEADER".Length));
		if (str != "LOGHEADER") {
			if(stream.CanSeek)
				stream.Seek(startPos - stream.Position, SeekOrigin.Current);
			return null;
		}

		uint ver = reader.ReadUInt32();
		switch (ver) {
			case BinaryLogger.BINARY_VERSION: {
				deserialized.LevelName    = reader.ReadString();
				deserialized.LevelChannel = reader.ReadString();
				deserialized.LineData     = reader.ReadString();

				int frameCount = reader.ReadInt32();
				deserialized.StackFrames = new string[frameCount];
				for (int i = 0; i < frameCount; i++) {
					deserialized.StackFrames[i] = reader.ReadString();
				}

				deserialized.TimeStamp = DateTime.FromBinary(reader.ReadInt64());
				break;
			}
		}
		
		return deserialized;
	}
}

public class DeserializedLoggerLevel {
	public DateTime TimeStamp;
	public string[] StackFrames  = Array.Empty<string>();
	public string   LineData     = "";
	public string   LevelChannel = "";
	public string   LevelName    = "";
}

namespace Kettu.Binary; 

public class BinaryLogger : LoggerBase {
	private Stream?       _targetStream;
	private BinaryWriter? _writer;

	public void SetTargetStream(Stream stream) {
		if (this._targetStream == null) {
			this._targetStream = stream;
			this._writer      = new BinaryWriter(stream);
			return;
		}
			
		lock (this._targetStream) {
			this._targetStream = stream;
			this._writer      = new BinaryWriter(stream);
		}
	}

	/// <summary>
	/// Creates a new binary logger
	/// </summary>
	/// <param name="stream">The stream to send to (null = none)</param>
	public BinaryLogger(Stream stream) {
		this.SetTargetStream(stream);
	}
		
	public override bool AllowMultiple => true;

	public const uint BINARY_VERSION = 0;
		
	public override void Send(LoggerLine line) {
		if (this._targetStream is not { CanWrite: true }) {
			return;
		}
			
		lock (this._targetStream) {
			BinarySerializer.Serialize(this._writer!, line);
			this._writer!.Flush();
		}
	}
}
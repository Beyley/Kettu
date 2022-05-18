using Eto.Drawing;
using Eto.Forms;
using Kettu.Binary;

namespace Kettu.BinaryReader; 

public sealed class ReaderForm : Form {
	private readonly ReaderControl _readerControl;
	
	public ReaderForm() {
		this.ClientSize = new Size(1024, 768);

		this.Title = "Kettu Binary Log Reader";

		this.ToolBar = new ToolBar {
			Items = { new ButtonToolItem { Text = "Open New", Command = new OpenNewCommand() } }
		};

		this.Content = this._readerControl = new ReaderControl();

		this.AllowDrop         =  true;
		this.DragDrop          += this.OnDragDrop;
		this.DragEnter         += OnDragEnter;
	}

	public void ReadFile(string path) {
		using FileStream stream = File.OpenRead(path);

		this._readerControl.SuspendLayout();
		this._readerControl.Clear();
		while (stream.Position < stream.Length) {
			DeserializedLoggerLevel? line = BinarySerializer.Deserialize(stream);
			
			this._readerControl.AddNewEntry(line!);
		}
		this._readerControl.ResumeLayout();
	}
	
	private static void OnDragEnter(object sender, DragEventArgs e) {
		e.Effects = DragEffects.Link;
	}

	private void OnDragDrop(object sender, DragEventArgs e) {
		//TODO: figure this out
	}
}

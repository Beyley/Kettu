using Eto.Forms;
using Kettu.Binary;

namespace Kettu.BinaryReader;

public class ReaderControl : Scrollable {
	private readonly TableLayout   _tableLayout;
	private readonly DynamicLayout _dynamicLayout;
	
	public ReaderControl() {
		this.Content = this._dynamicLayout = new DynamicLayout();
		this._tableLayout = new TableLayout();

		this._dynamicLayout.BeginVertical(new(5));
		this._dynamicLayout.Add(this._tableLayout);
		// this._dynamicLayout.Add(null);
		this._dynamicLayout.EndVertical();

		this._tableLayout.Size = new(-1, -1);
	}
	
	public void AddNewEntry(DeserializedLoggerLevel level) {
		DropDown dropDown;
		this._tableLayout.Rows.Add(new TableRow(
									   new TableCell(new Label { Text = level.LevelChannel.Length != 0 ? $"{level.LevelName} ({level.LevelChannel})" : $"{level.LevelName}" }, true),
									   new TableCell(new Label { Text = $"{level.LineData}" }, true),
									   new TableCell(dropDown = new DropDown(), true),
									   new TableCell(new Label { Text = $"{level.TimeStamp}" }, true)
								   ));

		foreach (string frame in level.StackFrames) {
			dropDown.Items.Add(frame.TrimEnd());
		}
	}
	public void Clear() {
		this._tableLayout.Rows.Clear();
	}
}

using Eto.Forms;

namespace Kettu.BinaryReader; 

public class OpenNewCommand : Command {
	protected override void OnExecuted(EventArgs e) {
		OpenFileDialog dialog = new() {
			MultiSelect = false
		};

		DialogResult        dialogResult = dialog.ShowDialog(Program.Form);
		string[] filenames    = dialog.Filenames.ToArray();
		
		dialog.Dispose();
		
		if (dialogResult != DialogResult.Ok)
			return;

		if (filenames.Length == 0)
			return;

		Program.App.Invoke(delegate {
			Program.Form.ReadFile(filenames[0]);
		});
		
		base.OnExecuted(e);
	}
}

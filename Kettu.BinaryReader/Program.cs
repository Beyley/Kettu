using Eto.Forms;

namespace Kettu.BinaryReader; 

public class Program {
	public static Application App;
	public static ReaderForm  Form;
	
	[STAThread]
	public static void Main(string[] args) {
		App  = new Application();
		Form = new ReaderForm();
		
		App.Run(Form);
	}
} 

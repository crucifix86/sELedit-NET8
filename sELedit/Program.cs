using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sELedit
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			// Register encoding provider to support GBK and other encodings
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}
	}
}

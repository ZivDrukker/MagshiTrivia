using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace client
{

	static class Program
	{
		public static bool notClosed = true;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Thread log = new Thread(() => Application.Run(new LogForm()));
			log.Start();
			Application.Run(new LoginScreen());


			var log2 = Application.OpenForms.OfType<LogForm>().Single();
			log2.Invoke((MethodInvoker)delegate
			{
				log2.closeLog();
			});
		}

		public static List<string> StrSplit(string str, char ch)
		{
			string[] values = str.Split(ch);
			List<string> toRet = new List<string>();

			for (int i = 0; i < values.Length; i++)
			{
				if (values[i] != "")
				{
					toRet.Add(values[i]);
				}
			}

			return toRet;
		}
	}
}

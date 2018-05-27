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
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//Application.Run(new GameScreen(null));
			Thread log = new Thread(() => Application.Run(new LogForm()));
			log.Start();
			Application.Run(new LoginScreen());
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

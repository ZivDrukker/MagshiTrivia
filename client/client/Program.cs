using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Text;


namespace client
{
	static class Program
	{
		//key is 1337 % UcfSecretEnc % 42 = 1337 % 1178 % 42= 159 % 42 = 33
		public const int key = 33;
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

		public static string Encrypto(string msg)
		{
			string t = "";
			for (int i = 0; i < msg.Length; i++)
			{
				t += (char)((int)(msg[i]) ^ key);
			}

			string a = "";

			for (int i = 0; i < t.Length; i++)
			{
				a += t[i];
			}
			a += (char)0;

			return Convert.ToBase64String(new ASCIIEncoding().GetBytes(a));
		}

		public static string Decrypto(string msgBytes)
		{
			string t = "", msg = new ASCIIEncoding().GetString((Convert.FromBase64String(msgBytes)));

			for (int i = 0; i < msg.Length; i++)
			{
				t += (char)((int)(msg[i]) ^ key);
			}

			string a = "";

			for(int i = 0; i < t.Length; i++)
			{
				a += t[i];
			}

			return a;
		}

		public static void SendMsg(NetworkStream sock, string msg)
		{
			var log = Application.OpenForms.OfType<LogForm>().Single();
			log.Invoke((MethodInvoker)delegate { log.SetLog("Sent: " + msg + "\n"); });

			msg = Encrypto(msg);
			
			byte[] buffer = new ASCIIEncoding().GetBytes(msg);
			try
			{
				if (sock.CanWrite)
				{
					sock.Write(buffer, 0, msg.Length);
				}
			}
			catch(Exception e)
			{
				MessageBox.Show(e.ToString());
			}
			sock.Flush();
		}

		public static string RecvMsg(NetworkStream sock)
		{
			bool endFound = false;

			//recive signin answer
			byte[] bufferIn = new byte[4096];
			int bytesRead = sock.Read(bufferIn, 0, 4096);
			string input = new ASCIIEncoding().GetString(bufferIn);

			input = Decrypto(input);

			for (int i = 0; i < input.Length && !endFound; i++)
			{
				if (input[i] == '\0')
				{
					endFound = true;
					input = input.Substring(0, i);
				}
			}

			var log = Application.OpenForms.OfType<LogForm>().Single();
			log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });

			return input;
		}
	}
}

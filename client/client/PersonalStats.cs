using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace client
{
	public partial class PersonalStats : Form
	{
		public PersonalStats(NetworkStream socket)
		{
			InitializeComponent();

			sock = socket;
		}

		private void PersonalStats_Load(object sender, EventArgs e)
		{
			HandleStats();
		}

		private void HandleStats()
		{
			try
			{
				//send request
				string msg = "225";

				var log = Application.OpenForms.OfType<LogForm>().Single();
				log.Invoke((MethodInvoker)delegate { log.SetLog("Sent: " + msg + "\n"); });

				byte[] buffer = new ASCIIEncoding().GetBytes(msg);
				sock.Write(buffer, 0, msg.Length);
				sock.Flush();

				//recive answer
				byte[] bufferIn = new byte[4096];
				int bytesRead = sock.Read(bufferIn, 0, 4096);
				string input = new ASCIIEncoding().GetString(bufferIn);

				input = input.Substring(0, input.IndexOf('\0'));

				List<string> reply = Program.StrSplit(input, '#');

				log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });

				if(reply[0] == "126")
				{
					this.stats.Text = "Number of Games: " + reply[1] + "\n\n\n"
									 + "Number of Right Answers: " + reply[2] + "\n\n\n"
									 + "Number of Wrong Answers: " + reply[3] + "\n\n\n"
									 + "Average Time per Answer: " + reply[4];//in our code the message sends the time as the final string
				}
				else
				{
					MessageBox.Show("Error!\n\nServer should not send this message...\nTry again");
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

		private void close_Click(object sender, EventArgs e)
		{
			this.Hide();
			this.Close();
		}
	}
}

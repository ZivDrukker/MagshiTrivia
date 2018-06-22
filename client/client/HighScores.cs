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
	public partial class HighScores : Form
	{
		public HighScores(NetworkStream socket)
		{
			InitializeComponent();

			sock = socket;
		}

		private void close_Click(object sender, EventArgs e)
		{
			this.Hide();
			this.Close();
		}

		private void HighScores_Load(object sender, EventArgs e)
		{
			HandleTopScores();
		}

		private void HandleTopScores()
		{
			try
			{
				//send request
				string msg = "223";

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

				if (reply[0] == "124")
				{
					this.scores.Text = "";

					if (reply.Count() != 1)
					{
						for (int i = 1; i < reply.Count(); i++)
						{
							this.scores.Text += reply[i];
							if (i != reply.Count() - 1)
							{
								this.scores.Text += "\n\n\n\n";
							}
						}
					}
					else
					{
						this.scores.Text = "No top scores!\n\n\nConsidered playing a game?";
					}
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
	}
}

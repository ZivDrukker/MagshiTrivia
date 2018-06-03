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
	public partial class Game : Form
	{
		public Game(NetworkStream socket, List<string> reply, int qTime, int qNum)
		{
			InitializeComponent();
			sock = socket;

			this.qNum = qNum;
			this.qTime = qTime;
			this.scoreCount = 0;
			this.current = null;

			HandleQuestions(reply);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (timeLeft > 0)
			{
				timeLeft = timeLeft - 1;
				time.Text = timeLeft.ToString();
			}
			else
			{
				timer1.Stop();
				this.clickedButton = "5";
				this.answer1.BackColor = System.Drawing.Color.Red;
				this.answer2.BackColor = System.Drawing.Color.Red;
				this.answer3.BackColor = System.Drawing.Color.Red;
				this.answer4.BackColor = System.Drawing.Color.Red;
			}
		}

		private void HandleQuestions(List<string> reply)
		{
			for(int i = 0; i < qNum; i++)
			{
				timeLeft = qTime * 1000;
				timer1.Start();
				this.question.Text = reply[1];
				this.questionNum.Text = i.ToString();
				this.score.Text = scoreCount.ToString();
				this.answer1.Text = reply[2];
				this.answer2.Text = reply[3];
				this.answer3.Text = reply[4];
				this.answer4.Text = reply[5];

				while(this.clickedButton == null)
				{
					continue;
				}

				try
				{
					//send request
					string msg = "219#" + this.clickedButton + "#" + (this.qTime - (this.timeLeft / 1000)).ToString();

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

					reply = Program.StrSplit(input, '#');

					log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });

					if (reply[0] == "120")
					{
						if (reply[1] == "1")
						{
							this.scoreCount++;
							if (this.current != null)
							{
								this.current.BackColor = System.Drawing.Color.Green;
							}
						}
						else
						{
							if (this.current != null)
							{
								this.current.BackColor = System.Drawing.Color.Red;
							}
						}
						this.current = null;

						this.score.Text = "Score: " + this.scoreCount + "/" + i.ToString();
					}


					while(reply[0] != "118")
					{
						//recive answer
						bufferIn = new byte[4096];
						bytesRead = sock.Read(bufferIn, 0, 4096);
						input = new ASCIIEncoding().GetString(bufferIn);

						input = input.Substring(0, input.IndexOf('\0'));

						reply = Program.StrSplit(input, '#');

						log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived again: " + input + "\n\n"); });
					}
				}
				catch (Exception e)
				{
					MessageBox.Show(e.ToString());
				}

				this.clickedButton = null;
			}
		}

		private void Button_Click(object sender, EventArgs e)
		{
			int count = this.scoreCount;

			this.clickedButton = ((Button)sender).Name.Substring(5);
			timer1.Stop();

			this.current = ((Button)sender);

			//while(this.clickedButton != null)
			//{
			//	continue;
			//}

			//((Button)sender).BackColor = (this.scoreCount > count ? System.Drawing.Color.Green : System.Drawing.Color.Red);
		}

		private void HandleCorrect()
		{
			
		}

		private void HandleWrong()
		{
			this.answer1.BackColor = System.Drawing.Color.Red;
			this.answer2.BackColor = System.Drawing.Color.Red;
			this.answer3.BackColor = System.Drawing.Color.Red;
			this.answer4.BackColor = System.Drawing.Color.Red;
		}
	}
}

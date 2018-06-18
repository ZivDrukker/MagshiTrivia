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
using System.Threading;

namespace client
{
	public partial class Game : Form
	{
		public Game(NetworkStream socket, List<string> reply, int qTime, int qNum)
		{
			InitializeComponent();
			sock = socket;

			_reply = reply;

			this.qNum = qNum;
			this.qTime = qTime;
			this.currQNum = 1;
			this.scoreCount = 0;
			this.current = null;

			//this.Show();
			setGroundForQuestion();
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

		private void setGroundForQuestion()
		{
			timeLeft = qTime;
			timer1.Start();
			this.question.Text = _reply[1];
			this.questionNum.Text = "Question: " + currQNum.ToString() + "/" + this.qNum.ToString();
			currQNum++;
			this.score.Text = "Score: " + scoreCount.ToString() + "/" + this.clicks.ToString();
			this.answer1.Text = _reply[2];
			this.answer2.Text = _reply[3];
			this.answer3.Text = _reply[4];
			this.answer4.Text = _reply[5];
		}

		private void HandleQuestions()
		{
			try
			{
				//send request
				string msg = "219#" + this.clickedButton + "#" + (this.qTime - this.timeLeft).ToString();

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

				_reply = Program.StrSplit(input, '#');

				log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });

				if (_reply[0] == "120")
				{
					if (_reply[1] == "1")
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
					this.score.Text = "Score: " + this.scoreCount + "/" + (currQNum--).ToString();
				}

				while (_reply[0] != "118")
				{
					//recive answer
					bufferIn = new byte[4096];
					bytesRead = sock.Read(bufferIn, 0, 4096);
					input = new ASCIIEncoding().GetString(bufferIn);

					input = input.Substring(0, input.IndexOf('\0'));

					_reply = Program.StrSplit(input, '#');

					log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived again: " + input + "\n\n"); });

					this.answer1.BackColor = System.Drawing.Color.White;
					this.answer2.BackColor = System.Drawing.Color.White;
					this.answer3.BackColor = System.Drawing.Color.White;
					this.answer4.BackColor = System.Drawing.Color.White;

					if (_reply[0] == "121")
					{
						string toPrint = "";
						List<Tuple<int, string>> scores = new List<Tuple<int, string>>();

						for (int i = 1; i < _reply.Count(); i += 2)
						{
							scores.Add(new Tuple<int, string>(Int32.Parse(_reply[i + 1]), _reply[i]));
						}

						scores.Sort();

						for (int i = 0; i < scores.Count(); i++)
						{
							toPrint += scores[i].Item2 + ": " + scores[i].Item1.ToString() + "\n\n";
						}

						MessageBox.Show(toPrint);
						this.Hide();
						_reply[0] = "118";
						this.Close();
					}

				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}

			this.clickedButton = null;
		}

		private void Button_Click(object sender, EventArgs e)
		{
			int count = this.scoreCount;
			clicks++;

			this.clickedButton = ((Button)sender).Name.Substring(6);
			timer1.Stop();

			this.current = ((Button)sender);
			HandleQuestions();
			if (this.clicks < this.qNum)
			{
				setGroundForQuestion();
			}

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

		private void Game_Load(object sender, EventArgs e)
		{
			//HandleQuestions(true);
		}
	}
}

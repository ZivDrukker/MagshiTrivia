using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace client
{
	public partial class CreateRoom : Form
	{
		public CreateRoom(NetworkStream socket)
		{
			InitializeComponent();

			this.sock = socket;
		}

		private void roomName_Enter(object sender, EventArgs e)
		{
			if (this.roomName.Text == "Insert room name here")
			{
				this.roomName.Text = "";
				this.roomName.ForeColor = System.Drawing.SystemColors.WindowText;
			}
		}

		private void roomName_Leave(object sender, EventArgs e)
		{
			if (this.roomName.Text == "")
			{
				this.roomName.ForeColor = System.Drawing.SystemColors.InactiveCaption;
				this.roomName.Text = "Insert room name here";
			}
		}

		private void playersNum_Enter(object sender, EventArgs e)
		{
			if (this.playersNum.Text == "Insert number of players here")
			{
				this.playersNum.Text = "";
				this.playersNum.ForeColor = System.Drawing.SystemColors.WindowText;
			}
		}

		private void playersNum_Leave(object sender, EventArgs e)
		{
			if (this.playersNum.Text == "")
			{
				this.playersNum.ForeColor = System.Drawing.SystemColors.InactiveCaption;
				this.playersNum.Text = "Insert number of players here";
			}
		}

		private void questionNum_Enter(object sender, EventArgs e)
		{
			if (this.questionNum.Text == "Insert number of questions here")
			{
				this.questionNum.Text = "";
				this.questionNum.ForeColor = System.Drawing.SystemColors.WindowText;
			}
		}

		private void questionNum_Leave(object sender, EventArgs e)
		{
			if (this.questionNum.Text == "")
			{
				this.questionNum.ForeColor = System.Drawing.SystemColors.InactiveCaption;
				this.questionNum.Text = "Insert number of questions here";
			}
		}

		private void timePerQuestion_Enter(object sender, EventArgs e)
		{
			if (this.timePerQuestion.Text == "Insert time per question here")
			{
				this.timePerQuestion.Text = "";
				this.timePerQuestion.ForeColor = System.Drawing.SystemColors.WindowText;
			}
		}

		private void timePerQuestion_Leave(object sender, EventArgs e)
		{
			if (this.timePerQuestion.Text == "")
			{
				this.timePerQuestion.ForeColor = System.Drawing.SystemColors.InactiveCaption;
				this.timePerQuestion.Text = "Insert time per question here";
			}
		}

		private void send_Click(object sender, EventArgs e)
		{
			HandleCreation();
		}

		private void back_Click(object sender, EventArgs e)
		{
			this.Hide();
			this.Close();
		}

		private void HandleCreation()
		{
			try
			{
				//send signin request
				string msg = "";
				msg = "213" + "##" + this.roomName.Text + "##" + this.playersNum.Text + "##" + this.questionNum.Text + "##" + this.timePerQuestion.Text;

				var log = Application.OpenForms.OfType<LogForm>().Single();
				log.Invoke((MethodInvoker)delegate { log.SetLog("Sent: " + msg + "\n"); });

				byte[] buffer = new ASCIIEncoding().GetBytes(msg);
				sock.Write(buffer, 0, msg.Length);
				sock.Flush();

				//recive signin answer
				byte[] bufferIn = new byte[4];
				int bytesRead = sock.Read(bufferIn, 0, 4);
				string input = new ASCIIEncoding().GetString(bufferIn);

				log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });

				//checking answer
				if (input == "1140")
				{
					WaitForRoom waiting = new WaitForRoom(sock, true, -1, roomName.Text, this.questionNum.Text, this.timePerQuestion.Text, this.playersNum.Text);//	-1 will be a default value for gameID
					this.Hide();
					waiting.ShowDialog();
					this.Close();
				}
				else
				{
					MessageBox.Show("The room was not created!\n\nPlease try again");
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}
	}
}

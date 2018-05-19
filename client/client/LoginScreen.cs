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
	public partial class LoginScreen : Form
	{
		public LoginScreen()
		{
			InitializeComponent();

			//socket init
			int port = 1337;
			string ip = "127.0.0.1";

			try
			{
				TcpClient client = new TcpClient();
				client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

				this.sock = client.GetStream();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

		private void username_Enter(object sender, EventArgs e)
		{
			if (this.username.Text == "Insert username here")
			{
				this.username.Text = "";
				this.username.ForeColor = System.Drawing.SystemColors.WindowText;
			}
		}

		private void password_Enter(object sender, EventArgs e)
		{
			if (this.password.Text == "Insert password here")
			{
				this.password.Text = "";
				this.password.ForeColor = System.Drawing.SystemColors.WindowText;
				this.password.PasswordChar = '*';
			}
		}

		private void email_Enter(object sender, EventArgs e)
		{
			if (this.email.Text == "Insert email here - FOR SIGN UP ONLY")
			{
				this.email.Text = "";
				this.email.ForeColor = System.Drawing.SystemColors.WindowText;
			}
		}

		private void username_Leave(object sender, EventArgs e)
		{
			if(this.username.Text == "")
			{
				this.username.ForeColor = System.Drawing.SystemColors.InactiveCaption;
				this.username.Text = "Insert username here";
			}
			
		}

		private void password_Leave(object sender, EventArgs e)
		{
			if(this.password.Text == "")
			{
				this.password.ForeColor = System.Drawing.SystemColors.InactiveCaption;
				this.password.PasswordChar = (char)0;
				this.password.Text = "Insert password here";
			}
		}

		private void email_Leave(object sender, EventArgs e)
		{
			if (this.email.Text == "")
			{
				this.email.ForeColor = System.Drawing.SystemColors.InactiveCaption;
				this.email.Text = "Insert email here - FOR SIGN UP ONLY";
			}

		}

		private void signin_Click(object sender, EventArgs e)
		{
			HandleSignin();
		}

		private void signup_Click(object sender, EventArgs e)
		{
			HandleSignup();
		}

		public void HandleSignin()
		{
			try
			{
				//send signin request
				string msg = "";
				msg = "200" + "##" + this.username.Text + "##" + this.password.Text;

				var log = Application.OpenForms.OfType<LogForm>().Single();
				log.SetLog("Sent: " + msg + "\n");

				byte[] buffer = new ASCIIEncoding().GetBytes(msg);
				sock.Write(buffer, 0, msg.Length);
				sock.Flush();

				//recive signin answer
				byte[] bufferIn = new byte[4];
				int bytesRead = sock.Read(bufferIn, 0, 4);
				string input = new ASCIIEncoding().GetString(bufferIn);

				log.SetLog(log.GetLog() + "Recived: " + input + "\n\n");

				//checking answer
				if (input == "1020")
				{
					GameScreen startGame = new GameScreen(sock);
					startGame.Activate();
					this.Close();//							REVIEW OPTION TO JUST HIDE AND COME BACK TO SAME LOGIN CREDENTIALS
				}
				else if (input == "1021")
				{
					MessageBox.Show("Wrong Details!\n\nPlease try again");
				}
				else
				{
					MessageBox.Show("The user is already connected!\n\nPlease try again");
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}
		
		public void HandleSignup()
		{
			try
			{
				//send signin request
				string msg = "";
				msg = "203" + "##" + this.username.Text + "##" + this.password.Text + "##" + this.email.Text;

				var log = Application.OpenForms.OfType<LogForm>().Single();
				log.SetLog("Sent: " + msg + "\n");

				byte[] buffer = new ASCIIEncoding().GetBytes(msg);
				sock.Write(buffer, 0, msg.Length);
				sock.Flush();

				//recive signin answer
				byte[] bufferIn = new byte[4];
				int bytesRead = sock.Read(bufferIn, 0, 4);
				string input = new ASCIIEncoding().GetString(bufferIn);

				log.SetLog(log.GetLog() + "Recived: " + input + "\n\n");

				//checking answer
				if (input == "1040")
				{
					GameScreen startGame = new GameScreen(sock);
					startGame.Activate();
					this.Close();//							REVIEW OPTION TO JUST HIDE AND COME BACK TO SAME LOGIN CREDENTIALS
				}
				else if (input == "1041")
				{
					MessageBox.Show("Password is Illegal!\n\nPlease try again");
				}
				else if(input == "1042")
				{
					MessageBox.Show("Username already exists!\n\nPlease try again");
				}
				else if(input == "1043")
				{
					MessageBox.Show("Username is Illegal!\n\nPlease try again");
				}
				else
				{
					MessageBox.Show("Email is Illegal!\n\nPlease try again");
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}
	}
}

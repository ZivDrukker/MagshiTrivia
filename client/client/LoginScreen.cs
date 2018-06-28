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
				client = new TcpClient();
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

		private void HandleSignin()
		{
			try
			{
				//send signin request
				string msg = "";
				msg = "200" + "##" + this.username.Text + "##" + this.password.Text;

				Program.SendMsg(sock, msg);

				string input = Program.RecvMsg(sock);

				//checking answer
				if (input == "1020")
				{
					GameScreen startGame = new GameScreen(sock);
					this.Hide();
					startGame.Activate();
					startGame.ShowDialog();
					this.Show();//							REVIEW OPTION TO JUST HIDE AND COME BACK TO SAME LOGIN CREDENTIALS

					//erasing old credantials
					this.email.ForeColor = System.Drawing.SystemColors.InactiveCaption;
					this.email.Text = "Insert email here - FOR SIGN UP ONLY";

					this.username.ForeColor = System.Drawing.SystemColors.InactiveCaption;
					this.username.Text = "Insert username here";

					this.password.ForeColor = System.Drawing.SystemColors.InactiveCaption;
					this.password.PasswordChar = (char)0;
					this.password.Text = "Insert password here";


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
				MessageBox.Show(e.Message);
			}
		}
		
		private void HandleSignup()
		{
			try
			{
				//send signin request
				string msg = "";

				if (this.email.Text == "Insert email here - FOR SIGN UP ONLY")
				{
					MessageBox.Show("Please insert email address!\n\nTry again");
				}
				else
				{
					msg = "203" + "##" + this.username.Text + "##" + this.password.Text + "##" + this.email.Text;

					Program.SendMsg(sock, msg);

					string input = Program.RecvMsg(sock);

					//checking answer
					if (input == "1040")
					{
						GameScreen startGame = new GameScreen(sock);
						this.Hide();
						startGame.Activate();
						startGame.ShowDialog();
						this.Show();//							REVIEW OPTION TO JUST HIDE AND COME BACK TO SAME LOGIN CREDENTIALS
					}
					else if (input == "1041")
					{
						MessageBox.Show("Password is Illegal!\n\nPlease try again");
					}
					else if (input == "1042")
					{
						MessageBox.Show("Username already exists!\n\nPlease try again");
					}
					else if (input == "1043")
					{
						MessageBox.Show("Username is Illegal!\n\nPlease try again");
					}
					else
					{
						MessageBox.Show("Email is Illegal!\n\nPlease try again");
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);

			if (e.CloseReason == CloseReason.WindowsShutDown) return;

			if (sock != null && client != null)
			{
				sock.Close();
				client.Close();
			}
		}
	}
}

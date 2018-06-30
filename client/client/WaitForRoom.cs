using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;

namespace client
{
	public partial class WaitForRoom : Form
	{
		public WaitForRoom(NetworkStream socket, bool isAdmin, int roomID, string roomName, string questionNum = "", string questionTime = "", string playersNum = "")
		{
			InitializeComponent();

			sock = socket;
			reply = new List<string>();

			if (!isAdmin)
			{
				HandleNotAdmin();
			}

			playersNum = HandleRoomDetails(GetRoomID(roomName));//filling up all fields that may be missing and users list

			this.info.Text = "Max Number of Players: " + playersNum + "\n\n\n\n"
							 + "Number of Questions: " + questionNum + "\n\n\n\n"
							 + "Time Per Question: " + questionTime;

			qTime = Int32.Parse(questionTime);
			qNum = Int32.Parse(questionNum);

			this.roomName.Text += roomName;

			Program.notClosed = true;
			Thread useless = new Thread(threadController);
			useless.Start();
		}

		private void threadController()
		{
			usersUpdater = new Thread(usersList);
			usersUpdater.Start();
			usersUpdater.Join();

			try
			{
				this.Invoke((MethodInvoker)delegate { this.Hide(); this.Close(); });
			}
			catch (Exception)
			{
				//If you are careful enough reading the code to get here I really really appreciate you! <3
			}
		}

		private string HandleRoomDetails(int id)
		{
			try
			{
				//send request
				string msg = "207" + "#" + id.ToString();

				Program.SendMsg(sock, msg);

				string input = Program.RecvMsg(sock);


				List<string> reply = Program.StrSplit(input, '#');

				//also insert all of the rooms into the GUI
				for(int i = 1; i < reply.Count(); i++)
				{
					this.users.Items.Add(reply[i]);
				}

				return (reply.Count() - 1).ToString();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

			return "ERROR ACCOURD";
		}

		private int GetRoomID(string name)
		{
			try
			{
				//send request
				string msg = "205";

				Program.SendMsg(sock, msg);

				string input = Program.RecvMsg(sock);

				List<string> reply = Program.StrSplit(input, '#');

				if(reply[1] != "0")
				{
					for(int i = 3; i < reply.Count(); i++)
					{
						if(reply[i] == name)
						{
							return Int32.Parse(reply[i - 1]);
						}
					}
				}

				return -1;//ERROR
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

			return -1;//ERROR
		}

		private void close_Click(object sender, EventArgs e)
		{
			HandleRoomClose();
		}

		private void HandleRoomClose()
		{
			try
			{
				//send request
				string msg = "215";

				Program.SendMsg(sock, msg);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		private void startOrLeave_Click(object sender, EventArgs e)
		{
			HandleStartRoom();
		}

		private void HandleStartRoom()
		{
			try
			{
				//send request
				string msg = (this.startOrLeave.Text == "Start Game" ? "217" : "211");

				Program.SendMsg(sock, msg);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		private void HandleNotAdmin()
		{
			this.close.Hide();
			this.startOrLeave.Location = new System.Drawing.Point(275, 400);
			this.startOrLeave.Text = "Leave Room";
		}

		private void usersList()
		{//handle all recieved messages in form to avoid bugs and to update the users list in one place
			try
			{
				while (Program.notClosed)//not to access after form closed and thread still running
				{
					string input = Program.RecvMsg(sock);

					this.reply = Program.StrSplit(input, '#');

					this.Invoke((MethodInvoker)delegate
					{
						if (reply[0] == "108")
						{
							this.users.Items.Clear();

							for (int i = 1; i < reply.Count(); i++)
							{
								this.users.Items.Add(reply[i]);
							}
						}
						else
						{
							if (this.startOrLeave.Text == "Start Game")
							{
								if (reply[0] != "118" && reply[0] != "116")
								{
									MessageBox.Show("Could not start room!");
								}
								else if (reply[0] == "116")
								{
									this.Hide();
									//this.Close(); GOING TO CLOSE FORM OUTSIDE OF THE THREAD
									Program.notClosed = false;
								}
								else
								{
									Game runningGame = new Game(sock, reply, qTime, qNum);
									this.Hide();
									runningGame.ShowDialog();
									this.Show();
									//this.Close(); GOING TO CLOSE FORM OUTSIDE OF THE THREAD
									Program.notClosed = false;
								}
							}
							else
							{
								if (reply[0] == "1120")
								{
									this.Hide();
									//this.Close(); GOING TO CLOSE FORM OUTSIDE OF THE THREAD
									Program.notClosed = false;
								}
								else if (reply[0] == "116")
								{
									this.Hide();
									//this.Close(); GOING TO CLOSE FORM OUTSIDE OF THE THREAD
									Program.notClosed = false;
								}
								else if (reply[0] == "118")
								{
									Game runningGame = new Game(sock, reply, qTime, qNum);
									this.Hide();
									runningGame.ShowDialog();
									this.Show();
									//this.Close(); GOING TO CLOSE FORM OUTSIDE OF THE THREAD
									Program.notClosed = false;
								}
								else
								{
									MessageBox.Show("ERROR!!!\n\nRoom already started or closed");
								}
							}
						}
					});
				}
			}
			catch(ThreadAbortException e)
			{
				MessageBox.Show(e.Message);
			}
		}
	}
}

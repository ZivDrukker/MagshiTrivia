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

			usersUpdater = new Thread(usersList);
			usersUpdater.Start();
		}

		private string HandleRoomDetails(int id)
		{
			try
			{
				//send request
				string msg = "207" + "#" + id.ToString();

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

				//usersCount = Int32.Parse(reply[1]);

				//also insert all of the rooms into the GUI
				for(int i = 1; i < reply.Count(); i++)
				{
					this.users.Items.Add(reply[i]);
				}

				return (reply.Count() - 1).ToString();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}

			return "ERROR ACCOURD";
		}

		private int GetRoomID(string name)
		{
			try
			{
				//send request
				string msg = "205";

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
				MessageBox.Show(e.ToString());
			}

			return -1;//ERROR
		}

		private void close_Click(object sender, EventArgs e)
		{
			HandleRoomClose();

			this.Hide();
			this.Close();
		}

		private void HandleRoomClose()
		{
			try
			{
				//send request
				string msg = "215";

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
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
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
				//usersUpdater.Abort();//killing the thread so we get the messages here

				//send request
				string msg = (this.startOrLeave.Text == "Start Game" ? "217" : "211");

				var log = Application.OpenForms.OfType<LogForm>().Single();
				log.Invoke((MethodInvoker)delegate { log.SetLog("Sent: " + msg + "\n"); });

				byte[] buffer = new ASCIIEncoding().GetBytes(msg);
				sock.Write(buffer, 0, msg.Length);
				sock.Flush();

				string input = "recived somewhere else";
				////recive answer
				//byte[] bufferIn = new byte[4096];
				//int bytesRead = sock.Read(bufferIn, 0, 4096);
				//string input = new ASCIIEncoding().GetString(bufferIn);

				//input = input.Substring(0, input.IndexOf('\0'));

				//List<string> reply = Program.StrSplit(input, '#');

				log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

		private void HandleNotAdmin()
		{
			this.close.Hide();
			this.startOrLeave.Location = new System.Drawing.Point(275, 400);
			this.startOrLeave.Text = "Leave Room";
		}

		private void WaitForRoom_Load(object sender, EventArgs e)
		{

		}

		private void usersList()
		{
			while(true)
			{
				var log = Application.OpenForms.OfType<LogForm>().Single();

				//recive answer
				byte[] bufferIn = new byte[4096];
				int bytesRead = sock.Read(bufferIn, 0, 4096);
				string input = new ASCIIEncoding().GetString(bufferIn);

				input = input.Substring(0, input.IndexOf('\0'));

				this.reply = Program.StrSplit(input, '#');

				log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });

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
							if (reply[0] != "118")
							{
								MessageBox.Show("Could not start room!");
							}
							else if (reply[0] == "116")
							{
								this.Hide();
								this.Close();
							}
							else
							{
								Game runningGame = new Game(sock, reply, qTime, qNum);
								this.Hide();
								runningGame.ShowDialog();
								this.Close();
							}
						}
						else
						{
							if (reply[0] == "1120")
							{
								this.Hide();
								this.Close();
							}
							else if (reply[0] == "116")
							{
								this.Hide();
								this.Close();
							}
							else if (reply[0] == "118")
							{
								Game runningGame = new Game(sock, reply, qTime, qNum);
								this.Hide();
								runningGame.ShowDialog();
								this.Close();
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
	}
}

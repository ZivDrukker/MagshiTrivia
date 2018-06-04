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
	public partial class WaitForRoom : Form
	{
		public WaitForRoom(NetworkStream socket, bool isAdmin, int roomID, string roomName, string questionNum = "", string questionTime = "", string playersNum = "")
		{
			InitializeComponent();

			sock = socket;

			if (!isAdmin)
			{
				HandleNotAdmin();
			}

			//if (roomID != -1)
			//{
			//	roomID = GetRoomID(roomName);
			//}

			playersNum = HandleRoomDetails(GetRoomID(roomName));//filling up all fields that may be missing and users list

			this.info.Text = "Max Number of Players: " + playersNum + "\n\n\n\n"
							 + "Number of Questions: " + questionNum + "\n\n\n\n"
							 + "Time Per Question: " + questionTime;

			qTime = Int32.Parse(questionTime);
			qNum = Int32.Parse(questionNum);

			this.roomName.Text += roomName;
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
				for(int i = 1; i <= reply.Count(); i++)
					{
					this.users.Items.Add(reply[i]);
				}

				return reply[1];
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
					for(int i = 2; i < reply.Count(); i +=2)
					{
						if(reply[i + 1] == name)
						{
							return Int32.Parse(reply[i]);
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
				//send request
				string msg = (this.startOrLeave.Text == "Start Room" ? "217" : "211");

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

				if(this.startOrLeave.Text == "Start Room")
				{
					if (reply[1] == "0")
					{
						MessageBox.Show("Could not start room!");
					}
					else
					{
						Game runningGame = new Game(sock, reply, qTime, qNum);
						this.Hide();
						runningGame.ShowDialog();
						this.Show();
					}
				}
				else
				{
					if(reply[1] == "0")
					{
						this.Hide();
						this.Close();
					}
					else
					{
						MessageBox.Show("ERROR!!!\n\nRoom already started or closed");
					}
				}
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
	}
}

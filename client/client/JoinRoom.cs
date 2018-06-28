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
using System.Net;
using System.Net.Sockets;

namespace client
{
	public partial class JoinRoom : Form
	{
		public JoinRoom(NetworkStream socket)
		{
			InitializeComponent();

			this.sock = socket;
			this.roomsDic = new System.Collections.Generic.Dictionary<string, string>();
		}

		private void back_Click(object sender, EventArgs e)
		{
			this.Hide();
			this.Close();
		}

		private void join_Click(object sender, EventArgs e)
		{
			HandleRoomJoin();
		}

		private void refresh_Click(object sender, EventArgs e)
		{
			this.roomsDic.Clear();
			HandleRoomsList();
		}

		private void HandleRoomsList()
		{
			try
			{
				this.rooms.Items.Clear();
				//send request
				string msg = "205";

				Program.SendMsg(sock, msg);

				string input = Program.RecvMsg(sock);

				List<string> reply = Program.StrSplit(input, '#');

				if (reply[1] == "0")
				{
					this.alert.Text = "No Rooms Found";
				}
				else
				{
					for (int i = 2; i < reply.Count; i += 2)
					{
						this.rooms.Items.Add(reply[i + 1]);
						this.roomsDic[reply[i + 1]] = reply[i];
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		private void HandleRoomJoin()
		{
			try
			{
				//send request
				string msg = "209";

				if(rooms.SelectedItem.ToString() != "")
				{
					msg = msg + "#" + roomsDic[rooms.SelectedItem.ToString()];
				}
				else
				{
					msg += "#-1";//expecting error code in return
				}

				Program.SendMsg(sock, msg);

				string input = Program.RecvMsg(sock);

				List<string> reply = Program.StrSplit(input, '#');

				if(reply[0] == "108")
				{
					input = Program.RecvMsg(sock);

					reply = Program.StrSplit(input, '#');

					if (reply[0] == "1100")
					{
						WaitForRoom waiting = new WaitForRoom(sock, false, Int32.Parse(roomsDic[rooms.Text]), rooms.Text, reply[1], reply[2], "");
						this.Hide();
						waiting.ShowDialog();
						this.Close();
					}
					else if (reply[0] == "1101")
					{
						this.alert.Text = "Room is full!";
					}
					else
					{
						this.alert.Text = "Room does not exist!";
					}
				}
				else
				{
					if (reply[0] == "1100")
					{
						WaitForRoom waiting = new WaitForRoom(sock, false, Int32.Parse(roomsDic[rooms.Text]), rooms.Text, reply[1], reply[2], "");
						this.Hide();
						waiting.ShowDialog();
						this.Close();
					}
					else if (reply[0] == "1101")
					{
						this.alert.Text = "Room is full!";
					}
					else
					{
						this.alert.Text = "Room does not exist!";
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		private void HandleUsersRequest()
		{
			try
			{
				//send request
				string msg = "207";

				if (rooms.SelectedItem.ToString() != "")
				{
					msg = msg + "#" + roomsDic[rooms.Text];
				}
				else
				{
					msg += "#-1";//expecting error code in return
				}

				Program.SendMsg(sock, msg);

				string input = Program.RecvMsg(sock);

				List<string> reply = Program.StrSplit(input, '#');

				if (reply[0] != "1080")
				{
					string message = "";
					alert.Text = "";
					alert.ForeColor = System.Drawing.Color.Black;

					for (int i = 1; i < reply.Count(); i++)
					{
						message += (i != reply.Count() - 1 ? reply[i] + ", " : reply[i]);
					}

					this.alert.Text = message;
				}
				else if (reply[0] == "1080")//	DOESN'T EXIST IN SERVER !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
				{
					this.alert.Text = "Room doesn't exist!";
				}
				else
				{
					this.alert.Text = "Hi! You broke our program! Not nice... Go away.";
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
		
		private void JoinRoom_Load(object sender, EventArgs e)
		{
			HandleRoomsList();
		}

		private void rooms_SelectedIndexChanged(object sender, EventArgs e)
		{
			HandleUsersRequest();
		}
	}
}

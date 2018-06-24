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

				if (reply[1] == "0")
				{
					this.alert.Text = "No Rooms Found";
				}
				else
				{
					//	CHECK AND INSERT ROOMS TO LIST CONSIDER MAKING A GLOBAL DATA TYPE TO CONTAIN THE ROOMS
					for (int i = 2; i < reply.Count; i += 2)
					{
						this.rooms.Items.Add(reply[i + 1]);
						this.roomsDic[reply[i + 1]] = reply[i];
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
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

				if(reply[0] == "108")
				{
					//recive answer again
					bufferIn = new byte[4096];
					bytesRead = sock.Read(bufferIn, 0, 4096);
					input = new ASCIIEncoding().GetString(bufferIn);

					input = input.Substring(0, input.IndexOf('\0'));

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
				
				log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
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

				log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
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

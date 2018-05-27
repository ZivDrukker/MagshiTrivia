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
	public partial class JoinRoom : Form
	{
		public JoinRoom(NetworkStream socket)
		{
			InitializeComponent();

			this.sock = socket;
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
			HandleRoomsList();
		}

		private void HandleRoomsList()
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
				
				rooms.Items.Add("hey there");//	FOR DEBUG !!!!!!!!!!!!!!!!!!!!!! DELETE

				if (reply[1] == "0")
				{
					this.alert.Text = "No Rooms Found";
				}
				else
				{
					//	CHECK AND INSERT ROOMS TO LIST CONSIDER MAKING A GLOBAL DATA TYPE TO CONTAIN THE ROOMS
					for (int i = 2; i < Int32.Parse(reply[1]) * 2; i += 2)
					{
						this.rooms.Items.Add(reply[i]);
						this.roomsDic[reply[i]] = reply[i + 1];
					}
				}

				log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });

				
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

				if(rooms.Text != "")
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


				if (reply[0] == "1100")
				{
					//WaitForRoom waiting = new WaitForRoom(sock);
					//this.Hide();
					//waiting.ShowDialog();
					//this.Show();
				}
				else if(reply[0] == "1101")
				{
					this.alert.Text = "Room is full!";
				}
				else
				{
					this.alert.Text = "Room does not exist!";
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

				if (rooms.Text != "")
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

				int numOfUser = Int32.Parse(reply[1]);

				alert.Text = "";
				for (int i = 0; i < numOfUser; i++)
				{
					alert.ForeColor = System.Drawing.Color.Black;
					alert.Text += " " + reply[i+1];
				}

				if (reply[0] == "1100")
				{
					//WaitForRoom waiting = new WaitForRoom(sock);
					//this.Hide();
					//waiting.ShowDialog();
					//this.Show();
				}
				else if (reply[0] == "1101")
				{
					this.alert.Text = "Room is full!";
				}
				else
				{
					this.alert.Text = "Room does not exist!";
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

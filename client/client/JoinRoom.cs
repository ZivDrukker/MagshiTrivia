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
				byte[] bufferIn = new byte[4];
				int bytesRead = sock.Read(bufferIn, 0, 4);
				string input = new ASCIIEncoding().GetString(bufferIn);

				if(input[5] == '0')
				{
					this.alert.Text = "";
				}

				log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });

				//	CHECK AND INSERT ROOMS TO LIST CONSIDER MAKING A GLOBAL DATA TYPE TO CONTAIN THE ROOMS
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
	}
}

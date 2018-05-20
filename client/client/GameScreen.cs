﻿using System;
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
	public partial class GameScreen : Form
	{
		public GameScreen(NetworkStream socket)//we get a socket to keep the connection alive
		{
			InitializeComponent();

			this.sock = socket;
		}

		private void createRoom_Click(object sender, EventArgs e)
		{
			CreateRoom newRoom = new CreateRoom(sock);
			newRoom.Activate();
			this.Hide();
			newRoom.ShowDialog();
			this.Show();
		}

		private void joinRoom_Click(object sender, EventArgs e)
		{
			JoinRoom roomListing = new JoinRoom(sock);
			roomListing.Activate();
			this.Hide();
			roomListing.ShowDialog();
			this.Show();
		}
	}
}
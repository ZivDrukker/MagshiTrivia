namespace client
{
	partial class WaitForRoom
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitForRoom));
			this.Title = new System.Windows.Forms.Label();
			this.roomName = new System.Windows.Forms.Label();
			this.info = new System.Windows.Forms.Label();
			this.close = new System.Windows.Forms.Button();
			this.startOrLeave = new System.Windows.Forms.Button();
			this.users = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.AutoSize = true;
			this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold);
			this.Title.Location = new System.Drawing.Point(200, 30);
			this.Title.Name = "Title";
			this.Title.Size = this.MinimumSize;
			this.Title.TabIndex = 3;
			this.Title.Text = "MagshiTrivia";
			// 
			// roomName
			// 
			this.roomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.roomName.ForeColor = System.Drawing.Color.Blue;
			this.roomName.Location = new System.Drawing.Point(100, 125);
			this.roomName.Name = "roomName";
			this.roomName.Size = new System.Drawing.Size(600, 20);
			this.roomName.TabIndex = 7;
			this.roomName.Text = "Connected to room: ";
			this.roomName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// info
			// 
			this.info.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
			this.info.Location = new System.Drawing.Point(50, 175);
			this.info.Name = "info";
			this.info.Size = new System.Drawing.Size(300, 200);
			this.info.TabIndex = 8;
			this.info.Text = "WAITING FOR INFO!!!\n\nWAITING FOR INFO!!!\n\nWAITING FOR INFO!!!";
			this.info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// close
			// 
			this.close.Location = new System.Drawing.Point(75, 400);
			this.close.Name = "close";
			this.close.Size = new System.Drawing.Size(250, 75);
			this.close.TabIndex = 5;
			this.close.Text = "Close Room";
			this.close.UseVisualStyleBackColor = true;
			this.close.Click += new System.EventHandler(this.close_Click);
			// 
			// startOrLeave
			// 
			this.startOrLeave.Location = new System.Drawing.Point(475, 400);
			this.startOrLeave.Name = "startOrLeave";
			this.startOrLeave.Size = new System.Drawing.Size(250, 75);
			this.startOrLeave.TabIndex = 6;
			this.startOrLeave.Text = "Start Game";
			this.startOrLeave.UseVisualStyleBackColor = true;
			this.startOrLeave.Click += new System.EventHandler(this.startOrLeave_Click);
			// 
			// users
			// 
			this.users.FormattingEnabled = true;
			this.users.Location = new System.Drawing.Point(475, 175);
			this.users.Name = "users";
			this.users.Size = new System.Drawing.Size(250, 199);
			this.users.TabIndex = 9;
			// 
			// WaitForRoom
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 500);
			this.Controls.Add(this.users);
			this.Controls.Add(this.info);
			this.Controls.Add(this.roomName);
			this.Controls.Add(this.startOrLeave);
			this.Controls.Add(this.close);
			this.Controls.Add(this.Title);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 70);
			this.Name = "WaitForRoom";
			this.Text = "WaitForRoom";
			this.Load += new System.EventHandler(this.WaitForRoom_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.Label roomName;
		private System.Windows.Forms.Label info;
		private System.Windows.Forms.Button startOrLeave;
		private System.Windows.Forms.Button close;
		private System.Windows.Forms.ListBox users;

		private System.Net.Sockets.NetworkStream sock;

		private int usersCount;
		private int qTime;
		private int qNum;
	}
}
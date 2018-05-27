namespace client
{
	partial class JoinRoom
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JoinRoom));
			this.Title = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.rooms = new System.Windows.Forms.ListBox();
			this.alert = new System.Windows.Forms.Label();
			this.join = new System.Windows.Forms.Button();
			this.refresh = new System.Windows.Forms.Button();
			this.back = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.AutoSize = true;
			this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold);
			this.Title.Location = new System.Drawing.Point(50, 10);
			this.Title.Name = "Title";
			this.Title.Size = this.MinimumSize;
			this.Title.TabIndex = 2;
			this.Title.Text = "MagshiTrivia";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
			this.label1.Location = new System.Drawing.Point(165, 110);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(175, 37);
			this.label1.TabIndex = 3;
			this.label1.Text = "Rooms list:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rooms
			// 
			this.rooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.rooms.ForeColor = System.Drawing.SystemColors.Window;
			this.rooms.FormattingEnabled = true;
			this.rooms.ItemHeight = 20;
			this.rooms.Location = new System.Drawing.Point(50, 160);
			this.rooms.Name = "rooms";
			this.rooms.Size = new System.Drawing.Size(400, 284);
			this.rooms.TabIndex = 4;
			// 
			// alert
			// 
			this.alert.AutoSize = true;
			this.alert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.alert.ForeColor = System.Drawing.Color.Red;
			this.alert.Location = new System.Drawing.Point(165, 460);
			this.alert.Name = "alert";
			this.alert.Size = new System.Drawing.Size(169, 20);
			this.alert.TabIndex = 5;
			this.alert.Text = "No Available Rooms";
			this.alert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// join
			// 
			this.join.Location = new System.Drawing.Point(275, 510);
			this.join.Name = "join";
			this.join.Size = new System.Drawing.Size(200, 50);
			this.join.TabIndex = 4;
			this.join.Text = "Join Room";
			this.join.UseVisualStyleBackColor = true;
			this.join.Click += new System.EventHandler(this.join_Click);
			// 
			// refresh
			// 
			this.refresh.Location = new System.Drawing.Point(25, 510);
			this.refresh.Name = "refresh";
			this.refresh.Size = new System.Drawing.Size(200, 50);
			this.refresh.TabIndex = 4;
			this.refresh.Text = "Refresh";
			this.refresh.UseVisualStyleBackColor = true;
			this.refresh.Click += new System.EventHandler(this.refresh_Click);
			// 
			// back
			// 
			this.back.Location = new System.Drawing.Point(150, 575);
			this.back.Name = "back";
			this.back.Size = new System.Drawing.Size(200, 50);
			this.back.TabIndex = 4;
			this.back.Text = "Back";
			this.back.UseVisualStyleBackColor = true;
			this.back.Click += new System.EventHandler(this.back_Click);
			// 
			// JoinRoom
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(500, 650);
			this.Controls.Add(this.alert);
			this.Controls.Add(this.rooms);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.back);
			this.Controls.Add(this.refresh);
			this.Controls.Add(this.join);
			this.Controls.Add(this.Title);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 70);
			this.Name = "JoinRoom";
			this.Text = "JoinRoom";
			this.Load += new System.EventHandler(this.JoinRoom_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox rooms;
		private System.Windows.Forms.Label alert;
		private System.Windows.Forms.Button join;
		private System.Windows.Forms.Button refresh;
		private System.Windows.Forms.Button back;

		private System.Net.Sockets.NetworkStream sock;
		private System.Collections.Generic.Dictionary<string, string> roomsDic;
	}
}
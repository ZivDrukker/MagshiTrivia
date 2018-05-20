namespace client
{
	partial class GameScreen
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameScreen));
			this.Title = new System.Windows.Forms.Label();
			this.joinRoom = new System.Windows.Forms.Button();
			this.createRoom = new System.Windows.Forms.Button();
			this.status = new System.Windows.Forms.Button();
			this.bestScores = new System.Windows.Forms.Button();
			this.quit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.AutoSize = true;
			this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold);
			this.Title.Location = new System.Drawing.Point(50, 25);
			this.Title.Name = "Title";
			this.Title.Size = this.MinimumSize;
			this.Title.TabIndex = 0;
			this.Title.Text = "MagshiTrivia";
			// 
			// joinRoom
			// 
			this.joinRoom.Location = new System.Drawing.Point(150, 220);
			this.joinRoom.Name = "joinRoom";
			this.joinRoom.Size = new System.Drawing.Size(200, 50);
			this.joinRoom.TabIndex = 2;
			this.joinRoom.Text = "Join Room";
			this.joinRoom.UseVisualStyleBackColor = true;
			this.joinRoom.Click += new System.EventHandler(this.joinRoom_Click);
			// 
			// createRoom
			// 
			this.createRoom.Location = new System.Drawing.Point(150, 290);
			this.createRoom.Name = "createRoom";
			this.createRoom.Size = new System.Drawing.Size(200, 50);
			this.createRoom.TabIndex = 2;
			this.createRoom.Text = "Create Room";
			this.createRoom.UseVisualStyleBackColor = true;
			this.createRoom.Click += new System.EventHandler(this.createRoom_Click);
			// 
			// status
			// 
			this.status.Location = new System.Drawing.Point(150, 360);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(200, 50);
			this.status.TabIndex = 2;
			this.status.Text = "My Status";
			this.status.UseVisualStyleBackColor = true;
			// 
			// bestScores
			// 
			this.bestScores.Location = new System.Drawing.Point(150, 430);
			this.bestScores.Name = "bestScores";
			this.bestScores.Size = new System.Drawing.Size(200, 50);
			this.bestScores.TabIndex = 2;
			this.bestScores.Text = "Best Scores";
			this.bestScores.UseVisualStyleBackColor = true;
			// 
			// quit
			// 
			this.quit.Location = new System.Drawing.Point(150, 500);
			this.quit.Name = "quit";
			this.quit.Size = new System.Drawing.Size(200, 50);
			this.quit.TabIndex = 2;
			this.quit.Text = "Quit";
			this.quit.UseVisualStyleBackColor = true;
			// 
			// GameScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = this.MaximumSize;
			this.Controls.Add(this.joinRoom);
			this.Controls.Add(this.createRoom);
			this.Controls.Add(this.status);
			this.Controls.Add(this.bestScores);
			this.Controls.Add(this.quit);
			this.Controls.Add(this.Title);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(500, 600);
			this.MinimumSize = new System.Drawing.Size(400, 70);
			this.Name = "GameScreen";
			this.Text = "MagshiTrivia";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.Button joinRoom;
		private System.Windows.Forms.Button createRoom;
		private System.Windows.Forms.Button status;
		private System.Windows.Forms.Button bestScores;
		private System.Windows.Forms.Button quit;

		private System.Net.Sockets.NetworkStream sock;
	}
}


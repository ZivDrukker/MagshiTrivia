namespace client
{
	partial class CreateRoom
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateRoom));
			this.Title = new System.Windows.Forms.Label();
			this.roomName = new System.Windows.Forms.TextBox();
			this.playersNum = new System.Windows.Forms.TextBox();
			this.questionNum = new System.Windows.Forms.TextBox();
			this.timePerQuestion = new System.Windows.Forms.TextBox();
			this.send = new System.Windows.Forms.Button();
			this.back = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.AutoSize = true;
			this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold);
			this.Title.Location = new System.Drawing.Point(50, 25);
			this.Title.Name = "Title";
			this.Title.Size = this.MinimumSize;
			this.Title.TabIndex = 1;
			this.Title.Text = "MagshiTrivia";
			// 
			// send
			// 
			this.send.Location = new System.Drawing.Point(150, 400);
			this.send.Name = "send";
			this.send.Size = new System.Drawing.Size(200, 50);
			this.send.TabIndex = 4;
			this.send.Text = "Send";
			this.send.UseVisualStyleBackColor = true;
			this.send.Click += new System.EventHandler(this.send_Click);
			// 
			// back
			// 
			this.back.Location = new System.Drawing.Point(150, 475);
			this.back.Name = "back";
			this.back.Size = new System.Drawing.Size(200, 50);
			this.back.TabIndex = 4;
			this.back.Text = "Back";
			this.back.UseVisualStyleBackColor = true;
			this.back.Click += new System.EventHandler(this.back_Click);
			// 
			// roomName
			// 
			this.roomName.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.roomName.Location = new System.Drawing.Point(150, 150);
			this.roomName.Name = "roomName";
			this.roomName.Size = new System.Drawing.Size(200, 20);
			this.roomName.TabIndex = 4;
			this.roomName.Text = "Insert room name here";
			this.roomName.Enter += new System.EventHandler(this.roomName_Enter);
			this.roomName.Leave += new System.EventHandler(this.roomName_Leave);
			// 
			// playersNum
			// 
			this.playersNum.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.playersNum.Location = new System.Drawing.Point(150, 200);
			this.playersNum.Name = "playersNum";
			this.playersNum.Size = new System.Drawing.Size(200, 20);
			this.playersNum.TabIndex = 5;
			this.playersNum.Text = "Insert number of players here";
			this.playersNum.Enter += new System.EventHandler(this.playersNum_Enter);
			this.playersNum.Leave += new System.EventHandler(this.playersNum_Leave);
			// 
			// questionNum
			// 
			this.questionNum.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.questionNum.Location = new System.Drawing.Point(150, 250);
			this.questionNum.Name = "questionNum";
			this.questionNum.Size = new System.Drawing.Size(200, 20);
			this.questionNum.TabIndex = 6;
			this.questionNum.Text = "Insert number of questions here";
			this.questionNum.Enter += new System.EventHandler(this.questionNum_Enter);
			this.questionNum.Leave += new System.EventHandler(this.questionNum_Leave);
			// 
			// timePerQuestion
			// 
			this.timePerQuestion.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.timePerQuestion.Location = new System.Drawing.Point(150, 300);
			this.timePerQuestion.Name = "timePerQuestion";
			this.timePerQuestion.Size = new System.Drawing.Size(200, 20);
			this.timePerQuestion.TabIndex = 7;
			this.timePerQuestion.Text = "Insert time per question here";
			this.timePerQuestion.Enter += new System.EventHandler(this.timePerQuestion_Enter);
			this.timePerQuestion.Leave += new System.EventHandler(this.timePerQuestion_Leave);
			// 
			// CreateRoom
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(500, 600);
			this.Controls.Add(this.Title);
			this.Controls.Add(this.send);
			this.Controls.Add(this.back);
			this.Controls.Add(this.roomName);
			this.Controls.Add(this.playersNum);
			this.Controls.Add(this.questionNum);
			this.Controls.Add(this.timePerQuestion);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 70);
			this.Name = "CreateRoom";
			this.Text = "CreateRoom";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.TextBox roomName;
		private System.Windows.Forms.TextBox playersNum;
		private System.Windows.Forms.TextBox questionNum;
		private System.Windows.Forms.TextBox timePerQuestion;
		private System.Windows.Forms.Button send;
		private System.Windows.Forms.Button back;

		private System.Net.Sockets.NetworkStream sock;
	}
}
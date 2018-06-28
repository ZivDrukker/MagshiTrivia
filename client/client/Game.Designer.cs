namespace client
{
	partial class Game
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.Title = new System.Windows.Forms.Label();
            this.answer1 = new System.Windows.Forms.Button();
            this.answer2 = new System.Windows.Forms.Button();
            this.answer3 = new System.Windows.Forms.Button();
            this.answer4 = new System.Windows.Forms.Button();
            this.question = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.questionNum = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.score = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold);
            this.Title.Location = new System.Drawing.Point(200, 15);
            this.Title.Name = "Title";
            this.Title.Size = this.MinimumSize;
            this.Title.TabIndex = 4;
            this.Title.Text = "MagshiTrivia";
            // 
            // answer1
            // 
            this.answer1.Location = new System.Drawing.Point(15, 300);
            this.answer1.Name = "answer1";
            this.answer1.Size = new System.Drawing.Size(375, 75);
            this.answer1.TabIndex = 5;
            this.answer1.Text = "answer1";
            this.answer1.UseVisualStyleBackColor = true;
            this.answer1.Click += new System.EventHandler(this.Button_Click);
            // 
            // answer2
            // 
            this.answer2.Location = new System.Drawing.Point(415, 300);
            this.answer2.Name = "answer2";
            this.answer2.Size = new System.Drawing.Size(375, 75);
            this.answer2.TabIndex = 6;
            this.answer2.Text = "answer2";
            this.answer2.UseVisualStyleBackColor = true;
            this.answer2.Click += new System.EventHandler(this.Button_Click);
            // 
            // answer3
            // 
            this.answer3.Location = new System.Drawing.Point(15, 400);
            this.answer3.Name = "answer3";
            this.answer3.Size = new System.Drawing.Size(375, 75);
            this.answer3.TabIndex = 7;
            this.answer3.Text = "answer3";
            this.answer3.UseVisualStyleBackColor = true;
            this.answer3.Click += new System.EventHandler(this.Button_Click);
            // 
            // answer4
            // 
            this.answer4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.answer4.Location = new System.Drawing.Point(415, 400);
            this.answer4.Name = "answer4";
            this.answer4.Size = new System.Drawing.Size(375, 75);
            this.answer4.TabIndex = 8;
            this.answer4.Text = "answer4";
            this.answer4.UseVisualStyleBackColor = false;
            this.answer4.Click += new System.EventHandler(this.Button_Click);
            // 
            // question
            // 
            this.question.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.question.ForeColor = System.Drawing.Color.Blue;
            this.question.Location = new System.Drawing.Point(100, 125);
            this.question.Name = "question";
            this.question.Size = new System.Drawing.Size(600, 40);
            this.question.TabIndex = 9;
            this.question.Text = "THIS IS A QUESTION EXAMPLE";
            this.question.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // questionNum
            // 
            this.questionNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.questionNum.Location = new System.Drawing.Point(75, 200);
            this.questionNum.Name = "questionNum";
            this.questionNum.Size = new System.Drawing.Size(200, 50);
            this.questionNum.TabIndex = 10;
            this.questionNum.Text = "Question X/X";
            this.questionNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // time
            // 
            this.time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.time.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.time.Location = new System.Drawing.Point(375, 200);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(50, 50);
            this.time.TabIndex = 11;
            this.time.Text = "-1";
            this.time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // score
            // 
            this.score.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.score.Location = new System.Drawing.Point(525, 200);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(200, 50);
            this.score.TabIndex = 12;
            this.score.Text = "Score X/X";
            this.score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.score);
            this.Controls.Add(this.time);
            this.Controls.Add(this.questionNum);
            this.Controls.Add(this.question);
            this.Controls.Add(this.answer4);
            this.Controls.Add(this.answer3);
            this.Controls.Add(this.answer2);
            this.Controls.Add(this.answer1);
            this.Controls.Add(this.Title);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 70);
            this.Name = "Game";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.Button answer4;
		private System.Windows.Forms.Button answer3;
		private System.Windows.Forms.Button answer2;
		private System.Windows.Forms.Button answer1;
		private System.Windows.Forms.Label question;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label score;
		private System.Windows.Forms.Label time;
		private System.Windows.Forms.Label questionNum;
		private System.Net.Sockets.NetworkStream sock;

		private int timeLeft;
		private int qNum;
		private int currQNum;
		private int qTime;
		private int scoreCount;
		private int clicks;
		private string clickedButton;
		private System.Windows.Forms.Button current;
		private System.Collections.Generic.List<string> _reply;
	}
}
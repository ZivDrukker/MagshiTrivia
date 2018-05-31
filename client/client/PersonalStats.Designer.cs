namespace client
{
	partial class PersonalStats
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonalStats));
			this.Title = new System.Windows.Forms.Label();
			this.close = new System.Windows.Forms.Button();
			this.stats = new System.Windows.Forms.Label();
			this.subtitle = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.AutoSize = true;
			this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold);
			this.Title.Location = new System.Drawing.Point(100, 25);
			this.Title.Name = "Title";
			this.Title.Size = this.MinimumSize;
			this.Title.TabIndex = 1;
			this.Title.Text = "MagshiTrivia";
			// 
			// close
			// 
			this.close.Location = new System.Drawing.Point(200, 500);
			this.close.Name = "close";
			this.close.Size = new System.Drawing.Size(200, 50);
			this.close.TabIndex = 3;
			this.close.Text = "Close";
			this.close.UseVisualStyleBackColor = true;
			this.close.Click += new System.EventHandler(this.close_Click);
			// 
			// stats
			// 
			this.stats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
			this.stats.Location = new System.Drawing.Point(100, 175);
			this.stats.Name = "stats";
			this.stats.Size = new System.Drawing.Size(400, 300);
			this.stats.TabIndex = 9;
			this.stats.Text = "WAITING FOR stats!!!\n\nWAITING FOR stats!!!\n\nWAITING FOR stats!!!";
			this.stats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// subtitle
			// 
			this.subtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
			this.subtitle.ForeColor = System.Drawing.Color.Blue;
			this.subtitle.Location = new System.Drawing.Point(150, 150);
			this.subtitle.Name = "subtitle";
			this.subtitle.Size = new System.Drawing.Size(300, 20);
			this.subtitle.TabIndex = 10;
			this.subtitle.Text = "My Performances:";
			this.subtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PersonalStats
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(600, 600);
			this.Controls.Add(this.subtitle);
			this.Controls.Add(this.stats);
			this.Controls.Add(this.close);
			this.Controls.Add(this.Title);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 70);
			this.Name = "PersonalStats";
			this.Text = "PersonalStats";
			this.Load += new System.EventHandler(this.PersonalStats_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.Button close;
		private System.Windows.Forms.Label stats;
		private System.Windows.Forms.Label subtitle;

		private System.Net.Sockets.NetworkStream sock;
	}
}
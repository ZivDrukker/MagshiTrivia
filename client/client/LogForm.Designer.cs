namespace client
{
	partial class LogForm
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
			this.title = new System.Windows.Forms.Label();
			this.log = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// title
			// 
			this.title.AutoSize = true;
			this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold);
			this.title.Location = new System.Drawing.Point(225, 10);
			this.title.Name = "title";
			this.title.Size = new System.Drawing.Size(129, 54);
			this.title.TabIndex = 0;
			this.title.Text = "Logs";
			// 
			// log
			// 
			this.log.AutoSize = true;
			this.log.Location = new System.Drawing.Point(25, 75);
			this.log.Name = "log";
			this.log.Size = new System.Drawing.Size(233, 39);
			this.log.TabIndex = 1;
			this.log.Text = "Waiting for communication... \nAll sent and recived messages will appear here. \nOn" +
    "e pair at a time.";
			// 
			// LogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(550, 250);
			this.Controls.Add(this.log);
			this.Controls.Add(this.title);
			this.Name = "LogForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "LogForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label title;
		private System.Windows.Forms.Label log;
	}
}
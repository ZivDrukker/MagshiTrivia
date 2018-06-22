namespace client
{
	partial class LoginScreen
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginScreen));
			this.Title = new System.Windows.Forms.Label();
			this.signin = new System.Windows.Forms.Button();
			this.signup = new System.Windows.Forms.Button();
			this.username = new System.Windows.Forms.TextBox();
			this.password = new System.Windows.Forms.TextBox();
			this.email = new System.Windows.Forms.TextBox();
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
			// signin
			// 
			this.signin.Location = new System.Drawing.Point(25, 325);
			this.signin.Name = "signin";
			this.signin.Size = new System.Drawing.Size(200, 50);
			this.signin.TabIndex = 4;
			this.signin.Text = "Sign In";
			this.signin.UseVisualStyleBackColor = true;
			this.signin.Click += new System.EventHandler(this.signin_Click);
			// 
			// signup
			// 
			this.signup.Location = new System.Drawing.Point(275, 325);
			this.signup.Name = "signup";
			this.signup.Size = new System.Drawing.Size(200, 50);
			this.signup.TabIndex = 5;
			this.signup.Text = "Sign Up";
			this.signup.UseVisualStyleBackColor = true;
			this.signup.Click += new System.EventHandler(this.signup_Click);
			// 
			// username
			// 
			this.username.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.username.Location = new System.Drawing.Point(150, 150);
			this.username.Name = "username";
			this.username.Size = new System.Drawing.Size(200, 20);
			this.username.TabIndex = 6;
			this.username.Text = "Insert username here";
			this.username.Enter += new System.EventHandler(this.username_Enter);
			this.username.Leave += new System.EventHandler(this.username_Leave);
			// 
			// password
			// 
			this.password.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.password.Location = new System.Drawing.Point(150, 200);
			this.password.Name = "password";
			this.password.Size = new System.Drawing.Size(200, 20);
			this.password.TabIndex = 6;
			this.password.Text = "Insert password here";
			this.password.Enter += new System.EventHandler(this.password_Enter);
			this.password.Leave += new System.EventHandler(this.password_Leave);
			// 
			// email
			// 
			this.email.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.email.Location = new System.Drawing.Point(150, 250);
			this.email.Name = "email";
			this.email.Size = new System.Drawing.Size(200, 20);
			this.email.TabIndex = 7;
			this.email.Text = "Insert email here - FOR SIGN UP ONLY";
			this.email.Enter += new System.EventHandler(this.email_Enter);
			this.email.Leave += new System.EventHandler(this.email_Leave);
			// 
			// LoginScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(500, 400);
			this.Controls.Add(this.email);
			this.Controls.Add(this.username);
			this.Controls.Add(this.password);
			this.Controls.Add(this.signup);
			this.Controls.Add(this.signin);
			this.Controls.Add(this.Title);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 70);
			this.Name = "LoginScreen";
			this.Text = "Login";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.Button signin;
		private System.Windows.Forms.Button signup;
		private System.Windows.Forms.TextBox username;
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.TextBox email;

		public System.Net.Sockets.NetworkStream sock;
	}
}
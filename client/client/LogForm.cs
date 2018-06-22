using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
	public partial class LogForm : Form
	{
		public LogForm()
		{
			InitializeComponent();
		}

		public void SetLog(string msg)
		{
			this.log.Text = msg;
		}

		public string GetLog()
		{
			return this.log.Text;
		}

		public void closeLog()
		{
			this.Hide();
			this.Close();
		}
	}
}

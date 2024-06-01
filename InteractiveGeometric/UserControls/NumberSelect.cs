using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractiveGeometric.UserControls
{
	public partial class NumberSelect : UserControl
	{
		public NumberSelect(string label)
		{
			InitializeComponent();
			labelName.Text = label;
			RefreshValueLabel();
		}

		private void trackBar_ValueChanged(object sender, EventArgs e)
		{
			RefreshValueLabel();
		}
		public void RefreshValueLabel()
		{
			labelValue.Text = trackBar.Value.ToString();
		}
	}
}

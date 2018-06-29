using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vsixprop.UserControls
{
	public partial class MyUserControl : UserControl
	{
		public MyUserControl()
		{
			InitializeComponent();
		}

		internal OptionPageCustom optionsPage;

		public void Initialize()
		{
			textBox2.Text = optionsPage.connectString;
			this.comboBox1.SelectedIndex = optionsPage.DBType;
			checkBox1.Checked = optionsPage.AddSummary;

		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			optionsPage.connectString = textBox2.Text.ToString();
		}

		private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
		{
			optionsPage.DBType = this.comboBox1.SelectedIndex;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			optionsPage.AddSummary = checkBox1.Checked;
		}
	}
}

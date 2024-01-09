using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Application
{
    public partial class DSK : Form
    {
        public DSK()
        {
            InitializeComponent();
        }

        private void DSK_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void Ask(string[] Parts)
        {
            TXT_1.Text = Parts[0];
            TXT_2.Text = Parts[1];
            TXT_3.Text = Parts[2];
            TXT_4.Text = Parts[3];
            TXT_5.Text = Parts[4];
            TXT_6.Text = Parts[5];
            TXT_7.Text = Parts[6];

            ShowDialog();
        }
    }
}

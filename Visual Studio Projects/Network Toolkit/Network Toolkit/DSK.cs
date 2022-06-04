using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network_Toolkit
{
    public partial class DSK : Form
    {
        public DSK(string[] Parts)
        {
        
            InitializeComponent();

            TXT_DSK1.Text = Parts[0];
            TXT_DSK2.Text = Parts[1];
            TXT_DSK3.Text = Parts[2];
            TXT_DSK4.Text = Parts[3];
            TXT_DSK5.Text = Parts[4];
            TXT_DSK6.Text = Parts[5];
            TXT_DSK7.Text = Parts[6];

            TXT_Pin.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWaveJS.NET;

namespace Demo_Application
{
    public partial class NIFWait : Form
    {
        public NIFWait()
        {
            InitializeComponent();
        }

        public void Start(bool Include)
        {
            if (Include)
                LBL_Title.Text = "Place your ZWave device into inclusion mode.";
            else
                LBL_Title.Text = "Place your ZWave device into exclusion mode.";

            this.ShowDialog();
        }

      


        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

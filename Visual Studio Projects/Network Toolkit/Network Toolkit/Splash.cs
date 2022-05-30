using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Network_Toolkit
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Enabled = false;
            LBL_Version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void MainFormTimer_Tick(object sender, EventArgs e)
        {
            this.Hide();
            Main M = new Main();
            M.FormClosed += M_FormClosed;
            M.Show();
            MainFormTimer.Enabled = false;
        }

        private void M_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

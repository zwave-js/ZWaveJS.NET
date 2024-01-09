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
    public partial class InclusionMode : Form
    {
        public ZWaveJS.NET.Enums.InclusionStrategy InclusionStrategy;
        public InclusionMode()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (RD_Default.Checked)
                InclusionStrategy = ZWaveJS.NET.Enums.InclusionStrategy.Default;

            if (RD_Insecure.Checked)
                InclusionStrategy = ZWaveJS.NET.Enums.InclusionStrategy.Insecure;

            if (RD_S0.Checked)
                InclusionStrategy = ZWaveJS.NET.Enums.InclusionStrategy.Security_S0;

            if (RD_S2.Checked)
                InclusionStrategy = ZWaveJS.NET.Enums.InclusionStrategy.Security_S2;
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

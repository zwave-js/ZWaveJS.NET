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
    public partial class GrantClasses : Form
    {
        public ZWaveJS.NET.Enums.SecurityClass[] Granted;
        public GrantClasses()
        {

            InitializeComponent();

            CB_S0.Tag = ZWaveJS.NET.Enums.SecurityClass.S0_Legacy;
            CB_S2_AccessControl.Tag = ZWaveJS.NET.Enums.SecurityClass.S2_AccessControl;
            CB_S2_Auth.Tag = ZWaveJS.NET.Enums.SecurityClass.S2_Authenticated;
            CB_S2_Unauth.Tag = ZWaveJS.NET.Enums.SecurityClass.S2_Unauthenticated;
        }

        public void Grant(ZWaveJS.NET.Enums.SecurityClass[] Requested)
        {
            if (Requested.Contains((ZWaveJS.NET.Enums.SecurityClass)CB_S0.Tag))
            {
                CB_S0.Enabled = true;
                CB_S0.Checked = true;
            }


            if (Requested.Contains((ZWaveJS.NET.Enums.SecurityClass)CB_S2_AccessControl.Tag))
            {
                CB_S2_AccessControl.Enabled = true;
                CB_S2_AccessControl.Checked = true;
            }


            if (Requested.Contains((ZWaveJS.NET.Enums.SecurityClass)CB_S2_Auth.Tag))
            {
                CB_S2_Auth.Enabled = true;
                CB_S2_Auth.Checked = true;
            }


            if (Requested.Contains((ZWaveJS.NET.Enums.SecurityClass)CB_S2_Unauth.Tag))
            {
                CB_S2_Unauth.Enabled = true;
                CB_S2_Unauth.Checked = true;
            }



            this.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<ZWaveJS.NET.Enums.SecurityClass> _Granted = new List<ZWaveJS.NET.Enums.SecurityClass>();

            if (CB_S0.Checked)
                _Granted.Add((ZWaveJS.NET.Enums.SecurityClass)CB_S0.Tag);

            if (CB_S2_AccessControl.Checked)
                _Granted.Add((ZWaveJS.NET.Enums.SecurityClass)CB_S2_AccessControl.Tag);

            if (CB_S2_Auth.Checked)
                _Granted.Add((ZWaveJS.NET.Enums.SecurityClass)CB_S2_Auth.Tag);

            if (CB_S2_Unauth.Checked)
                _Granted.Add((ZWaveJS.NET.Enums.SecurityClass)CB_S2_Unauth.Tag);

            this.Granted = _Granted.ToArray();

            this.Close();


        }
    }
}

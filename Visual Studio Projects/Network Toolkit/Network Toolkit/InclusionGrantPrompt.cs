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

namespace Network_Toolkit
{
    public partial class InclusionGrantPrompt : Form
    {
        public InclusionGrant IG;
        public InclusionGrantPrompt(InclusionGrant IG)
        {
            this.IG = IG;
            InitializeComponent();

            foreach(Enums.SecurityClass SC in IG.securityClasses)
            {
                Control[] CTRLs = this.Controls.Cast<Control>().Where((T) => T.Tag != null && T.Tag.Equals(SC.ToString())).ToArray();

                foreach(Control CTRL in CTRLs)
                {
                    CTRL.Enabled = true;
                    if (CTRL is CheckBox)
                    {
                        ((CheckBox)CTRL).Checked = true;
                    }
                }
               

            }
        }

        private void InclusionGrantPrompt_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Enums.SecurityClass> Classes = new List<Enums.SecurityClass>();

            if (CB_S0.Checked)
                Classes.Add(Enums.SecurityClass.S0_Legacy);

            if (CB_S2A.Checked)
                Classes.Add(Enums.SecurityClass.S2_Authenticated);

            if (CB_S2AC.Checked)
                Classes.Add(Enums.SecurityClass.S2_AccessControl);

            if (CB_S2U.Checked)
                Classes.Add(Enums.SecurityClass.S2_Unauthenticated);


            IG.securityClasses = Classes.ToArray();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

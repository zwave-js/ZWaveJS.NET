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
    public partial class SSList : Form
    {
        Driver _Driver;
        public SSList(Driver Driver)
        {
            InitializeComponent();
            _Driver = Driver;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(LST_SS.SelectedItems.Count > 0)
            {
                _Driver.Controller.UnprovisionSmartStartNode(LST_SS.SelectedItems[0].Text).ContinueWith((R) => {

                    if (R.Result.Success)
                    {
                        this.Invoke((MethodInvoker)delegate () {
                            LST_SS.Items.Remove(LST_SS.SelectedItems[0]);
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate () {
                            MessageBox.Show(R.Result.Message, "Could Not Remove Smart Start Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                
                });
            }
        }

        private void SSList_Load(object sender, EventArgs e)
        {
            _Driver.Controller.GetProvisioningEntries().ContinueWith((R) => {

                if (R.Result.Success)
                {
                    this.Invoke((MethodInvoker)delegate () {
                        SmartStartProvisioningEntry[] Entries = R.Result.ResultPayload as SmartStartProvisioningEntry[];
                        foreach(SmartStartProvisioningEntry SS in Entries)
                        {
                            ListViewItem LVI = new ListViewItem(SS.dsk);
                            LST_SS.Items.Add(LVI);
                        }
                    });

                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show(R.Result.Message, "Could Not Retrieve Smart Start Entries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }

            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

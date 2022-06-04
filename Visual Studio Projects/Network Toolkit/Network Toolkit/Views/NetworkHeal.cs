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

namespace Network_Toolkit.Views
{
    public partial class NetworkHeal : UserControl
    {
        Driver _Driver;
        public NetworkHeal(Driver Driver)
        {
            InitializeComponent();
            _Driver = Driver;

            if (_Driver.Controller.isHealNetworkActive)
            {
                _Driver.Controller.HealNetworkProgress += Controller_HealNetworkProgress;
                _Driver.Controller.HealNetworkDone += Controller_HealNetworkDone;

                LBL_Status.Text = "Heal Status: Running...";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_Driver.Controller.isHealNetworkActive)
            {
                
                _Driver.Controller.BeginHealingNetwork().ContinueWith((R) => {

                    if (!R.Result.Success)
                    {
                        this.Invoke((MethodInvoker)delegate () {
                            MessageBox.Show(R.Result.Message, "Could Not Heal Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                    else
                    {
                        _Driver.Controller.HealNetworkProgress += Controller_HealNetworkProgress;
                        _Driver.Controller.HealNetworkDone += Controller_HealNetworkDone;

                        this.Invoke((MethodInvoker)delegate () {
                            LBL_Status.Text = "Heal Status: Running...";
                        });
                    }

                });
            }
            
          
        }

        private void Controller_HealNetworkDone(Dictionary<string, string> Result)
        {
            _Driver.Controller.HealNetworkProgress -= Controller_HealNetworkProgress;
            _Driver.Controller.HealNetworkDone -= Controller_HealNetworkDone;

            this.Invoke((MethodInvoker)delegate () {

                LBL_Status.Text = "Heal Status: Not Running.";

                LST_Nodes.Items.Clear();

                foreach(string KEY in Result.Keys)
                {
                    ListViewItem LVI = new ListViewItem(KEY);
                    LVI.SubItems.Add(Result[KEY].ToUpper());
                    LST_Nodes.Items.Add(LVI);
                }
            });
        }

        private void Controller_HealNetworkProgress(Dictionary<string, string> Progress)
        {
            this.Invoke((MethodInvoker)delegate () {

                LST_Nodes.Items.Clear();

                foreach (string KEY in Progress.Keys)
                {
                    ListViewItem LVI = new ListViewItem(KEY);
                    LVI.SubItems.Add(Progress[KEY].ToUpper());
                    LST_Nodes.Items.Add(LVI);
                }

            });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _Driver.Controller.StopHealingNetwork().ContinueWith((R) => {

                if (!R.Result.Success)
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show(R.Result.Message, "Could Not Stop Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {
                        LBL_Status.Text = "Heal Status: Not Running.";
                    });

                    _Driver.Controller.HealNetworkProgress -= Controller_HealNetworkProgress;
                    _Driver.Controller.HealNetworkDone -= Controller_HealNetworkDone;
                }
            
            });
        }
    }
}

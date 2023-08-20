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
    public partial class RepairNetwork : Form
    {
        ZWaveJS.NET.Driver _Driver;
        public RepairNetwork()
        {
            InitializeComponent();
        }

        public void Start(ZWaveJS.NET.Driver Driver)
        {
            _Driver = Driver;
            _Driver.Controller.HealNetworkProgress += Controller_HealNetworkProgress;
            _Driver.Controller.HealNetworkDone += Controller_HealNetworkDone;
            _Driver.Controller.BeginHealingNetwork();

            this.ShowDialog();

        }

        private void Controller_HealNetworkDone(ZWaveJS.NET.NetworkHealDoneArgs Args)
        {
           
        }

        private void Controller_HealNetworkProgress(ZWaveJS.NET.NetworkHealProgressArgs Args)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Driver.Controller.StopHealingNetwork().ContinueWith((R) => {

                this.Invoke(new Action(() => {

                    this.Close();

                }));
                
             

            });
            
        }
    }
}

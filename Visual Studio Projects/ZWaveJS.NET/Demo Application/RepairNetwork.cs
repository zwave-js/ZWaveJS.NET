using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
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
            _Driver.Controller.HealNetworkProgress += Controller_RebuildProgress;
            _Driver.Controller.HealNetworkDone += Controller_RebuildDone;
            ZWaveJS.NET.RebuildRoutesOptions Options = new ZWaveJS.NET.RebuildRoutesOptions();
            Options.includeSleeping = true;
            _Driver.Controller.BeginRebuildingRoutes(Options);

            this.ShowDialog();

        }

        private void Controller_RebuildDone(ZWaveJS.NET.RebuildRoutesDoneArgs Args)
        {
            _Driver.Controller.HealNetworkProgress -= Controller_RebuildProgress;
            _Driver.Controller.HealNetworkDone -= Controller_RebuildDone;

            this.Invoke(new Action(() =>
            {
                this.Close();

            }));
        }

        private void Controller_RebuildProgress(ZWaveJS.NET.RebuildRoutesProgressArgs Args)
        {
            int Total = Args.FailedNodes.Length + Args.HealedNodes.Length + Args.SkippedNodes.Length + Args.PendingNodes.Length;
            int Pending = Args.PendingNodes.Length;
            int Done = Total - Pending;

            decimal Progress;
            try
            {
                Progress = (decimal)Done / (decimal)Total * 100;
            }
            catch(Exception Error)
            {
                Progress = 100;
            }
            

            this.Invoke(new Action(() => 
            {
                LBL_Title.Text = string.Format("Reparing Network ({0}%)...", Convert.ToInt32(Progress));
            }));
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Driver.Controller.StopRebuildingRoutes().ContinueWith((R) =>
            {
                _Driver.Controller.HealNetworkProgress -= Controller_RebuildProgress;
                _Driver.Controller.HealNetworkDone -= Controller_RebuildDone;

                this.Invoke(new Action(() =>
                {

                    this.Close();

                }));



            });

        }
    }
}

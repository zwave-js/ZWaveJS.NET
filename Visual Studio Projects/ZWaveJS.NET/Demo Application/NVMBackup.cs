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
    public partial class NVMBackup : Form
    {
        public NVMBackup()
        {
            InitializeComponent();
        }

        public void Start(Driver Driver, string FileName)
        {
            Driver.Controller.BackupNVMRaw(new Controller.BackupNVMProgress(Progress)).ContinueWith((R) =>
            {
                if (R.Result.Success)
                {
                    System.IO.File.WriteAllBytes(FileName, (byte[])R.Result.ResultPayload);
                    this.Invoke(new Action(() =>
                    {
                        this.Close();
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("There was an Error backing up the NVM :\r\n"+ R.Result.Message + "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }));
                }
              

            });
            this.ShowDialog();

        }

        void Progress(int Progress, int Total)
        {
            this.Invoke(new Action(() =>
            {
                decimal P = (decimal)Progress / (decimal)Total * 100;
                PB_Progress.Value = Convert.ToInt32(P);
            }));

        }
    }
}

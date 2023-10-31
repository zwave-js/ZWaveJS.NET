using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWaveJS.NET;

namespace Demo_Application
{
    public partial class NVMRestore : Form
    {
        public NVMRestore()
        {
            InitializeComponent();
        }

        public void Start(Driver Driver, string FileName)
        {
            Driver.Controller.RestoreNVM(System.IO.File.ReadAllBytes(FileName), _Convert, _Restore).ContinueWith((C) => {

                this.Invoke(new Action(() =>
                {
                    Close();
                }));

            });

            this.ShowDialog();
        }

        private void _Convert(int Progress, int Total)
        {
            this.Invoke(new Action(() =>
            {
                decimal P = (decimal)Progress / (decimal)Total * 100;
                PB_Convert.Value = Convert.ToInt32(P);
            }));
        }

        private void _Restore(int Progress, int Total)
        {
            this.Invoke(new Action(() =>
            {
                decimal P = (decimal)Progress / (decimal)Total * 100;
                PB_Restore.Value = Convert.ToInt32(P);
            }));
        }
    }
}

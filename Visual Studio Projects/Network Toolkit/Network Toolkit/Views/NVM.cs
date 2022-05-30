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
using System.IO;


namespace Network_Toolkit.Views
{
    public partial class NVM : UserControl
    {
        Driver _Driver;
        public NVM(Driver Driver)
        {
            _Driver = Driver;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.FileName = "ZWave NVM.bin";
            SFD.Filter = "ZWave NVM|*.bin";

            if (SFD.ShowDialog() == DialogResult.OK)
            {
                _Driver.Controller.BackupNVMRaw(Progress).ContinueWith((R) =>
                {
                    File.WriteAllBytes(SFD.FileName, R.Result);
                    this.Invoke((Action)delegate {

                        MessageBox.Show("NVM backup has completed", "Backup NVM", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    });
                });
            }
        }

        private void Progress(int Read, int Total)
        {
            this.Invoke((Action) delegate {

                PB_Progress.Value = Convert.ToInt32(((decimal)Read / (decimal)Total) * 100);
            
            
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.FileName = "ZWave NVM.bin";
            OFD.Filter = "ZWave NVM|*.bin";

            if (OFD.ShowDialog() == DialogResult.OK)
            {


                byte[] Data = File.ReadAllBytes(OFD.FileName);



                _Driver.Controller.RestoreNVM(Data,_Convert,Write).ContinueWith((R) =>
                {
                   
                    this.Invoke((Action)delegate {

                        MessageBox.Show("NVM restore has completed. Please allow 60 seconds for the Driver to restart.", "Restore NVM", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    });
                });
            }
        }

        private void _Convert(int Read, int Total)
        {
            this.Invoke((Action)delegate {

                PB_Progress.Value = Convert.ToInt32(((decimal)Read / (decimal)Total) * 100);


            });
        }

        private void Write(int Read, int Total)
        {
            this.Invoke((Action)delegate {

                PB_Progress.Value = Convert.ToInt32(((decimal)Read / (decimal)Total) * 100);


            });
        }
    }
}

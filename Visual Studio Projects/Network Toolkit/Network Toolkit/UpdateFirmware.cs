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
    public partial class UpdateFirmware : Form
    {
        ZWaveNode _Node;
        public UpdateFirmware(ZWaveNode Node)
        {
            InitializeComponent();
            _Node = Node;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            _Node.AbortFirmwareUpdate().ContinueWith((R) =>
            {
                if (R.Result.Success)
                {
                    _Node.FirmwareUpdateProgress -= _Node_FirmwareUpdateProgress;
                    _Node.FirmwareUpdateFinished -= _Node_FirmwareUpdateFinished;

                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show("The Firmware update has been aborted.", "Firmware Update Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show(R.Result.Message, "Could Not Abort Firmware Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                  
                }
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            _Node.BeginFirmwareUpdate(TXT_File.Text).ContinueWith((R) => {

                if (R.Result.Success)
                {
                    _Node.FirmwareUpdateProgress += _Node_FirmwareUpdateProgress;
                    _Node.FirmwareUpdateFinished += _Node_FirmwareUpdateFinished;

                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show(R.Result.Message, "Could Not Update Firmware", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
                

            });
            
        }

        private void _Node_FirmwareUpdateFinished(ZWaveNode Node, int Status, int WaitTime)
        {
            _Node.FirmwareUpdateProgress -= _Node_FirmwareUpdateProgress;
            _Node.FirmwareUpdateFinished -= _Node_FirmwareUpdateFinished;

            string Message = "";
            MessageBoxIcon Icon = MessageBoxIcon.Error;

            switch (Status)
            {
                case 253:
                    Message = "The firmware for node "+Node.id+"  has been updated. Activation is pending.";
                    Icon = MessageBoxIcon.Information;
                    break;

                case 254:
                    Message = "The firmware for node "+Node.id+" has been updated.";
                    Icon = MessageBoxIcon.Information;
                    break;

                case 255:
                    Message = "The firmware for node "+Node.id+" has been updated. A restart is required (which may happen automatically)";
                    Icon = MessageBoxIcon.Information;
                    break;

                default:
                    Message = "The firmware for node " + Node.id + " failed to get updated. Error Code: "+Status;
                    Icon = MessageBoxIcon.Error;
                    break;
            }
           

            this.Invoke((MethodInvoker)delegate () {
                MessageBox.Show(Message, "Firmware Updated", MessageBoxButtons.OK, Icon);
            });


        }

        private void _Node_FirmwareUpdateProgress(ZWaveNode Node, int SentFragments, int TotalFragments)
        {
            this.Invoke((MethodInvoker)delegate () {
                decimal P = Math.Round(((decimal)(SentFragments * 100)) / TotalFragments);
                PB_Progress.Value = Convert.ToInt32(P);
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            if(OFD.ShowDialog() == DialogResult.OK)
            {
                TXT_File.Text = OFD.FileName;
            }
        }

        private void UpdateFirmware_Load(object sender, EventArgs e)
        {
            this.FormClosing += UpdateFirmware_FormClosing;
        }

        private void UpdateFirmware_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Node.AbortFirmwareUpdate().ContinueWith((R) =>
            {
                _Node.FirmwareUpdateProgress -= _Node_FirmwareUpdateProgress;
                _Node.FirmwareUpdateFinished -= _Node_FirmwareUpdateFinished;

            });
        }
    }
}

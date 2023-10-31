using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWaveJS.NET;

namespace Demo_Application
{
    public partial class NodeFW : Form
    {
        ZWaveJS.NET.Driver _Driver;
        ZWaveJS.NET.ZWaveNode _Node;

        public NodeFW()
        {
            InitializeComponent();
        }

        public void Start(ZWaveJS.NET.Driver Driver, ZWaveJS.NET.ZWaveNode Node)
        {
            _Driver = Driver;
            _Node = Node;

            LBL_Node.Text = string.Format(LBL_Node.Text, Node.id);
            LBL_Node2.Text = LBL_Node.Text;
            ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _Node.AbortFirmwareUpdate().ContinueWith((C) =>
            {

                if (C.Result.Success)
                {
                    this.Invoke(new Action(() =>
                    {
                        Close();
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show(C.Result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }



            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Choose Firmware";
            OFD.FileName = "*.*";

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                TXT_Filename.Text = OFD.FileName;
                LBL_Status.Text = "Ready!";




            }

        }

        private void _Node_FirmwareUpdateFinished(ZWaveJS.NET.ZWaveNode Node, ZWaveJS.NET.NodeFirmwareUpdateResultArgs Args)
        {
            _Node.FirmwareUpdateProgress -= _Node_FirmwareUpdateProgress;
            _Node.FirmwareUpdateFinished -= _Node_FirmwareUpdateFinished;


        }

        private void _Node_FirmwareUpdateProgress(ZWaveJS.NET.ZWaveNode Node, ZWaveJS.NET.NodeFirmwareUpdateProgressArgs Args)
        {
            this.Invoke(new Action(() =>
            {
                PB_Progress.Value = Convert.ToInt32(Args.progress);
                LBL_Status.Text = string.Format("Flashing...{0}", Args.progress);
            }));
        }

        private void button2_Click(object sender, EventArgs e)
        {


            ZWaveJS.NET.FirmwareUpdate FWU = ZWaveJS.NET.FirmwareUpdate.Create(TXT_Filename.Text, Convert.ToInt32(NUM_Target.Value));

            _Node.FirmwareUpdateProgress += _Node_FirmwareUpdateProgress;
            _Node.FirmwareUpdateFinished += _Node_FirmwareUpdateFinished;
            _Node.UpdateFirmware(new[] { FWU }).ContinueWith((C) =>
            {

                if (C.Result.Success)
                {
                    this.Invoke(new Action(() =>
                    {
                        LBL_Status.Text = "Flashing...";
                    }));

                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show(C.Result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }

            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _Driver.Controller.GetAvailableFirmwareUpdates(_Node.id, false, ZWaveJS.NET.Enums.UsageEnvironment.Non_Commercial).ContinueWith((C) =>
            {

                if (C.Result.Success)
                {
                    ZWaveJS.NET.FirmwareUpdateInfo[] FWRI = (ZWaveJS.NET.FirmwareUpdateInfo[])C.Result.ResultPayload;

                    if (FWRI.Length > 0)
                    {
                        foreach (FirmwareUpdateInfo FW in FWRI)
                        {
                            this.Invoke(new Action(() =>
                            {
                                FirmwareUpdateCard Card = new FirmwareUpdateCard(FW, _Driver, _Node);
                                PAN_Updates.Controls.Add(Card);
                            }));


                        }
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show("No updates were found for your device.", "No Updates Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }));
                    }
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show(C.Result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }

            });
        }
    }
}

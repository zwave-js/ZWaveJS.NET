using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using ZWaveJS.NET;

namespace Network_Toolkit
{
    public partial class SmartStart : Form
    {
        VideoCaptureDevice Camera;
        ZXing.BarcodeReader Reader;
        Driver _Driver;
        List<string> Scanned;

        public SmartStart(Driver Driver)
        {
            InitializeComponent();
            _Driver = Driver;
        }

    
       
        private void SmartStart_Load(object sender, EventArgs e)
        {
            this.FormClosing += SmartStart_FormClosing;
            Reader = new ZXing.BarcodeReader();
            Scanned = new List<string>();

            FilterInfoCollection Cams = new AForge.Video.DirectShow.FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo FI in Cams)
            {
                ComboObject CO = new ComboObject(FI.Name, FI.MonikerString);
                COM_Cams.Items.Add(CO);
            }

          
        }

        private void SmartStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Camera != null)
            {
                Camera.SignalToStop();
                Camera = null;
            }
        }

        private void COM_Cams_SelectedValueChanged(object sender, EventArgs e)
        {
            if(Camera != null)
            {
                Camera.SignalToStop();
                Camera = null;
            }

            Camera = new VideoCaptureDevice(((ComboObject)COM_Cams.SelectedItem).Value.ToString());
            Camera.SetCameraProperty(CameraControlProperty.Focus, 1, CameraControlFlags.Auto);
            Camera.NewFrame += Cam_NewFrame;

            Camera.Start();
        }

       

        private void Cam_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            if(PAN_Image.BackgroundImage != null)
            {
                PAN_Image.BackgroundImage.Dispose();
            }

            PAN_Image.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();

            using(Bitmap IMG = (Bitmap)eventArgs.Frame.Clone())
            {
                
                ZXing.Result  Res = Reader.Decode(IMG);
                if (Res != null && Res.BarcodeFormat == ZXing.BarcodeFormat.QR_CODE && !Scanned.Contains(Res.Text))
                {
                    Camera.SignalToStop();
                    _Driver.Controller.ProvisionSmartStartNode(Res.Text).ContinueWith((R) => {

                        if (R.Result.Success)
                        {
                            Scanned.Add(Res.Text);

                            this.Invoke((MethodInvoker)delegate ()
                            {
                                MessageBox.Show("The ZWave Device has been Successfully Provisioned", "Smart Start", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Camera.Start();
                            });
                        }
                        else
                        {
                            Scanned.Add(Res.Text);

                            this.Invoke((MethodInvoker)delegate ()
                            {
                                MessageBox.Show(R.Result.Message, "Smart Start", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Camera.Start();
                            });
                        }
                    
                    });
                   
                }
               

            }
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

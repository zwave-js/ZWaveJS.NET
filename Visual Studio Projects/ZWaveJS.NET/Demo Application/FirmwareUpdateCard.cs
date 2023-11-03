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
    public partial class FirmwareUpdateCard : UserControl
    {
        ZWaveJS.NET.FirmwareUpdateInfo _Update;
        ZWaveJS.NET.Driver _Driver;
        ZWaveJS.NET.ZWaveNode _Node;

        public FirmwareUpdateCard(ZWaveJS.NET.FirmwareUpdateInfo Update, ZWaveJS.NET.Driver Driver, ZWaveJS.NET.ZWaveNode Node)
        {
            _Update = Update;
            _Driver = Driver;
            _Node = Node;

            InitializeComponent();

            TXT_Changelog.Text = Update.changelog.Replace("\n", Environment.NewLine);
            TXT_Changelog.Text += Environment.NewLine + Environment.NewLine+"Files:"+Environment.NewLine;

            foreach(FirmwareUpdateFileInfo FI in Update.files)
            {
                TXT_Changelog.Text += " - "+FI.url+Environment.NewLine;
            }

            LBL_Files.Text = Update.files.Length.ToString();
            LBL_Version.Text = Update.version;
            LBL_Downgrade.Text = Update.downgrade ? "Downgrade" : "Upgrade";
            if (Update.downgrade)
            {
                LBL_Downgrade.ForeColor = Color.Red;
            }
            else
            {
                LBL_Downgrade.ForeColor = Color.Black;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            _Driver.Controller.FirmwareUpdateOTA(_Node.id, _Update).ContinueWith((C) =>
            {

                if (!C.Result.Success)
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

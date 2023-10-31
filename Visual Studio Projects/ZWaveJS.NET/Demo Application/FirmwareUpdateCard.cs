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


    }
}

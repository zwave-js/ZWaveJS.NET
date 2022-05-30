using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using ZWaveJS.NET;

namespace Network_Toolkit.Views
{
    public partial class Connector : UserControl
    {
        public delegate void StartConnection(string SerialPort, ZWaveOptions Options);
        public event StartConnection StartConnectionEvent;

        public Connector()
        {
            InitializeComponent();
        }

        private void Connector_Load(object sender, EventArgs e)
        {
            COM_SerialPort.Items.Add("Select Port");
            foreach (string Port in SerialPort.GetPortNames())
            {
                COM_SerialPort.Items.Add(Port);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZWaveOptions Options = new ZWaveOptions();
            Options.logConfig.enabled = CB_Logging.Checked;
            Options.logConfig.logToFile = CB_Logging.Checked;

            if (TXT_S0.Text.Length > 0)
                Options.securityKeys.S0_Legacy = TXT_S0.Text;

            if (TXT_S2A.Text.Length > 0)
                Options.securityKeys.S2_Authenticated = TXT_S2A.Text;

            if (TXT_S2AC.Text.Length > 0)
                Options.securityKeys.S2_AccessControl = TXT_S2AC.Text;

            if (TXT_S2U.Text.Length > 0)
                Options.securityKeys.S2_Unauthenticated = TXT_S2U.Text;





            StartConnectionEvent?.Invoke(COM_SerialPort.SelectedItem.ToString(),Options);
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            byte[] Key = new byte[16];
            R.NextBytes(Key);
            TXT_S0.Text = BitConverter.ToString(Key).ToLower().Replace("-", "");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            byte[] Key = new byte[16];
            R.NextBytes(Key);
            TXT_S2AC.Text = BitConverter.ToString(Key).ToLower().Replace("-", "");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            byte[] Key = new byte[16];
            R.NextBytes(Key);
            TXT_S2A.Text = BitConverter.ToString(Key).ToLower().Replace("-", "");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            byte[] Key = new byte[16];
            R.NextBytes(Key);
            TXT_S2U.Text = BitConverter.ToString(Key).ToLower().Replace("-", "");
        }
    }
}

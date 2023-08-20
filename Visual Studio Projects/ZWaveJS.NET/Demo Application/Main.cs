using ZWaveJS.NET;
namespace Demo_Application
{
    public partial class Main : Form
    {
        Driver _Driver;
        public Main()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PB_Connect.Visible = true;
            BTN_Connect.Text = "Connecting...";

            ZWaveOptions Options = new ZWaveOptions();

            Options.enableSoftReset = CB_SoftResetUSB.Checked;
            Options.disableOptimisticValueUpdate = CB_DisableOptimistic.Checked;
            Options.interview.queryAllUserCodes = CB_LockCode.Checked;

            Options.securityKeys = new CFGSecurityKeys();
            Options.securityKeys.S0_Legacy = TXT_KEY_S0.Text.Length > 0 ? TXT_KEY_S0.Text : null;
            Options.securityKeys.S2_AccessControl = TXT_KEY_AC.Text.Length > 0 ? TXT_KEY_AC.Text : null;
            Options.securityKeys.S2_Authenticated = TXT_KEY_Auth.Text.Length > 0 ? TXT_KEY_Auth.Text : null;
            Options.securityKeys.S2_Unauthenticated = TXT_KEY_UAuth.Text.Length > 0 ? TXT_KEY_UAuth.Text : null;

            _Driver = new Driver(COM_SerialPort.SelectedItem.ToString(), Options);
            _Driver.DriverReady += _Driver_DriverReady;
            _Driver.Start();

        }

        private void Controller_NodeAdded(ZWaveNode Node, InclusionResultArgs Args)
        {

            this.Invoke(new Action(() =>
            {
                ListViewItem LVI = new ListViewItem(string.Format("#{0}", Node.id));
                LVI.SubItems.Add(Node.status.ToString());
                LVI.SubItems.Add(Node.deviceConfig?.manufacturer);
                LVI.SubItems.Add(Node.deviceConfig?.label);
                LVI.Tag = Node.id;

                LST_Nodes.Items.Add(LVI);
            }));

            Node.NodeReady += Node_NodeReady;
        }

        private void Node_NodeReady(ZWaveNode Node)
        {
            Node.NodeReady -= Node_NodeReady;

            this.Invoke(new Action(() =>
            {
                ListViewItem RemoveLVI = LST_Nodes.Items.Cast<ListViewItem>().FirstOrDefault((LVI) => LVI.Tag.Equals(Node.id));
                LST_Nodes.Items.Remove(RemoveLVI);

                ListViewItem LVI = new ListViewItem(string.Format("#{0}", Node.id));
                LVI.SubItems.Add(Node.status.ToString());
                LVI.SubItems.Add(Node.deviceConfig?.manufacturer);
                LVI.SubItems.Add(Node.deviceConfig?.label);
                LVI.Tag = Node.id;

                LST_Nodes.Items.Add(LVI);

            }));
        }

        private void Controller_NodeRemoved(ZWaveNode Node, Enums.RemoveNodeReason Reason)
        {
            this.Invoke(new Action(() =>
            {
                ListViewItem LVI = LST_Nodes.Items.Cast<ListViewItem>().FirstOrDefault((LVI) => LVI.Tag.Equals(Node.id));
                LST_Nodes.Items.Remove(LVI);
            }));
        }

        private void _Driver_DriverReady()
        {
            _Driver.Controller.NodeRemoved += Controller_NodeRemoved;
            _Driver.Controller.NodeAdded += Controller_NodeAdded;
            _Driver.Controller.StatisticsUpdated += Controller_StatisticsUpdated;

            this.Invoke(new Action(() =>
            {
                PB_Connect.Visible = false;
                BTN_Connect.Text = "Connected!";
                LBL_Versions.Text = string.Format("Server Version : {0} Driver Version : {1}", _Driver.ZWaveJSServerVersion, _Driver.ZWaveJSDriverVersion);

                ZWaveNode[] Nodes = _Driver.Controller.Nodes.AsArray();
                foreach (ZWaveNode N in Nodes)
                {
                    ListViewItem LVI = new ListViewItem(string.Format("#{0}", N.id));

                    LVI.SubItems.Add(N.status.ToString());
                    LVI.SubItems.Add(N.deviceConfig?.manufacturer);
                    LVI.SubItems.Add(N.deviceConfig?.label);
                    LVI.Tag = N.id;

                    LST_Nodes.Items.Add(LVI);
                }

            }));

        }

        private void Controller_StatisticsUpdated(ControllerStatisticsUpdatedArgs Args)
        {

            string JSON = Newtonsoft.Json.JsonConvert.SerializeObject(Args);
            Dictionary<string, int> Stats = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(JSON);

            this.Invoke(new Action(() =>
            {
                LST_ControllerStats.Items.Clear();

                foreach (string Key in Stats.Keys)
                {
                    ListViewItem LVI = new ListViewItem(Key);
                    LVI.SubItems.Add(Stats[Key].ToString());

                    LST_ControllerStats.Items.Add(LVI);
                }
            }));


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            foreach (string Port in System.IO.Ports.SerialPort.GetPortNames())
            {
                COM_SerialPort.Items.Add(Port);
            }
        }

        private void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            if (_Driver != null)
            {
                _Driver.Destroy();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Would you like to also remove the Smart Start Provisioning entry if one is found?", "Unprovision", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (Result)
            {
                case DialogResult.Yes:
                    _Driver.Controller.BeginExclusion(true).ContinueWith((R) =>
                    {
                        if (R.Result.Success)
                        {
                            this.Invoke(new Action(() =>
                            {

                            }));
                        }

                    });
                    break;

                case DialogResult.No:
                    _Driver.Controller.BeginExclusion(false).ContinueWith((R) =>
                    {
                        if (R.Result.Success)
                        {
                            this.Invoke(new Action(() =>
                            {

                            }));
                        }
                    });
                    break;
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("ZWave.NVM.{0}.bin", DateTime.Now.ToString("dd-MM-yyyy"));
            saveFileDialog.Title = "Choose Destinstion File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                NVMBackup NVMB = new NVMBackup();
                NVMB.Start(_Driver, saveFileDialog.FileName);
            }
        }
    }
}
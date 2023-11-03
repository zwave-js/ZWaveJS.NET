using System.Security.Cryptography;
using ZWaveJS.NET;
namespace Demo_Application
{
    public partial class Main : Form
    {
        Driver _Driver;
        NIFWait _NW;
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
            _Driver.StartUpError += _Driver_StartUpError;
            _Driver.ConnectionLost += _Driver_ConnectionLost;
            _Driver.Start();

        }

        private void _Driver_ConnectionLost(string Message)
        {
            this.Invoke(new Action(() =>
            {
                MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ));
        }

        private void Controller_InclusionStopped()
        {
            this.Invoke(new Action(() =>
            {
                _NW?.Close();
                _NW = null;
            }));

        }

        private void Controller_ExclusionStopped()
        {
            this.Invoke(new Action(() =>
            {
                _NW?.Close();
                _NW = null;
            }));
        }

        private void _Driver_StartUpError(string Message)
        {
            this.Invoke(new Action(() =>
            {
                MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ));
        }

        private void Controller_NodeAdded(ZWaveNode Node, InclusionResultArgs Args)
        {

            this.Invoke(new Action(() =>
            {


                ListViewItem LVI = new ListViewItem(string.Format("#{0}", Node.id));
                LVI.SubItems.Add(Node.interviewStage != "Complete" ? "Interview" : Node.status.ToString());
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

            Node.NodeAsleep += Node_NodeAsleep;
            Node.NodeAwake += Node_NodeAwake;

            this.Invoke(new Action(() =>
            {
                ListViewItem RemoveLVI = LST_Nodes.Items.Cast<ListViewItem>().FirstOrDefault((LVI) => LVI.Tag.Equals(Node.id));
                LST_Nodes.Items.Remove(RemoveLVI);

                ListViewItem LVI = new ListViewItem(string.Format("#{0}", Node.id));
                LVI.SubItems.Add(Node.interviewStage != "Complete" ? "Interview" : Node.status.ToString());
                LVI.SubItems.Add(Node.deviceConfig?.manufacturer);
                LVI.SubItems.Add(Node.deviceConfig?.label);
                LVI.Tag = Node.id;

                LST_Nodes.Items.Add(LVI);

            }));
        }

        private void Node_NodeAwake(ZWaveNode Node)
        {

        }

        private void Node_NodeAsleep(ZWaveNode Node)
        {

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
            _Driver.Controller.ExclusionStopped += Controller_ExclusionStopped;
            _Driver.Controller.InclusionStopped += Controller_InclusionStopped;
            _Driver.Controller.StatisticsUpdated += Controller_StatisticsUpdated;

            this.Invoke(new Action(() =>
            {
                PB_Connect.Visible = false;
                BTN_Connect.Text = "Connected!";
                LBL_Versions.Text = string.Format("Server Version : {0} Driver Version : {1}", _Driver.ZWJSS_ServerVersion, _Driver.ZWJSS_DriverVersion);
                GP_Controller.Enabled = true;
                GP_Network.Enabled = true;
                GP_Nodes.Enabled = true;
                GP_Settings.Enabled = false;


                LST_Nodes.Items.Clear();
                ZWaveNode[] Nodes = _Driver.Controller.Nodes.AsArray();
                foreach (ZWaveNode N in Nodes)
                {
                    ListViewItem LVI = new ListViewItem(string.Format("#{0}", N.id));

                    LVI.SubItems.Add(N.interviewStage != "Complete" ? "Interview" : N.status.ToString());
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

            if (Result == DialogResult.Cancel)
            {
                return;
            }

            ExclusionOptions EO = new ExclusionOptions();
            EO.strategy = (Result == DialogResult.Yes ? Enums.ExclusionStrategy.Unprovision : Enums.ExclusionStrategy.ExcludeOnly);

            _Driver.Controller.BeginExclusion(EO).ContinueWith((R) =>
            {
                if (R.Result.Success)
                {
                    this.Invoke(new Action(() =>
                    {
                        _NW = new NIFWait();
                        _NW.Start(false);

                        if (_NW?.DialogResult == DialogResult.Cancel)
                        {
                            _Driver.Controller.StopExclusion();
                        }

                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show(R.Result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }

            });





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

        private void button6_Click(object sender, EventArgs e)
        {
            InclusionMode IM = new InclusionMode();
            IM.ShowDialog();

            if (IM.DialogResult == DialogResult.OK)
            {
                InclusionOptions IO = new InclusionOptions();
                IO.strategy = IM.InclusionStrategy;

                IO.userCallbacks = new InclusionUserCallbacks();

                IO.userCallbacks.validateDSKAndEnterPIN = new ValidateDSKAndEnterPIN(ValidateDSK);
                IO.userCallbacks.grantSecurityClasses = new GrantSecurityClasses(Grant);
                IO.userCallbacks.abort = new Abort(Abort);

                _Driver.Controller.BeginInclusion(IO).ContinueWith((R) =>
                {

                    if (R.Result.Success)
                    {
                        this.Invoke(new Action(() =>
                        {
                            _NW = new NIFWait();
                            _NW.Start(true);

                            if (_NW?.DialogResult == DialogResult.Cancel)
                            {
                                _Driver.Controller.StopInclusion();
                            }
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show(R.Result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }));
                    }

                });
            }
        }

        private void Abort()
        {

        }

        private string ValidateDSK(string PartialDSK)
        {
            string[] Parts = PartialDSK.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

            ManualResetEvent MRE = new ManualResetEvent(false);

            string _DSK = string.Empty;

            this.Invoke(new Action(() =>
            {
                DSK DSK = new DSK();
                DSK.Ask(Parts);



                if (DSK.DialogResult == DialogResult.OK)
                {
                    _DSK = DSK.TXT_DSK.Text;
                    MRE.Set();
                }
            }));



            MRE.WaitOne();
            return _DSK;

        }

        private InclusionGrant Grant(InclusionGrant Requested)
        {
            ManualResetEvent MRE = new ManualResetEvent(false);

            InclusionGrant IG = new InclusionGrant();
            IG.clientSideAuth = Requested.clientSideAuth;

            this.Invoke(new Action(() =>
            {
                GrantClasses GC = new GrantClasses();
                GC.Grant(Requested.securityClasses);
                IG.securityClasses = GC.Granted;

                MRE.Set();
            }));


            MRE.WaitOne();
            return IG;


        }

        private void button13_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            byte[] Bytes = new byte[16];
            R.NextBytes(Bytes);

            TXT_KEY_S0.Text = BitConverter.ToString(Bytes).Replace("-", "").ToLower();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            byte[] Bytes = new byte[16];
            R.NextBytes(Bytes);

            TXT_KEY_AC.Text = BitConverter.ToString(Bytes).Replace("-", "").ToLower();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            byte[] Bytes = new byte[16];
            R.NextBytes(Bytes);

            TXT_KEY_Auth.Text = BitConverter.ToString(Bytes).Replace("-", "").ToLower();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            byte[] Bytes = new byte[16];
            R.NextBytes(Bytes);

            TXT_KEY_UAuth.Text = BitConverter.ToString(Bytes).Replace("-", "").ToLower();
        }

        private void GP_Network_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (LST_Nodes.SelectedItems.Count > 0)
            {
                DeviceEvents DE = new DeviceEvents();
                DE.Start(_Driver.Controller.Nodes.Get((int)LST_Nodes.SelectedItems[0].Tag));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to repair the network? This could take some time, especially if you have battery operated devices", "Are You Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                RepairNetwork RN = new RepairNetwork();
                RN.Start(_Driver);

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to reset your Controller? All data and settings will be lost, and will result in an empty Network.", "Are You Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                _Driver.HardReset().ContinueWith((C) =>
                {

                    if (C.Result.Success)
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show("Controller Reset Succeeded", "Controller Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show($"Controller Reset Failed : {C.Result.Message}", "Controller Reset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }));
                    }

                });

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LST_Nodes.SelectedItems.Count > 0)
            {
                Values V = new Values();
                V.Show(_Driver.Controller.Nodes.Get((int)LST_Nodes.SelectedItems[0].Tag));

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (LST_Nodes.SelectedItems.Count > 0)
            {
                Associations V = new Associations();
                V.Show(_Driver, _Driver.Controller.Nodes.Get((int)LST_Nodes.SelectedItems[0].Tag));

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "*.bin";
            openFileDialog.Title = "Choose Backup";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                NVMRestore NVMR = new NVMRestore();
                NVMR.Start(_Driver, openFileDialog.FileName);


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LST_Nodes.SelectedItems.Count > 0)
            {
                NodeFW FW = new NodeFW();
                FW.Start(_Driver, _Driver.Controller.Nodes.Get((int)LST_Nodes.SelectedItems[0].Tag));



            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (LST_Nodes.SelectedItems.Count > 0)
            {
                ZWaveJS.NET.ZWaveNode Node = _Driver.Controller.Nodes.Get((int)LST_Nodes.SelectedItems[0].Tag);

                Node.Ping().ContinueWith((C) =>
                {

                    if (C.Result.Success)
                    {
                        bool Responded = (bool)C.Result.ResultPayload;


                        if (Responded)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show("The Node responded to a ping", "Ping Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }));
                        }
                        else
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show("The Node did not respond to a ping", "Ping Unsucceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }));
                        }
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show(C.Result.Message, "Ping Command Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }));
                    }


                });



            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (LST_Nodes.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("This will check if the Node has Failed, and if, so will be removed. Are you sure?","Are You Sure",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    _Driver.Controller.RemoveFailedNode((int)LST_Nodes.SelectedItems[0].Tag).ContinueWith((C) => {

                        if (!C.Result.Success)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show(C.Result.Message, "Could Not Remove Node", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }));
                        }
                    
                    });
                }
            }
        }
    }
}
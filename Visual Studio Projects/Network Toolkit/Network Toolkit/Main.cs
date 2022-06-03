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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        Driver _Driver;

        private void Main_Load(object sender, EventArgs e)
        {
            this.FormClosing += Main_FormClosing;

            Views.Connector Connector = new Views.Connector();
            Connector.StartConnectionEvent += Connector_StartConnectionEvent;
            Connector.StartConnectionWSEvent += Connector_StartConnectionWSEvent;
            Connector.Parent = PAN_ViewContainer;

            PAN_ViewContainer.Controls.Add(Connector);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(_Driver != null)
            {
                _Driver.Destroy();
            }
        }

        private void Connector_StartConnectionWSEvent(string WS, int Schema)
        {
            LBL_Status.Text = string.Format("Connecting...");

            _Driver = new Driver(new Uri(WS), Schema);
            _Driver.DriverReady += _Driver_DriverReady;
            _Driver.StartupErrorEvent += _Driver_StartupErrorEvent;
            _Driver.Start();
        }

        private void Connector_StartConnectionEvent(string SerialPort, ZWaveJS.NET.ZWaveOptions Options)
        {
            LBL_Status.Text = string.Format("Fetching PSI...");
            Helpers.DownloadPSI().ContinueWith((R) => {

                this.Invoke((MethodInvoker)delegate () {

                    LBL_Status.Text = string.Format("Connecting...");

                    _Driver = new Driver(SerialPort, Options);
                    _Driver.DriverReady += _Driver_DriverReady;
                    _Driver.StartupErrorEvent += _Driver_StartupErrorEvent;
                    _Driver.Start();

                });
            
            });
            
        }

        private void _Driver_StartupErrorEvent(string Message)
        {
            MessageBox.Show(Message, "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowDefault()
        {
            Views.Default Default = new Views.Default();
            Default.Parent = PAN_ViewContainer;

            PAN_ViewContainer.Controls.Clear();
            PAN_ViewContainer.Controls.Add(Default);

            LBL_Status.Text = string.Format("Connected. Server Version: {0}, ZWave JS Driver Version: {1}", _Driver.ZWaveJSServerVersion, _Driver.ZWaveJSDriverVersion);

        }

        private void _Driver_DriverReady()
        {
            this.Invoke((MethodInvoker)delegate() {
                ShowDefault();
            });
            
            _Driver.Controller.NodeAdded += Controller_NodeAdded;
            _Driver.Controller.NodeRemoved += Controller_NodeRemoved;

            foreach (ZWaveNode ZWN in _Driver.Controller.Nodes.AsArray())
            {
                if (!ZWN.isControllerNode)
                {
                    this.Invoke((MethodInvoker)delegate () {
                        CustomControls.Node N = new CustomControls.Node(ZWN);
                        N.NodeSelectedEvent += N_NodeSelectedEvent;
                        N.Parent = PAN_Nodes;
                        PAN_Nodes.Controls.Add(N);
                    });
                   
                }
            }
        }

        private void N_NodeSelectedEvent(ZWaveNode ZwaveNode)
        {
            Views.NodeDetails ND = new Views.NodeDetails(ZwaveNode,_Driver);
            ND.Parent = PAN_ViewContainer;

            ND.StartReplaceEvent += ND_StartReplaceEvent;
         

            PAN_ViewContainer.Controls.Clear();
            PAN_ViewContainer.Controls.Add(ND);
        }

        private void ND_StartReplaceEvent(int NodeID)
        {
            Views.IncludeOptions IO = new Views.IncludeOptions(NodeID);
            IO.StartInclusionReplaceEvent += Include_StartInclusionReplaceEvent;
            IO.Parent = PAN_ViewContainer;


            PAN_ViewContainer.Controls.Clear();
            PAN_ViewContainer.Controls.Add(IO);
        }

        private void Controller_NodeRemoved(ZWaveNode Node)
        {
            this.Invoke((MethodInvoker)delegate () {
                ShowDefault();
            });

            this.Invoke((MethodInvoker)delegate () {
                CustomControls.Node N = PAN_Nodes.Controls.OfType<CustomControls.Node>().FirstOrDefault((_N) => _N.ZwaveNode.id.Equals(Node.id));
                PAN_Nodes.Controls.Remove(N);
            });


           


        }

        private void Controller_NodeAdded(ZWaveNode Node, InclusionResult Result)
        {
            this.Invoke((MethodInvoker)delegate () {
                ShowDefault();
            });


            this.Invoke((MethodInvoker)delegate () {
                CustomControls.Node N = new CustomControls.Node(Node);
                N.NodeSelectedEvent += N_NodeSelectedEvent;
                N.Parent = PAN_Nodes;
                PAN_Nodes.Controls.Add(N);
            });

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Views.NVM NVM = new Views.NVM(_Driver);
            NVM.Parent = PAN_ViewContainer;

            PAN_ViewContainer.Controls.Clear();
            PAN_ViewContainer.Controls.Add(NVM);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Views.IncludeOptions Include = new Views.IncludeOptions();
            Include.StartInclusionExclusionEvent += Include_StartInclusionEvent;
            Include.Parent = PAN_ViewContainer;

            PAN_ViewContainer.Controls.Clear();
            PAN_ViewContainer.Controls.Add(Include);
        }

        private void Include_StartInclusionReplaceEvent(InclusionOptions Options, int NodeID)
        {
            _Driver.Controller.ReplaceFailedNode(NodeID, Options).ContinueWith((R) =>
            {
                if (R.Result.Success)
                {

                    this.Invoke((MethodInvoker)delegate () {
                        Views.NIFWait NIF = new Views.NIFWait();
                        NIF.Parent = PAN_ViewContainer;

                        PAN_ViewContainer.Controls.Clear();
                        PAN_ViewContainer.Controls.Add(NIF);
                    });


                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show(R.Result.Message, "Failed To Start Inclsuion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });

                }


            });
        }

        private void Include_StartInclusionEvent(object OBJ)
        {

            if(OBJ is bool)
            {
                _Driver.Controller.BeginExclusion((bool)OBJ).ContinueWith((R) =>
                {
                    if (R.Result.Success)
                    {
                        this.Invoke((MethodInvoker)delegate () {
                            Views.NIFWait NIF = new Views.NIFWait();
                            NIF.Parent = PAN_ViewContainer;

                            PAN_ViewContainer.Controls.Clear();
                            PAN_ViewContainer.Controls.Add(NIF);
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate () {
                            MessageBox.Show(R.Result.Message, "Failed To Start Exclusion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });

                    }


                });
            }
            else
            {
                InclusionOptions IO = (InclusionOptions)OBJ;
                switch (IO.strategy)
                {
                    case Enums.InclusionStrategy.Default:
                    case Enums.InclusionStrategy.Security_S2:
                        IO.userCallbacks = new InclusionUserCallbacks();
                        IO.userCallbacks.grantSecurityClasses = HandleIG;
                        IO.userCallbacks.validateDSKAndEnterPIN = HandleDSK;
                        IO.userCallbacks.abort = HandleAbort;
                        break;
                }


                _Driver.Controller.BeginInclusion(IO).ContinueWith((R) =>
                {
                    if (R.Result.Success)
                    {

                        this.Invoke((MethodInvoker)delegate () {
                            Views.NIFWait NIF = new Views.NIFWait();
                            NIF.Parent = PAN_ViewContainer;

                            PAN_ViewContainer.Controls.Clear();
                            PAN_ViewContainer.Controls.Add(NIF);
                        });
                       

                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate () {
                            MessageBox.Show(R.Result.Message, "Failed To Start Inclsuion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });

                    }


                });
            }
        }

        private void HandleAbort()
        {

        }

        private InclusionGrant HandleIG(InclusionGrant IG)
        {
            this.Invoke((MethodInvoker) delegate() {
                Views.S2 S2 = new Views.S2();
                S2.Parent = PAN_ViewContainer;

                PAN_ViewContainer.Controls.Clear();
                PAN_ViewContainer.Controls.Add(S2);
            });
            

            InclusionGrant _IG = (InclusionGrant)this.Invoke((Func<InclusionGrant>)delegate {
                InclusionGrantPrompt IGP = new InclusionGrantPrompt(IG);
                IGP.ShowDialog();

                return IGP.IG;
            });

            return _IG;

           
           
           
        }

        private string HandleDSK(string Partial)
        {
            this.Invoke((MethodInvoker)delegate () {
                Views.S2 S2 = new Views.S2();
                S2.Parent = PAN_ViewContainer;

                PAN_ViewContainer.Controls.Clear();
                PAN_ViewContainer.Controls.Add(S2);
            });

            string[] Parts = Partial.Split(new string[] {"-"},StringSplitOptions.RemoveEmptyEntries);

            string _DSK = (string)this.Invoke((Func<string>)delegate () {
                DSK D = new DSK(Parts);
                D.ShowDialog();
                return D.TXT_Pin.Text;
            });

            return _DSK;
        }


    }
}

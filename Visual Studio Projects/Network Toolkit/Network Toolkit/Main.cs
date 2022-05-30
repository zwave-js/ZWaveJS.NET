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
            Views.Connector Connector = new Views.Connector();
            Connector.StartConnectionEvent += Connector_StartConnectionEvent;
            Connector.Parent = PAN_ViewContainer;

            PAN_ViewContainer.Controls.Add(Connector);
        }

        private void Connector_StartConnectionEvent(string SerialPort, ZWaveJS.NET.ZWaveOptions Options)
        {
            LBL_Status.Text = string.Format("Connecting...");

            _Driver = new Driver(SerialPort, Options);
            _Driver.DriverReady += _Driver_DriverReady;
            _Driver.StartupErrorEvent += _Driver_StartupErrorEvent;
            _Driver.Start();
        }

        private void _Driver_StartupErrorEvent(string Message)
        {
            MessageBox.Show(Message, "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _Driver_DriverReady()
        {
            LBL_Status.Text = string.Format("Connected. Server Version: {0}, ZWave JS Driver Version: {1}", _Driver.ZWaveJSServerVersion, _Driver.ZWaveJSDriverVersion);
            
            _Driver.Controller.NodeAdded += Controller_NodeAdded;
            _Driver.Controller.NodeRemoved += Controller_NodeRemoved;

            foreach (ZWaveNode ZWN in _Driver.Controller.Nodes.AsArray())
            {
                if(!ZWN.isControllerNode)
                this.Invoke((Action)delegate {

                    CustomControls.Node N = new CustomControls.Node(ZWN);
                    N.NodeSelectedEvent += N_NodeSelectedEvent;
                    N.Parent = PAN_Nodes;
                    PAN_Nodes.Controls.Add(N);
                });
            }

            

        }

        private void N_NodeSelectedEvent(ZWaveNode ZwaveNode)
        {
            Views.NodeDetails ND = new Views.NodeDetails(ZwaveNode);
            ND.Parent = PAN_ViewContainer;

            PAN_ViewContainer.Controls.Clear();
            PAN_ViewContainer.Controls.Add(ND);
        }

        private void Controller_NodeRemoved(ZWaveNode Node)
        {
            this.Invoke((Action)delegate {
                CustomControls.Node N =  PAN_Nodes.Controls.OfType<CustomControls.Node>().FirstOrDefault((_N) => _N.ZwaveNode.id.Equals(Node.id));
                PAN_Nodes.Controls.Remove(N);

            });
        }

        private void Controller_NodeAdded(ZWaveNode Node, InclusionResult Result)
        {
            this.Invoke((Action)delegate {

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

       

        private void Include_StartInclusionEvent(object OBJ)
        {

            if(OBJ is bool)
            {
                _Driver.Controller.BeginExclusion((bool)OBJ).ContinueWith((R) =>
                {
                    if (R.Result)
                    {
                        this.Invoke((Action)delegate {

                            Views.NIFWait NIF = new Views.NIFWait();
                            NIF.Parent = PAN_ViewContainer;

                            PAN_ViewContainer.Controls.Clear();
                            PAN_ViewContainer.Controls.Add(NIF);
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
                    if (R.Result)
                    {
                        this.Invoke((Action)delegate {

                            Views.NIFWait NIF = new Views.NIFWait();
                            NIF.Parent = PAN_ViewContainer;

                            PAN_ViewContainer.Controls.Clear();
                            PAN_ViewContainer.Controls.Add(NIF);
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
            InclusionGrantPrompt IGP = new InclusionGrantPrompt(IG);
            IGP.ShowDialog();

            return IG;
           
           
        }

        private string HandleDSK(string Partial)
        {
            return "e442";
        }


    }
}

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
using Newtonsoft.Json.Linq;

namespace Network_Toolkit.Views
{
    public partial class NodeDetails : UserControl
    {

        public delegate void StartReplace(int NodeID);
        public event StartReplace StartReplaceEvent;

        public ZWaveNode ZwaveNode;
        private Driver Driver;
        public NodeDetails(ZWaveNode Node, Driver Driver)
        {
            InitializeComponent();
            this.ZwaveNode = Node;
            this.Driver = Driver;

            LBL_NodeID.Text = "#"+Node.id.ToString();
            LBL_Label.Text = Node.deviceConfig?.label ?? "Unknown";
            LBL_Description.Text = Node.deviceConfig?.description;

            new Task(async () => {

                ValueID[] VIDs = null;
                CMDResult Res = await Node.GetDefinedValueIDs();

                if (Res.Success)
                {
                    VIDs = Res.ResultPayload as ValueID[];
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show(Res.Message, "Failed To Obtain Value IDs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });

                    return;
                }
                

                var CCGroups = VIDs.GroupBy((VID) => VID.commandClassName).OrderBy((G) => G.Key);

                foreach (var Group in CCGroups)
                {
                    string GroupName = Group.Key;
                    ListViewGroup LVG = new ListViewGroup(GroupName);

                    this.Invoke((MethodInvoker)delegate () {
                        LST_Values.Groups.Add(LVG);
                    });
                    
                    foreach (ValueID VID in Group)
                    {
                        CMDResult VMDCMD = await Node.GetValueMetadata(VID);
                        ValueMetadata VMD = null;
                        if (VMDCMD.Success)
                        {
                            VMD = VMDCMD.ResultPayload as ValueMetadata;
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                MessageBox.Show(Res.Message, "Failed To Obtain Value Meatadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            });

                            return;
                        }


                        CMDResult VCMD = await Node.GetValue(VID);
                        JObject V = null;
                        if (VCMD.Success)
                        {
                            V = VCMD.ResultPayload as JObject;
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                MessageBox.Show(Res.Message, "Failed To Obtain Value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            });

                            return;
                        }

                        ListViewItem LVI = new ListViewItem(VMD.label);
                        

                        LVI.Group = LVG;

                        LVI.SubItems.Add(VID.endpoint.ToString());
                        if (V.ContainsKey("value"))
                        {
                            LVI.SubItems.Add(V.SelectToken("value").ToString());
                        }
                        else
                        {
                            LVI.SubItems.Add("");

                        }
                        
                        if (VMD.type == "number" && VMD.writeable)
                        {
                            LVI.Tag = VID;
                        }


                        this.Invoke((MethodInvoker)delegate ()
                        {
                            LST_Values.Items.Add(LVI);
                        });

                    }
                }

            }).Start();

        }

        // Heal
        private void button8_Click(object sender, EventArgs e)
        {
            Driver.Controller.HealNode(ZwaveNode.id).ContinueWith((R) =>
            {
                if (R.Result.Success)
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show("The Node has been healed!", "Node Healed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show(R.Result.Message ?? "", "Failed To Heal Node", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
              
            });
        }


        // Interview
        private void button6_Click(object sender, EventArgs e)
        {
            ZwaveNode.RefreshInfo().ContinueWith((R) =>
            {
                if (!R.Result.Success)
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show(R.Result.Message, "Failed To Interview Node", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
            });
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Driver.Controller.RemoveFailedNode(ZwaveNode.id).ContinueWith((R) => {
                if(R.Result.Success)
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show("The node has been removed!", "Node Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show(R.Result.Message, "Failed To Remove Node", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }


            });
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Event E = new Event(ZwaveNode);
            E.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateFirmware UF = new UpdateFirmware(ZwaveNode);
            UF.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartReplaceEvent?.Invoke(ZwaveNode.id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HealthCheck HC = new HealthCheck(ZwaveNode);
            HC.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}

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

                ValueID[] VIDs = await Node.GetDefinedValueIDs();
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
                        ValueMetadata VMD = await Node.GetValueMetadata(VID);
                        JObject V = await Node.GetValue(VID);

                        ListViewItem LVI = new ListViewItem(VMD.label);
                        LVI.Group = LVG;
                       
                            if (V.ContainsKey("value"))
                            {
                                LVI.SubItems.Add(V.SelectToken("value").ToString());
                            }
                            else
                            {
                                LVI.SubItems.Add("");

                            }


                        this.Invoke((MethodInvoker)delegate () {
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
                if (R.Result)
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show("The node has been healed!", "Heal Node", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show("The node failed to heal!", "Heal Node", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
              
            });
        }


        // Interview
        private void button6_Click(object sender, EventArgs e)
        {
            ZwaveNode.RefreshInfo().ContinueWith((R) =>
            {
                if (R.Result)
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show("The node has been interviewed", "Interview Node", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show("The node failed to get interviewed!", "Interview Node", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
            });
           
        }
    }
}

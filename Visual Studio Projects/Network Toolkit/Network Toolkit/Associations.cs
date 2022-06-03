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
    public partial class Associations : Form
    {
        ZWaveNode _Node;
        Driver _Driver;
        public Associations(ZWaveNode Node, Driver Driver)
        {
            InitializeComponent();
            _Node = Node;
            _Driver = Driver;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Associations_Load(object sender, EventArgs e)
        {
            ListGroups();
        }

        private void ListGroups()
        {
            LST_Associations.Items.Clear();

            COM_Group.Items.Clear();
            COM_Group.Items.Add(new ComboObject("Select Group", null));
            COM_Group.Text = "Select Group";

            _Driver.Controller.GetAssociationGroups(_Node.id, Convert.ToInt32(NUM_EP.Value)).ContinueWith((R) =>
            {

                if (R.Result.Success)
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {


                        Dictionary<int, AssociationGroup> Groups = R.Result.ResultPayload as Dictionary<int, AssociationGroup>;
                        foreach (int AGID in Groups.Keys)
                        {

                            ComboObject CO = new ComboObject(Groups[AGID].label, AGID);
                            COM_Group.Items.Add(CO);
                        }
                    });

                }
                else
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        MessageBox.Show(R.Result.Message, "Failed To Get Association Groups", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }

            });

        }

        private void NUM_EP_ValueChanged(object sender, EventArgs e)
        {
            ListGroups();
        }

        private void COM_Group_SelectedValueChanged(object sender, EventArgs e)
        {
            LST_Associations.Items.Clear();
            ComboObject CO = COM_Group.SelectedItem as ComboObject;

            if (CO.Value == null)
            {
                return;

            }


            _Driver.Controller.GetAssociations(_Node.id, Convert.ToInt32(NUM_EP.Value)).ContinueWith((R) =>
            {

                if (R.Result.Success)
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        Dictionary<int, AssociationAddress[]> Associations = R.Result.ResultPayload as Dictionary<int, AssociationAddress[]>;

                        foreach (AssociationAddress ASS in Associations[(int)CO.Value])
                        {
                            ListViewItem LVI = new ListViewItem(ASS.nodeId.ToString());
                            LVI.Tag = ASS;
                            if (ASS.endpoint == null)
                            {
                                LVI.SubItems.Add("0 (root)");
                            }
                            else
                            {
                                LVI.SubItems.Add(ASS.endpoint.ToString());
                            }
                            LST_Associations.Items.Add(LVI);

                        }
                    });

                }
                else
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        MessageBox.Show(R.Result.Message, "Failed To Get Associations", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }

            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LST_Associations.SelectedItems.Count > 0)
            {
                AssociationAddress Target = LST_Associations.SelectedItems[0].Tag as AssociationAddress;

                AssociationAddress Source = new AssociationAddress();
                Source.nodeId = _Node.id;
                Source.endpoint = Convert.ToInt32(NUM_EP.Value);

                ComboObject CO = COM_Group.SelectedItem as ComboObject;

                _Driver.Controller.RemoveAssociations(Source, (int)CO.Value, new AssociationAddress[] { Target }).ContinueWith((R) =>
                {

                    if (R.Result.Success)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            LST_Associations.Items.Remove(LST_Associations.SelectedItems[0]);
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            MessageBox.Show(R.Result.Message, "Failed To Remove Association", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }

                });
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewAssociation NA = new NewAssociation();
            if (NA.ShowDialog() == DialogResult.OK)
            {
                AssociationAddress AA = new AssociationAddress();
                AA.nodeId = Convert.ToInt32(NA.NUM_Node.Value);
                if (NA.NUM_EP.Value > 0)
                {
                    AA.endpoint = Convert.ToInt32(NA.NUM_EP.Value);
                }

                AssociationAddress Source = new AssociationAddress();
                Source.nodeId = _Node.id;
                Source.endpoint = Convert.ToInt32(NUM_EP.Value);

                ComboObject CO = COM_Group.SelectedItem as ComboObject;

                List<AssociationAddress> Targets = new List<AssociationAddress>();
                Targets.Add(AA);
                
                _Driver.Controller.AddAssociations(Source, (int)CO.Value, Targets.ToArray()).ContinueWith((R) =>
                {

                    if (R.Result.Success)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            COM_Group_SelectedValueChanged(this, null);

                        });
                    }
                    else
                    {
                        MessageBox.Show(R.Result.Message, "Failed To Add Association", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                });
            }


        }
    }
}

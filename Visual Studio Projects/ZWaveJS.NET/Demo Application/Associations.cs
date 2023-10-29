using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Application
{
    public partial class Associations : Form
    {
        public Associations()
        {
            InitializeComponent();
        }

        ZWaveJS.NET.ZWaveNode _Node;
        ZWaveJS.NET.Driver _Driver;
        public void Show(ZWaveJS.NET.Driver Driver, ZWaveJS.NET.ZWaveNode Node)
        {
            _Node = Node;
            _Driver = Driver;

            foreach (ZWaveJS.NET.Endpoint EP in Node.endpoints)
            {
                string Label = string.IsNullOrEmpty(EP.endpointLabel) ? $"# {EP.index}" : $"# {EP.index} ({EP.endpointLabel})";
                CBoxItem C = new CBoxItem(EP.endpointLabel, EP.index);
                C.Text = Label;
                COM_Endpoints.Items.Add(C);
            }

            COM_Endpoints.SelectedIndex = 0;
            COM_Endpoints.SelectedIndexChanged += COM_Endpoints_SelectedIndexChanged;
            COM_Endpoints_SelectedIndexChanged(COM_Endpoints, null);

            ShowDialog();
        }

        private void COM_Endpoints_SelectedIndexChanged(object? sender, EventArgs e)
        {

            COM_AssociationGroup.Items.Clear();
            COM_AssociationGroup.Text = "";
            LST_Associations.Items.Clear();


            _Driver.Controller.GetAssociationGroups(_Node.id, (int)((CBoxItem)COM_Endpoints.SelectedItem).Value).ContinueWith((C) =>
            {
                if (C.Result.Success)
                {
                    Dictionary<int, ZWaveJS.NET.AssociationGroup> AGs = (Dictionary<int, ZWaveJS.NET.AssociationGroup>)C.Result.ResultPayload;

                    foreach (int GID in AGs.Keys)
                    {
                        this.Invoke(new Action(() =>
                        {
                            CBoxItem C = new CBoxItem(AGs[GID].label, GID);
                            COM_AssociationGroup.Items.Add(C);
                        }));
                    }
                }


            });
        }

        private void COM_AssociationGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LST_Associations.Items.Clear();

            _Driver.Controller.GetAssociations(_Node.id, (int)((CBoxItem)COM_Endpoints.SelectedItem).Value).ContinueWith((C) =>
            {

                if (C.Result.Success)
                {
                    Dictionary<int, ZWaveJS.NET.AssociationAddress[]> ASSs = (Dictionary<int, ZWaveJS.NET.AssociationAddress[]>)C.Result.ResultPayload;

                    this.Invoke(new Action(() =>
                    {
                        foreach (ZWaveJS.NET.AssociationAddress Address in ASSs[(int)((CBoxItem)COM_AssociationGroup.SelectedItem).Value])
                        {
                            ListViewItem LVI = new ListViewItem(Address.nodeId.ToString());
                            LVI.Tag = Address;
                            LVI.SubItems.Add(Address.endpoint.ToString());
                            LST_Associations.Items.Add(LVI);
                        }

                    }));
                }

            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LST_Associations.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you wish to remove this association?", "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ZWaveJS.NET.AssociationAddress Source = new ZWaveJS.NET.AssociationAddress();
                    Source.nodeId = _Node.id;
                    Source.endpoint = (int)((CBoxItem)COM_Endpoints.SelectedItem).Value;

                    ZWaveJS.NET.AssociationAddress Target = (ZWaveJS.NET.AssociationAddress)LST_Associations.SelectedItems[0].Tag;


                    _Driver.Controller.RemoveAssociations(Source, (int)((CBoxItem)COM_AssociationGroup.SelectedItem).Value, new[] { Target }).ContinueWith((C) =>
                    {

                        if (C.Result.Success)
                        {
                            this.Invoke(new Action(() =>
                            {
                                LST_Associations.Items.Remove(LST_Associations.SelectedItems[0]);
                            }));
                        }

                    });

                }
            }
        }
    }
}

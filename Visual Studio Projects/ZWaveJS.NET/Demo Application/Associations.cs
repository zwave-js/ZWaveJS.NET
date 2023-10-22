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
    public partial class Associations : Form
    {
        public Associations()
        {
            InitializeComponent();
        }

        public void Show(ZWaveJS.NET.Driver Driver, ZWaveJS.NET.ZWaveNode Node)
        {

            foreach(ZWaveJS.NET.Endpoint EP in Node.endpoints)
            {
                string Label = string.IsNullOrEmpty(EP.endpointLabel) ? $"# {EP.index}" : $"# {EP.index} ({EP.endpointLabel})";
                CBoxItem C = new CBoxItem(EP.endpointLabel, EP.index);
                C.Text = Label;
                COM_Endpoints.Items.Add(C);
            }

            COM_Endpoints.SelectedIndex = 0;

            Driver.Controller.GetAssociationGroups(Node.id, Node.endpoints.First().index).ContinueWith((C) => {


                if (C.Result.Success)
                {
                    Dictionary<int, ZWaveJS.NET.AssociationGroup> AGs = (Dictionary<int, ZWaveJS.NET.AssociationGroup>)C.Result.ResultPayload;

                    foreach(int GID in AGs .Keys)
                    {
                        this.Invoke(new Action(() =>
                        {
                            CBoxItem C = new CBoxItem(AGs[GID].label,GID);
                            COM_AssociationGroup.Items.Add(C);
                        }));
                    }
                }
            
            
            });

            ShowDialog();
        }
    }
}

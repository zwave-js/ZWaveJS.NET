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
            COM_Group.Items.Add(new ComboObject("Select Group", "Select Group"));
            COM_Group.Text = "Select Group";

            _Driver.Controller.GetAssociationGroups(_Node.id, Convert.ToInt32(NUM_EP.Value)).ContinueWith((R) => {

                if (R.Result.Success)
                {
                    this.Invoke((MethodInvoker)delegate () {


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
                    this.Invoke((MethodInvoker)delegate () {
                        MessageBox.Show(R.Result.Message, "Failed To Get Association Groups", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }

            });

        }

        private void NUM_EP_ValueChanged(object sender, EventArgs e)
        {
            ListGroups();
        }
    }
}

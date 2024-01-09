using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Application
{
    public partial class Values : Form
    {
        public Values()
        {
            InitializeComponent();
        }

        public void Show(ZWaveJS.NET.ZWaveNode Node)
        {
            Node.GetDefinedValueIDs().ContinueWith(async (C) =>
            {
                if (C.Result.Success)
                {
                    ZWaveJS.NET.ValueID[] ValueIDs = (ZWaveJS.NET.ValueID[])C.Result.ResultPayload;
                    var CCs = ValueIDs.GroupBy((C) => C.commandClassName);

                    foreach (var CC in CCs)
                    {

                        ListViewGroup Group = new ListViewGroup(CC.Key);

                        this.Invoke(new Action(() =>
                        {
                            LST_Values.Groups.Add(Group);
                        }));

                        foreach (ZWaveJS.NET.ValueID VID in CC)
                        {
                            ZWaveJS.NET.CMDResult Value = await Node.GetValue(VID);

                            ListViewItem LVI = new ListViewItem(VID.property.ToString());

                            if(VID.propertyKey != null)
                            {
                                LVI.Text += " / " + VID.propertyKey.ToString();
                            }

                            LVI.Tag = VID;
                            LVI.SubItems.Add(((JObject)Value.ResultPayload)["value"]?.ToString());
                            LVI.SubItems.Add(VID.endpoint.ToString());

                            LVI.Group = Group;

                            this.Invoke(new Action(() =>
                            {
                                LST_Values.Items.Add(LVI);
                            }));

                        }




                    }
                }
            });

            ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

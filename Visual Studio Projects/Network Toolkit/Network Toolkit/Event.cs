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
    public partial class Event : Form
    {
        ZWaveNode _Node;
        bool _Shown = false;
        public Event(ZWaveNode Node)
        {

            InitializeComponent();

            _Node = Node;

            Node.ValueUpdated += Node_ValueUpdated;
            Node.ValueNotification += Node_ValueNotification;
            Node.Notification += Node_Notification;
            Node.StatisticsUpdated += Node_StatisticsUpdated;
            Node.NodeAwake += Node_NodeAwake;
            Node.NodeAsleep += Node_NodeAsleep;
        }

        private void Node_NodeAsleep(ZWaveNode Node)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                ListViewItem LVI = new ListViewItem(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                LVI.SubItems.Add("SLEEP");
                LVI.SubItems.Add("{}");
                LST_Events.Items.Add(LVI);

                LST_Events.Items[LST_Events.Items.Count - 1].EnsureVisible();
            });
        }

        private void Node_NodeAwake(ZWaveNode Node)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                ListViewItem LVI = new ListViewItem(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                LVI.SubItems.Add("AWAKE");
                LVI.SubItems.Add("{}");
                LST_Events.Items.Add(LVI);

                LST_Events.Items[LST_Events.Items.Count - 1].EnsureVisible();
            });
        }

        private void Node_StatisticsUpdated(ZWaveNode Node, NodeStatistics Statistics)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                LST_Stats.Items.Cast<ListViewItem>().FirstOrDefault((LVI) => LVI.Tag.Equals("CTX")).SubItems[1].Text = Statistics.commandsTX.ToString();
                LST_Stats.Items.Cast<ListViewItem>().FirstOrDefault((LVI) => LVI.Tag.Equals("CTXD")).SubItems[1].Text = Statistics.commandsDroppedTX.ToString();
                LST_Stats.Items.Cast<ListViewItem>().FirstOrDefault((LVI) => LVI.Tag.Equals("CRX")).SubItems[1].Text = Statistics.commandsRX.ToString();
                LST_Stats.Items.Cast<ListViewItem>().FirstOrDefault((LVI) => LVI.Tag.Equals("CRXD")).SubItems[1].Text = Statistics.commandsDroppedRX.ToString();
                LST_Stats.Items.Cast<ListViewItem>().FirstOrDefault((LVI) => LVI.Tag.Equals("TO")).SubItems[1].Text = Statistics.timeoutResponse.ToString();
                LST_Stats.Items.Cast<ListViewItem>().FirstOrDefault((LVI) => LVI.Tag.Equals("RTT")).SubItems[1].Text = Statistics.rtt.ToString();
            });
        }

        private void Node_Notification(ZWaveNode Node, int ccId, Newtonsoft.Json.Linq.JObject Args)
        {
           
            this.Invoke((MethodInvoker)delegate ()
            {
                ListViewItem LVI = new ListViewItem(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                LVI.SubItems.Add("NOTIFICATION");
                LVI.SubItems.Add(Args.ToString());
                LST_Events.Items.Add(LVI);

                LST_Events.Items[LST_Events.Items.Count - 1].EnsureVisible();
            });

        }

        private void Node_ValueNotification(ZWaveNode Node, Newtonsoft.Json.Linq.JObject Args)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                ListViewItem LVI = new ListViewItem(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                LVI.SubItems.Add("VALUE NOTIFICATION");
                LVI.SubItems.Add(Args.ToString());
                LST_Events.Items.Add(LVI);

                LST_Events.Items[LST_Events.Items.Count - 1].EnsureVisible();
            });
        }

        private void Node_ValueUpdated(ZWaveNode Node, Newtonsoft.Json.Linq.JObject Args)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                ListViewItem LVI = new ListViewItem(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                LVI.SubItems.Add("VALUE UPDATED");
                LVI.SubItems.Add(Args.ToString());
                LST_Events.Items.Add(LVI);

                LST_Events.Items[LST_Events.Items.Count - 1].EnsureVisible();
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Event_Load(object sender, EventArgs e)
        {
            this.FormClosing += Event_FormClosing;
        }

        private void Event_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Node.ValueUpdated -= Node_ValueUpdated;
            _Node.ValueNotification -= Node_ValueNotification;
            _Node.Notification -= Node_Notification;
            _Node.StatisticsUpdated -= Node_StatisticsUpdated;
            _Node.NodeAwake -= Node_NodeAwake;
            _Node.NodeAsleep -= Node_NodeAsleep;
        }

        private void Event_Shown(object sender, EventArgs e)
        {
            if (!_Shown)
            {
                foreach (ListViewItem LVI in LST_Stats.Items)
                {
                    LVI.SubItems.Add("");
                }

                Node_StatisticsUpdated(_Node, _Node.statistics);

                _Shown = true;
            }
            
        }
    }
}

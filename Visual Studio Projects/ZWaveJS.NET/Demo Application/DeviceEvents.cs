using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ZWaveJS.NET;

namespace Demo_Application
{
    public partial class DeviceEvents : Form
    {
        ZWaveNode _Node;
        public DeviceEvents()
        {
            InitializeComponent();
        }

        private string Convert(object Message)
        {
            string JSON = Newtonsoft.Json.JsonConvert.SerializeObject(Message, Newtonsoft.Json.Formatting.Indented);
            return JSON;

        }

        public void Start(ZWaveNode Node)
        {
            _Node = Node;
            Node.NodeAsleep += Node_NodeAsleep;
            Node.NodeAwake += Node_NodeAwake;
            Node.NodeDead += Node_NodeDead;
            Node.Notification += Node_Notification;
            Node.ValueAdded += Node_ValueAdded;
            Node.ValueNotification += Node_ValueNotification;
            Node.ValueRemoved += Node_ValueRemoved;
            Node.ValueUpdated += Node_ValueUpdated;

            this.FormClosing += DeviceEvents_FormClosing;

            this.ShowDialog();

        }

        private void DeviceEvents_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _Node.NodeAsleep -= Node_NodeAsleep;
            _Node.NodeAwake -= Node_NodeAwake;
            _Node.NodeDead -= Node_NodeDead;
            _Node.Notification -= Node_Notification;
            _Node.ValueAdded -= Node_ValueAdded;
            _Node.ValueNotification -= Node_ValueNotification;
            _Node.ValueRemoved -= Node_ValueRemoved;
            _Node.ValueUpdated -= Node_ValueUpdated;
        }

        private void Node_ValueUpdated(ZWaveNode Node, ValueUpdatedArgs Args)
        {
            this.Invoke((Action)(() =>
            {
                TXT_Log.AppendText(string.Format("{0} : {1}{2}", DateTime.Now.ToString(), "VALUE UPDATED", Environment.NewLine));
                TXT_Log.AppendText(string.Format("{0}{1}", Convert(Args), Environment.NewLine));
                TXT_Log.AppendText(string.Format("------------------------------------------------------------------------------------------{0}", Environment.NewLine));
            }));
        }

        private void Node_ValueRemoved(ZWaveNode Node, ValueRemovedArgs Args)
        {
            this.Invoke((Action)(() =>
            {
                TXT_Log.AppendText(string.Format("{0} : {1}{2}", DateTime.Now.ToString(), "VALUE REMOVED", Environment.NewLine));
                TXT_Log.AppendText(string.Format("{0}{1}", Convert(Args), Environment.NewLine));
                TXT_Log.AppendText(string.Format("------------------------------------------------------------------------------------------{0}", Environment.NewLine));
            }));
        }

        private void Node_ValueNotification(ZWaveNode Node, ValueNotificationArgs Args)
        {
            this.Invoke((Action)(() =>
            {
                TXT_Log.AppendText(string.Format("{0} : {1}{2}", DateTime.Now.ToString(), "VALUE NOTIFICATION", Environment.NewLine));
                TXT_Log.AppendText(string.Format("{0}{1}", Convert(Args), Environment.NewLine));
                TXT_Log.AppendText(string.Format("------------------------------------------------------------------------------------------{0}", Environment.NewLine));
            }));
        }

        private void Node_ValueAdded(ZWaveNode Node, ValueAddedArgs Args)
        {
            this.Invoke((Action)(() =>
            {
                TXT_Log.AppendText(string.Format("{0} : {1}{2}", DateTime.Now.ToString(), "VALUE ADDED", Environment.NewLine));
                TXT_Log.AppendText(string.Format("{0}{1}", Convert(Args), Environment.NewLine));
                TXT_Log.AppendText(string.Format("------------------------------------------------------------------------------------------{0}", Environment.NewLine));
            }));
        }



        private void Node_Notification(ZWaveNode Node, int ccId, Newtonsoft.Json.Linq.JObject Args)
        {
            this.Invoke((Action)(() =>
            {
                TXT_Log.AppendText(string.Format("{0} : {1} CCID : {2}{3}", DateTime.Now.ToString(), "NOTIFICATION", ccId, Environment.NewLine));
                TXT_Log.AppendText(string.Format("{0}{1}", Convert(Args), Environment.NewLine));
                TXT_Log.AppendText(string.Format("------------------------------------------------------------------------------------------{0}", Environment.NewLine));
            }));
        }

        private void Node_NodeDead(ZWaveNode Node)
        {
            this.Invoke((Action)(() =>
            {
                TXT_Log.AppendText(string.Format("{0} : {1}{2}", DateTime.Now.ToString(), "DEAD", Environment.NewLine));
                TXT_Log.AppendText(string.Format("------------------------------------------------------------------------------------------{0}", Environment.NewLine));
            }));
        }

        private void Node_NodeAwake(ZWaveNode Node)
        {
            this.Invoke((Action)(() =>
            {
                TXT_Log.AppendText(string.Format("{0} : {1}{2}", DateTime.Now.ToString(), "AWAKE", Environment.NewLine));
                TXT_Log.AppendText(string.Format("------------------------------------------------------------------------------------------{0}", Environment.NewLine));
            }));
        }

        private void Node_NodeAsleep(ZWaveNode Node)
        {
            this.Invoke((Action)(() =>
            {
                TXT_Log.AppendText(string.Format("{0} : {1}{2}", DateTime.Now.ToString(), "SLEEP", Environment.NewLine));
                TXT_Log.AppendText(string.Format("------------------------------------------------------------------------------------------{0}", Environment.NewLine));
            }));
        }
    }
}

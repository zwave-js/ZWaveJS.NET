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
        public Event(ZWaveNode Node)
        {

            InitializeComponent();

            _Node = Node;

            Node.ValueUpdated += Node_ValueUpdated;
            Node.ValueNotification += Node_ValueNotification;
            Node.Notification += Node_Notification;
        }

        private void Node_Notification(ZWaveNode Node, int ccId, Newtonsoft.Json.Linq.JObject Args)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                TXT_Log.Text += DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " - " + "NOTIFICATION" + Environment.NewLine;
                TXT_Log.Text += "----------------------------------------------------------------" + Environment.NewLine + Environment.NewLine;
                TXT_Log.Text += Args.ToString(Newtonsoft.Json.Formatting.Indented) + Environment.NewLine + Environment.NewLine;
            });

        }

        private void Node_ValueNotification(ZWaveNode Node, Newtonsoft.Json.Linq.JObject Args)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                TXT_Log.Text += DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " - " + "VALUE NOTIIFCATION" + Environment.NewLine;
                TXT_Log.Text += "----------------------------------------------------------------" + Environment.NewLine + Environment.NewLine;
                TXT_Log.Text += Args.ToString(Newtonsoft.Json.Formatting.Indented) + Environment.NewLine + Environment.NewLine;
            });
        }

        private void Node_ValueUpdated(ZWaveNode Node, Newtonsoft.Json.Linq.JObject Args)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                TXT_Log.Text += DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " - " + "VALUE UPDATED" + Environment.NewLine;
                TXT_Log.Text += "----------------------------------------------------------------" + Environment.NewLine + Environment.NewLine;
                TXT_Log.Text += Args.ToString(Newtonsoft.Json.Formatting.Indented) + Environment.NewLine + Environment.NewLine;
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
        }
    }
}

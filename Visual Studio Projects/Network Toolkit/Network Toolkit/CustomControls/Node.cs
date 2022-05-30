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

namespace Network_Toolkit.CustomControls
{
    public partial class Node : UserControl
    {
        public ZWaveNode ZwaveNode;
        public delegate void NodeSelected(ZWaveNode ZwaveNode);
        public event NodeSelected NodeSelectedEvent;
        public Node(ZWaveNode Node)
        {
            InitializeComponent();

            this.ZwaveNode = Node;

            LBL_NodeID.Text = Node.id.ToString();
            LBL_Label.Text = Node.deviceConfig?.label ?? "Unknown";
            LBL_Description.Text = Node.deviceConfig?.description;
            SetStatus(Node.status);

            if (Node.ready)
            {
                MarkReady();
            }

            Node.NodeInterviewCompleted += Node_NodeInterviewCompleted;
            Node.NodeReady += Node_NodeReady;
            Node.NodeAwake += Node_NodeAwake;
            Node.NodeAsleep += Node_NodeAsleep;
            Node.NodeDead += Node_NodeDead;

          
        }

        private void Node_NodeDead(ZWaveNode Node)
        {
            this.Invoke((Action)delegate {
                SetStatus(Node.status);
            });
        }

        private void Node_NodeAsleep(ZWaveNode Node)
        {
            this.Invoke((Action)delegate {
                SetStatus(Node.status);
            });
        }

        private void Node_NodeAwake(ZWaveNode Node)
        {
            this.Invoke((Action)delegate {
                SetStatus(Node.status);
            });
        }

        private void Node_NodeReady(ZWaveNode Node)
        {
            this.Invoke((Action)delegate {
                MarkReady();
            });
                
        }

        private void Node_NodeInterviewCompleted(ZWaveNode Node)
        {
            this.Invoke((Action)delegate {
               LBL_Label.Text = Node.deviceConfig?.label;
            LBL_Description.Text = Node.deviceConfig?.description;
            });

            
        }

        public void MarkReady()
        {
            LBL_Ready.ForeColor = Color.White;
        }

        public void SetStatus(Enums.NodeStatus Status)
        {
            switch (Status)
            {
                // Alive
                case Enums.NodeStatus.Alive:
                case Enums.NodeStatus.Awake:
                    LBL_Status.Text = "R";
                    break;

                // A sleep
                case  Enums.NodeStatus.Asleep:
                    LBL_Status.Text = "«";
                    break;

                // daed
                case Enums.NodeStatus.Dead:
                    LBL_Status.Text = "N";
                    break;
            }
        }

        private void Node_Click(object sender, EventArgs e)
        {
            NodeSelectedEvent?.Invoke(this.ZwaveNode);
        }
    }
}

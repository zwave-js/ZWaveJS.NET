﻿using System;
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

            Node.NodeInterviewCompleted += Node_NodeInterviewCompleted;
            Node.NodeInterviewStarted += Node_NodeInterviewStarted;
            Node.NodeInterviewFailed += Node_NodeInterviewFailed;
            Node.NodeReady += Node_NodeReady;
            Node.NodeAwake += Node_NodeAwake;
            Node.NodeAsleep += Node_NodeAsleep;
            Node.NodeDead += Node_NodeDead;
            
            LBL_NodeID.Text = Node.id.ToString();
            LBL_Label.Text = Node.deviceConfig?.label ?? "Unknown";
            LBL_Description.Text = Node.deviceConfig?.description;
            SetStatus(Node.status);

            if (Node.ready)
            {
                MarkReady();
            }

            if(Node.interviewStage == "Complete")
            {
                PAN_Interviewed.BackColor = Color.White;
            }
        
        }

      

        private void Node_NodeDead(ZWaveNode Node)
        {
            this.Invoke((MethodInvoker)delegate() {
                SetStatus(Node.status);
            });
        }

        private void Node_NodeAsleep(ZWaveNode Node)
        {
            this.Invoke((MethodInvoker)delegate () {
                SetStatus(Node.status);
            });
        }

        private void Node_NodeAwake(ZWaveNode Node)
        {
            this.Invoke((MethodInvoker)delegate () {
                SetStatus(Node.status);
            });
        }

        private void Node_NodeReady(ZWaveNode Node)
        {
            this.Invoke((MethodInvoker)delegate () {
                MarkReady();
            });
                
        }

        private void Node_NodeInterviewFailed(ZWaveNode Node, NodeInterviewFailedEventArgs Args)
        {
            if (Args.isFinal)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    PAN_Ready.BackColor = Color.Black;
                    PAN_Interviewed.BackColor = Color.Red;

                });
            }
           
        }

        private void Node_NodeInterviewStarted(ZWaveNode Node)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                PAN_Ready.BackColor = Color.Black;
                PAN_Interviewed.BackColor = Color.Black;
 
            });
        }

        private void Node_NodeInterviewCompleted(ZWaveNode Node)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                PAN_Interviewed.BackColor = Color.White;
                PAN_Wake.BackColor = Color.White;
                PAN_Ready.BackColor = Color.White;

                LBL_Label.Text = Node.deviceConfig?.label;
                LBL_Description.Text = Node.deviceConfig?.description;
            });


        }

        public void MarkReady()
        {
            PAN_Ready.BackColor = Color.White;
        }

        public void SetStatus(Enums.NodeStatus Status)
        {
            switch (Status)
            {
                // Alive
                case Enums.NodeStatus.Alive:
                case Enums.NodeStatus.Awake:
                    PAN_Wake.BackColor = Color.White;
                    PAN_Dead.BackColor = Color.Black;
                    break;

                // A sleep
                case  Enums.NodeStatus.Asleep:
                    PAN_Wake.BackColor = Color.Black;
                    PAN_Dead.BackColor = Color.Black;
                    break;

                // daed
                case Enums.NodeStatus.Dead:
                    PAN_Wake.BackColor = Color.Black;
                    PAN_Dead.BackColor = Color.Red;
                    break;
            }
        }

        private void Node_Click(object sender, EventArgs e)
        {
            NodeSelectedEvent?.Invoke(this.ZwaveNode);
        }
    }
}

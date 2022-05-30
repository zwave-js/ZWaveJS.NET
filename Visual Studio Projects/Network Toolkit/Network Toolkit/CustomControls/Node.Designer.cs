
namespace Network_Toolkit.CustomControls
{
    partial class Node
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LBL_NodeID = new System.Windows.Forms.Label();
            this.LBL_Label = new System.Windows.Forms.Label();
            this.LBL_Description = new System.Windows.Forms.Label();
            this.PAN_DEAD = new System.Windows.Forms.Panel();
            this.LBL_Status = new System.Windows.Forms.Label();
            this.LBL_Ready = new System.Windows.Forms.Label();
            this.PAN_DEAD.SuspendLayout();
            this.SuspendLayout();
            // 
            // LBL_NodeID
            // 
            this.LBL_NodeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_NodeID.ForeColor = System.Drawing.Color.White;
            this.LBL_NodeID.Location = new System.Drawing.Point(13, 52);
            this.LBL_NodeID.Name = "LBL_NodeID";
            this.LBL_NodeID.Size = new System.Drawing.Size(39, 23);
            this.LBL_NodeID.TabIndex = 0;
            this.LBL_NodeID.Text = "888";
            this.LBL_NodeID.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LBL_Label
            // 
            this.LBL_Label.AutoSize = true;
            this.LBL_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Label.ForeColor = System.Drawing.Color.White;
            this.LBL_Label.Location = new System.Drawing.Point(53, 7);
            this.LBL_Label.Name = "LBL_Label";
            this.LBL_Label.Size = new System.Drawing.Size(55, 20);
            this.LBL_Label.TabIndex = 1;
            this.LBL_Label.Text = "Label";
            // 
            // LBL_Description
            // 
            this.LBL_Description.AutoSize = true;
            this.LBL_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.LBL_Description.ForeColor = System.Drawing.Color.White;
            this.LBL_Description.Location = new System.Drawing.Point(54, 27);
            this.LBL_Description.Name = "LBL_Description";
            this.LBL_Description.Size = new System.Drawing.Size(69, 15);
            this.LBL_Description.TabIndex = 2;
            this.LBL_Description.Text = "Description";
            // 
            // PAN_DEAD
            // 
            this.PAN_DEAD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(125)))), ((int)(((byte)(195)))));
            this.PAN_DEAD.Controls.Add(this.LBL_NodeID);
            this.PAN_DEAD.Location = new System.Drawing.Point(-9, -5);
            this.PAN_DEAD.Name = "PAN_DEAD";
            this.PAN_DEAD.Size = new System.Drawing.Size(56, 135);
            this.PAN_DEAD.TabIndex = 3;
            // 
            // LBL_Status
            // 
            this.LBL_Status.AutoSize = true;
            this.LBL_Status.Font = new System.Drawing.Font("Wingdings", 16.75F);
            this.LBL_Status.ForeColor = System.Drawing.Color.White;
            this.LBL_Status.Location = new System.Drawing.Point(78, 77);
            this.LBL_Status.Name = "LBL_Status";
            this.LBL_Status.Size = new System.Drawing.Size(32, 26);
            this.LBL_Status.TabIndex = 2;
            this.LBL_Status.Text = "R";
            this.LBL_Status.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LBL_Ready
            // 
            this.LBL_Ready.AutoSize = true;
            this.LBL_Ready.Font = new System.Drawing.Font("Wingdings", 14.75F);
            this.LBL_Ready.ForeColor = System.Drawing.Color.Black;
            this.LBL_Ready.Location = new System.Drawing.Point(53, 80);
            this.LBL_Ready.Name = "LBL_Ready";
            this.LBL_Ready.Size = new System.Drawing.Size(28, 22);
            this.LBL_Ready.TabIndex = 1;
            this.LBL_Ready.Text = "þ";
            this.LBL_Ready.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Node
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.Controls.Add(this.LBL_Ready);
            this.Controls.Add(this.LBL_Status);
            this.Controls.Add(this.PAN_DEAD);
            this.Controls.Add(this.LBL_Description);
            this.Controls.Add(this.LBL_Label);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Node";
            this.Size = new System.Drawing.Size(242, 115);
            this.Click += new System.EventHandler(this.Node_Click);
            this.PAN_DEAD.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBL_NodeID;
        private System.Windows.Forms.Label LBL_Label;
        private System.Windows.Forms.Label LBL_Description;
        private System.Windows.Forms.Panel PAN_DEAD;
        private System.Windows.Forms.Label LBL_Ready;
        private System.Windows.Forms.Label LBL_Status;
    }
}

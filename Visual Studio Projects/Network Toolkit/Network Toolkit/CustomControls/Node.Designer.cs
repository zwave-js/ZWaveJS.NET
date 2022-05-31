
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
            this.pan1 = new System.Windows.Forms.Panel();
            this.PAN_Ready = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PAN_Interviewed = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.PAN_Wake = new System.Windows.Forms.Panel();
            this.pan1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LBL_NodeID
            // 
            this.LBL_NodeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_NodeID.ForeColor = System.Drawing.Color.White;
            this.LBL_NodeID.Location = new System.Drawing.Point(13, 68);
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
            // pan1
            // 
            this.pan1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(125)))), ((int)(((byte)(195)))));
            this.pan1.Controls.Add(this.LBL_NodeID);
            this.pan1.Location = new System.Drawing.Point(-9, -5);
            this.pan1.Name = "pan1";
            this.pan1.Size = new System.Drawing.Size(56, 165);
            this.pan1.TabIndex = 3;
            // 
            // PAN_Ready
            // 
            this.PAN_Ready.BackColor = System.Drawing.Color.Black;
            this.PAN_Ready.Location = new System.Drawing.Point(57, 54);
            this.PAN_Ready.Name = "PAN_Ready";
            this.PAN_Ready.Size = new System.Drawing.Size(16, 16);
            this.PAN_Ready.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(81, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ready";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(81, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Interviewed";
            // 
            // PAN_Interviewed
            // 
            this.PAN_Interviewed.BackColor = System.Drawing.Color.Black;
            this.PAN_Interviewed.Location = new System.Drawing.Point(57, 72);
            this.PAN_Interviewed.Name = "PAN_Interviewed";
            this.PAN_Interviewed.Size = new System.Drawing.Size(16, 16);
            this.PAN_Interviewed.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(81, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Alive / Wake OR Dead";
            // 
            // PAN_Wake
            // 
            this.PAN_Wake.BackColor = System.Drawing.Color.Black;
            this.PAN_Wake.Location = new System.Drawing.Point(57, 90);
            this.PAN_Wake.Name = "PAN_Wake";
            this.PAN_Wake.Size = new System.Drawing.Size(16, 16);
            this.PAN_Wake.TabIndex = 8;
            // 
            // Node
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PAN_Wake);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PAN_Interviewed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PAN_Ready);
            this.Controls.Add(this.pan1);
            this.Controls.Add(this.LBL_Description);
            this.Controls.Add(this.LBL_Label);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Node";
            this.Size = new System.Drawing.Size(242, 148);
            this.Click += new System.EventHandler(this.Node_Click);
            this.pan1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBL_NodeID;
        private System.Windows.Forms.Label LBL_Label;
        private System.Windows.Forms.Label LBL_Description;
        private System.Windows.Forms.Panel pan1;
        private System.Windows.Forms.Panel PAN_Ready;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PAN_Interviewed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel PAN_Wake;
    }
}

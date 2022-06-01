
namespace Network_Toolkit
{
    partial class Event
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.TXT_Log = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LBL_CTX = new System.Windows.Forms.Label();
            this.LBL_CTXD = new System.Windows.Forms.Label();
            this.LBL_CRX = new System.Windows.Forms.Label();
            this.LBL_CRXD = new System.Windows.Forms.Label();
            this.LBL_TO = new System.Windows.Forms.Label();
            this.LBL_RT = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(-5, -12);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 94);
            this.panel1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 29);
            this.label2.TabIndex = 22;
            this.label2.Text = "Device Events";
            // 
            // TXT_Log
            // 
            this.TXT_Log.Location = new System.Drawing.Point(16, 117);
            this.TXT_Log.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TXT_Log.Multiline = true;
            this.TXT_Log.Name = "TXT_Log";
            this.TXT_Log.ReadOnly = true;
            this.TXT_Log.Size = new System.Drawing.Size(523, 197);
            this.TXT_Log.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(415, 466);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 42);
            this.button1.TabIndex = 23;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = "ZWave Events";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "Network Statistics";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Commands TX";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 391);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 16);
            this.label6.TabIndex = 28;
            this.label6.Text = "Commands TX Dropped";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(254, 391);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 16);
            this.label5.TabIndex = 30;
            this.label5.Text = "Commands RX Dropped";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(254, 368);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 16);
            this.label7.TabIndex = 29;
            this.label7.Text = "Commands RX";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(18, 415);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 31;
            this.label8.Text = "Timeout";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(254, 415);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 16);
            this.label9.TabIndex = 32;
            this.label9.Text = "RTT";
            // 
            // LBL_CTX
            // 
            this.LBL_CTX.AutoSize = true;
            this.LBL_CTX.Location = new System.Drawing.Point(201, 368);
            this.LBL_CTX.Name = "LBL_CTX";
            this.LBL_CTX.Size = new System.Drawing.Size(15, 16);
            this.LBL_CTX.TabIndex = 33;
            this.LBL_CTX.Text = "0";
            // 
            // LBL_CTXD
            // 
            this.LBL_CTXD.AutoSize = true;
            this.LBL_CTXD.Location = new System.Drawing.Point(201, 391);
            this.LBL_CTXD.Name = "LBL_CTXD";
            this.LBL_CTXD.Size = new System.Drawing.Size(15, 16);
            this.LBL_CTXD.TabIndex = 34;
            this.LBL_CTXD.Text = "0";
            // 
            // LBL_CRX
            // 
            this.LBL_CRX.AutoSize = true;
            this.LBL_CRX.Location = new System.Drawing.Point(442, 368);
            this.LBL_CRX.Name = "LBL_CRX";
            this.LBL_CRX.Size = new System.Drawing.Size(15, 16);
            this.LBL_CRX.TabIndex = 35;
            this.LBL_CRX.Text = "0";
            // 
            // LBL_CRXD
            // 
            this.LBL_CRXD.AutoSize = true;
            this.LBL_CRXD.Location = new System.Drawing.Point(442, 391);
            this.LBL_CRXD.Name = "LBL_CRXD";
            this.LBL_CRXD.Size = new System.Drawing.Size(15, 16);
            this.LBL_CRXD.TabIndex = 36;
            this.LBL_CRXD.Text = "0";
            // 
            // LBL_TO
            // 
            this.LBL_TO.AutoSize = true;
            this.LBL_TO.Location = new System.Drawing.Point(201, 415);
            this.LBL_TO.Name = "LBL_TO";
            this.LBL_TO.Size = new System.Drawing.Size(15, 16);
            this.LBL_TO.TabIndex = 37;
            this.LBL_TO.Text = "0";
            // 
            // LBL_RT
            // 
            this.LBL_RT.AutoSize = true;
            this.LBL_RT.Location = new System.Drawing.Point(442, 421);
            this.LBL_RT.Name = "LBL_RT";
            this.LBL_RT.Size = new System.Drawing.Size(15, 16);
            this.LBL_RT.TabIndex = 38;
            this.LBL_RT.Text = "0";
            // 
            // Event
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(555, 527);
            this.Controls.Add(this.LBL_RT);
            this.Controls.Add(this.LBL_TO);
            this.Controls.Add(this.LBL_CRXD);
            this.Controls.Add(this.LBL_CRX);
            this.Controls.Add(this.LBL_CTXD);
            this.Controls.Add(this.LBL_CTX);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TXT_Log);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Event";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Device Events";
            this.Load += new System.EventHandler(this.Event_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TXT_Log;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label LBL_CTX;
        private System.Windows.Forms.Label LBL_CTXD;
        private System.Windows.Forms.Label LBL_CRX;
        private System.Windows.Forms.Label LBL_CRXD;
        private System.Windows.Forms.Label LBL_TO;
        private System.Windows.Forms.Label LBL_RT;
    }
}
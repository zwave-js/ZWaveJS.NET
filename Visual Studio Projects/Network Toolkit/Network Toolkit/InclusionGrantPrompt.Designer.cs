
namespace Network_Toolkit
{
    partial class InclusionGrantPrompt
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
            this.CB_S2AC = new System.Windows.Forms.CheckBox();
            this.CB_S2A = new System.Windows.Forms.CheckBox();
            this.CB_S2U = new System.Windows.Forms.CheckBox();
            this.CB_S0 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CB_S2AC
            // 
            this.CB_S2AC.AutoSize = true;
            this.CB_S2AC.Enabled = false;
            this.CB_S2AC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.CB_S2AC.Location = new System.Drawing.Point(91, 99);
            this.CB_S2AC.Name = "CB_S2AC";
            this.CB_S2AC.Size = new System.Drawing.Size(168, 24);
            this.CB_S2AC.TabIndex = 0;
            this.CB_S2AC.Tag = "S2_AccessControl";
            this.CB_S2AC.Text = "S2 Access Control";
            this.CB_S2AC.UseVisualStyleBackColor = true;
            // 
            // CB_S2A
            // 
            this.CB_S2A.AutoSize = true;
            this.CB_S2A.Enabled = false;
            this.CB_S2A.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.CB_S2A.Location = new System.Drawing.Point(91, 156);
            this.CB_S2A.Name = "CB_S2A";
            this.CB_S2A.Size = new System.Drawing.Size(155, 24);
            this.CB_S2A.TabIndex = 1;
            this.CB_S2A.Tag = "S2_Authenticated";
            this.CB_S2A.Text = "S2 Authenticated";
            this.CB_S2A.UseVisualStyleBackColor = true;
            // 
            // CB_S2U
            // 
            this.CB_S2U.AutoSize = true;
            this.CB_S2U.Enabled = false;
            this.CB_S2U.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.CB_S2U.Location = new System.Drawing.Point(91, 214);
            this.CB_S2U.Name = "CB_S2U";
            this.CB_S2U.Size = new System.Drawing.Size(174, 24);
            this.CB_S2U.TabIndex = 2;
            this.CB_S2U.Tag = "S2_Unauthenticated";
            this.CB_S2U.Text = "S2 Unauthenticated";
            this.CB_S2U.UseVisualStyleBackColor = true;
            // 
            // CB_S0
            // 
            this.CB_S0.AutoSize = true;
            this.CB_S0.Enabled = false;
            this.CB_S0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.CB_S0.Location = new System.Drawing.Point(91, 278);
            this.CB_S0.Name = "CB_S0";
            this.CB_S0.Size = new System.Drawing.Size(107, 24);
            this.CB_S0.TabIndex = 3;
            this.CB_S0.Tag = "S0_Legacy";
            this.CB_S0.Text = "S0 Legacy";
            this.CB_S0.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(-4, -10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 76);
            this.panel1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(384, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 34);
            this.button1.TabIndex = 21;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 29);
            this.label2.TabIndex = 22;
            this.label2.Text = "Allow Security Classes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(110, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 13);
            this.label1.TabIndex = 22;
            this.label1.Tag = "S2_AccessControl";
            this.label1.Text = "S2 for door locks, garage doors, access control systems etc.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(110, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(287, 13);
            this.label3.TabIndex = 23;
            this.label3.Tag = "S2_Authenticated";
            this.label3.Text = "Allows the device that is being added, to be verifed that it is";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(111, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(298, 13);
            this.label4.TabIndex = 24;
            this.label4.Tag = "S2_Unauthenticated";
            this.label4.Text = "Like S2 Authenticated, but without verification that the device";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(109, 301);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 13);
            this.label5.TabIndex = 25;
            this.label5.Tag = "S0_Legacy";
            this.label5.Text = "S0 for devices that don\'t support S2";
            // 
            // InclusionGrantPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 410);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CB_S0);
            this.Controls.Add(this.CB_S2U);
            this.Controls.Add(this.CB_S2A);
            this.Controls.Add(this.CB_S2AC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InclusionGrantPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Allow Security Classes";
            this.Load += new System.EventHandler(this.InclusionGrantPrompt_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CB_S2AC;
        private System.Windows.Forms.CheckBox CB_S2A;
        private System.Windows.Forms.CheckBox CB_S2U;
        private System.Windows.Forms.CheckBox CB_S0;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
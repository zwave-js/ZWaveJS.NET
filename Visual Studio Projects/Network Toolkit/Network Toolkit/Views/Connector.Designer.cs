
namespace Network_Toolkit.Views
{
    partial class Connector
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
            this.label2 = new System.Windows.Forms.Label();
            this.COM_SerialPort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TXT_S0 = new System.Windows.Forms.TextBox();
            this.TXT_S2AC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TXT_S2A = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TXT_S2U = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CB_Logging = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.NUM_Schema = new System.Windows.Forms.NumericUpDown();
            this.button6 = new System.Windows.Forms.Button();
            this.TXT_WS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_Schema)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.75F);
            this.label2.Location = new System.Drawing.Point(18, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Connection";
            // 
            // COM_SerialPort
            // 
            this.COM_SerialPort.FormattingEnabled = true;
            this.COM_SerialPort.Location = new System.Drawing.Point(216, 42);
            this.COM_SerialPort.Name = "COM_SerialPort";
            this.COM_SerialPort.Size = new System.Drawing.Size(260, 21);
            this.COM_SerialPort.TabIndex = 4;
            this.COM_SerialPort.Text = "Select Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sierial Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "S0 Key";
            // 
            // TXT_S0
            // 
            this.TXT_S0.Location = new System.Drawing.Point(216, 84);
            this.TXT_S0.Name = "TXT_S0";
            this.TXT_S0.Size = new System.Drawing.Size(207, 20);
            this.TXT_S0.TabIndex = 7;
            // 
            // TXT_S2AC
            // 
            this.TXT_S2AC.Location = new System.Drawing.Point(216, 112);
            this.TXT_S2AC.Name = "TXT_S2AC";
            this.TXT_S2AC.Size = new System.Drawing.Size(207, 20);
            this.TXT_S2AC.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "S2 Key (Access Control)";
            // 
            // TXT_S2A
            // 
            this.TXT_S2A.Location = new System.Drawing.Point(216, 140);
            this.TXT_S2A.Name = "TXT_S2A";
            this.TXT_S2A.Size = new System.Drawing.Size(207, 20);
            this.TXT_S2A.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "S2 Key (Authorised)";
            // 
            // TXT_S2U
            // 
            this.TXT_S2U.Location = new System.Drawing.Point(216, 168);
            this.TXT_S2U.Name = "TXT_S2U";
            this.TXT_S2U.Size = new System.Drawing.Size(207, 20);
            this.TXT_S2U.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "S2 Key (Unauthorised)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Enable Logging";
            // 
            // CB_Logging
            // 
            this.CB_Logging.AutoSize = true;
            this.CB_Logging.Location = new System.Drawing.Point(216, 212);
            this.CB_Logging.Name = "CB_Logging";
            this.CB_Logging.Size = new System.Drawing.Size(15, 14);
            this.CB_Logging.TabIndex = 15;
            this.CB_Logging.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(447, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 34);
            this.button1.TabIndex = 20;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(429, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 20);
            this.button2.TabIndex = 21;
            this.button2.Text = "Gen";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(429, 112);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(47, 20);
            this.button3.TabIndex = 22;
            this.button3.Text = "Gen";
            this.button3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(429, 143);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(47, 20);
            this.button4.TabIndex = 23;
            this.button4.Text = "Gen";
            this.button4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(429, 168);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(47, 20);
            this.button5.TabIndex = 24;
            this.button5.Text = "Gen";
            this.button5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.COM_SerialPort);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.TXT_S0);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.TXT_S2AC);
            this.groupBox1.Controls.Add(this.CB_Logging);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.TXT_S2A);
            this.groupBox1.Controls.Add(this.TXT_S2U);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(23, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 278);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Self Hosted";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.NUM_Schema);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.TXT_WS);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(23, 339);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(548, 141);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ZWave JS Server Client";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Schema Version";
            // 
            // NUM_Schema
            // 
            this.NUM_Schema.Location = new System.Drawing.Point(216, 65);
            this.NUM_Schema.Name = "NUM_Schema";
            this.NUM_Schema.Size = new System.Drawing.Size(260, 20);
            this.NUM_Schema.TabIndex = 22;
            this.NUM_Schema.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(447, 101);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(95, 34);
            this.button6.TabIndex = 21;
            this.button6.Text = "Connect";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // TXT_WS
            // 
            this.TXT_WS.Location = new System.Drawing.Point(216, 36);
            this.TXT_WS.Name = "TXT_WS";
            this.TXT_WS.Size = new System.Drawing.Size(260, 20);
            this.TXT_WS.TabIndex = 8;
            this.TXT_WS.Text = "ws://";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "WS Socket Address";
            // 
            // Connector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Name = "Connector";
            this.Size = new System.Drawing.Size(600, 499);
            this.Load += new System.EventHandler(this.Connector_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_Schema)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox COM_SerialPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TXT_S0;
        private System.Windows.Forms.TextBox TXT_S2AC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TXT_S2A;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TXT_S2U;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox CB_Logging;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox TXT_WS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown NUM_Schema;
    }
}

namespace Demo_Application
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GP_Settings = new GroupBox();
            button16 = new Button();
            button15 = new Button();
            button14 = new Button();
            button13 = new Button();
            CB_LockCode = new CheckBox();
            CB_DisableOptimistic = new CheckBox();
            CB_SoftResetUSB = new CheckBox();
            BTN_Connect = new Button();
            COM_SerialPort = new ComboBox();
            label5 = new Label();
            TXT_KEY_UAuth = new TextBox();
            label4 = new Label();
            TXT_KEY_Auth = new TextBox();
            label3 = new Label();
            TXT_KEY_AC = new TextBox();
            label2 = new Label();
            TXT_KEY_S0 = new TextBox();
            label1 = new Label();
            LST_Nodes = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            GP_Nodes = new GroupBox();
            button18 = new Button();
            button17 = new Button();
            button12 = new Button();
            button4 = new Button();
            button2 = new Button();
            button1 = new Button();
            button3 = new Button();
            GP_Network = new GroupBox();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            GP_Controller = new GroupBox();
            label9 = new Label();
            label8 = new Label();
            numericUpDown2 = new NumericUpDown();
            button20 = new Button();
            numericUpDown1 = new NumericUpDown();
            label7 = new Label();
            button19 = new Button();
            label6 = new Label();
            comboBox1 = new ComboBox();
            button11 = new Button();
            button9 = new Button();
            button8 = new Button();
            button10 = new Button();
            statusStrip1 = new StatusStrip();
            LBL_Versions = new ToolStripStatusLabel();
            PB_Connect = new ToolStripProgressBar();
            groupBox5 = new GroupBox();
            LST_ControllerStats = new ListView();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            GP_Settings.SuspendLayout();
            GP_Nodes.SuspendLayout();
            GP_Network.SuspendLayout();
            GP_Controller.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            statusStrip1.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // GP_Settings
            // 
            GP_Settings.Controls.Add(button16);
            GP_Settings.Controls.Add(button15);
            GP_Settings.Controls.Add(button14);
            GP_Settings.Controls.Add(button13);
            GP_Settings.Controls.Add(CB_LockCode);
            GP_Settings.Controls.Add(CB_DisableOptimistic);
            GP_Settings.Controls.Add(CB_SoftResetUSB);
            GP_Settings.Controls.Add(BTN_Connect);
            GP_Settings.Controls.Add(COM_SerialPort);
            GP_Settings.Controls.Add(label5);
            GP_Settings.Controls.Add(TXT_KEY_UAuth);
            GP_Settings.Controls.Add(label4);
            GP_Settings.Controls.Add(TXT_KEY_Auth);
            GP_Settings.Controls.Add(label3);
            GP_Settings.Controls.Add(TXT_KEY_AC);
            GP_Settings.Controls.Add(label2);
            GP_Settings.Controls.Add(TXT_KEY_S0);
            GP_Settings.Controls.Add(label1);
            GP_Settings.Location = new Point(12, 12);
            GP_Settings.Name = "GP_Settings";
            GP_Settings.Size = new Size(358, 258);
            GP_Settings.TabIndex = 0;
            GP_Settings.TabStop = false;
            GP_Settings.Text = "Driver Settings";
            // 
            // button16
            // 
            button16.Location = new Point(266, 143);
            button16.Name = "button16";
            button16.Size = new Size(53, 23);
            button16.TabIndex = 16;
            button16.Text = "Gen";
            button16.UseVisualStyleBackColor = true;
            button16.Click += button16_Click;
            // 
            // button15
            // 
            button15.Location = new Point(266, 117);
            button15.Name = "button15";
            button15.Size = new Size(53, 23);
            button15.TabIndex = 15;
            button15.Text = "Gen";
            button15.UseVisualStyleBackColor = true;
            button15.Click += button15_Click;
            // 
            // button14
            // 
            button14.Location = new Point(266, 85);
            button14.Name = "button14";
            button14.Size = new Size(53, 23);
            button14.TabIndex = 14;
            button14.Text = "Gen";
            button14.UseVisualStyleBackColor = true;
            button14.Click += button14_Click;
            // 
            // button13
            // 
            button13.Location = new Point(266, 56);
            button13.Name = "button13";
            button13.Size = new Size(53, 23);
            button13.TabIndex = 11;
            button13.Text = "Gen";
            button13.UseVisualStyleBackColor = true;
            button13.Click += button13_Click;
            // 
            // CB_LockCode
            // 
            CB_LockCode.AutoSize = true;
            CB_LockCode.Location = new Point(139, 184);
            CB_LockCode.Name = "CB_LockCode";
            CB_LockCode.Size = new Size(138, 19);
            CB_LockCode.TabIndex = 13;
            CB_LockCode.Text = "Interview Lock Codes";
            CB_LockCode.UseVisualStyleBackColor = true;
            // 
            // CB_DisableOptimistic
            // 
            CB_DisableOptimistic.AutoSize = true;
            CB_DisableOptimistic.Location = new Point(17, 209);
            CB_DisableOptimistic.Name = "CB_DisableOptimistic";
            CB_DisableOptimistic.Size = new Size(168, 19);
            CB_DisableOptimistic.TabIndex = 12;
            CB_DisableOptimistic.Text = "Disable Optimistic Updates";
            CB_DisableOptimistic.UseVisualStyleBackColor = true;
            // 
            // CB_SoftResetUSB
            // 
            CB_SoftResetUSB.AutoSize = true;
            CB_SoftResetUSB.Checked = true;
            CB_SoftResetUSB.CheckState = CheckState.Checked;
            CB_SoftResetUSB.Location = new Point(17, 184);
            CB_SoftResetUSB.Name = "CB_SoftResetUSB";
            CB_SoftResetUSB.Size = new Size(102, 19);
            CB_SoftResetUSB.TabIndex = 11;
            CB_SoftResetUSB.Text = "Soft Reset USB";
            CB_SoftResetUSB.UseVisualStyleBackColor = true;
            // 
            // BTN_Connect
            // 
            BTN_Connect.Location = new Point(232, 225);
            BTN_Connect.Name = "BTN_Connect";
            BTN_Connect.Size = new Size(108, 27);
            BTN_Connect.TabIndex = 10;
            BTN_Connect.Text = "Connect";
            BTN_Connect.UseVisualStyleBackColor = true;
            BTN_Connect.Click += button4_Click;
            // 
            // COM_SerialPort
            // 
            COM_SerialPort.FormattingEnabled = true;
            COM_SerialPort.Location = new Point(139, 26);
            COM_SerialPort.Name = "COM_SerialPort";
            COM_SerialPort.Size = new Size(180, 23);
            COM_SerialPort.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 29);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 8;
            label5.Text = "Serial Port";
            // 
            // TXT_KEY_UAuth
            // 
            TXT_KEY_UAuth.Location = new Point(139, 143);
            TXT_KEY_UAuth.Name = "TXT_KEY_UAuth";
            TXT_KEY_UAuth.Size = new Size(121, 23);
            TXT_KEY_UAuth.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 146);
            label4.Name = "label4";
            label4.Size = new Size(116, 15);
            label4.TabIndex = 6;
            label4.Text = "Unauthenticated key";
            // 
            // TXT_KEY_Auth
            // 
            TXT_KEY_Auth.Location = new Point(139, 114);
            TXT_KEY_Auth.Name = "TXT_KEY_Auth";
            TXT_KEY_Auth.Size = new Size(121, 23);
            TXT_KEY_Auth.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 117);
            label3.Name = "label3";
            label3.Size = new Size(103, 15);
            label3.TabIndex = 4;
            label3.Text = "Authenticated key";
            // 
            // TXT_KEY_AC
            // 
            TXT_KEY_AC.Location = new Point(139, 85);
            TXT_KEY_AC.Name = "TXT_KEY_AC";
            TXT_KEY_AC.Size = new Size(121, 23);
            TXT_KEY_AC.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 88);
            label2.Name = "label2";
            label2.Size = new Size(107, 15);
            label2.TabIndex = 2;
            label2.Text = "Access Control key";
            // 
            // TXT_KEY_S0
            // 
            TXT_KEY_S0.Location = new Point(139, 56);
            TXT_KEY_S0.Name = "TXT_KEY_S0";
            TXT_KEY_S0.Size = new Size(121, 23);
            TXT_KEY_S0.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 59);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 0;
            label1.Text = "S0 Key";
            // 
            // LST_Nodes
            // 
            LST_Nodes.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            LST_Nodes.FullRowSelect = true;
            LST_Nodes.Location = new Point(6, 22);
            LST_Nodes.MultiSelect = false;
            LST_Nodes.Name = "LST_Nodes";
            LST_Nodes.Size = new Size(246, 238);
            LST_Nodes.TabIndex = 1;
            LST_Nodes.UseCompatibleStateImageBehavior = false;
            LST_Nodes.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "ID";
            columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Status";
            columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Vender";
            columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Label";
            columnHeader4.Width = 70;
            // 
            // GP_Nodes
            // 
            GP_Nodes.Controls.Add(button18);
            GP_Nodes.Controls.Add(button17);
            GP_Nodes.Controls.Add(button12);
            GP_Nodes.Controls.Add(button4);
            GP_Nodes.Controls.Add(button2);
            GP_Nodes.Controls.Add(LST_Nodes);
            GP_Nodes.Controls.Add(button1);
            GP_Nodes.Controls.Add(button3);
            GP_Nodes.Enabled = false;
            GP_Nodes.Location = new Point(12, 276);
            GP_Nodes.Name = "GP_Nodes";
            GP_Nodes.Size = new Size(358, 266);
            GP_Nodes.TabIndex = 2;
            GP_Nodes.TabStop = false;
            GP_Nodes.Text = "Nodes";
            // 
            // button18
            // 
            button18.Location = new Point(266, 172);
            button18.Name = "button18";
            button18.Size = new Size(74, 23);
            button18.TabIndex = 8;
            button18.Text = "Remove";
            button18.UseVisualStyleBackColor = true;
            // 
            // button17
            // 
            button17.Location = new Point(266, 143);
            button17.Name = "button17";
            button17.Size = new Size(74, 23);
            button17.TabIndex = 7;
            button17.Text = "Replace";
            button17.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            button12.Location = new Point(266, 56);
            button12.Name = "button12";
            button12.Size = new Size(74, 23);
            button12.TabIndex = 6;
            button12.Text = "Assocation";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            // 
            // button4
            // 
            button4.Location = new Point(266, 114);
            button4.Name = "button4";
            button4.Size = new Size(74, 23);
            button4.TabIndex = 5;
            button4.Text = "Health";
            button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(266, 27);
            button2.Name = "button2";
            button2.Size = new Size(74, 23);
            button2.TabIndex = 3;
            button2.Text = "Values";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(266, 201);
            button1.Name = "button1";
            button1.Size = new Size(74, 23);
            button1.TabIndex = 2;
            button1.Text = "Update FW";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.Location = new Point(266, 85);
            button3.Name = "button3";
            button3.Size = new Size(74, 23);
            button3.TabIndex = 4;
            button3.Text = "Events";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // GP_Network
            // 
            GP_Network.Controls.Add(button7);
            GP_Network.Controls.Add(button6);
            GP_Network.Controls.Add(button5);
            GP_Network.Enabled = false;
            GP_Network.Location = new Point(384, 276);
            GP_Network.Name = "GP_Network";
            GP_Network.Size = new Size(428, 77);
            GP_Network.TabIndex = 3;
            GP_Network.TabStop = false;
            GP_Network.Text = "Network Commands";
            GP_Network.Enter += GP_Network_Enter;
            // 
            // button7
            // 
            button7.Location = new Point(161, 27);
            button7.Name = "button7";
            button7.Size = new Size(108, 27);
            button7.TabIndex = 13;
            button7.Text = "Remove Node";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.Location = new Point(291, 27);
            button6.Name = "button6";
            button6.Size = new Size(108, 27);
            button6.TabIndex = 12;
            button6.Text = "Add Node";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Location = new Point(30, 27);
            button5.Name = "button5";
            button5.Size = new Size(108, 27);
            button5.TabIndex = 11;
            button5.Text = "Rebuild Routes";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // GP_Controller
            // 
            GP_Controller.Controls.Add(label9);
            GP_Controller.Controls.Add(label8);
            GP_Controller.Controls.Add(numericUpDown2);
            GP_Controller.Controls.Add(button20);
            GP_Controller.Controls.Add(numericUpDown1);
            GP_Controller.Controls.Add(label7);
            GP_Controller.Controls.Add(button19);
            GP_Controller.Controls.Add(label6);
            GP_Controller.Controls.Add(comboBox1);
            GP_Controller.Controls.Add(button11);
            GP_Controller.Controls.Add(button9);
            GP_Controller.Controls.Add(button8);
            GP_Controller.Controls.Add(button10);
            GP_Controller.Enabled = false;
            GP_Controller.Location = new Point(384, 359);
            GP_Controller.Name = "GP_Controller";
            GP_Controller.Size = new Size(428, 183);
            GP_Controller.TabIndex = 4;
            GP_Controller.TabStop = false;
            GP_Controller.Text = "Controller Commands";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(125, 147);
            label9.Name = "label9";
            label9.Size = new Size(52, 15);
            label9.TabIndex = 24;
            label9.Text = "@ 0dbm";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(125, 118);
            label8.Name = "label8";
            label8.Size = new Size(34, 15);
            label8.TabIndex = 23;
            label8.Text = "Level";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(183, 145);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(120, 23);
            numericUpDown2.TabIndex = 22;
            // 
            // button20
            // 
            button20.Location = new Point(315, 132);
            button20.Name = "button20";
            button20.Size = new Size(84, 23);
            button20.TabIndex = 21;
            button20.Text = "Update";
            button20.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(183, 116);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 20;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(30, 136);
            label7.Name = "label7";
            label7.Size = new Size(40, 15);
            label7.TabIndex = 19;
            label7.Text = "Power";
            // 
            // button19
            // 
            button19.Location = new Point(315, 79);
            button19.Name = "button19";
            button19.Size = new Size(84, 23);
            button19.TabIndex = 18;
            button19.Text = "Update";
            button19.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(30, 83);
            label6.Name = "label6";
            label6.Size = new Size(44, 15);
            label6.TabIndex = 17;
            label6.Text = "Region";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(125, 80);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(178, 23);
            comboBox1.TabIndex = 16;
            // 
            // button11
            // 
            button11.Location = new Point(315, 31);
            button11.Name = "button11";
            button11.Size = new Size(84, 27);
            button11.TabIndex = 15;
            button11.Text = "Backup NVM";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button9
            // 
            button9.Location = new Point(219, 31);
            button9.Name = "button9";
            button9.Size = new Size(84, 27);
            button9.TabIndex = 14;
            button9.Text = "Restore NVM";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.Location = new Point(125, 31);
            button8.Name = "button8";
            button8.Size = new Size(84, 27);
            button8.TabIndex = 13;
            button8.Text = "Update FW";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button10
            // 
            button10.Location = new Point(30, 31);
            button10.Name = "button10";
            button10.Size = new Size(84, 27);
            button10.TabIndex = 11;
            button10.Text = "Hard Reset";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { LBL_Versions, PB_Connect });
            statusStrip1.Location = new Point(0, 563);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(818, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // LBL_Versions
            // 
            LBL_Versions.Name = "LBL_Versions";
            LBL_Versions.Size = new Size(193, 17);
            LBL_Versions.Text = "Server Version : ?? Driver Version : ??";
            // 
            // PB_Connect
            // 
            PB_Connect.MarqueeAnimationSpeed = 50;
            PB_Connect.Name = "PB_Connect";
            PB_Connect.Size = new Size(100, 16);
            PB_Connect.Style = ProgressBarStyle.Marquee;
            PB_Connect.Value = 25;
            PB_Connect.Visible = false;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(LST_ControllerStats);
            groupBox5.Location = new Point(378, 12);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(428, 258);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "Controller Statistics";
            // 
            // LST_ControllerStats
            // 
            LST_ControllerStats.Columns.AddRange(new ColumnHeader[] { columnHeader5, columnHeader6 });
            LST_ControllerStats.FullRowSelect = true;
            LST_ControllerStats.Location = new Point(6, 22);
            LST_ControllerStats.Name = "LST_ControllerStats";
            LST_ControllerStats.Size = new Size(416, 229);
            LST_ControllerStats.TabIndex = 2;
            LST_ControllerStats.UseCompatibleStateImageBehavior = false;
            LST_ControllerStats.View = View.Details;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Statistic";
            columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Value";
            columnHeader6.Width = 200;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(818, 585);
            Controls.Add(groupBox5);
            Controls.Add(statusStrip1);
            Controls.Add(GP_Controller);
            Controls.Add(GP_Network);
            Controls.Add(GP_Nodes);
            Controls.Add(GP_Settings);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ZWaveJS.NET :: Demo Application";
            Load += Form1_Load;
            GP_Settings.ResumeLayout(false);
            GP_Settings.PerformLayout();
            GP_Nodes.ResumeLayout(false);
            GP_Network.ResumeLayout(false);
            GP_Controller.ResumeLayout(false);
            GP_Controller.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox5.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox GP_Settings;
        private TextBox TXT_KEY_UAuth;
        private Label label4;
        private TextBox TXT_KEY_Auth;
        private Label label3;
        private TextBox TXT_KEY_AC;
        private Label label2;
        private TextBox TXT_KEY_S0;
        private Label label1;
        private ListView LST_Nodes;
        private ComboBox COM_SerialPort;
        private Label label5;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private GroupBox GP_Nodes;
        private Button button2;
        private Button button1;
        private Button button3;
        private Button BTN_Connect;
        private Button button4;
        private GroupBox GP_Network;
        private Button button7;
        private Button button6;
        private Button button5;
        private GroupBox GP_Controller;
        private Button button8;
        private Button button10;
        private CheckBox CB_DisableOptimistic;
        private CheckBox CB_SoftResetUSB;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel LBL_Versions;
        private GroupBox groupBox5;
        private ListView LST_ControllerStats;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private Button button11;
        private Button button9;
        private ToolStripProgressBar PB_Connect;
        private CheckBox CB_LockCode;
        private Button button12;
        private Button button16;
        private Button button15;
        private Button button14;
        private Button button13;
        private Button button18;
        private Button button17;
        private Label label6;
        private ComboBox comboBox1;
        private NumericUpDown numericUpDown2;
        private Button button20;
        private NumericUpDown numericUpDown1;
        private Label label7;
        private Button button19;
        private Label label9;
        private Label label8;
    }
}
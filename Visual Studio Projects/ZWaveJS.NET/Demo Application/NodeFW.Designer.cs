namespace Demo_Application
{
    partial class NodeFW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeFW));
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label4 = new Label();
            NUM_Target = new NumericUpDown();
            LBL_Status = new Label();
            button3 = new Button();
            LBL_Node = new Label();
            label3 = new Label();
            button2 = new Button();
            TXT_Filename = new TextBox();
            button1 = new Button();
            PB_Progress1 = new ProgressBar();
            label2 = new Label();
            tabPage2 = new TabPage();
            LBL_Status2 = new Label();
            button5 = new Button();
            PB_Progress2 = new ProgressBar();
            button4 = new Button();
            PAN_Updates = new FlowLayoutPanel();
            LBL_Node2 = new Label();
            label1 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUM_Target).BeginInit();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(643, 484);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(NUM_Target);
            tabPage1.Controls.Add(LBL_Status);
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(LBL_Node);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(TXT_Filename);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(PB_Progress1);
            tabPage1.Controls.Add(label2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(635, 456);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Upload Firmware";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(437, 318);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 11;
            label4.Text = "Target Chip";
            // 
            // NUM_Target
            // 
            NUM_Target.Location = new Point(437, 336);
            NUM_Target.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NUM_Target.Name = "NUM_Target";
            NUM_Target.Size = new Size(72, 23);
            NUM_Target.TabIndex = 10;
            // 
            // LBL_Status
            // 
            LBL_Status.AutoSize = true;
            LBL_Status.Location = new Point(128, 393);
            LBL_Status.Name = "LBL_Status";
            LBL_Status.Size = new Size(94, 15);
            LBL_Status.TabIndex = 9;
            LBL_Status.Text = "Waiting for file...";
            // 
            // button3
            // 
            button3.Location = new Point(437, 393);
            button3.Name = "button3";
            button3.Size = new Size(72, 23);
            button3.TabIndex = 8;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // LBL_Node
            // 
            LBL_Node.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LBL_Node.Location = new Point(17, 155);
            LBL_Node.Name = "LBL_Node";
            LBL_Node.Size = new Size(597, 19);
            LBL_Node.TabIndex = 7;
            LBL_Node.Text = "You are about to update the Firmware for Node : {0}";
            LBL_Node.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(128, 318);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 6;
            label3.Text = "Firmware File";
            // 
            // button2
            // 
            button2.Location = new Point(359, 393);
            button2.Name = "button2";
            button2.Size = new Size(72, 23);
            button2.TabIndex = 5;
            button2.Text = "Start";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // TXT_Filename
            // 
            TXT_Filename.Location = new Point(128, 336);
            TXT_Filename.Name = "TXT_Filename";
            TXT_Filename.ReadOnly = true;
            TXT_Filename.Size = new Size(222, 23);
            TXT_Filename.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(359, 335);
            button1.Name = "button1";
            button1.Size = new Size(72, 23);
            button1.TabIndex = 3;
            button1.Text = "Open";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PB_Progress1
            // 
            PB_Progress1.Location = new Point(128, 364);
            PB_Progress1.Name = "PB_Progress1";
            PB_Progress1.Size = new Size(381, 23);
            PB_Progress1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(17, 19);
            label2.Name = "label2";
            label2.Size = new Size(597, 114);
            label2.TabIndex = 1;
            label2.Text = resources.GetString("label2.Text");
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(LBL_Status2);
            tabPage2.Controls.Add(button5);
            tabPage2.Controls.Add(PB_Progress2);
            tabPage2.Controls.Add(button4);
            tabPage2.Controls.Add(PAN_Updates);
            tabPage2.Controls.Add(LBL_Node2);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(635, 456);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Firmware Update Service";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // LBL_Status2
            // 
            LBL_Status2.AutoSize = true;
            LBL_Status2.Location = new Point(38, 420);
            LBL_Status2.Name = "LBL_Status2";
            LBL_Status2.Size = new Size(57, 15);
            LBL_Status2.TabIndex = 13;
            LBL_Status2.Text = "Waiting...";
            // 
            // button5
            // 
            button5.Location = new Point(530, 416);
            button5.Name = "button5";
            button5.Size = new Size(70, 23);
            button5.TabIndex = 12;
            button5.Text = "Cancel";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button3_Click;
            // 
            // PB_Progress2
            // 
            PB_Progress2.Location = new Point(155, 416);
            PB_Progress2.Name = "PB_Progress2";
            PB_Progress2.Size = new Size(288, 23);
            PB_Progress2.TabIndex = 11;
            // 
            // button4
            // 
            button4.Location = new Point(449, 416);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 10;
            button4.Text = "Check";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // PAN_Updates
            // 
            PAN_Updates.AutoScroll = true;
            PAN_Updates.BackColor = Color.WhiteSmoke;
            PAN_Updates.Location = new Point(38, 177);
            PAN_Updates.Name = "PAN_Updates";
            PAN_Updates.Size = new Size(562, 233);
            PAN_Updates.TabIndex = 9;
            // 
            // LBL_Node2
            // 
            LBL_Node2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LBL_Node2.Location = new Point(17, 155);
            LBL_Node2.Name = "LBL_Node2";
            LBL_Node2.Size = new Size(597, 19);
            LBL_Node2.TabIndex = 8;
            LBL_Node2.Text = "You are about to update the Firmware for Node : {0}";
            LBL_Node2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(17, 19);
            label1.Name = "label1";
            label1.Size = new Size(597, 114);
            label1.TabIndex = 0;
            label1.Text = resources.GetString("label1.Text");
            // 
            // NodeFW
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(667, 508);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "NodeFW";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Node Firmware Update";
            FormClosing += NodeFW_FormClosing;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUM_Target).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label2;
        private Label label1;
        private TextBox TXT_Filename;
        private Button button1;
        private ProgressBar PB_Progress1;
        private Button button3;
        private Label LBL_Node;
        private Label label3;
        private Button button2;
        private Label LBL_Status;
        private Label label4;
        private NumericUpDown NUM_Target;
        private FlowLayoutPanel PAN_Updates;
        private Label LBL_Node2;
        private Button button4;
        private ProgressBar PB_Progress2;
        private Button button5;
        private Label LBL_Status2;
    }
}
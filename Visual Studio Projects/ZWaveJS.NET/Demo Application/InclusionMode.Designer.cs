namespace Demo_Application
{
    partial class InclusionMode
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
            label1 = new Label();
            RD_Default = new RadioButton();
            label2 = new Label();
            RD_S2 = new RadioButton();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            label4 = new Label();
            RD_Insecure = new RadioButton();
            label5 = new Label();
            RD_S0 = new RadioButton();
            RD_SmartStart = new RadioButton();
            label6 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(40, 41);
            label1.Name = "label1";
            label1.Size = new Size(168, 19);
            label1.TabIndex = 0;
            label1.Text = "Choose Inclusion Strategy";
            // 
            // RD_Default
            // 
            RD_Default.AutoSize = true;
            RD_Default.Checked = true;
            RD_Default.Location = new Point(63, 96);
            RD_Default.Name = "RD_Default";
            RD_Default.Size = new Size(63, 19);
            RD_Default.TabIndex = 1;
            RD_Default.TabStop = true;
            RD_Default.Text = "Default";
            RD_Default.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(162, 91);
            label2.Name = "label2";
            label2.Size = new Size(233, 30);
            label2.TabIndex = 2;
            label2.Text = "Include with S2, if supported by the device,\r\nelse use S0, ONLY if absolutley necessary.";
            // 
            // RD_S2
            // 
            RD_S2.AutoSize = true;
            RD_S2.Location = new Point(63, 129);
            RD_S2.Name = "RD_S2";
            RD_S2.Size = new Size(37, 19);
            RD_S2.TabIndex = 3;
            RD_S2.Text = "S2";
            RD_S2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(162, 131);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 4;
            label3.Text = "Include with S2";
            // 
            // button1
            // 
            button1.Location = new Point(336, 303);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(255, 303);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(162, 162);
            label4.Name = "label4";
            label4.Size = new Size(108, 15);
            label4.TabIndex = 8;
            label4.Text = "Do not use security";
            // 
            // RD_Insecure
            // 
            RD_Insecure.AutoSize = true;
            RD_Insecure.Location = new Point(63, 160);
            RD_Insecure.Name = "RD_Insecure";
            RD_Insecure.Size = new Size(69, 19);
            RD_Insecure.TabIndex = 7;
            RD_Insecure.Text = "Insecure";
            RD_Insecure.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(162, 197);
            label5.Name = "label5";
            label5.Size = new Size(87, 15);
            label5.TabIndex = 10;
            label5.Text = "Include with S0";
            // 
            // RD_S0
            // 
            RD_S0.AutoSize = true;
            RD_S0.Location = new Point(63, 195);
            RD_S0.Name = "RD_S0";
            RD_S0.Size = new Size(37, 19);
            RD_S0.TabIndex = 9;
            RD_S0.Text = "S0";
            RD_S0.UseVisualStyleBackColor = true;
            // 
            // RD_SmartStart
            // 
            RD_SmartStart.AutoSize = true;
            RD_SmartStart.Location = new Point(63, 231);
            RD_SmartStart.Name = "RD_SmartStart";
            RD_SmartStart.Size = new Size(83, 19);
            RD_SmartStart.TabIndex = 11;
            RD_SmartStart.Text = "Smart Start";
            RD_SmartStart.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(162, 227);
            label6.Name = "label6";
            label6.Size = new Size(152, 30);
            label6.TabIndex = 12;
            label6.Text = "Provisiong A device with its\r\nSmart Start QR Code";
            // 
            // InclusionMode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 338);
            ControlBox = false;
            Controls.Add(label6);
            Controls.Add(RD_SmartStart);
            Controls.Add(label5);
            Controls.Add(RD_S0);
            Controls.Add(label4);
            Controls.Add(RD_Insecure);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(RD_S2);
            Controls.Add(label2);
            Controls.Add(RD_Default);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "InclusionMode";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inclusion Strategy";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private RadioButton RD_Default;
        private Label label2;
        private RadioButton RD_S2;
        private Label label3;
        private Button button1;
        private Button button2;
        private Label label4;
        private RadioButton RD_Insecure;
        private Label label5;
        private RadioButton RD_S0;
        private RadioButton RD_SmartStart;
        private Label label6;
    }
}
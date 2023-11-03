namespace Demo_Application
{
    partial class DSK
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
            TXT_DSK = new TextBox();
            TXT_1 = new TextBox();
            TXT_2 = new TextBox();
            TXT_3 = new TextBox();
            TXT_4 = new TextBox();
            TXT_5 = new TextBox();
            TXT_6 = new TextBox();
            TXT_7 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // TXT_DSK
            // 
            TXT_DSK.Location = new Point(36, 97);
            TXT_DSK.Name = "TXT_DSK";
            TXT_DSK.Size = new Size(42, 23);
            TXT_DSK.TabIndex = 0;
            TXT_DSK.TextAlign = HorizontalAlignment.Center;
            // 
            // TXT_1
            // 
            TXT_1.Location = new Point(84, 97);
            TXT_1.Name = "TXT_1";
            TXT_1.ReadOnly = true;
            TXT_1.Size = new Size(42, 23);
            TXT_1.TabIndex = 1;
            TXT_1.Text = "42899";
            TXT_1.TextAlign = HorizontalAlignment.Center;
            // 
            // TXT_2
            // 
            TXT_2.Location = new Point(132, 97);
            TXT_2.Name = "TXT_2";
            TXT_2.ReadOnly = true;
            TXT_2.Size = new Size(42, 23);
            TXT_2.TabIndex = 2;
            TXT_2.Text = "42899";
            TXT_2.TextAlign = HorizontalAlignment.Center;
            // 
            // TXT_3
            // 
            TXT_3.Location = new Point(180, 97);
            TXT_3.Name = "TXT_3";
            TXT_3.ReadOnly = true;
            TXT_3.Size = new Size(42, 23);
            TXT_3.TabIndex = 3;
            TXT_3.Text = "42899";
            TXT_3.TextAlign = HorizontalAlignment.Center;
            // 
            // TXT_4
            // 
            TXT_4.Location = new Point(228, 97);
            TXT_4.Name = "TXT_4";
            TXT_4.ReadOnly = true;
            TXT_4.Size = new Size(42, 23);
            TXT_4.TabIndex = 4;
            TXT_4.Text = "42899";
            TXT_4.TextAlign = HorizontalAlignment.Center;
            // 
            // TXT_5
            // 
            TXT_5.Location = new Point(276, 97);
            TXT_5.Name = "TXT_5";
            TXT_5.ReadOnly = true;
            TXT_5.Size = new Size(42, 23);
            TXT_5.TabIndex = 5;
            TXT_5.Text = "42899";
            TXT_5.TextAlign = HorizontalAlignment.Center;
            // 
            // TXT_6
            // 
            TXT_6.Location = new Point(324, 97);
            TXT_6.Name = "TXT_6";
            TXT_6.ReadOnly = true;
            TXT_6.Size = new Size(42, 23);
            TXT_6.TabIndex = 6;
            TXT_6.Text = "42899";
            TXT_6.TextAlign = HorizontalAlignment.Center;
            // 
            // TXT_7
            // 
            TXT_7.Location = new Point(372, 97);
            TXT_7.Name = "TXT_7";
            TXT_7.ReadOnly = true;
            TXT_7.Size = new Size(42, 23);
            TXT_7.TabIndex = 7;
            TXT_7.Text = "42899";
            TXT_7.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 79);
            label1.Name = "label1";
            label1.Size = new Size(28, 15);
            label1.TabIndex = 8;
            label1.Text = "DSK";
            // 
            // button1
            // 
            button1.Location = new Point(339, 142);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(40, 36);
            label2.Name = "label2";
            label2.Size = new Size(86, 19);
            label2.TabIndex = 10;
            label2.Text = "Validate DSK";
            // 
            // DSK
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(449, 190);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(TXT_7);
            Controls.Add(TXT_6);
            Controls.Add(TXT_5);
            Controls.Add(TXT_4);
            Controls.Add(TXT_3);
            Controls.Add(TXT_2);
            Controls.Add(TXT_1);
            Controls.Add(TXT_DSK);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "DSK";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DSK";
            Load += DSK_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox TXT_1;
        private TextBox TXT_2;
        private TextBox TXT_3;
        private TextBox TXT_4;
        private TextBox TXT_5;
        private TextBox TXT_6;
        private TextBox TXT_7;
        private Label label1;
        private Button button1;
        private Label label2;
        internal TextBox TXT_DSK;
    }
}
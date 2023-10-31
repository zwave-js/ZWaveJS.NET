namespace Demo_Application
{
    partial class FirmwareUpdateCard
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
            label1 = new Label();
            label2 = new Label();
            LBL_Version = new Label();
            LBL_Downgrade = new Label();
            button1 = new Button();
            label5 = new Label();
            TXT_Changelog = new TextBox();
            label3 = new Label();
            LBL_Files = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(18, 19);
            label1.Name = "label1";
            label1.Size = new Size(48, 15);
            label1.TabIndex = 0;
            label1.Text = "Version";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(126, 19);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 1;
            label2.Text = "Type";
            // 
            // LBL_Version
            // 
            LBL_Version.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LBL_Version.Location = new Point(72, 15);
            LBL_Version.Name = "LBL_Version";
            LBL_Version.Size = new Size(46, 19);
            LBL_Version.TabIndex = 2;
            LBL_Version.Text = "1.0";
            // 
            // LBL_Downgrade
            // 
            LBL_Downgrade.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LBL_Downgrade.Location = new Point(165, 17);
            LBL_Downgrade.Name = "LBL_Downgrade";
            LBL_Downgrade.Size = new Size(111, 19);
            LBL_Downgrade.TabIndex = 3;
            LBL_Downgrade.Text = "Yes";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = SystemColors.Control;
            button1.Location = new Point(371, 19);
            button1.Name = "button1";
            button1.Size = new Size(134, 41);
            button1.TabIndex = 4;
            button1.Text = "Upgrade";
            button1.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(18, 77);
            label5.Name = "label5";
            label5.Size = new Size(65, 15);
            label5.TabIndex = 5;
            label5.Text = "Changelog";
            // 
            // TXT_Changelog
            // 
            TXT_Changelog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TXT_Changelog.Location = new Point(18, 95);
            TXT_Changelog.Multiline = true;
            TXT_Changelog.Name = "TXT_Changelog";
            TXT_Changelog.ReadOnly = true;
            TXT_Changelog.Size = new Size(487, 123);
            TXT_Changelog.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(18, 45);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 7;
            label3.Text = "Files";
            // 
            // LBL_Files
            // 
            LBL_Files.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LBL_Files.Location = new Point(72, 41);
            LBL_Files.Name = "LBL_Files";
            LBL_Files.Size = new Size(46, 19);
            LBL_Files.TabIndex = 8;
            LBL_Files.Text = "2";
            // 
            // FirmwareUpdateCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(LBL_Files);
            Controls.Add(label3);
            Controls.Add(TXT_Changelog);
            Controls.Add(label5);
            Controls.Add(button1);
            Controls.Add(LBL_Downgrade);
            Controls.Add(LBL_Version);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(5);
            Name = "FirmwareUpdateCard";
            Padding = new Padding(2);
            Size = new Size(530, 236);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label LBL_Version;
        private Label LBL_Downgrade;
        private Button button1;
        private Label label5;
        private TextBox TXT_Changelog;
        private Label label3;
        private Label LBL_Files;
    }
}

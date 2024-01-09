namespace Demo_Application
{
    partial class NVMRestore
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
            PB_Convert = new ProgressBar();
            label2 = new Label();
            PB_Restore = new ProgressBar();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 22);
            label1.Name = "label1";
            label1.Size = new Size(115, 15);
            label1.TabIndex = 3;
            label1.Text = "Conversion Progress";
            // 
            // PB_Convert
            // 
            PB_Convert.Location = new Point(29, 40);
            PB_Convert.Name = "PB_Convert";
            PB_Convert.Size = new Size(381, 23);
            PB_Convert.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 70);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 5;
            label2.Text = "Restore Progress";
            // 
            // PB_Restore
            // 
            PB_Restore.Location = new Point(29, 88);
            PB_Restore.Name = "PB_Restore";
            PB_Restore.Size = new Size(381, 23);
            PB_Restore.TabIndex = 4;
            // 
            // NVMRestore
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 150);
            ControlBox = false;
            Controls.Add(label2);
            Controls.Add(PB_Restore);
            Controls.Add(label1);
            Controls.Add(PB_Convert);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "NVMRestore";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NVM Restore";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ProgressBar PB_Convert;
        private Label label2;
        private ProgressBar PB_Restore;
    }
}
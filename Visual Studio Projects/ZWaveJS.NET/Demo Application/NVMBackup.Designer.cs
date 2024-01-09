namespace Demo_Application
{
    partial class NVMBackup
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
            PB_Progress = new ProgressBar();
            label1 = new Label();
            SuspendLayout();
            // 
            // PB_Progress
            // 
            PB_Progress.Location = new Point(23, 56);
            PB_Progress.Name = "PB_Progress";
            PB_Progress.Size = new Size(381, 23);
            PB_Progress.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 38);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 1;
            label1.Text = "Backing up NVM";
            // 
            // NVMBackup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 136);
            ControlBox = false;
            Controls.Add(label1);
            Controls.Add(PB_Progress);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "NVMBackup";
            StartPosition = FormStartPosition.CenterParent;
            Text = "NVM Backup";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar PB_Progress;
        private Label label1;
    }
}
namespace Demo_Application
{
    partial class DeviceEvents
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
            TXT_Log = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // TXT_Log
            // 
            TXT_Log.BackColor = Color.Black;
            TXT_Log.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TXT_Log.ForeColor = Color.Lime;
            TXT_Log.Location = new Point(12, 43);
            TXT_Log.Multiline = true;
            TXT_Log.Name = "TXT_Log";
            TXT_Log.Size = new Size(592, 439);
            TXT_Log.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(244, 19);
            label1.TabIndex = 5;
            label1.Text = "Monitoring Node Messages (Node #0)";
            // 
            // DeviceEvents
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(628, 515);
            Controls.Add(label1);
            Controls.Add(TXT_Log);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "DeviceEvents";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Device Events";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TXT_Log;
        private Label label1;
    }
}
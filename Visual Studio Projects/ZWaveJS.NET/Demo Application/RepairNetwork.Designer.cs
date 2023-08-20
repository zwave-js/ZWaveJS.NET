namespace Demo_Application
{
    partial class RepairNetwork
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
            button1 = new Button();
            PB_Wait = new ProgressBar();
            LBL_Title = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(260, 122);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Stop";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PB_Wait
            // 
            PB_Wait.Location = new Point(45, 78);
            PB_Wait.MarqueeAnimationSpeed = 50;
            PB_Wait.Name = "PB_Wait";
            PB_Wait.Size = new Size(290, 23);
            PB_Wait.Style = ProgressBarStyle.Marquee;
            PB_Wait.TabIndex = 4;
            // 
            // LBL_Title
            // 
            LBL_Title.AutoSize = true;
            LBL_Title.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LBL_Title.Location = new Point(45, 41);
            LBL_Title.Name = "LBL_Title";
            LBL_Title.Size = new Size(128, 19);
            LBL_Title.TabIndex = 3;
            LBL_Title.Text = "Reparing Network...";
            // 
            // RepairNetwork
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(381, 171);
            ControlBox = false;
            Controls.Add(PB_Wait);
            Controls.Add(LBL_Title);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "RepairNetwork";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Repairing Network";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ProgressBar PB_Wait;
        private Label LBL_Title;
    }
}
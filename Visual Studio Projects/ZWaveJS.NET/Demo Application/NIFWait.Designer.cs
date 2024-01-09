namespace Demo_Application
{
    partial class NIFWait
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
            LBL_Title = new Label();
            PB_Wait = new ProgressBar();
            label1 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // LBL_Title
            // 
            LBL_Title.AutoSize = true;
            LBL_Title.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LBL_Title.Location = new Point(46, 46);
            LBL_Title.Name = "LBL_Title";
            LBL_Title.Size = new Size(290, 19);
            LBL_Title.TabIndex = 1;
            LBL_Title.Text = "Place your ZWave device into inclusion mode. ";
            // 
            // PB_Wait
            // 
            PB_Wait.Location = new Point(46, 88);
            PB_Wait.MarqueeAnimationSpeed = 50;
            PB_Wait.Name = "PB_Wait";
            PB_Wait.Size = new Size(290, 23);
            PB_Wait.Style = ProgressBarStyle.Marquee;
            PB_Wait.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(46, 23);
            label1.Name = "label1";
            label1.Size = new Size(110, 19);
            label1.TabIndex = 3;
            label1.Text = "Waiting for NIF...";
            // 
            // button1
            // 
            button1.Location = new Point(261, 127);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // NIFWait
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(381, 171);
            ControlBox = false;
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(PB_Wait);
            Controls.Add(LBL_Title);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "NIFWait";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NIF Wait";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LBL_Title;
        private ProgressBar PB_Wait;
        private Label label1;
        private Button button1;
    }
}
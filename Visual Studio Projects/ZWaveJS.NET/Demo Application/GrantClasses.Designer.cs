namespace Demo_Application
{
    partial class GrantClasses
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
            CB_S2_AccessControl = new CheckBox();
            CB_S2_Auth = new CheckBox();
            CB_S2_Unauth = new CheckBox();
            CB_S0 = new CheckBox();
            label1 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // CB_S2_AccessControl
            // 
            CB_S2_AccessControl.AutoSize = true;
            CB_S2_AccessControl.Enabled = false;
            CB_S2_AccessControl.Location = new Point(84, 90);
            CB_S2_AccessControl.Name = "CB_S2_AccessControl";
            CB_S2_AccessControl.Size = new Size(120, 19);
            CB_S2_AccessControl.TabIndex = 0;
            CB_S2_AccessControl.Text = "S2 Access Control";
            CB_S2_AccessControl.UseVisualStyleBackColor = true;
            // 
            // CB_S2_Auth
            // 
            CB_S2_Auth.AutoSize = true;
            CB_S2_Auth.Enabled = false;
            CB_S2_Auth.Location = new Point(84, 115);
            CB_S2_Auth.Name = "CB_S2_Auth";
            CB_S2_Auth.Size = new Size(116, 19);
            CB_S2_Auth.TabIndex = 1;
            CB_S2_Auth.Text = "S2 Authenticated";
            CB_S2_Auth.UseVisualStyleBackColor = true;
            // 
            // CB_S2_Unauth
            // 
            CB_S2_Unauth.AutoSize = true;
            CB_S2_Unauth.Enabled = false;
            CB_S2_Unauth.Location = new Point(84, 140);
            CB_S2_Unauth.Name = "CB_S2_Unauth";
            CB_S2_Unauth.Size = new Size(129, 19);
            CB_S2_Unauth.TabIndex = 2;
            CB_S2_Unauth.Text = "S2 Unauthenticated";
            CB_S2_Unauth.UseVisualStyleBackColor = true;
            // 
            // CB_S0
            // 
            CB_S0.AutoSize = true;
            CB_S0.Enabled = false;
            CB_S0.Location = new Point(84, 165);
            CB_S0.Name = "CB_S0";
            CB_S0.Size = new Size(78, 19);
            CB_S0.TabIndex = 3;
            CB_S0.Text = "S0 Legacy";
            CB_S0.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(39, 40);
            label1.Name = "label1";
            label1.Size = new Size(213, 19);
            label1.TabIndex = 4;
            label1.Text = "Choose Allowed Seccurity Classes";
            // 
            // button1
            // 
            button1.Location = new Point(192, 218);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // GrantClasses
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(293, 266);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(CB_S0);
            Controls.Add(CB_S2_Unauth);
            Controls.Add(CB_S2_Auth);
            Controls.Add(CB_S2_AccessControl);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "GrantClasses";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Grant Security Classes";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox CB_S2_AccessControl;
        private CheckBox CB_S2_Auth;
        private CheckBox CB_S2_Unauth;
        private CheckBox CB_S0;
        private Label label1;
        private Button button1;
    }
}
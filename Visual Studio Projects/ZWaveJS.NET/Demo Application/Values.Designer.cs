namespace Demo_Application
{
    partial class Values
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
            LST_Values = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // LST_Values
            // 
            LST_Values.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            LST_Values.FullRowSelect = true;
            LST_Values.Location = new Point(22, 80);
            LST_Values.Name = "LST_Values";
            LST_Values.Size = new Size(420, 351);
            LST_Values.TabIndex = 0;
            LST_Values.UseCompatibleStateImageBehavior = false;
            LST_Values.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Property/Key";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Value";
            columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Endpoint";
            columnHeader3.Width = 100;
            // 
            // button1
            // 
            button1.Location = new Point(205, 437);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 10;
            button1.Text = "Set Value";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(286, 437);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 11;
            button2.Text = "Poll Value";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(367, 437);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 12;
            button3.Text = "Refresh";
            button3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(22, 31);
            label2.Name = "label2";
            label2.Size = new Size(85, 19);
            label2.TabIndex = 13;
            label2.Text = "Node Values";
            // 
            // Values
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 472);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(LST_Values);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Values";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Node Values";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView LST_Values;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label2;
    }
}
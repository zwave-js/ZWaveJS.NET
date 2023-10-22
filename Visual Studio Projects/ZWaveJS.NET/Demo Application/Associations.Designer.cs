namespace Demo_Application
{
    partial class Associations
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
            label2 = new Label();
            COM_Endpoints = new ComboBox();
            label1 = new Label();
            label3 = new Label();
            COM_AssociationGroup = new ComboBox();
            LST_Values = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            button3 = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(22, 38);
            label2.Name = "label2";
            label2.Size = new Size(121, 19);
            label2.TabIndex = 14;
            label2.Text = "Node Associations";
            // 
            // COM_Endpoints
            // 
            COM_Endpoints.FormattingEnabled = true;
            COM_Endpoints.Location = new Point(22, 90);
            COM_Endpoints.Name = "COM_Endpoints";
            COM_Endpoints.Size = new Size(142, 23);
            COM_Endpoints.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 72);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 16;
            label1.Text = "Endpoint";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(170, 72);
            label3.Name = "label3";
            label3.Size = new Size(104, 15);
            label3.TabIndex = 18;
            label3.Text = "Association Group";
            // 
            // COM_AssociationGroup
            // 
            COM_AssociationGroup.FormattingEnabled = true;
            COM_AssociationGroup.Location = new Point(170, 90);
            COM_AssociationGroup.Name = "COM_AssociationGroup";
            COM_AssociationGroup.Size = new Size(142, 23);
            COM_AssociationGroup.TabIndex = 17;
            // 
            // LST_Values
            // 
            LST_Values.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            LST_Values.FullRowSelect = true;
            LST_Values.Location = new Point(22, 119);
            LST_Values.Name = "LST_Values";
            LST_Values.Size = new Size(444, 256);
            LST_Values.TabIndex = 19;
            LST_Values.UseCompatibleStateImageBehavior = false;
            LST_Values.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Property";
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
            // button3
            // 
            button3.Location = new Point(391, 381);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 20;
            button3.Text = "Add";
            button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(310, 381);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 21;
            button1.Text = "Remove";
            button1.UseVisualStyleBackColor = true;
            // 
            // Associations
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(492, 425);
            Controls.Add(button1);
            Controls.Add(button3);
            Controls.Add(LST_Values);
            Controls.Add(label3);
            Controls.Add(COM_AssociationGroup);
            Controls.Add(label1);
            Controls.Add(COM_Endpoints);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Associations";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Node Associations";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private ComboBox COM_Endpoints;
        private Label label1;
        private Label label3;
        private ComboBox COM_AssociationGroup;
        private ListView LST_Values;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Button button3;
        private Button button1;
    }
}
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
            LST_Associations = new ListView();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
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
            COM_AssociationGroup.SelectedIndexChanged += COM_AssociationGroup_SelectedIndexChanged;
            // 
            // LST_Associations
            // 
            LST_Associations.Columns.AddRange(new ColumnHeader[] { columnHeader3, columnHeader4 });
            LST_Associations.FullRowSelect = true;
            LST_Associations.Location = new Point(22, 119);
            LST_Associations.Name = "LST_Associations";
            LST_Associations.Size = new Size(444, 256);
            LST_Associations.TabIndex = 19;
            LST_Associations.UseCompatibleStateImageBehavior = false;
            LST_Associations.View = View.Details;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Target Node";
            columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Target Endpoint";
            columnHeader4.Width = 150;
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
            button1.Click += button1_Click;
            // 
            // Associations
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(492, 425);
            Controls.Add(button1);
            Controls.Add(button3);
            Controls.Add(LST_Associations);
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
        private ListView LST_Associations;
        private ColumnHeader columnHeader3;
        private Button button3;
        private Button button1;
        private ColumnHeader columnHeader4;
    }
}
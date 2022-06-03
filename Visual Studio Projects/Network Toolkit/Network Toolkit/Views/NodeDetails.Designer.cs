
namespace Network_Toolkit.Views
{
    partial class NodeDetails
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
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.LBL_NodeID = new System.Windows.Forms.Label();
            this.LBL_Label = new System.Windows.Forms.Label();
            this.LBL_Description = new System.Windows.Forms.Label();
            this.LST_Values = new System.Windows.Forms.ListView();
            this.Property = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Endpoint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.75F);
            this.label2.Location = new System.Drawing.Point(18, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 29);
            this.label2.TabIndex = 23;
            this.label2.Text = "Node Details";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(23, 125);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(61, 46);
            this.button5.TabIndex = 31;
            this.button5.Text = "Remove Failed";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(90, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 46);
            this.button1.TabIndex = 32;
            this.button1.Text = "Replace Failed";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(156, 125);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 46);
            this.button2.TabIndex = 33;
            this.button2.Text = "Update FW";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(221, 125);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 46);
            this.button3.TabIndex = 34;
            this.button3.Text = "Asso MGMT";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(290, 125);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 46);
            this.button4.TabIndex = 35;
            this.button4.Text = "Health Check";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // LBL_NodeID
            // 
            this.LBL_NodeID.AutoSize = true;
            this.LBL_NodeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.75F);
            this.LBL_NodeID.Location = new System.Drawing.Point(168, 14);
            this.LBL_NodeID.Name = "LBL_NodeID";
            this.LBL_NodeID.Size = new System.Drawing.Size(51, 29);
            this.LBL_NodeID.TabIndex = 36;
            this.LBL_NodeID.Text = ": #1";
            // 
            // LBL_Label
            // 
            this.LBL_Label.AutoSize = true;
            this.LBL_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.LBL_Label.Location = new System.Drawing.Point(19, 54);
            this.LBL_Label.Name = "LBL_Label";
            this.LBL_Label.Size = new System.Drawing.Size(50, 20);
            this.LBL_Label.TabIndex = 37;
            this.LBL_Label.Text = "Label";
            // 
            // LBL_Description
            // 
            this.LBL_Description.AutoSize = true;
            this.LBL_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.LBL_Description.Location = new System.Drawing.Point(20, 74);
            this.LBL_Description.Name = "LBL_Description";
            this.LBL_Description.Size = new System.Drawing.Size(83, 18);
            this.LBL_Description.TabIndex = 38;
            this.LBL_Description.Text = "Description";
            // 
            // LST_Values
            // 
            this.LST_Values.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Property,
            this.Endpoint,
            this.Value});
            this.LST_Values.FullRowSelect = true;
            this.LST_Values.HideSelection = false;
            this.LST_Values.Location = new System.Drawing.Point(23, 213);
            this.LST_Values.Name = "LST_Values";
            this.LST_Values.Size = new System.Drawing.Size(548, 256);
            this.LST_Values.TabIndex = 39;
            this.LST_Values.UseCompatibleStateImageBehavior = false;
            this.LST_Values.View = System.Windows.Forms.View.Details;
            // 
            // Property
            // 
            this.Property.Text = "Property";
            this.Property.Width = 241;
            // 
            // Endpoint
            // 
            this.Endpoint.Text = "Endpoint";
            this.Endpoint.Width = 75;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 93;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.label1.Location = new System.Drawing.Point(20, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 18);
            this.label1.TabIndex = 40;
            this.label1.Text = "Command Class Values";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(428, 125);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(63, 46);
            this.button6.TabIndex = 41;
            this.button6.Text = "Interview Node";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(497, 125);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(63, 46);
            this.button7.TabIndex = 42;
            this.button7.Text = "Event Monitor";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(115)))), ((int)(((byte)(185)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(359, 125);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(63, 46);
            this.button8.TabIndex = 43;
            this.button8.Text = "Heal Node";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // NodeDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LST_Values);
            this.Controls.Add(this.LBL_Description);
            this.Controls.Add(this.LBL_Label);
            this.Controls.Add(this.LBL_NodeID);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label2);
            this.Name = "NodeDetails";
            this.Size = new System.Drawing.Size(600, 499);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label LBL_NodeID;
        private System.Windows.Forms.Label LBL_Label;
        private System.Windows.Forms.Label LBL_Description;
        private System.Windows.Forms.ListView LST_Values;
        private System.Windows.Forms.ColumnHeader Property;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ColumnHeader Endpoint;
    }
}

namespace Hydromodel.GSSHA
{
    partial class GridGSSHA
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uxExport1 = new ExportValue();
            this.uxUpElev = new System.Windows.Forms.CheckBox();
            this.uxCreate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.uxResolution = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uxDEM = new System.Windows.Forms.ComboBox();
            this.uxExport2 = new ExportValue();
            this.uxName = new System.Windows.Forms.TextBox();
            this.uxField = new System.Windows.Forms.Label();
            this.uxFields = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.uxPolygon = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uxTable = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.uxTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uxExport1);
            this.splitContainer1.Panel1.Controls.Add(this.uxUpElev);
            this.splitContainer1.Panel1.Controls.Add(this.uxCreate);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.uxResolution);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.uxDEM);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uxTable);
            this.splitContainer1.Size = new System.Drawing.Size(715, 245);
            this.splitContainer1.SplitterDistance = 123;
            this.splitContainer1.TabIndex = 0;
            // 
            // uxExport1
            // 
            this.uxExport1.Location = new System.Drawing.Point(14, 69);
            this.uxExport1.Name = "uxExport1";
            this.uxExport1.Size = new System.Drawing.Size(537, 45);
            this.uxExport1.TabIndex = 6;
            // 
            // uxUpElev
            // 
            this.uxUpElev.AutoSize = true;
            this.uxUpElev.Location = new System.Drawing.Point(27, 53);
            this.uxUpElev.Name = "uxUpElev";
            this.uxUpElev.Size = new System.Drawing.Size(107, 17);
            this.uxUpElev.TabIndex = 5;
            this.uxUpElev.Text = "Update elevation";
            this.uxUpElev.UseVisualStyleBackColor = true;
            // 
            // uxCreate
            // 
            this.uxCreate.Location = new System.Drawing.Point(569, 21);
            this.uxCreate.Name = "uxCreate";
            this.uxCreate.Size = new System.Drawing.Size(120, 23);
            this.uxCreate.TabIndex = 4;
            this.uxCreate.Text = "Create  new domain";
            this.uxCreate.UseVisualStyleBackColor = true;
            this.uxCreate.Click += new System.EventHandler(this.uxCreate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.No;
            this.label2.Location = new System.Drawing.Point(295, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "New resolution";
            // 
            // uxResolution
            // 
            this.uxResolution.Location = new System.Drawing.Point(386, 22);
            this.uxResolution.Name = "uxResolution";
            this.uxResolution.Size = new System.Drawing.Size(145, 20);
            this.uxResolution.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Reference raster";
            // 
            // uxDEM
            // 
            this.uxDEM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uxDEM.FormattingEnabled = true;
            this.uxDEM.Location = new System.Drawing.Point(137, 22);
            this.uxDEM.Name = "uxDEM";
            this.uxDEM.Size = new System.Drawing.Size(148, 21);
            this.uxDEM.TabIndex = 0;
            this.uxDEM.SelectedIndexChanged += new System.EventHandler(this.uxDEM_SelectedIndexChanged);
            // 
            // uxExport2
            // 
            this.uxExport2.Location = new System.Drawing.Point(13, 69);
            this.uxExport2.Name = "uxExport2";
            this.uxExport2.Size = new System.Drawing.Size(537, 45);
            this.uxExport2.TabIndex = 7;
            // 
            // uxName
            // 
            this.uxName.Location = new System.Drawing.Point(386, 48);
            this.uxName.Name = "uxName";
            this.uxName.Size = new System.Drawing.Size(145, 20);
            this.uxName.TabIndex = 5;
            // 
            // uxField
            // 
            this.uxField.AutoSize = true;
            this.uxField.Location = new System.Drawing.Point(24, 52);
            this.uxField.Name = "uxField";
            this.uxField.Size = new System.Drawing.Size(29, 13);
            this.uxField.TabIndex = 13;
            this.uxField.Text = "Field";
            // 
            // uxFields
            // 
            this.uxFields.FormattingEnabled = true;
            this.uxFields.Items.AddRange(new object[] {
            "Mask",
            "Soil",
            "Land-Soil",
            "Infiltration"});
            this.uxFields.Location = new System.Drawing.Point(138, 48);
            this.uxFields.Name = "uxFields";
            this.uxFields.Size = new System.Drawing.Size(148, 21);
            this.uxFields.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.No;
            this.label5.Location = new System.Drawing.Point(304, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Name";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(569, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // uxPolygon
            // 
            this.uxPolygon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uxPolygon.FormattingEnabled = true;
            this.uxPolygon.Location = new System.Drawing.Point(138, 18);
            this.uxPolygon.Name = "uxPolygon";
            this.uxPolygon.Size = new System.Drawing.Size(148, 21);
            this.uxPolygon.TabIndex = 8;
            this.uxPolygon.SelectedIndexChanged += new System.EventHandler(this.uxPolygon_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Index map";
            // 
            // uxTable
            // 
            this.uxTable.Controls.Add(this.button1);
            this.uxTable.Controls.Add(this.uxExport2);
            this.uxTable.Controls.Add(this.label4);
            this.uxTable.Controls.Add(this.uxName);
            this.uxTable.Controls.Add(this.uxPolygon);
            this.uxTable.Controls.Add(this.uxField);
            this.uxTable.Controls.Add(this.label5);
            this.uxTable.Controls.Add(this.uxFields);
            this.uxTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxTable.Enabled = false;
            this.uxTable.Location = new System.Drawing.Point(0, 0);
            this.uxTable.Name = "uxTable";
            this.uxTable.Size = new System.Drawing.Size(715, 118);
            this.uxTable.TabIndex = 14;
            this.uxTable.TabStop = false;
            // 
            // GridGSSHA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 245);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GridGSSHA";
            this.Text = "Configure GSSHA";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.uxTable.ResumeLayout(false);
            this.uxTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button uxCreate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uxResolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox uxDEM;
        private System.Windows.Forms.CheckBox uxUpElev;
        private System.Windows.Forms.TextBox uxName;
        private System.Windows.Forms.Label uxField;
        private System.Windows.Forms.ComboBox uxFields;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox uxPolygon;
        private System.Windows.Forms.Label label4;
        private ExportValue uxExport1;
        private ExportValue uxExport2;
        private System.Windows.Forms.GroupBox uxTable;
    }
}
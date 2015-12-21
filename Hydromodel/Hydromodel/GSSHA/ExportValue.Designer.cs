namespace Hydromodel.GSSHA
{
    partial class ExportValue
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
            this.uxSearch = new System.Windows.Forms.Button();
            this.uxNameFile = new System.Windows.Forms.TextBox();
            this.uxExport = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // uxSearch
            // 
            this.uxSearch.Location = new System.Drawing.Point(490, 11);
            this.uxSearch.Name = "uxSearch";
            this.uxSearch.Size = new System.Drawing.Size(27, 23);
            this.uxSearch.TabIndex = 17;
            this.uxSearch.Text = "...";
            this.uxSearch.UseVisualStyleBackColor = true;
            this.uxSearch.Visible = false;
            this.uxSearch.Click += new System.EventHandler(this.uxSearch_Click);
            // 
            // uxNameFile
            // 
            this.uxNameFile.Location = new System.Drawing.Point(123, 12);
            this.uxNameFile.Name = "uxNameFile";
            this.uxNameFile.Size = new System.Drawing.Size(361, 20);
            this.uxNameFile.TabIndex = 16;
            this.uxNameFile.Visible = false;
            // 
            // uxExport
            // 
            this.uxExport.AutoSize = true;
            this.uxExport.Location = new System.Drawing.Point(13, 14);
            this.uxExport.Name = "uxExport";
            this.uxExport.Size = new System.Drawing.Size(98, 17);
            this.uxExport.TabIndex = 15;
            this.uxExport.Text = "Export to ASCII";
            this.uxExport.UseVisualStyleBackColor = true;
            this.uxExport.CheckedChanged += new System.EventHandler(this.uxExport_CheckedChanged);
            // 
            // export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uxSearch);
            this.Controls.Add(this.uxNameFile);
            this.Controls.Add(this.uxExport);
            this.Name = "export";
            this.Size = new System.Drawing.Size(537, 45);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxSearch;
        public System.Windows.Forms.TextBox uxNameFile;
        public System.Windows.Forms.CheckBox uxExport;
    }
}

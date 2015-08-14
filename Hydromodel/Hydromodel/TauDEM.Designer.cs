namespace Hydromodel
{
    partial class TauDEM
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.uxListRasters = new System.Windows.Forms.ComboBox();
            this.uxCommand = new System.Windows.Forms.TextBox();
            this.uxView = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.uxList = new System.Windows.Forms.ComboBox();
            this.uxAppend = new System.Windows.Forms.CheckBox();
            this.uxExecute = new System.Windows.Forms.Button();
            this.uxCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uxAppend);
            this.splitContainer1.Panel2.Controls.Add(this.uxExecute);
            this.splitContainer1.Panel2.Controls.Add(this.uxCancel);
            this.splitContainer1.Size = new System.Drawing.Size(520, 223);
            this.splitContainer1.SplitterDistance = 184;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.uxCommand);
            this.splitContainer2.Panel2.Controls.Add(this.uxView);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.uxList);
            this.splitContainer2.Size = new System.Drawing.Size(520, 184);
            this.splitContainer2.SplitterDistance = 32;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.uxListRasters);
            this.splitContainer3.Size = new System.Drawing.Size(520, 32);
            this.splitContainer3.SplitterDistance = 366;
            this.splitContainer3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "DEM";
            // 
            // uxListRasters
            // 
            this.uxListRasters.FormattingEnabled = true;
            this.uxListRasters.Location = new System.Drawing.Point(131, 3);
            this.uxListRasters.Name = "uxListRasters";
            this.uxListRasters.Size = new System.Drawing.Size(215, 21);
            this.uxListRasters.TabIndex = 0;
            this.uxListRasters.SelectedIndexChanged += new System.EventHandler(this.uxListRasters_SelectedIndexChanged);
            // 
            // uxCommand
            // 
            this.uxCommand.Location = new System.Drawing.Point(15, 55);
            this.uxCommand.Multiline = true;
            this.uxCommand.Name = "uxCommand";
            this.uxCommand.Size = new System.Drawing.Size(493, 78);
            this.uxCommand.TabIndex = 3;
            // 
            // uxView
            // 
            this.uxView.Location = new System.Drawing.Point(352, 16);
            this.uxView.Name = "uxView";
            this.uxView.Size = new System.Drawing.Size(29, 23);
            this.uxView.TabIndex = 2;
            this.uxView.Text = "...";
            this.uxView.UseVisualStyleBackColor = true;
            this.uxView.Click += new System.EventHandler(this.uxView_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "TauDEM function";
            // 
            // uxList
            // 
            this.uxList.FormattingEnabled = true;
            this.uxList.Location = new System.Drawing.Point(131, 16);
            this.uxList.Name = "uxList";
            this.uxList.Size = new System.Drawing.Size(215, 21);
            this.uxList.TabIndex = 0;
            // 
            // uxAppend
            // 
            this.uxAppend.AutoSize = true;
            this.uxAppend.Location = new System.Drawing.Point(15, 9);
            this.uxAppend.Name = "uxAppend";
            this.uxAppend.Size = new System.Drawing.Size(78, 17);
            this.uxAppend.TabIndex = 5;
            this.uxAppend.Text = "Add results";
            this.uxAppend.UseVisualStyleBackColor = true;
            this.uxAppend.CheckedChanged += new System.EventHandler(this.uxAppend_CheckedChanged);
            // 
            // uxExecute
            // 
            this.uxExecute.Location = new System.Drawing.Point(312, 5);
            this.uxExecute.Name = "uxExecute";
            this.uxExecute.Size = new System.Drawing.Size(75, 23);
            this.uxExecute.TabIndex = 4;
            this.uxExecute.Text = "Execute";
            this.uxExecute.UseVisualStyleBackColor = true;
            this.uxExecute.Click += new System.EventHandler(this.uxExecute_Click);
            // 
            // uxCancel
            // 
            this.uxCancel.Location = new System.Drawing.Point(393, 5);
            this.uxCancel.Name = "uxCancel";
            this.uxCancel.Size = new System.Drawing.Size(75, 23);
            this.uxCancel.TabIndex = 3;
            this.uxCancel.Text = "Close";
            this.uxCancel.UseVisualStyleBackColor = true;
            // 
            // TauDEM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 223);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TauDEM";
            this.Text = "TauDEM";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox uxListRasters;
        private System.Windows.Forms.Button uxView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox uxList;
        private System.Windows.Forms.TextBox uxCommand;
        private System.Windows.Forms.CheckBox uxAppend;
        private System.Windows.Forms.Button uxExecute;
        private System.Windows.Forms.Button uxCancel;
    }
}
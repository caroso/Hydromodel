using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hydromodel.GSSHA
{
    public partial class ExportValue : UserControl
    {
        public ExportValue()
        {
            InitializeComponent();
        }

        private void uxExport_CheckedChanged(object sender, EventArgs e)
        {
            if (uxExport.CheckState == CheckState.Checked)
            {
                uxNameFile.Visible = true;
                uxSearch.Visible = true;

            }else
            {
                uxNameFile.Visible = false;
                uxSearch.Visible = false;
            }

        }

        private void uxSearch_Click(object sender, EventArgs e)
        {
             SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "text (*.txt)|*.txt";
            dialog.Title = "Save file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                uxNameFile.Text = dialog.FileName;
            }
        }
    }
}

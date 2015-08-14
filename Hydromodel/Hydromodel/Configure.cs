using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.IO;
namespace Hydromodel
{
    public partial class Configure : Form
    {
        public Configure()
        {
            InitializeComponent();
            if (File.Exists(Path.GetTempPath()+ "cfgHydro.txt"))
            {
                string[] f = File.ReadAllLines(Path.GetTempPath() + "cfgHydro.txt");
                if (f.Length==0) return;
                if (f.Length == 1)
                    uxFoderTauDEM.Text = f[0];
                if (f.Length == 2)
                {
                    uxFoderTauDEM.Text = f[0];
                    uxCREST.Text = f[1];
                }
            }
        }
        public static string GetTauDEM()
        {
            if (File.Exists(Path.GetTempPath() + "cfgHydro.txt"))
            {
                string[] f = File.ReadAllLines(Path.GetTempPath() + "cfgHydro.txt");
                if (f.Length == 0) return "";
                if (f.Length == 2)
                    return f[0];
            }
            return "";
        }

       public static string GetCREST()
        {
            if (File.Exists(Path.GetTempPath() + "cfgHydro.txt"))
            {
                string[] f = File.ReadAllLines(Path.GetTempPath() + "cfgHydro.txt");
                if (f.Length == 0) return "";
                if (f.Length == 2)
                    return f[1];
            }
            return "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            folderBrowserDialog1.Description =
           "Select folder of TauDEM" ;
            folderBrowserDialog1.ShowNewFolderButton = false;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                uxFoderTauDEM.Text = folderBrowserDialog1.SelectedPath + "\\";

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            folderBrowserDialog1.Description =
           "Select folder of CREST";
            folderBrowserDialog1.ShowNewFolderButton = false;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                uxCREST.Text = folderBrowserDialog1.SelectedPath + "\\";
            }
        }

        private void uxClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void uxAccept_Click(object sender, EventArgs e)
        {
            File.WriteAllLines(Path.GetTempPath() + "cfgHydro.txt", new string[2] { uxFoderTauDEM.Text, uxCREST.Text });
            Close();
        }
    }
}

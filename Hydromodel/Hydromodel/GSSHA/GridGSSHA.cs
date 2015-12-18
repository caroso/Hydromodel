using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hydromodel;
using DotSpatial.Controls;
namespace Hydromodel.GSSHA
{
    public partial class GridGSSHA : Form
    {
        public IMap map;
        public GridGSSHA(IMap map)
        {
            this.map = map;
            InitializeComponent();
            Dictionary<string, string> rasters = Utils.GetList(map);
            foreach (string item in rasters.Keys)
            {
                uxDEM.Items.Add(item);
         
            }
        }

        private void uxCreate_Click(object sender, EventArgs e)
        {
            double c = Convert.ToDouble(uxResolution.Text);

   


        }
    }
}

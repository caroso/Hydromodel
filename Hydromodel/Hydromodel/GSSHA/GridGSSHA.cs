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
using DotSpatial.Data;
namespace Hydromodel.GSSHA
{
    public partial class GridGSSHA : Form
    {
        Dictionary<string, IMapRasterLayer> rasters;
        Dictionary<string, IMapPolygonLayer> polygon;
        public IMap map;
        public GridGSSHA(IMap map)
        {
            this.map = map;
            InitializeComponent();

            UpdateData(map);
            this.map.LayerAdded +=map_LayerAdded;
        }

        private void map_LayerAdded(object sender, DotSpatial.Symbology.LayerEventArgs e)
        {
            UpdateData(sender as Map);
        }

        private void UpdateData(IMap map)
        {
            uxDEM.Items.Clear();
            uxPolygon.Items.Clear();
            rasters = Utils.GetListRaster(map);
            foreach (string item in rasters.Keys)
            {
                uxDEM.Items.Add(item);

            }

            foreach (var item in Utils.GetListPoly(map))
            {
                uxPolygon.Items.Add(item);
            }
        }

        private void uxCreate_Click(object sender, EventArgs e)
        {
        

            double c = Convert.ToDouble(uxResolution.Text);
            IMapRasterLayer r = rasters[uxDEM.Text];

            IRaster ras = r.DataSet;
            double width = ras.Bounds.Width;
            double height = ras.Bounds.Height;

            int nc = Convert.ToInt32(Math.Floor( width / c));
            int nr = Convert.ToInt32(Math.Floor(height / c));
            AreaInterest area = new AreaInterest();


            //AreaInterest area1 = new AreaInterest();
            //configArea(c, ras, nc, nr, area1);
            //VectorGrid ex = new Grid(area1);
            //ex.DrawExtent(map as Map,"Original Extent");

            if ((nc*c)==ras.Bounds.NumColumns && (nr*c)== ras.Bounds.NumRows)
            {
                configArea(c, ras, nc, nr, area);
                
            }else
            {
                nc++;
                nr++;
                area.CellSizeX = c;
                area.CellSizeY = c;
                area.NumColumns=nc;
                area.NumRows=nr;
                area.CellSizeZ=0;
                area.NumLayers=1;
                double cdif= (nc*c -width)/2;
                double rdif= (nr*c -height)/2;
                area.MinX = ras.Bounds.Extent.MinX-cdif;
                area.MinY = ras.Bounds.Extent.MinY-rdif;
                area.MaxX = ras.Bounds.Extent.MaxX+cdif;
                area.MaxY = ras.Bounds.Extent.MaxY+cdif;
                area.TypeDomain = MadInterfaces.Type_Domain.Grid;
                area.SymetricDimension = true;

            }
            VectorGrid v = new Grid(area);
            v.CreateMap(map as Map, "L"+uxDEM.Text ,ras);
            
            if (uxExport1.uxExport.CheckState== CheckState.Checked)
                if(uxExport1.uxNameFile.Text!="")
                {
                    v.Export(uxExport1.uxNameFile.Text, "Field");
                }



            //v.DrawExtent(map as Map,"New Extent");
        
 
        }

        private static void configArea(double c, DotSpatial.Data.IRaster ras, int nc, int nr, AreaInterest area)
        {
            area.CellSizeX = c;
            area.CellSizeY = c;
            area.NumColumns = nc;
            area.NumRows = nr;
            area.CellSizeZ = 0;
            area.NumLayers = 1;
            area.MinX = ras.Bounds.Extent.MinX;
            area.MinY = ras.Bounds.Extent.MinY;
            area.MaxX = ras.Bounds.Extent.MaxX;
            area.MaxY = ras.Bounds.Extent.MaxY;
            area.TypeDomain = MadInterfaces.Type_Domain.Grid;
            area.SymetricDimension = true;
        }

        private void uxDEM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

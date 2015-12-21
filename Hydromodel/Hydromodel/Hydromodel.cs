using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Extensions;
using DotSpatial.Controls;
using System.IO;
using DotSpatial.Controls.Header;
using System.Windows.Forms;
using DotSpatial.Data;
using DotSpatial.Topology;
using Hydromodel.GSSHA;
namespace Hydromodel
{
    public class Hydromodel:Extension
    {
        public override void Activate()
        {
          //   string temp = Path.GetTempPath();
             AddIcons();
             base.Activate();

           
             Console.WriteLine("Activate Hydromodel");
        }
        const string kMad3 = "kHydroModel__";
        const string group = "HydroModel";
        bool val = true;
        private void AddIcons()
        {
            IHeaderControl head = App.HeaderControl;
            //Search ribbon tab
            var cr = new RootItem(kMad3, group);
            cr.SortOrder = 20;

            head.Add(cr);
            SimpleActionItem rbBlockIBox = new SimpleActionItem(kMad3,"Configure", configure_Click);
            rbBlockIBox.GroupCaption = group;
            SimpleActionItem rbBlockIBox1 = new SimpleActionItem(kMad3,"TauDEM", tauDEM_Click);
           rbBlockIBox1.GroupCaption = group;
            head.Add(rbBlockIBox);
            head.Add(rbBlockIBox1);
            App.HeaderControl.Add(new SimpleActionItem(HeaderControl.HomeRootItemKey, "Add Point", AddPoint_Click) { GroupCaption = "Add point Tool" });
            App.HeaderControl.Add(new SimpleActionItem(HeaderControl.HomeRootItemKey, "Cancel add Point", CancelPoint_Click) { GroupCaption = "Add point Tool" });

            SimpleActionItem rbBlockIBoxG = new SimpleActionItem(kMad3, "GSSHA", G_Click);
            rbBlockIBox.GroupCaption = group;
            head.Add(rbBlockIBoxG);
           // App.HeaderControl.Add(cr);

        }
        Map map1;
        private void CancelPoint_Click(object sender, EventArgs e)
        {
            map1 = App.Map as Map;
            map1.MouseClick -= map_MouseClick;
        }
        FeatureSet point;
        int cont = 0;
        private void AddPoint_Click(object sender, EventArgs e)
        {
             map1 = App.Map as Map;

          
            map1.MouseClick += map_MouseClick;
            FeatureSet f = new FeatureSet(DotSpatial.Topology.FeatureType.Point);
            f.Projection = App.Map.Projection;
            f.DataTable.Columns.Add("id", typeof(int));
            point = f;
            MapPointLayer m = new MapPointLayer(f);
            m.Name = "outlet";
            m.LegendText = "outlet";
            App.Map.Layers.Add(m);
            cont=0;

        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
             Map map = App.Map as Map;
              Coordinate c =map.PixelToProj(new System.Drawing.Point(e.X, e.Y));
              Point p = new Point(c);
              IFeature f = point.AddFeature(p);
              f.DataRow["id"] = cont;
              cont++;
              map.ResetBuffer();
             

        }

        private void tauDEM_Click(object sender, EventArgs e)
        {
            TauDEM t = new TauDEM(App.Map);

            t.Show();
        }

        private void G_Click(object sender, EventArgs e)
        {
            GridGSSHA g = new GridGSSHA(App.Map);
            g.Show();

        }
        private void configure_Click(object sender, EventArgs e)
        {
            Configure cfg = new Configure();
            cfg.Show();

        }
        public override void Deactivate()
        {
            App.HeaderControl.RemoveAll();
            base.Deactivate();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Topology;
using DotSpatial.Controls;
namespace Hydromodel
{




    public struct Stat
    {

        public double value;
        public long num;
        public double quad;
        private double max;
        private double min;
        private long posMin;
        private long posMax;

        public long PosMin
        {
            get
            {
                return this.posMin;
            }
        }

        public long PosMax
        {
            get
            {
                return this.posMax;
            }
        }


        public double Min
        {
            get
            {
                return this.min;
            }
        }


        public double Max
        {
            get
            {
                return this.max;
            }
        }

        public double Mean
        {

            get
            {
                if (num > 0)
                    return value / this.num;
                else
                    return 0;
            }
        }

        public double StdDevP
        {

            get
            {
                if (num > 0)
                    return Math.Sqrt((quad - (2 * this.value * this.Mean) + (this.num * this.Mean * this.Mean)) / this.num);
                else
                    return 0;
            }
        }

        public double StdDevS
        {

            get
            {
                if (num > 2)
                    return Math.Sqrt((quad - (2 * this.value * this.Mean) + (this.num * this.Mean * this.Mean)) / (this.num - 1));
                else
                    return 0;
            }
        }

        public Stat(bool v)
        {
            this.value = 0;
            this.quad = 0;
            this.min = double.MaxValue;
            this.max = double.MinValue;
            this.num = 0;
            this.posMax = 0;
            this.posMin = 0;
        }


        public Stat(double value)
        {
            this.value = value;
            this.quad = value * value;
            this.min = value;
            this.max = value;
            this.num = 1;
            this.posMax = 1;
            this.posMin = 1;


        }

        //Stat(double value, int v)
        //{
        //    this.value = value;
        //    this.quad = (value * value);
        //    this.num = v;
        //}

        Stat(Stat c, double value1, long v)
        {
            this.value = c.value + value1;
            this.quad = c.quad;
            this.quad += value1 * value1;
            this.num = v;
            //this.posMin =v;
            //this.posMax=v;


            if (c.Min > value1)
            {
                this.min = value1;
                this.posMin = v;
            }
            else
            {
                this.min = c.Min;
                this.posMin = c.posMin;
            }

            if (c.Max < value1)
            {
                this.max = value1;
                this.posMax = v;
            }
            else
            {
                this.max = c.Max;
                this.posMax = c.posMax;
            }

        }

        public static Stat operator +(Stat c1, Stat c2)
        {

            return new Stat(c1, c2.value, c1.num + 1);
        }

    }

    public class Utils
    {
        public  static Dictionary<string, string> GetList(IMap map)
        {
            Dictionary<string, string> n = new Dictionary<string, string>();
            foreach (IMapRasterLayer item in map.GetRasterLayers())
            {
                string name = item.DataSet.Filename;
                n.Add(item.LegendText, name);
            }
            return n;
        }

        public static Dictionary<string, IMapRasterLayer> GetListRaster(IMap map)
        {
            Dictionary<string, IMapRasterLayer> n = new Dictionary<string, IMapRasterLayer>();
            foreach (IMapRasterLayer item in map.GetRasterLayers())
            {
                string name = item.DataSet.Filename;
                n.Add(item.LegendText, item);
            }
            return n;
        }

        public static Dictionary<string, IMapPolygonLayer> GetListPoly(IMap map)
        {
            Dictionary<string, IMapPolygonLayer> n = new Dictionary<string, IMapPolygonLayer>();
            foreach (IMapPolygonLayer item in map.GetRasterLayers())
            {
                string name = item.DataSet.Filename;
                n.Add(item.LegendText, item);
            }
            return n;
        }



        public static string GetFolderFile(string path)
        {
            if (path == string.Empty) return "";
            string[] folderMod = path.Split('\\');
            string folder = "";
            for (int i = 0; i < folderMod.Length - 1; i++)
            {
                folder += folderMod[i] + "\\";
            }
            return folder;
        }

        public static string GetNameSourceFile(string path)
        {
            if (path == string.Empty) return "";
            string[] folderMod = path.Split('\\');
            string[] name = folderMod[folderMod.Length - 1].Split('.');
            if (name.Length == 2)
            { return name[0]; }
            else
            { return ""; }
        }







        public static double ApplyTransformation(double v, string trans)
        {


            if (trans.Contains('^'))
            {
                string[] part = trans.Split('^');
                if (part[0].ToLower() == "e")
                {
                    return Math.Exp(v);

                }
                else
                {
                    double value;
                    try
                    {
                        value = Convert.ToDouble(part[0]);
                        return Math.Pow(value, v);
                    }
                    catch
                    {

                    }

                    return Math.Exp(v);

                }
            }

            if (trans.ToLower().Contains("log"))
            {
                string basem = trans.ToLower().Replace("log", string.Empty);
                if (basem.ToLower() == "e")
                {
                    return Math.Log(v, Math.E);
                }
                else
                {
                    double valueBase;
                    try
                    {
                        valueBase = Convert.ToDouble(basem);
                        return Math.Log(v, valueBase);
                    }
                    catch
                    {

                    }

                    return Math.Log(v, Math.E);
                }

            }

            return v;




        }

 


        public static int CalculatePositionArray(int period, int step, int realization, int nRealizations, int[] steps)
        {
            int id = 0;
            for (int i = 0; i < period - 1; i++)
            {
                id += steps[i];
            }
            return (realization - 1) * steps.Sum() + (id + step - 1);
        }


        public static double DegToRad(double ang)
        {
            return Math.PI * ang / 180.0;

        }


        public static double GetAzimut(Coordinate origin, Coordinate target)
        {
            double dx = target.X - origin.X;
            double dy = target.Y - origin.Y;

            // if (dx == 0 && dy == 0) return 0;
            if (dx == 0 && dy > 0) return 0;
            if (dx == 0 && dy < 0) return Math.PI;
            if (dx > 0 && dy == 0) return Math.PI / 2;
            if (dx < 0 && dy == 0) return 3 * Math.PI / 2;
            if (dx > 0 && dy > 0) return Math.Atan(dx / dy);
            if (dx > 0 && dy < 0) return Math.PI + Math.Atan(dx / dy);
            if (dx < 0 && dy < 0) return (Math.PI) + Math.Atan(dx / dy);
            if (dx < 0 && dy > 0) return (2 * Math.PI) + Math.Atan(dx / dy);
            return 0;
            //  return Math.Atan(dx / dy);

        }

        public static double GetAzimut(double x, double y, double x1, double y1)
        {
            double dx = x1 - x;
            double dy = y1 - y;

            // if (dx == 0 && dy == 0) return 0;
            if (dx == 0 && dy > 0) return 0;
            if (dx == 0 && dy < 0) return Math.PI;
            if (dx > 0 && dy == 0) return Math.PI / 2;
            if (dx < 0 && dy == 0) return 3 * Math.PI / 2;
            if (dx > 0 && dy > 0) return Math.Atan(dx / dy);
            if (dx > 0 && dy < 0) return Math.PI + Math.Atan(dx / dy);
            if (dx < 0 && dy < 0) return (Math.PI) + Math.Atan(dx / dy);
            if (dx < 0 && dy > 0) return (2 * Math.PI) + Math.Atan(dx / dy);
            return 0;
            //  return Math.Atan(dx / dy);

        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MadInterfaces;
using DotSpatial.Controls;
using DotSpatial.Topology;
using DotSpatial.Data;
using System.Drawing;
using DotSpatial.Symbology;
using System.Threading.Tasks;
using DotSpatial.Projections;
using System.Globalization;
using System.Windows.Forms;
using System.IO;

namespace Hydromodel.GSSHA
{
    public static class Validator
    {
        static System.Globalization.CultureInfo myRI = new CultureInfo("en-US");

        public static DateTime GetDateTime(string date)
        {
            return DateTime.Parse(date, myRI);
        }


        public static double GetDouble(string value)
        {
            return Convert.ToDouble(value, myRI);
        }

        public static string GetString(double value)
        {
            return Convert.ToString(value, myRI);
        }

        public static string GetString(DateTime value)
        {
            return Convert.ToString(value, myRI);
        }


        public static bool ContainSpecialCharacters(string value)
        {
            var withoutSpecial = new string(value.Where(c => Char.IsLetterOrDigit(c)
                                                || !Char.IsWhiteSpace(c)).ToArray());

            if (value != withoutSpecial)
            {
                return true;
            }
            else
                return false;


        }


        private static string title = "Entry Error";



        public static string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public static void RemoveLayer(Map _mapArgs, string name)
        {

            //_mapArgs.FunctionMode = FunctionMode.None;
            foreach (IMapLayer lay in _mapArgs.GetLayers())
            {
                if (lay.LegendText == name && lay.LegendText != "Online Basemap")
                {
                    _mapArgs.Layers.Remove(lay);

                }
            }

        }

        public static IMapLayer AvailableLayer(Map _mapArgs, string name)
        {

            //_mapArgs.FunctionMode = FunctionMode.None;
            foreach (IMapLayer lay in _mapArgs.GetLayers())
            {
                if (lay.LegendText == name)
                {
                    return lay;

                }
            }
            return null;

        }

        public static AreaInterest ReprojectAreaInterestByPointOrigin(AreaInterest area, ProjectionInfo source, ProjectionInfo dest)
        {
            double xMin = area.MinX;
            double yMin = area.MinY;


            double[] xy = new double[] { xMin, yMin };


            Reproject.ReprojectPoints(xy, new double[] { 0 }, source, dest, 0, 1);

            AreaInterest area_output = new AreaInterest();

            area_output.MinX = xy[0];
            area_output.MinY = xy[1];
            area_output.NumColumns = area.NumColumns;
            area_output.NumRows = area.NumRows;
            area_output.CellSizeX = area.CellSizeX;
            area_output.CellSizeY = area.CellSizeY;
            area_output.Azimut = area.Azimut;
            return area_output;
        }

        public static AreaInterest ReprojectAreaInterest(AreaInterest area, ProjectionInfo source, ProjectionInfo dest)
        {


            if (area.MinX < -180 || area.MaxX > 180 || area.MinY < -90 || area.MaxX > 90)
            {

                if (source == KnownCoordinateSystems.Geographic.World.WGS1984)
                {

                    return area;

                }


            }
            else
            {

                // var i = 0;

            }

            double xMin = area.MinX;
            double yMin = area.MinY;
            double xMax = area.MaxX;
            double yMax = area.MaxY;

            double[] xy = new double[] { xMin, yMin, xMax, yMax };


            DotSpatial.Projections.Reproject.ReprojectPoints(xy, new double[] { 0, 0 }, source, dest, 0, 2);

            AreaInterest area_output = new AreaInterest();

            area_output.MinX = xy[0];
            area_output.MinY = xy[1];
            area_output.MaxX = xy[2];
            area_output.MaxY = xy[3];
            area_output.NumColumns = area.NumColumns;
            area_output.NumRows = area.NumRows;
            area_output.CellSizeX = area.CellSizeX;
            area_output.CellSizeY = area.CellSizeY;
            area_output.Azimut = area.Azimut;
            return area_output;
        }


        public static bool IsZero(TextBox t)
        {
            if (Validator.IsDouble(t))
                if (Convert.ToDouble(t.Text) <= 0)
                {
                    MessageBox.Show("The value of " + t.Tag + " should be greater than zero.");
                    t.Focus();
                    return true;
                }
            return false;
        }

        public static bool IsExist(string path, TextBox tex, string extension)
        {
            if (File.Exists(path + tex.Text + "." + extension))
            {
                MessageBox.Show("The file " + tex.Text + " exists.", Title);
                tex.Focus();
                return true;
            }

            return false;

        }
        public static bool IsPresent(TextBox textBox)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(textBox.Tag + " is a required field.", Title);
                textBox.Focus();
                return false;
            }
            return true;
        }
        public static string GetNameFile(string file)
        {
            char[] v = file.ToCharArray();
            for (int i = v.Length - 1; i > 0; i--)
            {
                if (v[i] == '/' || v[i] == '\\')
                {
                    return file.Substring(i + 1, file.Length - i - 5);
                }
            }
            return "default";
        }

        public static string GetNameFile(string file, int extSize)
        {
            char[] v = file.ToCharArray();
            for (int i = v.Length - 1; i > 0; i--)
            {
                if (v[i] == '/' || v[i] == '\\')
                {
                    return file.Substring(i + 1, file.Length - i - 2 - (extSize));
                }
            }
            return "default";
        }
        public static bool IsDecimal(TextBox textBox)
        {
            try
            {
                Convert.ToDecimal(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(textBox.Tag + " must be a decimal number.", Title);
                textBox.Focus();
                return false;
            }
        }


        //public static void  ReprojectPoints(RadioButton radio,double[] xy, double[] z, ProjectionInfo source, ProjectionInfo dest, int startIndex, int numPoints)
        //{
        //    if (radio.Checked)
        //    {
        //        Reproject.ReprojectPoints(xy, z, source, dest, startIndex, numPoints);
        //    }
        // }

        public static bool IsDouble(TextBox textBox)
        {
            try
            {
                if (textBox.Text == "")
                {
                    if ((string)textBox.Tag == "Left" || textBox.Text == "Bottom")
                    {
                        MessageBox.Show("Provide a location on the map before entering the grid dimensions.");
                    }
                    else
                        MessageBox.Show(textBox.Tag + " must be a double number.", Title);

                    return false;
                }
                if (textBox.Text == "-" ||
                    textBox.Text == "+")
                { return true; }
                Convert.ToDouble(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(textBox.Tag + " must be a double number.", Title);
                textBox.Focus();
                return false;
            }
        }

        public static bool IsInt32(TextBox textBox)
        {
            try
            {
                Convert.ToInt32(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(textBox.Tag + " must be an integer.", Title);
                textBox.Focus();
                return false;
            }
        }

        public static bool IsWithinRange(TextBox textBox, decimal min, decimal max)
        {
            decimal number = Convert.ToDecimal(textBox.Text);
            if (number < min || number > max)
            {
                MessageBox.Show(textBox.Tag + " must be between " + min
                    + " and " + max + ".", Title);
                textBox.Focus();
                return false;
            }
            return true;
        }

        public static bool EqualAreaInterest(AreaInterest a1, AreaInterest a2)
        {
            if (a1 == null || a2 == null)
                return false;

            if (a1.CellSizeX == a2.CellSizeX &&
                a1.NumColumns == a2.NumColumns &&
                a1.NumRows == a2.NumRows &&
                    a1.CellSizeY == a2.CellSizeY)
                return true;
            else
                return false
                    ;
        }
        public static bool EqualAreaInterestColRow(AreaInterest a1, AreaInterest a2)
        {
            if (a1 == null || a2 == null)
                return false;

            if (
                a1.NumColumns == a2.NumColumns &&
                a1.NumRows == a2.NumRows)
                return true;
            else
                return false
                    ;
        }

    }

    public class Cell : ICell
    {
        private int[] id;
        private double width;
        private double height;
        private double depth;
        private double valueC;
        public Cell()
        {
            id = new int[3];
        }
        public int[] Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public double Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;

            }
        }
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
        public double Value
        {
            get
            {
                return valueC;
            }
            set
            {
                valueC = value;
            }
        }
    }

    public static class AreaInterestEx
    {
        public static Coordinate ProjectPointAzimut(this IAreaInterest area, double x, double y)
        {
            return area.ProjectPointAzimut(new Coordinate(x, y));
        }

        private static double GetAzimut(this IAreaInterest area, Coordinate target)
        {
            return Utils.GetAzimut(new Coordinate(area.MinX, area.MinY), target);
        }

        public static Coordinate ProjectPointAzimut(this IAreaInterest area, Coordinate c)
        {
            if (area.Azimut == 0) return c;
            double angle = Utils.DegToRad(area.Azimut);
            double azi = area.GetAzimut(c);
            double v = (angle + azi) * 180.0 / Math.PI;
            double dist = Math.Sqrt((area.MinX - c.X) * (area.MinX - c.X) + (area.MinY - c.Y) * (area.MinY - c.Y));
            return new Coordinate((Math.Sin(angle + azi) * dist) + area.MinX, (Math.Cos(angle + azi) * dist) + area.MinY);
        }

        public static Coordinate UnProjectPointAzimut(this IAreaInterest area, double x, double y)
        {
            return area.UnProjectPointAzimut(new Coordinate(x, y));
        }

        public static Coordinate UnProjectPointAzimut(this IAreaInterest area, Coordinate c)
        {
            if (area.Azimut == 0) return c;
            double azi = Utils.DegToRad(area.Azimut);
            double angleP = area.GetAzimut(c);
            double angle = angleP - azi;
            double dist = Math.Sqrt(((area.MinX - c.X) * (area.MinX - c.X)) + ((area.MinY - c.Y) * (area.MinY - c.Y)));
            return new Coordinate((Math.Sin(angle) * dist) + area.MinX, (Math.Cos(angle) * dist) + area.MinY);
        }






        public static void FillSymmetricDimensions(this IAreaInterest area)
        {
            if (area.CellSizeY == -1 || area.CellSizeX == -1)
                return;

            double[] cols, rows;
            cols = new double[area.NumColumns];
            rows = new double[area.NumRows];
            for (int i = 0; i < area.NumColumns; i++)
            {
                cols[i] = area.CellSizeX;
            }
            for (int i = 0; i < area.NumRows; i++)
            {
                rows[i] = area.CellSizeY;
            }
            double[] lays = new double[area.NumLayers];
            for (int i = 0; i < area.NumLayers; i++)
            {
                lays[i] = area.CellSizeZ;
            }

            area.ColumnsSize = cols;
            area.RowsSize = rows;
            area.LayerSize = lays;
        }

        public static Extent GetExtent(this IAreaInterest area, double offset)
        {

            double[] xValues = new double[4]
                               {
                                 area.ProjectPointAzimut(new Coordinate(area.MinX,area.MinY)).X,
                                 area.ProjectPointAzimut(new Coordinate(area.MinX,area.MaxY)).X,
                                area.ProjectPointAzimut(new Coordinate(area.MaxX,area.MaxY)).X,
                                 area.ProjectPointAzimut(new Coordinate(area.MaxX,area.MinY)).X,
                               };
            double[] yValues = new double[4]
                               {
                                 area.ProjectPointAzimut(new Coordinate(area.MinX,area.MinY)).Y,
                                 area.ProjectPointAzimut(new Coordinate(area.MinX,area.MaxY)).Y,
                                 area.ProjectPointAzimut(new Coordinate(area.MaxX,area.MaxY)).Y,
                                 area.ProjectPointAzimut(new Coordinate(area.MaxX,area.MinY)).Y,
                               };


            double percX = (xValues.Max() - xValues.Min()) * offset;
            double percY = (yValues.Max() - yValues.Min()) * offset;

            return new Extent(xValues.Min() - percX, yValues.Min() - percY, xValues.Max() + percX, yValues.Max() + percX);

        }

        public static bool IsSymmetrical(this IAreaInterest area)
        {

            if (area.ColumnsSize == null || area.RowsSize == null)
            {
                area.FillSymmetricDimensions();
                area.SymetricDimension = true;
                return true;
            }
            if (area.ColumnsSize.Count() == 0 || area.RowsSize.Count() == 0)
            {
                area.SymetricDimension = false;
                return false;
            }


            if (area.ColumnsSize.Length != area.NumColumns ||
                area.RowsSize.Length != area.NumRows)
            {
                area.ColumnsSize = new double[area.NumColumns];
                area.RowsSize = new double[area.NumRows];

            }

            double valc = area.ColumnsSize[0];

            for (int i = 0; i < area.NumColumns; i++)
            {
                if (valc != area.ColumnsSize[i])
                {
                    area.CellSizeX = -1;
                    area.CellSizeY = -1;
                    area.SymetricDimension = false;
                    return false;
                }

            }
            valc = area.RowsSize[0];
            for (int i = 0; i < area.NumRows; i++)
            {
                if (valc != area.RowsSize[i])
                {
                    area.CellSizeX = -1;
                    area.CellSizeY = -1;
                    area.SymetricDimension = false;
                    return false;
                }
            }

            area.CellSizeX = area.ColumnsSize[0];
            area.CellSizeY = area.RowsSize[0];
            area.SymetricDimension = true;
            return true;


        }

        public static int[] GetCell(this IAreaInterest area, double xo, double yo)
        {
            double x = area.UnProjectPointAzimut(xo, yo).X;
            double y = area.UnProjectPointAzimut(xo, yo).Y;

            if ((x < area.MinX) || (x > area.MinX + area.ColumnsSize.Sum()) || (y < area.MinY) || (y > area.MinY + area.RowsSize.Sum()))
            {
                return new int[2] { -1, -1 };
            }
            else
            {
                if (area.SymetricDimension)
                {
                    int[] val = new int[2] { (int)(Math.Floor(((x - area.MinX) / area.CellSizeX)) + 1), (int)(Math.Floor(((area.MaxY - y) / area.CellSizeY)) + 1) };
                    return area.GetCellOrigenXY(val[0], val[1]);
                }
                else
                {
                    int[] val = new int[2] { -1, -1 };

                    double sumx = area.MinX;
                    for (int i = 0; i < area.ColumnsSize.Length; i++)
                    {
                        if (x > (sumx) && x <= (sumx + area.ColumnsSize[i]))
                        {
                            val[0] = i;
                            break;
                        }
                        sumx += area.ColumnsSize[i];
                    }

                    double sumy = area.MinY;
                    for (int j = 0; j < area.RowsSize.Length; j++)
                    {
                        if (y > (sumy) && x <= (sumy + area.RowsSize[j]))
                        {
                            val[1] = j;
                            break;
                        }
                        sumy += area.RowsSize[j];
                    }

                    return val;
                }
            }
        }

        public static int[] GetCellOrigenXY(this IAreaInterest area, int col, int row)
        {
            return new int[2] { col, area.NumRows - row + 1 };
        }

        public static double[] GetCoordenatesByCell1D(this IAreaInterest area, int layer)
        {
            if ((layer < 1) || (layer > area.NumLayers))
            {
                return new double[2] { 0d, 0d };
            }
            else
            {
                // return new double[2] { area.minX + (area.cellSize * col) - (area.cellSize / 2), area.maxY - (area.cellSize * row) + (area.cellSize / 2) };
                area.Azimut = 180;
                if (area.SymetricDimension)
                {
                    Coordinate pointProj = area.ProjectPointAzimut(
                          new Coordinate(area.MinX + (area.CellSizeX * 1) - (area.CellSizeX / 2),
                                         area.MinY + (area.CellSizeZ * layer) - (area.CellSizeZ / 2)));
                    area.Azimut = 0;

                    return new double[2] { pointProj.X, pointProj.Y };
                }
                else
                {
                    double minx;
                    double miny;
                    double sumx = area.MinX;

                    for (int ii = 0; ii < 1; ii++)
                        sumx += area.ColumnsSize[ii];

                    minx = sumx + area.ColumnsSize[0] / 2;

                    double sumy = area.MinY;
                    for (int jj = 0; jj < layer; jj++)
                        sumy += area.RowsSize[jj];

                    miny = sumy + area.RowsSize[layer] / 2;

                    area.Azimut = 180;
                    Coordinate pointProj = area.ProjectPointAzimut(new Coordinate(minx, miny));
                    area.Azimut = 0;
                    return new double[2] { pointProj.X, pointProj.Y };
                }





            }



        }


        public static double[] GetCoordenatesByCell(this IAreaInterest area, int col, int row, int layer)
        {
            if ((col < 1) || (col > area.NumColumns) || (row < 1) || (row > area.NumRows) || (layer < 1) || (layer > area.LayerSize.Count()))
            {
                return new double[3] { 0d, 0d, 0d };
            }
            else
            {
                // return new double[2] { area.minX + (area.cellSize * col) - (area.cellSize / 2), area.maxY - (area.cellSize * row) + (area.cellSize / 2) };

                if (area.SymetricDimension)
                {
                    Coordinate pointProj = area.ProjectPointAzimut(
                          new Coordinate(area.MinX + (area.CellSizeX * col) - (area.CellSizeX / 2),
                                         area.MinY + (area.CellSizeY * row) - (area.CellSizeY / 2)));
                    return new double[3] { pointProj.X, pointProj.Y, layer * (-1) * area.CellSizeZ + (area.CellSizeZ / 2) };
                }
                else
                {
                    double minx;
                    double miny;
                    double sumx = area.MinX;

                    for (int ii = 0; ii < col; ii++)
                        sumx += area.ColumnsSize[ii];

                    minx = sumx + area.ColumnsSize[col] / 2;

                    double sumy = area.MinY;
                    for (int jj = 0; jj < row; jj++)
                        sumy += area.RowsSize[jj];

                    miny = sumy + area.RowsSize[row] / 2;


                    Coordinate pointProj = area.ProjectPointAzimut(new Coordinate(minx, miny));
                    return new double[3] { pointProj.X, pointProj.Y, layer * (-1) * area.CellSizeZ + (area.CellSizeZ / 2) };
                }





            }



        }



        public static double[] GetCoordenatesByCell(this IAreaInterest area, int col, int row)
        {
            if ((col < 1) || (col > area.NumColumns) || (row < 1) || (row > area.NumRows))
            {
                return new double[2] { 0d, 0d };
            }
            else
            {
                // return new double[2] { area.minX + (area.cellSize * col) - (area.cellSize / 2), area.maxY - (area.cellSize * row) + (area.cellSize / 2) };

                if (area.SymetricDimension)
                {
                    Coordinate pointProj = area.ProjectPointAzimut(
                          new Coordinate(area.MinX + (area.CellSizeX * col) - (area.CellSizeX / 2),
                                         area.MinY + (area.CellSizeY * row) - (area.CellSizeY / 2)));
                    return new double[2] { pointProj.X, pointProj.Y };
                }
                else
                {
                    double minx;
                    double miny;
                    double sumx = area.MinX;

                    for (int ii = 0; ii < col; ii++)
                        sumx += area.ColumnsSize[ii];

                    minx = sumx + area.ColumnsSize[col] / 2;

                    double sumy = area.MinY;
                    for (int jj = 0; jj < row; jj++)
                        sumy += area.RowsSize[jj];

                    miny = sumy + area.RowsSize[row] / 2;


                    Coordinate pointProj = area.ProjectPointAzimut(new Coordinate(minx, miny));
                    return new double[2] { pointProj.X, pointProj.Y };
                }





            }



        }

        public static int IdCell(this IAreaInterest area, int col, int row)
        {
            if (col >= 0 && col < area.NumColumns && row >= 0 && row < area.NumRows)
                return row * area.NumColumns + col;
            else
                return -1;

        }

        public static int[] ColRowbyId(this IAreaInterest area, int id)
        {
            if (id >= 0 && id < (area.NumRows * area.NumColumns))
                return new int[2] { id % area.NumColumns, Convert.ToInt32(Math.Floor((double)(id / area.NumColumns))) };
            else
                return
                    new int[2] { -1, -1 };
        }

    }

    public static class VectorGridEx
    {
        public static ICell CreateCell(this VectorGrid field, int i, int j, int k)
        {
            Cell cell = new Cell();
            cell.Id = new int[3] { i, j, k };
            cell.Width = field.ColumnsSize[i];
            cell.Height = field.RowsSize[j];
            cell.Value = field.Data[i, j, k];
            return cell;

        }

        public static IAreaInterest Get_area(this VectorGrid vector)
        {
            return (IAreaInterest)vector;
        }

        public static void UpdateAreaInterestByOrigin(this VectorGrid vector, IAreaInterest p)
        {
            vector.Area = (AreaInterest)p;
            vector.MaxX = p.MinX + (p.CellSizeX * p.NumColumns);
            vector.MaxY = p.MinY + (p.CellSizeY * p.NumRows);
        }

        public static void UpdateRowColsByCellSize(this VectorGrid field, double cellSizeX, double cellSizeY)
        {
            IAreaInterest are = (IAreaInterest)field.Clone();
            are.CellSizeX = cellSizeX;
            are.CellSizeY = cellSizeY;
            field.UpdateAreaInterestByCellSize(are);
        }

        public static void UpdateByRowColsDimesion(this VectorGrid original)
        {

            original.MaxY = original.MinY + (original.ColumnsSize.Sum());
            original.MaxX = original.MinX + (original.RowsSize.Sum());


        }

        public static void UpdateAreaInterestByCellSize(this VectorGrid field, IAreaInterest original)
        {
            double dx = original.MaxX - original.MinX;
            double dy = original.MaxY - original.MinY;
            double ncol = Math.Floor(dx / original.CellSizeX);
            double nrow = Math.Floor(dy / original.CellSizeY);

            field.MinX = original.MinX;
            field.MinY = original.MinY;
            field.MaxY = original.MinY + (original.CellSizeY * nrow);
            field.MaxX = original.MinX + (original.CellSizeX * ncol);

            field.NumColumns = Convert.ToInt16(ncol);
            field.NumRows = Convert.ToInt16(nrow);
            field.CellSizeX = original.CellSizeX;
            field.CellSizeY = original.CellSizeY;
            field.Azimut = original.Azimut;

        }

        public static void UpdateCellSizeByColRows(this VectorGrid field, int col, int row)
        {
            IAreaInterest are = (IAreaInterest)field.Clone();
            are.NumColumns = col;
            are.NumRows = row;
            field.UpdateAreaInterestByColumnsRows(are);

        }

        public static void UpdateAreaInterestByColumnsRows(this VectorGrid field, IAreaInterest original)
        {
            double dx = original.MaxX - original.MinX;
            double dy = original.MaxY - original.MinY;
            double cellSizeX = dx / original.NumColumns;
            double cellSizeY = dy / original.NumRows;

            field.MinX = original.MinX;
            field.MinY = original.MinY;
            field.MaxY = original.MinY + (original.NumRows * cellSizeY);
            field.MaxX = original.MinX + (original.NumColumns * cellSizeX);

            field.NumColumns = original.NumColumns;
            field.NumRows = original.NumRows;
            field.CellSizeX = cellSizeX;
            field.CellSizeY = cellSizeY;
            field.Azimut = original.Azimut;
        }

        public static Coordinate ReprojectPointToWgs84(this VectorGrid field, ProjectionInfo current, double x, double y)
        {
            double[] xy = new double[2];
            xy[0] = x;
            xy[1] = y;
            double[] z = new double[1];
            string esri = Properties.Resources.wgs_84_esri_string;
            ProjectionInfo wgs84 = new ProjectionInfo();
            wgs84 = KnownCoordinateSystems.Geographic.World.WGS1984;

            Reproject.ReprojectPoints(xy, z, current, wgs84, 0, 1);
            return new Coordinate(xy[0], xy[1]);
        }


        private static double[] CalculatePlaneNodes(this VectorGrid field, List<NodeMAD> nodes)
        {

            double exmin = double.MaxValue;
            double exmax = double.MinValue;
            double eymin = double.MaxValue;
            double eymax = double.MinValue;
            double ezmin = double.MaxValue;
            double ezmax = double.MinValue;

            foreach (NodeMAD line in nodes)
            {


                double x = line.x;
                double y = line.y;
                double z = line.z;


                if (x > exmax)
                    exmax = x;
                if (x < exmin)
                    exmin = x;
                if (y > eymax)
                    eymax = y;
                if (y < eymin)
                    eymin = y;
                if (z > ezmax)
                    ezmax = z;
                if (z < ezmin)
                    ezmin = z;



            }


            double[] l = new double[6];
            l[0] = exmin;
            l[1] = exmax;
            l[2] = eymin;
            l[3] = eymax;
            l[4] = ezmin;
            l[5] = ezmax;

            return l;




        }

        public static Polygon CreateElement(this 
       VectorGrid field, MADBound ma, DrawPlane plane)
        {

            double[] lis = new double[6];
            lis[0] = ma.MinX();
            lis[1] = ma.MaxX();
            lis[2] = ma.MinY();
            lis[3] = ma.MaxY();
            lis[4] = ma.MinZ();
            lis[5] = ma.MaxZ();


            Coordinate[] array = new Coordinate[5];
            if (plane == DrawPlane.xy)
            {
                array[0] = new Coordinate(lis[0], lis[2]);
                array[1] = new Coordinate(lis[0], lis[3]);
                array[2] = new Coordinate(lis[1], lis[3]);
                array[3] = new Coordinate(lis[1], lis[2]);
                array[4] = new Coordinate(lis[0], lis[2]);
            }
            if (plane == DrawPlane.xz)
            {
                array[0] = new Coordinate(lis[0], lis[4]);
                array[1] = new Coordinate(lis[0], lis[5]);
                array[2] = new Coordinate(lis[1], lis[5]);
                array[3] = new Coordinate(lis[1], lis[4]);
                array[4] = new Coordinate(lis[0], lis[4]);
            }
            if (plane == DrawPlane.yz)
            {
                array[0] = new Coordinate(lis[2], lis[4]);
                array[1] = new Coordinate(lis[2], lis[5]);
                array[2] = new Coordinate(lis[3], lis[5]);
                array[3] = new Coordinate(lis[3], lis[4]);
                array[4] = new Coordinate(lis[2], lis[4]);
            }
            LinearRing shell = new LinearRing(array);
            Polygon poly = new Polygon(shell);
            return poly;

        }

        public static Polygon CreateElement(this VectorGrid field, List<NodeMAD> elem, int[] seq, DrawPlane plane)
        {

            if (seq != null && seq.Length > 0)
            {

                Coordinate[] array = new Coordinate[seq.Length];
                if (seq.Length == 5 || elem.Count == 8 || elem.Count == 4)
                {
                    double[] lis = CalculatePlaneNodes(field, elem);
                    if (plane == DrawPlane.xy)
                    {
                        array[0] = new Coordinate(lis[0], lis[2]);
                        array[1] = new Coordinate(lis[0], lis[3]);
                        array[2] = new Coordinate(lis[1], lis[3]);
                        array[3] = new Coordinate(lis[1], lis[2]);
                        array[4] = new Coordinate(lis[0], lis[2]);
                    }
                    if (plane == DrawPlane.xz)
                    {
                        array[0] = new Coordinate(lis[0], lis[4]);
                        array[1] = new Coordinate(lis[0], lis[5]);
                        array[2] = new Coordinate(lis[1], lis[5]);
                        array[3] = new Coordinate(lis[1], lis[4]);
                        array[4] = new Coordinate(lis[0], lis[4]);
                    }
                    if (plane == DrawPlane.yz)
                    {
                        array[0] = new Coordinate(lis[2], lis[4]);
                        array[1] = new Coordinate(lis[2], lis[5]);
                        array[2] = new Coordinate(lis[3], lis[5]);
                        array[3] = new Coordinate(lis[3], lis[4]);
                        array[4] = new Coordinate(lis[2], lis[4]);
                    }

                }
                else
                {





                    if (plane == DrawPlane.xy)
                    {
                        for (int i = 0; i < seq.Length; i++)
                        {
                            array[i] = new Coordinate(elem[seq[i]].x, elem[seq[i]].y);
                        }
                    }
                    if (plane == DrawPlane.xz)
                    {
                        for (int i = 0; i < seq.Length; i++)
                        {
                            array[i] = new Coordinate(elem[seq[i]].x, elem[seq[i]].z);
                        }
                    }
                    if (plane == DrawPlane.yz)
                    {
                        for (int i = 0; i < seq.Length; i++)
                        {
                            array[i] = new Coordinate(elem[seq[i]].y, elem[seq[i]].z);
                        }
                    }




                }
                LinearRing shell = new LinearRing(array);
                Polygon poly = new Polygon(shell);
                return poly;

            }
            else
            {
                Coordinate[] array = null;
                if (elem.Count == 1)
                {
                    array = new Coordinate[5];

                    double[] ax = new double[5] { 1, 1, -1, -1, 1 };
                    double[] ay = new double[5] { 1, -1, -1, 1, 1 };
                    if (plane == DrawPlane.xy)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            array[i] = new Coordinate(elem[0].x + ax[i], elem[0].y + ay[i]);
                        }
                    }
                    if (plane == DrawPlane.xz)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            array[i] = new Coordinate(elem[0].x + ax[i], elem[0].z + ay[i]);
                        }
                    }
                    if (plane == DrawPlane.yz)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            array[i] = new Coordinate(elem[0].y + ax[i], elem[0].z + ay[i]);
                        }
                    }

                }
                else
                {



                    array = new Coordinate[elem.Count + 1];
                    int i = 0;
                    for (; i < elem.Count; i++)
                    {
                        array[i] = new Coordinate(elem[i].x, elem[i].y);
                    }
                    array[i] = new Coordinate(elem[0].x, elem[0].y);
                }
                LinearRing shell = new LinearRing(array);
                Polygon poly = new Polygon(shell);
                return poly;
            }
        }

        public static Polygon createExtend(this VectorGrid field)
        {
            Coordinate[] array = new Coordinate[5];
            array[0] = new Coordinate(field.MinX, field.MinY);
            array[1] = new Coordinate(field.MaxX, field.MinY);
            array[2] = new Coordinate(field.MaxX, field.MaxY);
            array[3] = new Coordinate(field.MinX, field.MaxY);
            array[4] = new Coordinate(field.MinX, field.MinY);
            LinearRing shell = new LinearRing(array);
            Polygon poly = new Polygon(shell);
            return poly;
        }

        public static Polygon CreatePolygonPixel(this VectorGrid field, int i, int j, int k, DrawPlane plane)
        {
            double minx;
            double miny;
            double sumx = 0;


                sumx = field.CellSizeX*i;

            minx = field.MinX + sumx;

            double sumy = 0;
            miny = field.MaxY - (field.CellSizeY * j) - field.CellSizeY;


            Coordinate[] array = new Coordinate[5];


            if (plane == DrawPlane.xz)
            {
                if (field.Nodes == null)
                    field.Azimut = -180;

                sumy = 0;
                for (int jj = 0; jj < k; jj++)
                    sumy += field.LayerSize[jj];

                miny = field.MinY + sumy;


                array[0] = field.ProjectPointAzimut(new Coordinate(minx, miny));
                array[1] = field.ProjectPointAzimut(new Coordinate(minx + field.ColumnsSize[i], miny));
                array[2] = field.ProjectPointAzimut(new Coordinate(minx + field.ColumnsSize[i], miny + field.LayerSize[j]));
                array[3] = field.ProjectPointAzimut(new Coordinate(minx, miny + field.LayerSize[j]));
                array[4] = field.ProjectPointAzimut(new Coordinate(minx, miny));
                field.Azimut = 0;
            }
            if (plane == DrawPlane.xy)
            {
                array[0] = field.ProjectPointAzimut(new Coordinate(minx, miny));
                array[1] = field.ProjectPointAzimut(new Coordinate(minx + field.ColumnsSize[i], miny));
                array[2] = field.ProjectPointAzimut(new Coordinate(minx + field.ColumnsSize[i], miny + field.RowsSize[j]));
                array[3] = field.ProjectPointAzimut(new Coordinate(minx, miny + field.RowsSize[j]));
                array[4] = field.ProjectPointAzimut(new Coordinate(minx, miny));
            }

            LinearRing shell = new LinearRing(array);
            Polygon poly = new Polygon(shell);
            return poly;
        }



        public static bool UpdateAreaInterest(this VectorGrid vector, IAreaInterest p)
        {
            if (p.NumColumns == 0 && p.NumRows == 0 && p.CellSizeX == 0 && p.CellSizeY == 0)
                return false;

            if (p.CellSizeX == -1 && p.CellSizeY == -1 &&
                p.ColumnsSize != null && p.RowsSize != null)
            {
                vector.Area = (AreaInterest)p;
                vector.MaxY = p.MinY + (p.RowsSize.Sum());
                vector.MaxX = p.MinX + (p.ColumnsSize.Sum());
                return true;
            }


            if ((Math.Abs(p.MinX - (p.MaxX - (p.NumColumns * p.CellSizeX))) < 0.001) &&
                (Math.Abs(p.MinY - (p.MaxY - (p.NumRows * p.CellSizeY))) < 0.001))
            {
                vector.Area = (AreaInterest)p;
                return false;
            }
            else
            {
                vector.Area = (AreaInterest)p;
                vector.MaxY = p.MinY + (p.CellSizeY * p.NumRows);
                vector.MaxX = p.MinX + (p.CellSizeX * p.NumColumns);
                return true;
            }


        }
    }

    public enum DrawPlane
    {
        xy,
        xz,
        yz
    }
    
    public class AreaInterest : IAreaInterest
    {

        public Dictionary<int, NodeMAD> Nodes { set; get; }
        public Dictionary<int, ElementMAD> Elements { set; get; }

        private double minX;
        private double minY;
        private double maxX;
        private double maxY;
        private int numRows;
        private int numColumns;
        private int numLayers;
        private double cellSizeX;
        private double cellSizeY;
        private double cellSizeZ;
        private double azimut;
        private string name = "Domain";
        private string description = "";
        private string typeCoor = "Projec";
        private string projection;
        private string date = "";
        private string lastChange = "";
        private double[] columnsSize;
        private double[] rowsSize;
        private double[] layerSize;
        private bool symmetricDimension;
        Type_Domain type;
        public Type_Domain TypeDomain
        {
            get
            {
                return type;
            }
            set
            {
                if (value == Type_Domain.Unstructured)
                {
                    type = value;
                    cellSizeX = 0;
                    cellSizeY = 0;
                    cellSizeZ = 0;
                    numColumns = 0;
                    numRows = 0;
                    numLayers = 0;


                }
                else
                    type = value;

            }



        }
        public AreaInterest()
        {
            symmetricDimension = true;
        }

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        public int NumLayers
        {
            get { return numLayers; }
            set { numLayers = value; }
        }
        public double CellSizeZ
        {
            get { return this.cellSizeZ; }
            set
            {
                this.cellSizeZ = value;
                this.FillSymmetricDimensions();
            }
        }
        public string Projection
        {
            get { return projection; }
            set { projection = value; }
        }
        public string TypeCoor
        {
            get { return typeCoor; }
            set { this.typeCoor = value; }
        }
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        public string Date
        {
            get { return this.date; }
            set { this.date = value; }
        }
        public string LastChange
        {
            get { return this.lastChange; }
            set { this.lastChange = value; }
        }
        public double MinX
        {
            get { return this.minX; }
            set { this.minX = value; }
        }
        public double MinY
        {
            get { return this.minY; }
            set { this.minY = value; }
        }
        public double MaxX
        {
            get { return this.maxX; }
            set { this.maxX = value; }
        }
        public double MaxY
        {
            get { return this.maxY; }
            set { this.maxY = value; }
        }
        public int NumRows
        {
            get { return this.numRows; }
            set { this.numRows = value; }
        }
        public int NumColumns
        {
            get { return this.numColumns; }
            set { this.numColumns = value; }
        }
        public double Azimut
        {
            get { return this.azimut; }
            set { this.azimut = value; }
        }
        public double CellSizeX
        {
            get { return this.cellSizeX; }
            set
            {
                this.cellSizeX = value;
                this.FillSymmetricDimensions();
            }
        }
        public double CellSizeY
        {
            get { return this.cellSizeY; }
            set
            {
                this.cellSizeY = value;
                this.FillSymmetricDimensions();
            }
        }
        public object Clone()
        {

            return this.MemberwiseClone();



        }
        public bool SymetricDimension
        {
            get
            {
                return symmetricDimension;
            }
            set
            {
                symmetricDimension = value;
                if (!value)
                {
                    this.cellSizeX = -1;
                    this.cellSizeY = -1;
                }
            }
        }


        public double[] LayerSize
        {
            get { return layerSize; }
            set { layerSize = value; }
        }
        public double[] ColumnsSize
        {
            set { columnsSize = value; }
            get { return columnsSize; }
        }
        public double[] RowsSize
        {
            set { rowsSize = value; }
            get { return rowsSize; }
        }

        public void SetProjection(ProjectionInfo pro)
        {
            Projection = pro.ToEsriString();
        }
        public ProjectionInfo GetProjection()
        {

            ProjectionInfo pro = new ProjectionInfo();
            if (pro.TryParseEsriString(Projection))
            {
                pro.ParseEsriString(Projection);
                return pro;
            }
            return pro;

        }
    }

    public abstract class VectorGrid : IField, IAreaInterest
    {

        public Dictionary<string, int[]> index = new Dictionary<string, int[]>();

        void LoadTypes()
        {
            index = new Dictionary<string, int[]>();
            index.Add("B8", new int[24] { 0, 1, 1, 6, 6, 3, 3, 0, 0, 2, 2, 5, 5, 3, 5, 7, 7, 6, 4, 1, 4, 2, 7, 4 });
            index.Add("B8p", new int[5] { 3, 6, 7, 5, 3 });
            index.Add("quad", new int[8] { 0, 1, 1, 2, 2, 3, 3, 0 });
            index.Add("quadp", new int[5] { 0, 1, 2, 3, 0 });
            index.Add("T", new int[8] { 0, 1, 1, 2, 2, 3, 3, 0 });
            index.Add("Tp", new int[5] { 0, 1, 2, 3, 0 });
            index.Add("tri", new int[6] { 0, 1, 1, 2, 2, 0 });
            index.Add("trip", new int[4] { 0, 1, 2, 0 });
        }

        public int[] GetSeq(string name, int dim)
        {
            if (dim == 2 && index.ContainsKey(name + "p"))
            {

                return index[name + "p"];
            }

            if (dim == 3 && index.ContainsKey(name))
            {

                return index[name];
            }
            return null;
        }


        public Dictionary<int, NodeMAD> Nodes { set; get; }
        public Dictionary<int, ElementMAD> Elements { set; get; }

        public Type_Domain TypeDomain { get; set; }
        private MapPolygonLayer _GridLayer;
        private FeatureSet rectangleFs;

        private string nameField;
        private int idSample;
        private int realization;
        private string reference;
        private int originType;

        public double[, ,] data;
        public AreaInterest area;
        public List<ICell> cells;

        private double valueNoUsedStatistics = -1000;
        #region Grid_Fields


        public AreaInterest Area
        {
            get { return area; }
            set { area = value; }
        }



        public MapPolygonLayer GridLayer
        {
            get { return _GridLayer; }
            set { _GridLayer = value; }
        }


        public FeatureSet RectangleFs
        {
            get { return rectangleFs; }
            set { rectangleFs = value; }
        }

        #endregion

        #region VectorGrid_Methods

        public bool Exist(Map mainMap, DrawPlane plane)
        {
            try
            {
                MapPolygonLayer grid = null;
                foreach (IMapPolygonLayer lay in mainMap.GetPolygonLayers())
                {
                    if (lay.LegendText == "Grid"                        )
                    {
                        grid = (MapPolygonLayer)lay;

                    }
                }

                if (grid == null) return false;

                if (grid.DataSet.Features.Count != (this.NumColumns * this.NumRows))
                    return false;


                Int32 v = ((this.NumColumns * this.NumRows) - 1);
                grid.SelectByAttribute("[Id]=" + v.ToString());
                IList<Coordinate> fea = grid.DataSet.Features[v].Coordinates;
                Polygon poly1 = new Polygon(fea);

                int i = this.NumColumns - 1;
                int j = this.NumRows - 1;
                int k = 1;

                if (poly1.Equals(this.CreatePolygonPixel(i, j, k, plane)))
                {
                    GridLayer = grid;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw
                    new ArgumentException("It is not possible to evaluate the VectorGrid");

            }
        }

        public AreaInterest Get_area()
        {
            return area;
        }



        private void AddFeaturesData(int id, int i, int j, int k, IFeature newF, int index=-9999, object field=null)
        {
            newF.DataRow["ID"] = id;
            newF.DataRow["row"] = j + 1;
            newF.DataRow["col"] = i + 1;
            newF.DataRow["Layer"] = k + 1;
          
            if (FieldType== typeof(int))
            newF.DataRow["Field"] = Convert.ToInt32(field);
            if (FieldType == typeof(string))
                newF.DataRow["Field"] = Convert.ToString(field);
            if (FieldType == typeof(double))
                newF.DataRow["Field"] = Convert.ToDouble(field);

            newF.DataRow["Index"] = index;

        }

        private void AddFeaturesData(MADBound id, IFeature newF)
        {
            newF.DataRow["ID"] = id.center.id;
            newF.DataRow["row"] = Convert.ToInt32(id.center.x);
            newF.DataRow["col"] = Convert.ToInt32(id.center.y);
            newF.DataRow["Layer"] = Convert.ToInt32(id.center.z);
            newF.DataRow["Field"] = id.center.value;
        }

        private void AddFeaturesData(ElementMAD el, IFeature newF)
        {
            newF.DataRow["ID"] = el.id;
            newF.DataRow["Field"] = el.value;
        }



        public void FillData()
        {

            this.Data = new double[this.NumColumns, this.NumRows, this.NumLayers];
            //for (int i = 0; i < this.NumColumns; i++)
            //{
            //    for (int j = 0; j < this.NumRows; j++)
            //    {
            //        for (int k = 0; k < this.NumLayers; k++)
            //        {
            //            this.Data[i, j, k] = 0;
            //        }
            //    }
            //}
        }

        public void CreateThematicMap(Map mainMap, bool ChangeValues, DrawPlane plane)
        {
            GridLayer = (MapPolygonLayer)Validator.AvailableLayer(mainMap, Properties.Resources.VerctorGrid);

            if (GridLayer == null) return;

            IntroduceDataSequence(plane, 1);
            CreateSymbology(1);
            mainMap.ResetBuffer();
        }

        struct CRL
        {

            public int c;
            public int r;
            public int l;
            public int id;

        }
        struct PCRL
        {
            public Polygon pol;
            public CRL id;
        }

        private void IntroduceDataSequence(DrawPlane plane, int layer)
        {
            //int id = 0;
            //List<CRL> lis = new List<CRL>();
            //for (int i = 0; i < this.NumColumns; i++)
            //{
            //    for (int j = 0; j < this.NumRows; j++)
            //    {
            //        for (int k = 0; k < this.NumLayers; k++)
            //        {

            //            id++;
            //        }
            //    }
            //}
            int idv2 = 30;

            int ll = Convert.ToInt32(Math.Ceiling((double)idv2 / (this.NumColumns * this.NumRows)));
            int lr = Convert.ToInt32(Math.Ceiling((double)(idv2 - (ll - 1) * (this.NumColumns * this.NumRows)) / this.NumColumns));
            int cl = idv2 - (ll - 1) * (this.NumColumns * this.NumRows) - (lr - 1) * this.NumColumns;
            //  Polygon poly = this.CreatePolygonPixel(c, r, l, plane);

            if (this.NumLayers == 0) this.NumLayers = 1;
            PCRL[] lisp = new PCRL[this.NumColumns * this.NumRows * 1];



            Parallel.For(1, this.NumColumns * this.NumRows * 1 + 1, idv =>
            {
                int l = Convert.ToInt32(Math.Ceiling((double)idv / (this.NumColumns * this.NumRows)));
                int r = Convert.ToInt32(Math.Ceiling((double)(idv - (l - 1) * (this.NumColumns * this.NumRows)) / this.NumColumns));
                int c = idv - (l - 1) * (this.NumColumns * this.NumRows) - (r - 1) * this.NumColumns;
                Polygon poly = this.CreatePolygonPixel(c - 1, r - 1, l - 1, plane);
                CRL idc = new CRL();
                idc.c = c;
                idc.r = r;
                idc.l = l;
                idc.id = idv;
                PCRL p = new PCRL();
                p.pol = poly;
                p.id = idc;
                lisp[idv - 1] = p;
            });

            //Parallel.ForEach(lis, item =>
            //{


            //});

            foreach (PCRL item in lisp)
            {
                IFeature newF = GridLayer.DataSet.AddFeature(item.pol);
                AddFeaturesData(item.id.id, item.id.c - 1, item.id.r - 1, layer - 1, newF);
                // newF.DataRow["Field"] = this.data[item.id.c-1, item.id.r-1, item.id.l-1];
            }

        }
        public void DrawExtent(Map mainMap,string name="Extent")
        {
            Validator.RemoveLayer(mainMap, name);
            AddGridLayer(mainMap,name);
            IFeature newF = GridLayer.DataSet.AddFeature(this.createExtend());
            newF.DataRow["ID"] = -1;
            newF.DataRow["row"] = -1;
            newF.DataRow["col"] = -1;
            newF.DataRow["Layer"] =-1;
            mainMap.ResetBuffer();
        }

        public void AddGridLayer(Map mainMap, string name, string color="Transparent")
        {
            Color col = Color.Transparent;
            if (color!="Transparent")
                col= Color.Beige;
            if (FieldType == null)
                FieldType = typeof(int);

            RectangleFs = new FeatureSet(FeatureType.Polygon);
            RectangleFs.DataTable.Columns.Add("ID");
            RectangleFs.DataTable.Columns.Add("row");
            RectangleFs.DataTable.Columns.Add("col");
            RectangleFs.DataTable.Columns.Add("Layer");
            RectangleFs.DataTable.Columns.Add(new System.Data.DataColumn("Field", FieldType));
            RectangleFs.DataTable.Columns.Add(new System.Data.DataColumn("Index", typeof(double)));
            RectangleFs.Projection = mainMap.Projection;

            GridLayer = new MapPolygonLayer(RectangleFs);
            GridLayer.LegendText = name;

            //_rectangleLayer.LegendItemVisible = false;
            Color redColor = Color.Red.ToTransparent(0.8f);
            GridLayer.Symbolizer = new PolygonSymbolizer();
            // 
            GridLayer.Symbolizer.SetOutline(Color.Red, 1.0);
            GridLayer.Symbolizer.SetFillColor(col);
            GridLayer.SelectionSymbolizer = GridLayer.Symbolizer;
            GridLayer.Symbolizer.SetOutline(Color.Red, 1.0);
            // Extent ext = this.GetExtent(0.15);
            mainMap.Layers.Add(GridLayer);
            // _mainMap.ViewExtents = ext;

        }


        public void AddGridLayer(Map mainMap)
        {
            string name = "Grid";
            if (GridLayerIsInMap(mainMap,name))
            {
                GridLayer.DataSet.Features.Clear();
            }
            else
            {
                RectangleFs = new FeatureSet(FeatureType.Polygon);
                RectangleFs.DataTable.Columns.Add("ID");
                RectangleFs.DataTable.Columns.Add("row");
                RectangleFs.DataTable.Columns.Add("col");
                RectangleFs.DataTable.Columns.Add("Layer");
                RectangleFs.DataTable.Columns.Add(new System.Data.DataColumn("Field", typeof(double)));
                RectangleFs.DataTable.Columns.Add(new System.Data.DataColumn("Index", typeof(int)));
                RectangleFs.Projection = mainMap.Projection;

                GridLayer = new MapPolygonLayer(RectangleFs);
                GridLayer.LegendText = Properties.Resources.VerctorGrid;

                //_rectangleLayer.LegendItemVisible = false;
                Color redColor = Color.Red.ToTransparent(0.8f);
                GridLayer.Symbolizer = new PolygonSymbolizer();
                // 
                GridLayer.Symbolizer.SetOutline(Color.Red, 1.0);
                GridLayer.Symbolizer.SetFillColor(Color.Transparent);
                GridLayer.SelectionSymbolizer = GridLayer.Symbolizer;
                GridLayer.Symbolizer.SetOutline(Color.Red, 1.0);
                // Extent ext = this.GetExtent(0.15);

                // _mainMap.ViewExtents = ext;

            }

            if (!GridLayerIsInMap(mainMap,name))
            {
                //    Extent ext = this.GetExtent(0.15);
                mainMap.Layers.Add(GridLayer);
                //   _mainMap.ViewExtents = ext;
            }
        }

        public void CalculateStats()
        {

            double exmin = double.MaxValue;
            double exmax = double.MinValue;
            double eymin = double.MaxValue;
            double eymax = double.MinValue;
            double ezmin = double.MaxValue;
            double ezmax = double.MinValue;

            foreach (NodeMAD line in Nodes.Values)
            {


                double x = line.x;
                double y = line.y;
                double z = line.z;


                if (x > exmax)
                    exmax = x;
                if (x < exmin)
                    exmin = x;
                if (y > eymax)
                    eymax = y;
                if (y < eymin)
                    eymin = y;
                if (z > ezmax)
                    ezmax = z;
                if (z < ezmin)
                    ezmin = z;



            }
            this.MinX = exmin;
            this.MaxX = exmax;
            this.MinY = eymin;
            this.MaxY = eymax;
            this.MinZ = ezmin;
            this.MaxZ = ezmax;




        }


        public double MinZ;
        public double MaxZ;

        public void CreateThematicMap(Map mainMap, DrawPlane plane, int layer, bool viewGrid)
        {
            Validator.RemoveLayer(mainMap, Properties.Resources.VerctorGrid);
            AddGridLayer(mainMap,"Grid");
            GenerateFeatureSetGrid(plane, 1, viewGrid);
            // mainMap.Layers.Add(GridLayer);
            CreateSymbology(layer);

            IMapLayer grd =
             Validator.AvailableLayer(mainMap, "Grid");
            grd.LegendText = "ZZZZZZ";

            SortLayersDes(mainMap);

            grd.LegendText = "Grid";


            mainMap.ResetBuffer();

        }

        public void CreateThematicMap(Map mainMap, DrawPlane plane, bool viewGrid)
        {
            Validator.RemoveLayer(mainMap, Properties.Resources.VerctorGrid);
            AddGridLayer(mainMap,"Grid");
            GenerateFeatureSetGrid(plane, 1, viewGrid);
            // mainMap.Layers.Add(GridLayer);
            CreateSymbology(1);



            mainMap.ResetBuffer();





        }

        private void SortLayersDes(Map map)
        {

            var newLayers = map.Layers.OrderByDescending(l => l.LegendText).ToList();

            // Suspending events speeds our changes up and prevents redrawing multiple times.
            // The events will be called only once, when we call ResumeEvents().
            map.Layers.SuspendEvents();

            // By default, it appears some part of the layer is disposed when it is removed from the collection.
            // If we were to use App.Map.Layers.Clear(), we would stlil need to LockDispose on each layer.
            while (map.Layers.Any())
            {
                var layer = map.Layers[0];
                layer.LockDispose();
                map.Layers.RemoveAt(0);
            }

            // As we add each layer back in, in the correct order, we UnlockDispose so that the layer can be disposed
            // at the appropriate time.
            foreach (IMapLayer newLayer in newLayers)
            {
                map.Layers.Add(newLayer);
                newLayer.UnlockDispose();
            }
            map.Layers.ResumeEvents();

        }

        public void CreateEmptyMap(Map mainMap, DrawPlane plane, bool viewGrid)
        {
            Validator.RemoveLayer(mainMap, Properties.Resources.VerctorGrid);
            AddGridLayer(mainMap,"Grid");
            GenerateFeatureSetGrid(plane, 1, viewGrid);
            // mainMap.Layers.Add(GridLayer);
            // CreateSymbology();
            mainMap.ResetBuffer();
        }
        public void CreateMap(Map mainMap,string name)
        {
            Validator.RemoveLayer(mainMap, name);
            AddGridLayer(mainMap,name);
            GenerateFeatureSetGrid(DrawPlane.xy, 1, true);
            // mainMap.Layers.Add(GridLayer);
            // CreateSymbology();
            mainMap.ResetBuffer();

        }

        public void CreateMap(Map mainMap, string name, IFeatureSet Fea)
        {
            Validator.RemoveLayer(mainMap, name);
            AddGridLayer(mainMap, name);
            GenerateFeatureIntersect(DrawPlane.xy, 1, true,Fea);
            // mainMap.Layers.Add(GridLayer);
            // CreateSymbology();
            mainMap.ResetBuffer();

        }

        public void CreateMap(Map mainMap, string name, IRaster Ras)
        {
            Validator.RemoveLayer(mainMap, name);
            AddGridLayer(mainMap, name);
            GenerateFeatureIntersect(DrawPlane.xy, 1, true,Ras);
            // mainMap.Layers.Add(GridLayer);
            // CreateSymbology();
            mainMap.ResetBuffer();

        }

        private void CreateSymbology(int layer)
        {
            Stat stat = new Stat(false);
            stat = CalculateStatisticsofField(stat, layer);
            IPolygonSymbolizer sim = new PolygonSymbolizer(Color.White);
            GridLayer.Symbolizer = sim;
            PolygonScheme polScheme = new PolygonScheme();
            polScheme.EditorSettings.FieldName = "Field";
            polScheme.EditorSettings.ClassificationType = ClassificationType.Custom;

            for (int i = 0; i < 12; i++)
            {
                PolygonCategory cat = new PolygonCategory();
                cat.SetColor(Color.FromArgb(i * 20, 50, 250 - (i * 20)));
                cat.Symbolizer.SetOutline(Color.FromArgb(i * 20, 50, 250 - (i * 20)), 0.1);

                cat.Range = new Range(stat.Min + (i - 1) * (stat.Max - stat.Min) / 10, stat.Min + (i) * (stat.Max - stat.Min) / 10);
                cat.FilterExpression = string.Format("[Field]>{0} AND [Field]<={1}", Math.Round((double)cat.Range.Minimum, 3), Math.Round((double)cat.Range.Maximum, 3));
                cat.LegendText = string.Format("{0} - {1}", Math.Round((double)cat.Range.Minimum, 3), Math.Round((double)cat.Range.Maximum, 3));
                polScheme.AddCategory(cat);
            }
            PolygonCategory catd = new PolygonCategory();
            catd.SetColor(Color.White);
            catd.Symbolizer.SetOutline(Color.White, 0.1);
            catd.FilterExpression = string.Format("[Field]>{0} AND [Field]<={1}", -999, -1001);
            catd.LegendText = "Inactive Cells";
            polScheme.AddCategory(catd);
            GridLayer.Symbology = polScheme;
        }


        public void SetExpectionNumber(double value)
        {

            valueNoUsedStatistics = value;
        }


        private Stat CalculateStatisticsofField(Stat stat, int layer)
        {
            if (this.TypeDomain == Type_Domain.Grid)
            {
                for (int col = 0; col < this.data.GetLength(0); col++)
                {
                    for (int row = 0; row < this.data.GetLength(1); row++)
                    {
                        for (int lay = layer - 1; lay < layer; lay++)
                        {

                            if (Math.Abs(this.data[col, row, lay] - valueNoUsedStatistics) > 0.1)
                                stat += new Stat(this.data[col, row, lay]);
                        }
                    }
                }

            }
            if (this.TypeDomain == Type_Domain.Unstructured)
            {
                foreach (var item in this.Elements.Values)
                {
                    stat += new Stat(item.value);
                }

            }
            return stat;
        }
        public bool GridLayerIsInMap(Map mainMap,string name)
        {
            foreach (IMapLayer lay in mainMap.GetAllLayers())
            {
                if (lay.LegendText == name)
                {
                    GridLayer = (MapPolygonLayer)lay;
                    return true;
                }
            }
            return false;
        }

        public void AddLayer(Map mainMap,string name)
        {
            if (GridLayerIsInMap(mainMap,name))
            {
                GridLayer.DataSet.Features.Clear();
            }
            else
            {
                RectangleFs = new FeatureSet(FeatureType.Polygon);
                RectangleFs.DataTable.Columns.Add("ID");
                RectangleFs.DataTable.Columns.Add("row");
                RectangleFs.DataTable.Columns.Add("col");
                RectangleFs.DataTable.Columns.Add("Layer");
                RectangleFs.DataTable.Columns.Add(new System.Data.DataColumn("Field", typeof(double)));
                RectangleFs.DataTable.Columns.Add(new System.Data.DataColumn("Index", typeof(int)));
                RectangleFs.Projection = area.GetProjection();

                GridLayer = new MapPolygonLayer(RectangleFs);
                GridLayer.LegendText = Properties.Resources.VerctorGrid;

                //_rectangleLayer.LegendItemVisible = false;
                Color redColor = Color.Red.ToTransparent(0.8f);
                GridLayer.Symbolizer = new PolygonSymbolizer();
                // 
                GridLayer.Symbolizer.SetOutline(Color.Red, 1.0);
                GridLayer.Symbolizer.SetFillColor(Color.Beige);
                GridLayer.SelectionSymbolizer = GridLayer.Symbolizer;
                GridLayer.Symbolizer.SetOutline(Color.Red, 1.0);
                // Extent ext = this.GetExtent(0.15);
                mainMap.Layers.Add(GridLayer);
                // _mainMap.ViewExtents = ext;

            }

            if (!GridLayerIsInMap(mainMap,name))
            {
                //    Extent ext = this.GetExtent(0.15);
                mainMap.Layers.Add(GridLayer);
                //   _mainMap.ViewExtents = ext;
            }
        }

        public string Sequence = "";


        public void GenerateFeatures(MADBound b, DrawPlane plane)
        {

            Polygon pol = this.CreateElement(b, plane);
            IFeature newF = GridLayer.DataSet.AddFeature(pol);
            AddFeaturesData(b, newF);


        }


        public void DrawBounds()
        {
            RectangleFs = (FeatureSet)GridLayer.DataSet;
            RectangleFs.InitializeVertices();
            RectangleFs.UpdateExtent();
        }
        public void GenerateFeatureSetGrid(DrawPlane plane, int layer, bool viewGrid)
        {
            if (Nodes != null && Nodes.Count > 0 && !viewGrid)
            {
                LoadTypes();

                foreach (ElementMAD item in Elements.Values)
                {
                    List<NodeMAD> nod = new List<NodeMAD>();
                    foreach (int item1 in item.IdNodes)
                    {
                        nod.Add(Nodes[item1]);
                    }


                    Polygon pol = this.CreateElement(nod, GetSeq(item.type, 2), plane);
                    IFeature newF = GridLayer.DataSet.AddFeature(pol);
                    AddFeaturesData(item, newF);

                }

                RectangleFs = (FeatureSet)GridLayer.DataSet;
                RectangleFs.InitializeVertices();
                RectangleFs.UpdateExtent();
                return;

            }





            if (this.NumLayers == 0) this.NumLayers = 1;
            if (plane == DrawPlane.xy)
            {
                PCRL[] lisp = new PCRL[this.NumColumns * this.NumRows * 1];


                Parallel.For(1, this.NumColumns * this.NumRows * 1 + 1, idv =>
                {
                    int l = Convert.ToInt32(Math.Ceiling((double)idv / (this.NumColumns * this.NumRows)));
                    int r = Convert.ToInt32(Math.Ceiling((double)(idv - (l - 1) * (this.NumColumns * this.NumRows)) / this.NumColumns));
                    int c = idv - (l - 1) * (this.NumColumns * this.NumRows) - (r - 1) * this.NumColumns;
                    Polygon poly = this.CreatePolygonPixel(c - 1, r - 1, l - 1, plane);
                    CRL idc = new CRL();
                    idc.c = c;
                    idc.r = r;
                    idc.l = l;
                    idc.id = idv;
                    PCRL p = new PCRL();
                    p.pol = poly;
                    p.id = idc;
                    lisp[idv - 1] = p;
                });


                cells = new List<ICell>();
                foreach (PCRL item in lisp)
                {
                    IFeature newF = GridLayer.DataSet.AddFeature(item.pol);
                    AddFeaturesData(item.id.id, item.id.c - 1, item.id.r - 1, layer - 1, newF);
                    cells.Add(this.CreateCell(item.id.c - 1, item.id.r - 1, layer - 1));
                    //   newF.DataRow["Field"] = this.data[item.id.c, item.id.r, item.id.l];
                }



                RectangleFs = (FeatureSet)GridLayer.DataSet;
                RectangleFs.InitializeVertices();
                RectangleFs.UpdateExtent();
            }
            if (plane == DrawPlane.xz)
            {
                PCRL[] lisp = new PCRL[this.NumColumns * this.NumRows * this.NumLayers];


                Parallel.For(1, this.NumColumns * this.NumRows * this.NumLayers + 1, idv =>
                {
                    int l = Convert.ToInt32(Math.Ceiling((double)idv / (this.NumColumns * this.NumRows)));
                    int r = Convert.ToInt32(Math.Ceiling((double)(idv - (l - 1) * (this.NumColumns * this.NumRows)) / this.NumColumns));
                    int c = idv - (l - 1) * (this.NumColumns * this.NumRows) - (r - 1) * this.NumColumns;
                    Polygon poly = this.CreatePolygonPixel(c - 1, r - 1, l - 1, plane);
                    CRL idc = new CRL();
                    idc.c = c;
                    idc.r = r;
                    idc.l = l;
                    idc.id = idv;
                    PCRL p = new PCRL();
                    p.pol = poly;
                    p.id = idc;
                    lisp[idv - 1] = p;
                });


                cells = new List<ICell>();
                foreach (PCRL item in lisp)
                {
                    IFeature newF = GridLayer.DataSet.AddFeature(item.pol);
                    AddFeaturesData(item.id.id, item.id.c - 1, item.id.r - 1, item.id.l - 1, newF);
                    cells.Add(this.CreateCell(item.id.c - 1, item.id.r - 1, item.id.l - 1));
                    //   newF.DataRow["Field"] = this.data[item.id.c, item.id.r, item.id.l];
                }



                RectangleFs = (FeatureSet)GridLayer.DataSet;
                RectangleFs.InitializeVertices();
                RectangleFs.UpdateExtent();
            }

        }

        public void GenerateFeatureIntersect(DrawPlane plane, int layer, bool viewGrid, IFeatureSet fea)
        {
            if (Nodes != null && Nodes.Count > 0 && !viewGrid)
            {
                LoadTypes();

                foreach (ElementMAD item in Elements.Values)
                {
                    List<NodeMAD> nod = new List<NodeMAD>();
                    foreach (int item1 in item.IdNodes)
                    {
                        nod.Add(Nodes[item1]);
                    }


                    Polygon pol = this.CreateElement(nod, GetSeq(item.type, 2), plane);
                    IFeature newF = GridLayer.DataSet.AddFeature(pol);
                    AddFeaturesData(item, newF);

                }

                RectangleFs = (FeatureSet)GridLayer.DataSet;
                RectangleFs.InitializeVertices();
                RectangleFs.UpdateExtent();
                return;

            }





            if (this.NumLayers == 0) this.NumLayers = 1;
            if (plane == DrawPlane.xy)
            {
                PCRL[] lisp = new PCRL[this.NumColumns * this.NumRows * 1];
                Type type = typeof(int);

                if(FieldType== null)
                    type=FieldType;

                Dictionary<int,object> ids= new Dictionary<int,object>();

                Parallel.For(1, this.NumColumns * this.NumRows * 1 + 1, idv =>
                {
                    int l = Convert.ToInt32(Math.Ceiling((double)idv / (this.NumColumns * this.NumRows)));
                    int r = Convert.ToInt32(Math.Ceiling((double)(idv - (l - 1) * (this.NumColumns * this.NumRows)) / this.NumColumns));
                    int c = idv - (l - 1) * (this.NumColumns * this.NumRows) - (r - 1) * this.NumColumns;
                    Polygon poly = this.CreatePolygonPixel(c - 1, r - 1, l - 1, plane);
                    CRL idc = new CRL();
                    idc.c = c;
                    idc.r = r;
                    idc.l = l;
                    idc.id = idv;
                    PCRL p = new PCRL();
                    p.pol = poly;
                    p.id = idc;
                    lisp[idv - 1] = p;
                    ids.Add(idv,GetValueFea(fea,poly));
                });


                cells = new List<ICell>();
                foreach (PCRL item in lisp)
                {
                    IFeature newF = GridLayer.DataSet.AddFeature(item.pol);
                    
                    AddFeaturesData(item.id.id, item.id.c - 1, item.id.r - 1, layer - 1, newF,-9999,ids[item.id.id]);
                    cells.Add(this.CreateCell(item.id.c - 1, item.id.r - 1, layer - 1));
                    //   newF.DataRow["Field"] = this.data[item.id.c, item.id.r, item.id.l];
                }



                RectangleFs = (FeatureSet)GridLayer.DataSet;
                RectangleFs.InitializeVertices();
                RectangleFs.UpdateExtent();
            }
            if (plane == DrawPlane.xz)
            {
                PCRL[] lisp = new PCRL[this.NumColumns * this.NumRows * this.NumLayers];


                Parallel.For(1, this.NumColumns * this.NumRows * this.NumLayers + 1, idv =>
                {
                    int l = Convert.ToInt32(Math.Ceiling((double)idv / (this.NumColumns * this.NumRows)));
                    int r = Convert.ToInt32(Math.Ceiling((double)(idv - (l - 1) * (this.NumColumns * this.NumRows)) / this.NumColumns));
                    int c = idv - (l - 1) * (this.NumColumns * this.NumRows) - (r - 1) * this.NumColumns;
                    Polygon poly = this.CreatePolygonPixel(c - 1, r - 1, l - 1, plane);
                    CRL idc = new CRL();
                    idc.c = c;
                    idc.r = r;
                    idc.l = l;
                    idc.id = idv;
                    PCRL p = new PCRL();
                    p.pol = poly;
                    p.id = idc;
                    lisp[idv - 1] = p;
                });


                cells = new List<ICell>();
                foreach (PCRL item in lisp)
                {
                    IFeature newF = GridLayer.DataSet.AddFeature(item.pol);
                    AddFeaturesData(item.id.id, item.id.c - 1, item.id.r - 1, item.id.l - 1, newF);
                    cells.Add(this.CreateCell(item.id.c - 1, item.id.r - 1, item.id.l - 1));
                    //   newF.DataRow["Field"] = this.data[item.id.c, item.id.r, item.id.l];
                }



                RectangleFs = (FeatureSet)GridLayer.DataSet;
                RectangleFs.InitializeVertices();
                RectangleFs.UpdateExtent();
            }

        }

        struct RasterCount
        {
            public int count;
            public int nulls;
            public double values;

            public double GetValue()
            {
                if (count>=nulls)
                {
                    return values / count;
                }else
                {
                    return -9999;
                }

            }

        }

        public void GenerateFeatureIntersect(DrawPlane plane, int layer, bool viewGrid, IRaster raster)
        {
            if (Nodes != null && Nodes.Count > 0 && !viewGrid)
            {
                LoadTypes();

                foreach (ElementMAD item in Elements.Values)
                {
                    List<NodeMAD> nod = new List<NodeMAD>();
                    foreach (int item1 in item.IdNodes)
                    {
                        nod.Add(Nodes[item1]);
                    }


                    Polygon pol = this.CreateElement(nod, GetSeq(item.type, 2), plane);
                    IFeature newF = GridLayer.DataSet.AddFeature(pol);
                    AddFeaturesData(item, newF);

                }

                RectangleFs = (FeatureSet)GridLayer.DataSet;
                RectangleFs.InitializeVertices();
                RectangleFs.UpdateExtent();
                return;

            }





            if (this.NumLayers == 0) this.NumLayers = 1;
            if (plane == DrawPlane.xy)
            {
                PCRL[] lisp = new PCRL[this.NumColumns * this.NumRows * 1];
               
                Dictionary<int, int> RCol = new Dictionary<int, int>();
                Dictionary<int, int> RRow = new Dictionary<int, int>();
                CompleteRasterIndex(plane, raster, RCol, RRow);
                RasterCount[,] values = new RasterCount[this.NumColumns, this.NumRows];
                //for (int i = 0; i < this.NumColumns; i++)
                //{
                //    for (int j = 0; j < this.NumRows; j++)
                //    {
                //        values[i,j] = new RasterCount();
                //        }
                //}

                LoadValuesFromRaster(raster, RCol, RRow, values);

                
                
                Parallel.For(1, this.NumColumns * this.NumRows * 1 + 1, idv =>
                {
                    int l = Convert.ToInt32(Math.Ceiling((double)idv / (this.NumColumns * this.NumRows)));
                    int r = Convert.ToInt32(Math.Ceiling((double)(idv - (l - 1) * (this.NumColumns * this.NumRows)) / this.NumColumns));
                    int c = idv - (l - 1) * (this.NumColumns * this.NumRows) - (r - 1) * this.NumColumns;
                    Polygon poly = this.CreatePolygonPixel(c - 1, r - 1, l - 1, plane);
                    CRL idc = new CRL();
                    idc.c = c;
                    idc.r = r;
                    idc.l = l;
                    idc.id = idv;
                    PCRL p = new PCRL();
                    p.pol = poly;
                    p.id = idc;
                    lisp[idv - 1] = p;
                   // ids.Add(idv,);
                });


                cells = new List<ICell>();
                foreach (PCRL item in lisp)
                {
                    IFeature newF = GridLayer.DataSet.AddFeature(item.pol);

                    AddFeaturesData(item.id.id, item.id.c - 1, item.id.r - 1, layer - 1, newF, -9999, values[item.id.c - 1, item.id.r - 1].GetValue());
                    cells.Add(this.CreateCell(item.id.c - 1, item.id.r - 1, layer - 1));
                    //   newF.DataRow["Field"] = this.data[item.id.c, item.id.r, item.id.l];
                }



                RectangleFs = (FeatureSet)GridLayer.DataSet;
                RectangleFs.InitializeVertices();
                RectangleFs.UpdateExtent();
            }
            if (plane == DrawPlane.xz)
            {
                PCRL[] lisp = new PCRL[this.NumColumns * this.NumRows * this.NumLayers];


                Parallel.For(1, this.NumColumns * this.NumRows * this.NumLayers + 1, idv =>
                {
                    int l = Convert.ToInt32(Math.Ceiling((double)idv / (this.NumColumns * this.NumRows)));
                    int r = Convert.ToInt32(Math.Ceiling((double)(idv - (l - 1) * (this.NumColumns * this.NumRows)) / this.NumColumns));
                    int c = idv - (l - 1) * (this.NumColumns * this.NumRows) - (r - 1) * this.NumColumns;
                    Polygon poly = this.CreatePolygonPixel(c - 1, r - 1, l - 1, plane);
                    CRL idc = new CRL();
                    idc.c = c;
                    idc.r = r;
                    idc.l = l;
                    idc.id = idv;
                    PCRL p = new PCRL();
                    p.pol = poly;
                    p.id = idc;
                    lisp[idv - 1] = p;
                });


                cells = new List<ICell>();
                foreach (PCRL item in lisp)
                {
                    IFeature newF = GridLayer.DataSet.AddFeature(item.pol);
                    AddFeaturesData(item.id.id, item.id.c - 1, item.id.r - 1, item.id.l - 1, newF);
                    cells.Add(this.CreateCell(item.id.c - 1, item.id.r - 1, item.id.l - 1));
                    //   newF.DataRow["Field"] = this.data[item.id.c, item.id.r, item.id.l];
                }



                RectangleFs = (FeatureSet)GridLayer.DataSet;
                RectangleFs.InitializeVertices();
                RectangleFs.UpdateExtent();
            }

        }

        private static void LoadValuesFromRaster(IRaster raster, Dictionary<int, int> RCol, Dictionary<int, int> RRow, RasterCount[,] values)
        {
            for (int i = 0; i < raster.NumColumns; i++)
            {
                for (int j = 0; j < raster.NumRows; j++)
                {
                    double value = raster.Value[j, i];
                    int Gc = RCol[i];
                    int Gr = RRow[j];

                    if (value == raster.NoDataValue)
                    {
                        values[Gc, Gr].nulls++;

                    }
                    else
                    {
                        values[Gc, Gr].count++;
                        values[Gc, Gr].values += value;
                    }

                }
            }
        }

        private void CompleteRasterIndex(DrawPlane plane, IRaster raster, Dictionary<int, int> RCol, Dictionary<int, int> RRow)
        {
            for (int i = 0; i < this.NumColumns; i++)
            {
                Polygon poly = this.CreatePolygonPixel(i, 0, 0, plane);
                for (int ri = 0; ri < raster.NumColumns; ri++)
                {
                    Coordinate c = raster.CellToProj(0, ri);
                    IGeometry f = Geometry.FromBasicGeometry(new Feature(c));
                    if (poly.Contains(f))
                    {
                        RCol.Add(ri, i);
                    }

                }
            }

            for (int i = 0; i < this.NumRows; i++)
            {
                Polygon poly = this.CreatePolygonPixel(0, i, 0, plane);
                for (int ri = 0; ri < raster.NumRows; ri++)
                {
                    Coordinate c = raster.CellToProj(ri, 0);
                    IGeometry f = Geometry.FromBasicGeometry(new Feature(c));
                    if (poly.Contains(f))
                    {
                        RRow.Add(ri, i);
                    }

                }
            }
        }



        private object GetValueFea(IFeatureSet fea, Polygon poly)
        {
            if (FieldDataset == null)
                FieldDataset = "Index";

            foreach (IFeature item in fea.Features)
            {
                if(item.Contains(poly.Centroid.Centroid))
                {
                    return item.DataRow[FieldDataset];
                }
            }
            if (FieldType == typeof(string))
                return "N/A";
            else
            return -9999;
        }




        #endregion

        #region IField_Fields

        public int Period { set; get; }

        public int Step { set; get; }

        public double[, ,] Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        public string NameField
        {
            get
            {
                return this.nameField;
            }
            set
            {
                nameField = value;
            }
        }

        public int IdSample
        {
            get
            {
                return this.idSample;
            }
            set
            {
                this.idSample = value;
            }
        }

        public int Realization
        {
            get
            {
                return realization;
            }
            set
            {
                realization = value;
            }
        }

        public string Reference
        {
            get
            {
                return this.reference;
            }
            set
            {
                this.reference = value;
            }
        }

        public int OriginType
        {
            get
            {
                return this.originType;
            }
            set
            {
                this.originType = value;
            }
        }

        public bool IsSymmetricDimension
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region IField_Methods

        public bool IsReferencedFile()
        {
            if (reference == string.Empty)
                return false;
            else
                return true;
        }

        public abstract string PopulateFromReferenceFile(string file);

        public abstract void Open(Map mainMap, string file);

        public abstract void Save(string file);


        public double[, ,] GetValues()
        {
            return this.data;
        }

        public void PopulateValues(double[, ,] matrix)
        {
            if (this.data.Length == matrix.Length)
                this.data = matrix;
            else
                throw new ArgumentException("The matrix size is different");
        }

        public List<ICell> GetListCells(int[] id)
        {
            List<ICell> cells = new List<ICell>();
            for (int i = 0; i < id.Length; i++)
            {
                Cell cell = new Cell();
                cell.Id = area.ColRowbyId(id[i]);
                if (cell.Id[0] != -1 && cell.Id[1] != -1)
                {
                    cell.Width = area.ColumnsSize[cell.Id[0]];
                    cell.Height = area.RowsSize[cell.Id[1]];
                    cell.Value = this.data[cell.Id[0], cell.Id[1], 0];
                }
                cells.Add(cell);

            }
            return cells;
        }
        #endregion

        #region IAreaInterest_Fields

        public bool SymetricDimension
        {
            get { return area.SymetricDimension; }
            set { this.area.SymetricDimension = value; }
        }

        public double Azimut
        {
            get { return area.Azimut; }
            set { area.Azimut = value; }
        }

        public double CellSizeX
        {
            get { return area.CellSizeX; }
            set { area.CellSizeX = value; }
        }

        public double CellSizeZ
        {
            get { return area.CellSizeZ; }
            set { area.CellSizeZ = value; }
        }


        public double CellSizeY
        {
            get { return area.CellSizeY; }
            set { area.CellSizeY = value; }
        }

        public double[] ColumnsSize
        {
            get { return area.ColumnsSize; }
            set { area.ColumnsSize = value; }
        }

        public double[] LayerSize
        {
            get { return area.LayerSize; }
            set { area.LayerSize = value; }
        }

        public string Date
        {
            get { return area.Date; }
            set { area.Date = value; }
        }

        public string Description
        {
            get { return area.Description; }
            set { area.Description = value; }
        }

        public string LastChange
        {
            get { return area.LastChange; }
            set { area.LastChange = value; }
        }

        public double MaxX
        {
            get { return area.MaxX; }
            set { area.MaxX = value; }
        }

        public double MaxY
        {
            get { return area.MaxY; }
            set { area.MaxY = value; }
        }

        public double MinX
        {
            get { return area.MinX; }
            set { area.MinX = value; }
        }

        public double MinY
        {
            get { return area.MinY; }
            set { area.MinY = value; }
        }

        public string Name
        {
            get { return area.Name; }
            set { area.Name = value; }
        }

        public int NumColumns
        {
            get { return area.NumColumns; }
            set { area.NumColumns = value; }
        }

        public int NumRows
        {
            get { return area.NumRows; }
            set { area.NumRows = value; }
        }

        public int NumLayers
        {
            get { return area.NumLayers; }
            set { area.NumLayers = value; }
        }


        public string Projection
        {
            get { return area.Projection; }
            set { area.Projection = value; }
        }

        public double[] RowsSize
        {
            get { return area.RowsSize; }
            set { area.RowsSize = value; }
        }

        public string TypeCoor
        {
            get { return area.TypeCoor; }
            set { area.TypeCoor = value; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion






        internal void Export(string file, string field)
        {
            
        }

        public string FieldDataset { get; set; }

        public Type FieldType { get; set; }
    }
    public class Grid : VectorGrid
    {
        private IGridConfiguration g;


        public Grid(IAreaInterest area, double[, ,] data, string namefield, int idsample,
             int realization, string reference, int originType)
            : base()
        {
            this.area = (AreaInterest)area;
            this.Data = data;
            this.NameField = namefield;
            this.IdSample = idsample;
            this.Realization = realization;
            this.Reference = reference;
            this.OriginType = originType;
            this.TypeDomain = area.TypeDomain;

        }

        public Grid(IAreaInterest area, double[, ,] data, IMap map)
            : this(area, data, "", 0, 0, "", 0)
        {
            this.area = (AreaInterest)area;
            this.Data = data;
            this.TypeDomain = area.TypeDomain;
        }

        public Grid(AreaInterest area) :
            this(area, null, "", 0, 0, "", 0)
        {
            this.area = area;
            if (area.TypeDomain == Type_Domain.Grid)
            {
                FillData();
                this.TypeDomain = area.TypeDomain;
                this.UpdateAreaInterest(area);
            }
            else
            {

                this.Nodes = area.Nodes;
                this.Elements = area.Elements;
                this.TypeDomain = area.TypeDomain;

            }

        }


        public Grid(AreaInterest area, DimensionFM proj) :
            this(area, null, "", 0, 0, "", 0)
        {
            this.area = area;
            FillData();
            this.TypeDomain = area.TypeDomain;
            this.UpdateAreaInterest(area);

        }

        public Grid() :
            this(null, null, "", 0, 0, "", 0)
        {

        }

        public Grid(AreaInterest area, IGridConfiguration g)
        {
            // TODO: Complete member initialization
            this.area = area;
            this.g = g;
            this.Nodes = g.Nodes;
            this.Elements = g.Elements;
        }

        #region Grid_methods

        public void SetGrid(IField field)
        {
            this.area = new AreaInterest();

            this.area.Azimut = field.Azimut;
            this.area.CellSizeX = field.CellSizeX;
            this.area.CellSizeY = field.CellSizeY;
            this.area.CellSizeY = field.CellSizeZ;
            this.area.ColumnsSize = field.ColumnsSize;
            this.area.LayerSize = field.LayerSize;
            this.area.Date = field.Date;
            this.area.Description = field.Description;
            this.area.LastChange = field.LastChange;
            this.area.MaxX = field.MaxX;
            this.area.MaxY = field.MaxY;
            this.area.MinX = field.MinX;
            this.area.MinY = field.MinY;
            this.area.Name = field.Name;
            this.area.NumColumns = field.NumColumns;
            this.area.NumRows = field.NumRows;
            this.area.Projection = field.Projection;
            this.area.RowsSize = field.RowsSize;
            this.area.TypeCoor = field.TypeCoor;
            this.Nodes = field.Nodes;
            this.Elements = field.Elements;


            this.Data = field.Data;
            this.NameField = field.NameField;
            this.IdSample = field.IdSample;
            this.Realization = field.Realization;
            this.Reference = field.Reference;
            this.OriginType = field.OriginType;
            this.Period = field.Period;
            this.Step = field.Step;
        }

        public Grid CloneInfoNotData(IField field)
        {
            Grid g = new Grid();

            g.area = new AreaInterest();

            g.area.Azimut = field.Azimut;
            g.area.CellSizeX = field.CellSizeX;
            g.area.CellSizeY = field.CellSizeY;
            g.area.CellSizeY = field.CellSizeZ;
            g.area.ColumnsSize = field.ColumnsSize;
            g.area.LayerSize = field.LayerSize;
            g.area.Date = field.Date;
            g.area.Description = field.Description;
            g.area.LastChange = field.LastChange;
            g.area.MaxX = field.MaxX;
            g.area.MaxY = field.MaxY;
            g.area.MinX = field.MinX;
            g.area.MinY = field.MinY;
            g.area.Name = field.Name;
            g.area.NumColumns = field.NumColumns;
            g.area.NumRows = field.NumRows;
            g.area.Projection = field.Projection;
            g.area.RowsSize = field.RowsSize;
            g.area.TypeCoor = field.TypeCoor;
            g.area.TypeDomain = field.TypeDomain;


            if (field.TypeDomain == Type_Domain.Unstructured)
            {
                g.Nodes = field.Nodes;

                Dictionary<int, ElementMAD> el = new Dictionary<int, ElementMAD>();
                foreach (var item in this.Elements.Values)
                {
                    ElementMAD e = new ElementMAD();
                    e.id = item.id;
                    e.IdNodes = item.IdNodes;
                    e.numberElemenst = item.numberElemenst;
                    e.type = item.type;
                    e.value = item.value;
                    e.centroid = item.centroid;
                    e.group = item.group;


                    el.Add(item.id, e);

                }


                g.Elements = el;
            }

            if (field.TypeDomain == Type_Domain.Grid)
            {
                double[, ,] d = new double[field.Data.GetLength(0), field.Data.GetLength(1), field.Data.GetLength(2)];
                for (int i = 0; i < field.Data.GetLength(0); i++)
                {
                    for (int j = 0; j < field.Data.GetLength(1); j++)
                    {
                        for (int k = 0; k < field.Data.GetLength(2); k++)
                        {
                            d[i, j, k] = 0;
                        }

                    }
                }
                g.data = d;

            }



            g.NameField = field.NameField;
            g.IdSample = field.IdSample;
            g.Realization = field.Realization;
            g.Reference = field.Reference;
            g.OriginType = field.OriginType;
            g.Period = field.Period;
            g.Step = field.Step;
            return g;
        }

        public override string PopulateFromReferenceFile(string file)
        {
            RectangleFs = (FeatureSet)FeatureSet.Open(@file);

            if (!RectangleFs.FeatureType.Equals(FeatureType.Polygon))
            {
                System.Windows.Forms.MessageBox.Show("This is not a polygon shapefile.");
                return "";
            }



            foreach (Feature fea in RectangleFs.Features)
            {
                int i = Convert.ToInt32(fea.DataRow["col"]);
                int j = Convert.ToInt32(fea.DataRow["row"]);
                double d = Convert.ToDouble(fea.DataRow["Field"]);
                this.data[i - 1, j - 1, 0] = d;

            }


            return file;
        }

        public override void Open(Map mainMap, string file)
        {
            PopulateFromReferenceFile(@file);
            GridLayer = new MapPolygonLayer(RectangleFs);
            GridLayer.LegendText = Properties.Resources.VerctorGrid;

            //_rectangleLayer.LegendItemVisible = false;
            Color redColor = Color.Red.ToTransparent(0.8f);
            GridLayer.Symbolizer = new PolygonSymbolizer();
            // 
            GridLayer.Symbolizer.SetOutline(Color.Red, 1.0);
            GridLayer.Symbolizer.SetFillColor(Color.Transparent);
            GridLayer.SelectionSymbolizer = GridLayer.Symbolizer;
            GridLayer.Symbolizer.SetOutline(Color.Red, 1.0);
            // Extent ext = this.GetExtent(0.15);
            mainMap.Layers.Add(GridLayer);
        }

        public override void Save(string file)
        {
            RectangleFs.SaveAs(file, true);
        }


        #endregion



    }

    class Domain
    {
    }
}

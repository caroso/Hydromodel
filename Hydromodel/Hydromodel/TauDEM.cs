using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MadInterfaces.AuxiliarImplementations;
using MadInterfaces;
using DotSpatial.Controls;
using DotSpatial.Data;
using System.IO;
namespace Hydromodel
{
    public partial class TauDEM : Form
    {
        public IMap map;
        List<string> list = new List<string>()
            {
                "PitRemove","D8FlowDir",
                 "DinfFlowDir","AreaD8",
                 "AreaDinf", "GridNet",
                 "PeukerDouglas", "Threshold",
                 "D8FlowPathExtremeUp","SlopeArea","LengthArea",
                 "DropAnalysis","StreamNet",
                 "MoveOutletsToStreams",
                 "SlopeAreaRatio","D8HDistToStrm",
                 "DinfUpDependence","DinfDecayAccum",
                 "DinfTransLimAccum","DinfRevAccum",
                 "DinfDistUp","DinfAvalanche",
                 "SlopeAveDown"

            };



        public TauDEM(IMap m)
        {
            map=m;
            InitializeComponent();
            foreach (string item in list)
            {
                uxList.Items.Add(item);
            }
            Dictionary<string, string> rasters = GetList(map);
            foreach (string item in rasters.Keys)
            {
                uxListRasters.Items.Add(item);
            }
            
        }

        AuxConfigurationForm p;
        Dictionary<string, string> inputT = new Dictionary<string, string>();
        Dictionary<string, string> outputT = new Dictionary<string, string>();

        public void select(string command)
        {
            string[] v = Properties.Resources.values.Split('\n');
            List<string> input = new List<string>();
            List<string> output = new List<string>();
             inputT = new Dictionary<string, string>();
           outputT = new Dictionary<string, string>();
            foreach (string l in v)
            {
                string[] values= l.Trim().Split('\t');
                if (values[1].ToLower()== command.ToLower())
                {
                    if (values[2]=="input")
                    {
                        input.Add(values[0]);
                        inputT.Add(values[0], values[3]);
                       
                    }else
                    {
                        output.Add(values[0]);
                        outputT.Add(values[0], values[3]);
                    }
                }
            }


            string val = Parm(command, uxListRasters.Text, input, output, inputT, outputT);

            uxCommand.Text = val;
        }
        ConfigurationParameter[] ValuesParameters;
        private string Parm(string command, string dems, List<string> input, List<string> output, Dictionary<string, string> inputT, Dictionary<string, string> outputT)
        {
            Dictionary<string, string> rasters = GetList(map);
            string raster = GetList(rasters.Keys.ToArray());
            string folder = "";
            try
            {
                folder=Path.GetDirectoryName(rasters[dems]) + "\\";
            }catch
            {

            }
            List<ConfigurationParameter> listParameters = new List<ConfigurationParameter>();
            listParameters.Add(
               new ConfigurationParameter("folder", "String",folder , true, "Search_Folder", "Folder to save profile"));
            listParameters.Add(new ConfigurationParameter("raster", "String", "", true, raster, "Available raster"));

            foreach (string item in input)
            {
                bool found = false;
                if (inputT[item] == "-")
                {
                    listParameters.Add(new ConfigurationParameter(item, "String", "", true, "TextBox", "-" + item + " input"));
                }
                else
                {
                    string ext = inputT[item];
                    if (rasters.Count > 0)
                    {
                        foreach (string name in rasters.Keys)
                        {
                            if (name.Contains(item))
                            {
                                string file = rasters[name].ToLower().Replace("."+ext, "") + item + ".ext";
                                if (!File.Exists(file))
                                {
                                    file += "[doesn't exist]";
                                }
                                listParameters.Add(
                  new ConfigurationParameter(item, "String", file, true, "Search_File:"+ext, "-" + item + " input"));
                                // listParameters.Add(new ConfigurationParameter(item, "String", name, true, "TextBox", "-" + item + " input"));

                                found = true;
                                break;
                            }

                        }

                    }

                    if (!found)
                    {
                        string file = rasters[dems].ToLower().Replace("."+ext, "") + item + "."+ext;
                        if (!File.Exists(file))
                        {
                            file += "[doesn't exist]";
                        }

                        listParameters.Add(new ConfigurationParameter(item, "String", file, true, "Search_File:"+ext, "-" + item + " input"));
                    }
                }
              
               
            }

            foreach (string item in output)
            {
                string ext = outputT[item];
                if (ext == "-")
                {
                    listParameters.Add(new ConfigurationParameter(item, "String", "", true, "TextBox", "-" + item + " output"));
                    
                }
                else
                {
                    listParameters.Add(new ConfigurationParameter(item, "String", folder + dems + item + "."+ext, true, "Save_File:"+ext, "-" + item + " output"));

                }
            }


            p = new AuxConfigurationForm(listParameters.ToArray());
            p.Text = command;

            foreach (var item in p.Inputs)
	        {
		     if (item.Parameter.NameParameter=="raster")
             {
                 item.uxOptions.SelectionChangeCommitted +=uxOptions_TextChanged;

             }
             if (item.Parameter.NameParameter == "folder")
             {
                 item.uxValue.TextChanged += uxOptions_TextChanged2;

             }

	        }


            outputsv = new List<string>();
            if (DialogResult.OK == p.ShowDialog())
            {

     
                ConfigurationParameter[] ppo = p.List;
                if (ppo == null) return "";
                ValuesParameters = ppo;

                string p1 = command ;
                p1 += " ";


            
                foreach (string item in input){

                    if (AuxConfigurationForm.GetValue(item, ppo) != "")
                    {
                        p1 += " -" + item + " " + AuxConfigurationForm.GetValue(item, ppo);
                    }
                
                }
               foreach (string item in output){

                   if (AuxConfigurationForm.GetValue(item, ppo) != "")
                   {
                       outputsv.Add(AuxConfigurationForm.GetValue(item, ppo));
                       p1 += " -" + item + " " + AuxConfigurationForm.GetValue(item, ppo);
                   }
               }

               return p1;
            }

            return "";

        }
        List<string> outputsv = new List<string>();
        private void uxOptions_TextChanged(object sender, EventArgs e)
        {
            ComboBox c= sender as ComboBox;
            string folder = "";
            foreach (Input item in p.Inputs)
            {
                if (item.Parameter.NameParameter=="folder")
                {
                    folder = item.uxValue.Text;
                }
            }

            foreach (Input item in p.Inputs)
            {
                
                if ( item.uxLabel.Text.Contains("input") ||item.uxLabel.Text.Contains("output"))
                {
                    string ext = "";
                    if (item.uxLabel.Text.Contains("input")) ext = inputT[item.Parameter.NameParameter];
                    if (item.uxLabel.Text.Contains("output")) ext = outputT[item.Parameter.NameParameter];
                    item.uxValue.Text = folder + c.Text + item.Parameter.NameParameter + "." + ext;
                    item.Refresh();
                }
            }
        }

        private void uxOptions_TextChanged2(object sender, EventArgs e)
        {
            TextBox c = sender as TextBox;
            string name = "";
             foreach (Input item in p.Inputs)
            {
                if ( item.uxLabel.Text.Contains("raster"))
                {
                    name = item.uxOptions.Text;
                }
            }


            foreach (Input item in p.Inputs)
            {
                if ( item.uxLabel.Text.Contains("output"))
                {
                    string ext = "";
                    ext = outputT[item.Parameter.NameParameter];
                    item.uxValue.Text = c.Text + name + item.Parameter.NameParameter + "." + ext;
                   
                    item.Refresh();
                }
            }
        }

        private Dictionary<string, string> GetList(IMap map)
        {
            Dictionary<string, string> n = new Dictionary<string, string>();
            foreach (IMapRasterLayer item in map.GetRasterLayers())
            {
                string name = item.DataSet.Filename;
                n.Add(item.LegendText, name);
            }
            return n;
        }

         private string GetList(string[] p)
         {
             string l = "Options:";
             if (p.Length == 0) return l;
             foreach (string item in p)
             {
                 l += item + ",";
             }
             return l.Substring(0, l.Length - 1);
         }



        private void uxView_Click(object sender, EventArgs e)
        {
            if (uxList.Text == "") return;
            select(uxList.Text);
        }

        public enum Command
        {
            PitRemove, D8FlowDir,
            DinfFlowDir, AreaD8,
            AreaDinf, GridNet,
            PeukerDouglas, Threshold,
            D8FlowPathExtremeUp, SlopeArea, LengthArea,
            DropAnalysis, StreamNet,
            MoveOutletsToStreams,
            SlopeAreaRatio, D8HDistToStrm,
            DinfUpDependence, DinfDecayAccum,
            DinfTransLimAccum, DinfRevAccum,
            DinfDistUp, DinfAvalanche,
            SlopeAveDown, DinfConcLimAccum, DinfDistDown


        }

        private void RunFM(string file)
        {
            var e = System.Reflection.Assembly.GetExecutingAssembly().Location;

            System.Diagnostics.ProcessStartInfo p = new System.Diagnostics.ProcessStartInfo();
            p.WorkingDirectory = Path.GetDirectoryName(@file);
            p.FileName = file;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            p.CreateNoWindow = true;
            p.UseShellExecute = false;
            proc.StartInfo = p;
            proc.Start();
            proc.WaitForExit();

        }

        private void uxExecute_Click(object sender, EventArgs e)
        {
            string taudem = Configure.GetTauDEM();
            string command = uxCommand.Text;
            try
            {
                List<string> bat = new List<string>();
                bat.Add(taudem+ command);
                File.WriteAllLines(Path.GetTempPath() + "\\taudem.bat", bat.ToArray());
                RunFM(Path.GetTempPath() + "\\taudem.bat");

                foreach (string file in outputsv)
                {
                  if (File.Exists(file) && file.Contains(".tif"))
                  {
                      MapRasterLayer p = new MapRasterLayer(Raster.OpenFile(file));
                      p.LegendText=Path.GetFileName(file);
                      map.Layers.Add(p);

                  }
                  if (File.Exists(file) && file.Contains(".shp"))
                  {
                      IFeatureSet f = Shapefile.Open(file);
                      map.Layers.Add(f);

                  }
                  if (File.Exists(file) && file.Contains(".txt"))
                  {
                      string Fil = File.ReadAllText(file);
                      MessageBox.Show(Fil, file);

                  }

                }

            }
            catch
            {

            }
        }

        private void uxOpen_Click(object sender, EventArgs e)
        {

        }

        private void uxListRasters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void uxAppend_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}

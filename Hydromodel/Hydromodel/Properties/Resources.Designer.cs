﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hydromodel.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Hydromodel.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon TauDEM {
            get {
                object obj = ResourceManager.GetObject("TauDEM", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ad8	AreaD8	output	tif
        ///ad8	DropAnalysis	input	tif
        ///ad8	LengthArea	input	tif
        ///ad8	StreamNet	input	tif
        ///ad8	Threshold	input	tif
        ///ang	AreaDinf	input	tif
        ///ang	DinfAvalanche	input	tif
        ///ang	DinfConcLimAccum	input	tif
        ///ang	DinfDecayAccum	input	tif
        ///ang	DinfDistDown	input	tif
        ///ang	DinfDistUp	input	tif
        ///ang	DinfFlowDir	output	tif
        ///ang	DinfRevAccum	input	tif
        ///ang	DinfTransLimAccum	input	tif
        ///ang	DinfUpDependence	input	tif
        ///ass	DinfAvalanche	input	tif
        ///coord	StreamNet	output	dat
        ///cs	DinfTransLimAccum 	input	tif
        ///ctpt	 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string values {
            get {
                return ResourceManager.GetString("values", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Grid.
        /// </summary>
        internal static string VerctorGrid {
            get {
                return ResourceManager.GetString("VerctorGrid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GEOGCS[&quot;GCS_WGS_1984&quot;,DATUM[&quot;D_WGS_1984&quot;,SPHEROID[&quot;WGS_1984&quot;,6378137,298.257223562997]],PRIMEM[&quot;Greenwich&quot;,0],UNIT[&quot;Degree&quot;,0.0174532925199433]].
        /// </summary>
        internal static string wgs_84_esri_string {
            get {
                return ResourceManager.GetString("wgs_84_esri_string", resourceCulture);
            }
        }
    }
}

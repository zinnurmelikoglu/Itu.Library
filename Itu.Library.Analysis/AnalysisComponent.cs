using Eto.Forms;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Collections;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace Itu.Library.Analysis
{
  public class AnalysisComponent : GH_Component
  {
    /// <summary>
    /// Each implementation of GH_Component must provide a public 
    /// constructor without any arguments.
    /// Category represents the Tab in which the component will appear, 
    /// Subcategory the panel. If you use non-existing tab or panel names, 
    /// new tabs/panels will automatically be created.
    /// </summary>
    public AnalysisComponent()
      : base("AnalysisComponent", "Analysis",
        "Understanding of Notre-Dame du Haut Roman Catholic chapel in Ronchamp",
        "Curve", "Analysis")
    {
    }

    /// <summary>
    /// Registers all the input parameters for this component.
    /// </summary>
    protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
    {
      pManager.AddCurveParameter("curve", "cL", "Curve for analysis", GH_ParamAccess.item);
      //pManager.AddParameter(IGH_Param. DataType , "cL", "plGeometry", "Geometry for analysis", GH_ParamAccess.item);
    }

    /// <summary>
    /// Registers all the output parameters for this component.
    /// </summary>
    protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
    {
      pManager.AddCurveParameter("retLine", "rL", "Line List", GH_ParamAccess.list);
      pManager.AddTextParameter("retCloseness", "rS", "Closeness", GH_ParamAccess.list);
      pManager.AddTextParameter("retIntersect", "rI", "Intersect", GH_ParamAccess.list);
      pManager.AddTextParameter("retAlignedStatus", "rA", "Aligned Element Status", GH_ParamAccess.list);
    }

    /// <summary>
    /// This is the method that actually does the work.
    /// </summary>
    /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
    /// to store data in output parameters.</param>
    protected override void SolveInstance(IGH_DataAccess DA)
    {
      Curve curve = null;
      DA.GetData("curve", ref curve);

      Alignment.AlignmentComponent alignemnt = new Alignment.AlignmentComponent();
      var alignedElementStatusList = alignemnt._AlignedElementStatusList;

    }

    /// <summary>
    /// Provides an Icon for every component that will be visible in the User Interface.
    /// Icons need to be 24x24 pixels.
    /// You can add image files to your project resources and access them like this:
    /// return Resources.IconForThisComponent;
    /// </summary>
    protected override System.Drawing.Bitmap Icon => null;

    /// <summary>
    /// Each component must have a unique Guid to identify it. 
    /// It is vital this Guid doesn't change otherwise old ghx files 
    /// that use the old ID will partially fail during loading.
    /// </summary>
    public override Guid ComponentGuid => new Guid("2a7359c9-1f89-4fdb-a42a-ec2c4a9ad40f");
  }
}
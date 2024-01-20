using Grasshopper;
using Grasshopper.Documentation;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using Itu.Library.Alignment.Prepare;
using Itu.Library.Alignment.Util;
using Rhino.Display;
using Rhino.DocObjects.Custom;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Itu.Library.Alignment
{
  public class AlignmentComponent : GH_Component
  {
    public AlignedElementStatusList _AlignedElementStatusList { get; set; }

    /// <summary>
    /// Each implementation of GH_Component must provide a public 
    /// constructor without any arguments.
    /// Category represents the Tab in which the component will appear, 
    /// Subcategory the panel. If you use non-existing tab or panel names, 
    /// new tabs/panels will automatically be created.
    /// </summary>
    public AlignmentComponent()
      : base("AlignmentComp", "Alignment",
        "Understanding of Notre-Dame du Haut Roman Catholic chapel in Ronchamp",
        "Curve", "Alignment")
    //"The thesis for Istanbul Technical University", "The Facade Analysis")
    {
      //_base = new EntityBase();
    }

    /// <summary>
    /// Registers all the input parameters for this component.
    /// </summary>
    protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
    {

      pManager.AddCurveParameter("curveList", "cL", "Curve for analysis", GH_ParamAccess.list);
      pManager.AddRectangleParameter("area", "ar", "A rectancgle draw a border of the facade", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY, 800.0, 500.0));
      int index = pManager.AddCurveParameter("curveBase", "cB", "Base curve for analysis", GH_ParamAccess.item);
      this.Params.Input[index].Optional = true;

    }

    /// <summary>
    /// Registers all the output parameters for this component.
    /// </summary>
    protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
    {
      pManager.AddTextParameter("out", "out", "AlignmentFactor", GH_ParamAccess.item);
      //pManager.AddTextParameter("retFactor", "rF", "AlignmentFactor", GH_ParamAccess.item);
      pManager.AddCurveParameter("retLine", "rL", "Line List", GH_ParamAccess.list);
      pManager.AddTextParameter("retCloseness", "rS", "Closeness", GH_ParamAccess.list);
      pManager.AddTextParameter("retIntersect", "rI", "Intersect", GH_ParamAccess.list);
      pManager.AddTextParameter("retAlignedStatus", "rA", "Aligned Element Status", GH_ParamAccess.list);
      pManager.AddTextParameter("retTag", "rT", "Text Tag", GH_ParamAccess.list);


      //retAlignedStatusJson

    }

    /// <summary>
    /// This is the method that actually does the work.
    /// </summary>
    /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
    /// to store data in output parameters.</param>
    protected override void SolveInstance(IGH_DataAccess DA)
    {
      _AlignedElementStatusList = new AlignedElementStatusList();
      List<Curve> curveList = new List<Curve>();
      Curve curveBase = Curve.CreateControlPointCurve(new List<Point3d>());
      Rectangle3d area = new Rectangle3d();
      var dictName = "GeometryList";

      DA.GetDataList("curveList", curveList);
      DA.GetData("area", ref area);
      DA.GetData("curveBase", ref curveBase);

      #region Specifying Facade Area

      Rectangle3d rect = area;
      var facadeArea = new FacadeArea(rect.Width, rect.Height);
      EntityBase.SetValue("area", facadeArea);
      var listCount = curveList.Count();

      #endregion


      #region Filling Polyline Geometry List

      /*
      Determined the whole nurb points
      Converted curves to polyline by using detemined nurbs location
      Every created polyline is delegated by PLGeometry derived by Polyline
      GeometryList is filled by PLGeometry
      */

      List<Point3d> point;
      List<PLGeometry> geometryList = new List<PLGeometry>();
      int i = 0;
      foreach (var _curve in curveList)
      {
        point = new List<Point3d>();
        var controlPoint = _curve.ToNurbsCurve().Points;
        var geometryName = i.ToString();

        foreach (var item in controlPoint)
        {
          point.Add(item.Location);

        }

        geometryList.Add(new PLGeometry(point) { GeometryName = geometryName, isSelected = _curve != null ? new CurveEquality(_curve, curveBase).Equals() : false });
        i++;

      }

      EntityBase.RemoveDictionary(dictName);
      EntityBase.SetValue(dictName, geometryList);

      #endregion


      #region Filling ElementList

      /*
      Every single (poly)line which is the part of main geometry is filled to ElementList
      */

      List<PLElement> ElementList = new List<PLElement>();
      foreach (var geometry in geometryList)
      {
        var elementList = geometry.GetElementList();
        ElementList.AddRange(elementList);

      }

      #endregion


      #region Calculation and Drawing Alignment

      /*
      Temporarily I assigned rounded tangent value.
      This assignment provide us to draw two differen line and can be seen how many lines we have
      Which means we prevent lines overwritten.

      GeometryList has all geometries assigned as a curve
      TempGeometryList has all geometries except selected
      All geometry includes elements extracted by GetElementList method
      */

      List<Line> lineList = new List<Line>();
      List<double> ClosenessList = new List<Double>();
      List<int> InBetweenList = new List<int>();
      List<PLGeometry> storageList = new List<PLGeometry>();
      CompareGeometryList compareList = new CompareGeometryList();
      var delegateGeometryList = geometryList.Where(s => s.isSelected).Any() ? geometryList.Where(s => s.isSelected) : geometryList;

      foreach (var geometry in delegateGeometryList)
      {
        storageList.Add(geometry);
        var tempGeometryList = geometryList.Except(storageList);
        //var tempGeometryList = geometryList.Except(new List<PLGeometry>(){geometry}); It is marked so that matched geometry may be matched in the future

        foreach (var tempGeometry in tempGeometryList)
        {
          //var remainList = geometryList.Except(new List<PLGeometry>() { geometry, tempGeometry }).ToList();
          compareList.AddGeometry(new CompareGeometry(geometry, tempGeometry));
        }
      }

      /*
      CompareList is filled out with all matches regardless of whether they are aligned
      Compare method is called in the CompareList full of CompareGeometry
      lineList, ClosenessList and result are requested from CompareList IEnumerable Class by calling suitable methods giving matched geometry information
      */
      compareList.compareGeometryList.ForEach(compare => { compare.Compare(); });  //Lets compare each geometry


      lineList = compareList.GetAlignedLineList();
      ClosenessList = compareList.GetAlignedClosenessList();
      InBetweenList = compareList.GetInBetweenGeometryCount();
      double result = compareList.GetFactor();

      var alignedElementStatusList = compareList.GetAlignedElementStatusList();
      _AlignedElementStatusList.AddRangeAlignedElement(alignedElementStatusList);
   
      #endregion

      //var alignedTreeTest = new PrepareDataTree(_AlignedElementStatusList).GetDataTree<object>();
      var outputList =  _AlignedElementStatusList.Select(s => new OutputParam { AlignedLine = s.AlignedLine, AlignedCloseness = s.ClosenessFactor, InBetweenGeometryCount = s.InBetweenGeometryCount }).ToList();
      var alignedTree = new PrepareDataTree<OutputParam>(outputList).GetDataTree();

      //DA.SetData("retFactor", result);
      DA.SetDataList("retLine", lineList);
      DA.SetDataList("retCloseness", ClosenessList);
      DA.SetDataList("retIntersect", InBetweenList);
      DA.SetDataList("retAlignedStatus", alignedElementStatusList);
      DA.SetDataTree(0, alignedTree);
      DA.SetDataList("retTag", alignedTree.Branches);

    }

    protected class DataTreeObject
    {
      //public string GeometryName { get; set; }
      public double Closeness { get; set; }
      public int InBetweenCount { get; set; }
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
    public override Guid ComponentGuid => new Guid("aee4fca0-7810-4579-aab4-948b71a85ea2");
  }
}
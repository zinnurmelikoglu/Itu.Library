using Grasshopper;
using Grasshopper.Kernel;
using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using Itu.Library.Alignment.Util;
using Rhino.Collections;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Itu.Library.Alignment
{
    public class AlignmentComponent : GH_Component
  {
    //EntityBase _base { get; set; }

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
        "Curve", "Analysis")
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
      pManager.AddNumberParameter("curveNumber", "cN", "Curve number for analysis", GH_ParamAccess.item);

    }

    /// <summary>
    /// Registers all the output parameters for this component.
    /// </summary>
    protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
    {
      pManager.AddTextParameter("out", "out", "AlignmentFactor", GH_ParamAccess.item);
      pManager.AddTextParameter("retFactor", "rF", "AlignmentFactor", GH_ParamAccess.item);
      pManager.AddCurveParameter("retLine", "rL", "Line List", GH_ParamAccess.list);
      pManager.AddTextParameter("retStrength", "rS", "Strength", GH_ParamAccess.list);
      pManager.AddTextParameter("retIntersect", "rI", "Intersect", GH_ParamAccess.list);

    }

    /// <summary>
    /// This is the method that actually does the work.
    /// </summary>
    /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
    /// to store data in output parameters.</param>
    protected override void SolveInstance(IGH_DataAccess DA)
    {
      List<Curve> curveList = new List<Curve>();
      Rectangle3d area = new Rectangle3d();

      DA.GetDataList("curveList", curveList);
      DA.GetData("area", ref area);

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
      foreach (var _curve in curveList)
      {
        point = new List<Point3d>();
        var controlPoint = _curve.ToNurbsCurve().Points;

        foreach (var item in controlPoint)
        {
          point.Add(item.Location);

        }

        geometryList.Add(new PLGeometry(point));

      }

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
      List<Double> StrengthList = new List<Double>();
      List<int> IntersectList = new List<int>();
      List<PLGeometry> storageList = new List<PLGeometry>();
      CompareGeometryList compareList = new CompareGeometryList();

      foreach (var geometry in geometryList)
      {
        storageList.Add(geometry);
        //var tempGeometryList = geometryList.Except(new List<PLGeometry>(){geometry}); It is marked so that matched geometry may be matched in the future
        var tempGeometryList = geometryList.Except(storageList);
        
        foreach (var tempGeometry in tempGeometryList)
        {
          var restList =  geometryList.Except(new List<PLGeometry>() { geometry, tempGeometry }).ToList() ;
          compareList.AddGeometry(new CompareGeometry(geometry, tempGeometry, restList));
        }
      }

      /*
      CompareList is filled out with all matches regardless of whether they are aligned
      Compare method is called in the CompareList full of CompareGeometry
      lineList, StrenghtList and result are requested from CompareList IEnumerable Class by calling suitable methods giving matched geometry information
      */
      compareList.compareGeometryList.ForEach(compare => { compare.Compare(); });  //Lets compare each geometry

      lineList = compareList.GetLineList();
      StrengthList = compareList.GetAlignedStrengthList();
      IntersectList = compareList.GetIntersectGeometryCount();
      double result = compareList.GetFactor();


      //var testOutputList = compareList.GetAlignedElementStatusList();

      #endregion

      DA.SetData("retFactor", result);
      DA.SetDataList("retLine", lineList);
      DA.SetDataList("retStrength", StrengthList );
      DA.SetDataList("retIntersect", IntersectList);

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
using Grasshopper;
using Grasshopper.Kernel;
using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.Correspond;
using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
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
        "Curve", "Primitive")
    //"The thesis for Istanbul Technical University", "The Facade Analysis")
    {
    }

    /// <summary>
    /// Registers all the input parameters for this component.
    /// </summary>
    protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
    {

      pManager.AddCurveParameter("curveList", "cL", "Curve for analysis", GH_ParamAccess.list);
      pManager.AddRectangleParameter("area", "ar", "A rectancgle draw a border of the facade", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY, 800.0, 500.0));

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
      pManager.AddTextParameter("testOutput", "Out", "Output", GH_ParamAccess.item);

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

      int counter = 0;
      List<Line> lineList = new List<Line>();
      List<AlignmentStatus> alignmentList = new List<AlignmentStatus>();
      List<Double> StrengthList = new List<Double>();


      List<PLGeometry> storageList = new List<PLGeometry>();
      CorrespondList correspondList = new CorrespondList();
      foreach (var geometry in geometryList)
      {
        storageList.Add(geometry);
        //var tempGeometryList = geometryList.Except(new List<PLGeometry>(){geometry});
        var tempGeometryList = geometryList.Except(storageList);
        
        foreach (var tempGeometry in tempGeometryList)
        {
          correspondList.AddGeometry(new CorrespondGeometry(geometry, tempGeometry));
        }
      }


      foreach (var correspond in correspondList) {

        correspond.Compare();

      }

      //List<PLGeometry> storageList = new List<PLGeometry>();
      //foreach (var geometry in geometryList)
      //{
      //  storageList.Add(geometry);
      //  //var tempGeometryList = geometryList.Except(new List<PLGeometry>(){geometry});
      //  var tempGeometryList = geometryList.Except(storageList);
      //  var elementList = geometry.GetElementList();

      //  foreach (var element in elementList)
      //  {
      //    var tanVal = element.TanVal;
      //    var ref_X = element.Ref_X;
      //    var ref_Y = element.Ref_Y;

      //    double neutral = 0.0;
      //    Point3d pointFirst = new Point3d(ref_X, neutral, neutral);
      //    Point3d pointSecond = new Point3d(ref_X, ref_Y, neutral);

      //    foreach (var tempGeometry in tempGeometryList)
      //    {

      //      var comparedGeometry = geometryList.Intersect(new List<PLGeometry>() { tempGeometry });
      //      var tempElementList = tempGeometry.GetElementList();
      //      foreach (var temp in tempElementList)
      //      {
      //        CompareFactory compareFactory = new CompareFactory(element, temp);
      //        ICompare compare = compareFactory.CompareType();
      //        if (compare.CompareElement())
      //        {
      //          lineList.Add(new DrawAlignment(element.PointFirst.X, element.PointFirst.Y, tanVal, facadeArea).GenerateAlignment());

      //          var strength = compare.AlignmentStrength();
      //          //StrengthList.Add(tanVal);
      //          StrengthList.Add(strength);

      //          counter++;

      //          geometry.isAligned = true;
      //          geometry.AlignedGeometry.AddRange(comparedGeometry);

      //        }

      //      }
      //    }

      //  }
      //}

      #endregion







      double result = (double)counter / (listCount * 4);
      //Print(result.ToString());
      //Print(counter.ToString());

      
      DA.SetData("retFactor", result);
      DA.SetData("retLine", lineList);
      //DA.SetData("retStrength", );
      //DA.SetData("testOutput", );
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
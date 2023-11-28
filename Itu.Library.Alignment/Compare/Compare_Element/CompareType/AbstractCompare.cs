using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public abstract class AbstractCompare
  {
    public virtual PLElement Element_First { get; set; }
    public virtual PLElement Element_Second { get; set; }
    public virtual double TolerateVal { get; set; }
    public abstract TangentType TangentType { get; }
    public virtual bool isAligned
    {
      get { return _isAligned; }
      set
      {
        if (value)
        {
          AlignmentStrength();
          AlignmentLine();
        }
        _isAligned = value;
      }
    }
    private bool _isAligned;
    public virtual Point3d point_First => Element_First.Element.ClosestPoint(Element_Second.PointFirst);
    public virtual Point3d point_Second => Element_Second.Element.ClosestPoint(Element_First.PointFirst);
    protected virtual AlignedElementStatus _AlignedElementStatus { get; set; }
    
    public AbstractCompare() { }
    public AbstractCompare(PLElement element_First, PLElement element_Second)
    {
      Element_First = element_First;
      Element_Second = element_Second;
      TolerateVal = 5.0;
      _AlignedElementStatus = new AlignedElementStatus();
    }

    //CurveProxy.GetDistancesBetweenCurves may be used
    public abstract bool CompareElement();
    public virtual AlignedElementStatus GetAlignedElementStatus()
    {
      AlignmentStrength();
      AlignmentLine();
      return _AlignedElementStatus;
    }
    protected virtual double AlignmentStrength()
    {
      var length_base = Element_First.Element.Length;
      var length_temp = Element_Second.Element.Length;

      Point3d point_First = Element_First.Element.ClosestPoint(Element_Second.PointFirst);
      Point3d point_Second = Element_Second.Element.ClosestPoint(Element_First.PointFirst);

      double distance = point_First.DistanceTo(point_Second);
      double plDistance = length_base + length_temp;
      return _AlignedElementStatus.AlignedStrength = plDistance / (plDistance + Math.Abs(distance));
    }
    protected virtual Line AlignmentLine()
    {
      return _AlignedElementStatus.AlignedLine = new DrawAlignment(point_First, point_Second, TangentType).GenerateAlignmentLine();
    }

    public List<CurveIntersections> GetIntersectGeometryList(List<PLGeometry> geometryList)
    {
      var intersectGeometryList = new List<CurveIntersections>();
      Line line = this.AlignmentLine();
      
      foreach (var geometry in geometryList)
      {
        double neutral = 0.0;
        var intersect = Intersection.CurveLine(geometry.ToPolylineCurve(), line, neutral, neutral);
        if (intersect.Count > 0)
          intersectGeometryList.Add(intersect);

      }

      return _AlignedElementStatus.IntersectGeometryList = intersectGeometryList;
    }
    //public int GetIntersectGeometryCount()=> IntersectGeometryList.Count;

    

  }
}

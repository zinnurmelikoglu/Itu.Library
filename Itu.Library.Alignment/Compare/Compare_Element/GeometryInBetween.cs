using Itu.Library.Alignment.Geometry;
using Rhino.Geometry.Intersect;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Itu.Library.Alignment.Compare
{
  internal class GeometryInBetween
  {
    //protected AlignedElementStatus _AlignedElementStatus { get; set; }
    protected List<PLGeometry> GeometryList { get; set; }
    protected AbstractCompare _AbstactCompany { get; set; }
    public GeometryInBetween(List<PLGeometry> geometryList, AbstractCompare abstractCompare)
    {
      GeometryList= geometryList;
      _AbstactCompany= abstractCompare;
    }
    protected List<CurveIntersections> GetIntersectGeometryList(AlignedElementStatus alignedElementStatus)
    {
      var intersectGeometryList = new List<CurveIntersections>();
      Line line = _AbstactCompany.AlignmentLine();

      foreach (var geometry in GeometryList)
      {
        double neutral = 0.0;
        var intersect = Intersection.CurveLine(geometry.ToPolylineCurve(), line, neutral, neutral);
        if (intersect.Count > 0)
        {
          //intersectGeometryList.Add(intersect);

          Point3d intersectPoint = intersect[0].PointA;
          var distanceMax = line.MaximumDistanceTo(intersectPoint);
          var length = line.Length;

          if (length >= distanceMax)
            intersectGeometryList.Add(intersect);
        }

      }

      return alignedElementStatus.IntersectGeometryList = intersectGeometryList;
    }

  }
}

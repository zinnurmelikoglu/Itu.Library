using Rhino.Geometry.Intersect;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itu.Library.Alignment.Geometry;
using Itu.Library.Alignment.Util;

namespace Itu.Library.Alignment.Compare
{
  internal class InBetweenProp:CommonProp
  {
    List<CurveIntersections> IntersectGeometryList { get; set; }
    //ElementCouple _ElementCouple { get; }
    public InBetweenProp(ElementCouple elementCouple):base (elementCouple) => _ElementCouple = elementCouple;

    public List<CurveIntersections> GetIntersectGeometryList()
    {
      IntersectGeometryList = new List<CurveIntersections>();
      var geometryCouple = _ElementCouple.GetGeometryCouple();
      var remainGeometry = geometryCouple.Geometry_Remain;
      Line line = _ElementCouple.AlignmentLine;

      foreach (var geometry in remainGeometry)
      {
        double neutral = 0.0;
        var intersect = Intersection.CurveLine(geometry.ToPolylineCurve(), line, neutral, neutral);
        if (intersect.Count > 0)
        {
          Point3d intersectPoint = intersect[0].PointA;
          var distanceMax = line.MaximumDistanceTo(intersectPoint);
          var length = line.Length;

          if (length >= distanceMax)
            IntersectGeometryList.Add(intersect);
        }

      }

      return IntersectGeometryList;
    }
    public int IntersectGeometryCount => IntersectGeometryList.Count;

  }
}

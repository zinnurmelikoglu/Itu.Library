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
  internal class InBetweenProp : CommonProp
  {
    List<CurveIntersections> InBetweenGeometryList { get; set; }
    public InBetweenFactor _InBetweenFactor { get; set; }
    public InBetweenProp(ElementCouple elementCouple) : base(elementCouple)
    {
      _ElementCouple = elementCouple;
      _InBetweenFactor = new InBetweenFactor();
    }

    public List<CurveIntersections> GetInBetweenGeometryList()
    {
      InBetweenGeometryList = new List<CurveIntersections>();
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
            InBetweenGeometryList.Add(intersect);
        }

      }

      return InBetweenGeometryList;
    }
    public int InBetweenGeometryCount => InBetweenGeometryList.Count;

    public double InBetweenFactor()
    {
      var count = InBetweenGeometryCount;
      var inBetweenFactor = Math.Max(1 - 0.4 * count, 0);
      _InBetweenFactor.Factor = inBetweenFactor;
      _ElementCouple._LikelihoodFactorList.AddLikelihoodFactor(_InBetweenFactor);

      return inBetweenFactor;
    }

  }

}

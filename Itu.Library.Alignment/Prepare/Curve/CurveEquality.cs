using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Prepare
{
  class CurveEquality
  {
    Curve FirstCurve { get; }
    Curve SecondCurve { get; }

    public CurveEquality(Curve firstCurve, Curve secondCurve)
    {
      FirstCurve = firstCurve;
      SecondCurve = secondCurve;
    }

    public bool Equals()
    {
      var firstCurve = FirstCurve;
      var secondCurve = SecondCurve;

      if (firstCurve != null && secondCurve != null)
      {
        var firstEquality = firstCurve.PointAtStart.Equals(secondCurve.PointAtStart);
        var secondEquality = firstCurve.PointAtEnd.Equals(secondCurve.PointAtEnd);

        return firstEquality && secondEquality;
      }
      
      return false;

    }

  }
}

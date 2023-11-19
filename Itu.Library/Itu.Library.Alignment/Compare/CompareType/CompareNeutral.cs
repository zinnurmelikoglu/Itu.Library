using Itu.Library.Alignment.Element;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class CompareNeutral : ICompare
  {
    public PLElement PLElement_Base { get; set; }
    public PLElement PLElement_Temp { get; set; }
    public double TolerateVal { get; set; }

    public CompareNeutral(PLElement plElement_Base, PLElement plElement_Temp)
    {
      PLElement_Base = plElement_Base;
      PLElement_Temp = plElement_Temp;
      TolerateVal = 5.0;
    }

    public Boolean CompareElement()
    {
      double neutral = 0.0;
      double tanVal_base = PLElement_Base.TanVal_Rounded;
      double tanVal_temp = PLElement_Temp.TanVal_Rounded;
      double ref_Y_base = PLElement_Base.Ref_Y;
      double ref_Y_temp = PLElement_Temp.Ref_Y;

      //return tanVal_base == neutral && tanVal_temp == neutral && ref_Y_base == ref_Y_temp ? true : false;
      return tanVal_base == neutral && tanVal_temp == neutral &&
        (ref_Y_base > (ref_Y_temp - TolerateVal)) &&
        ((ref_Y_temp + TolerateVal) > ref_Y_base) ? true : false;

    }

    public double AlignmentStrength()
    {
      var length_base = PLElement_Base.Element.Length;
      var length_temp = PLElement_Temp.Element.Length;

      Point3d basePoint = ((Polyline)PLElement_Base.Element).ClosestPoint(new Point3d(PLElement_Temp.PointFirst.X, PLElement_Base.PointFirst.Y, 0.0));
      var disFirst = basePoint.DistanceTo(PLElement_Temp.PointFirst);
      var disSecond = basePoint.DistanceTo(PLElement_Temp.PointSecond);

      double distance = disFirst > disSecond ? disSecond : disFirst;
      return (length_base + length_temp) / (length_base + length_temp + Math.Abs(distance));
    }

  }
}

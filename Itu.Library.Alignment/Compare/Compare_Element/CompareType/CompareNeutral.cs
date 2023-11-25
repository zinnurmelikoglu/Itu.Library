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
    public PLElement PLElement_First { get; set; }
    public PLElement PLElement_Second { get; set; }
    public double TolerateVal { get; set; }
    public TangentType TangentType => TangentType.Neutral;

    public CompareNeutral(PLElement plElement_First, PLElement plElement_Second)
    {
      PLElement_First = plElement_First;
      PLElement_Second = plElement_Second;
      TolerateVal = 5.0;
    }

    public Boolean CompareElement()
    {
      double neutral = 0.0;
      double tanVal_base = PLElement_First.TanVal_Rounded;
      double tanVal_temp = PLElement_Second.TanVal_Rounded;
      double ref_Y_base = PLElement_First.Ref_Y;
      double ref_Y_temp = PLElement_Second.Ref_Y;

      //return tanVal_base == neutral && tanVal_temp == neutral && ref_Y_base == ref_Y_temp ? true : false;
      //return tanVal_base == neutral && tanVal_temp == neutral &&
      //  (ref_Y_base > (ref_Y_temp - TolerateVal)) &&
      //  ((ref_Y_temp + TolerateVal) > ref_Y_base) ? true : false;

      return ((tanVal_base == neutral && tanVal_temp == neutral) && (Math.Abs(ref_Y_base - ref_Y_temp) <= TolerateVal));

    }

    public AlignedElement GetAlignedElement()
    {
      return new AlignedElement(PLElement_First, PLElement_Second);
    }

    public double AlignmentStrength()
    {
      var length_base = PLElement_First.Element.Length;
      var length_temp = PLElement_Second.Element.Length;

      Point3d point_First = ((Polyline)PLElement_First.Element).ClosestPoint(PLElement_Second.PointFirst);
      Point3d point_Second = ((Polyline)PLElement_Second.Element).ClosestPoint(PLElement_First.PointFirst);

      double distance = point_First.DistanceTo(point_Second);
      double plDistance = length_base + length_temp;
      return plDistance / (plDistance + Math.Abs(distance));
    }

  }

}

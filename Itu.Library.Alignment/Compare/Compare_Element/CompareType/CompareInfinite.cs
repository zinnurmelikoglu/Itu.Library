using Itu.Library.Alignment.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
    public class CompareInfinite : ICompare
  {
    public PLElement PLElement_First { get; set; }
    public PLElement PLElement_Second { get; set; }
    public double TolerateVal { get; set; }
    public TangentType TangentType => TangentType.Infinite;

    public CompareInfinite(PLElement plElement_Base, PLElement plElement_Temp)
    {
      PLElement_First = plElement_Base;
      PLElement_Second = plElement_Temp;
      TolerateVal = 5.0;
    }

    public Boolean CompareElement()
    {
      double tanVal_base = PLElement_First.TanVal_Rounded;
      double tanVal_temp = PLElement_Second.TanVal_Rounded;
      double ref_X_base = PLElement_First.Ref_X;
      double ref_X_temp = PLElement_Second.Ref_X;

      //return Double.IsInfinity(tanVal_temp) && ref_X_base == ref_X_temp ? true : false;
      //return Double.IsInfinity(tanVal_temp) &&
      //  (ref_X_base > (ref_X_temp - TolerateVal)) &&
      //  ((ref_X_temp + TolerateVal) > ref_X_base) ? true : false;

      return ((Double.IsInfinity(tanVal_base) && (Double.IsInfinity(tanVal_temp))) && (Math.Abs(ref_X_base - ref_X_temp) <= TolerateVal));

    }
    public AlignedElement GetAlignedElement()
    {
      return new AlignedElement(PLElement_First, PLElement_Second);
    }

    //    public double AlignmentStrength()
    //    {
    //      var length_base = PLElement_Base.Element.Length;
    //      var length_temp = PLElement_Temp.Element.Length;
    //
    //      Point3d basePoint = ((Polyline) PLElement_Base.Element).ClosestPoint(new Point3d(PLElement_Base.PointFirst.X, PLElement_Temp.PointFirst.Y, 0.0));
    //      var disFirst = basePoint.DistanceTo(PLElement_Temp.PointFirst);
    //      var disSecond = basePoint.DistanceTo(PLElement_Temp.PointSecond);
    //
    //      double distance = disFirst > disSecond ? disSecond : disFirst;
    //      return (length_base + length_temp) / (length_base + length_temp + Math.Abs(distance));
    //    }

    public double AlignmentStrength() { return 0.0; }
  }
}

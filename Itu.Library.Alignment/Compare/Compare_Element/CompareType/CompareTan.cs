using Itu.Library.Alignment.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class CompareTan : ICompare
  {
    public PLElement PLElement_Base { get; set; }
    public PLElement PLElement_Temp { get; set; }
    public double TolerateVal { get; set; }

    public CompareTan(PLElement plElement_Base, PLElement plElement_Temp)
    {
      PLElement_Base = plElement_Base;
      PLElement_Temp = plElement_Temp;
      TolerateVal = 5.0;
    }

    public Boolean CompareElement()
    {
      double tanVal_base = PLElement_Base.TanVal_Rounded;
      double tanVal_temp = PLElement_Temp.TanVal_Rounded;
      double ref_X_base = PLElement_Base.Ref_X;
      double ref_X_temp = PLElement_Temp.Ref_X;
      double ref_Y_base = PLElement_Base.Ref_Y;
      double ref_Y_temp = PLElement_Temp.Ref_Y;

      //return tanVal_base == tanVal_temp && ref_X_base == ref_X_temp && ref_Y_base == ref_Y_temp ? true : false;
      return tanVal_base == tanVal_temp &&
        (ref_X_base > (ref_X_temp - TolerateVal)) &&
        ((ref_X_temp + TolerateVal) > ref_X_base) &&
        (ref_Y_base > (ref_Y_temp - TolerateVal)) &&
        ((ref_Y_temp + TolerateVal) > ref_Y_base) ? true : false;

    }

    //    public double AlignmentStrength()
    //    {
    //      var length_base = PLElement_Base.Element.Length;
    //      var length_temp = PLElement_Temp.Element.Length;
    //
    //      Point3d basePoint = ((Polyline) PLElement_Base.Element).ClosestPoint(new Point3d(PLElement_Temp.PointFirst.X, PLElement_Base.PointFirst.Y, 0.0));
    //      var disFirst = basePoint.DistanceTo(PLElement_Temp.PointFirst);
    //      var disSecond = basePoint.DistanceTo(PLElement_Temp.PointSecond);
    //
    //      double distance = disFirst > disSecond ? disSecond : disFirst;
    //      return (length_base + length_temp) / (length_base + length_temp + Math.Abs(distance));
    //    }
    public double AlignmentStrength() { return 54321.0; }
  }
}

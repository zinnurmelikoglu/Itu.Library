using Itu.Library.Alignment.Element;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
    public class CompareInfinite : AbstractCompare
  {
    public override PLElement Element_First { get; set; }
    public override PLElement Element_Second { get; set; }
    public override double TolerateVal { get; set; }
    public override TangentType TangentType => TangentType.Infinite;
    //public override bool isAligned { get; set; }
    //public override Point3d point_First { get; }
    //public override Point3d point_Second { get; }

    public CompareInfinite(PLElement element_First, PLElement element_Second) : base(element_First, element_Second)
    {
    }

    public override Boolean CompareElement()
    {
      double tanVal_base = Element_First.TanVal_Rounded;
      double tanVal_temp = Element_Second.TanVal_Rounded;
      double ref_X_base = Element_First.Ref_X;
      double ref_X_temp = Element_Second.Ref_X;

      return isAligned = ((Double.IsInfinity(tanVal_base) && (Double.IsInfinity(tanVal_temp))) && (Math.Abs(ref_X_base - ref_X_temp) <= TolerateVal));

    }
    //public AlignedElement GetAlignedElement()
    //{
    //  return new AlignedElement(Element_First, Element_Second);
    //}



  }
}

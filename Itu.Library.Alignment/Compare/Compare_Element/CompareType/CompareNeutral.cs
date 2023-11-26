using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class CompareNeutral : AbstractCompare
  {
    public override PLElement Element_First { get; set; }
    public override PLElement Element_Second { get; set; }
    public override double TolerateVal { get; set; }
    public override TangentType TangentType => TangentType.Neutral;
    public override bool isAligned { get; set; }
    //public override Point3d point_First { get;}
    //public override Point3d point_Second { get;}

    public CompareNeutral(PLElement element_First, PLElement element_Second) : base(element_First, element_Second)
    {
    }
    public override Boolean CompareElement()
    {
      double neutral = 0.0;
      double tanVal_base = Element_First.TanVal_Rounded;
      double tanVal_temp = Element_Second.TanVal_Rounded;
      double ref_Y_base = Element_First.Ref_Y;
      double ref_Y_temp = Element_Second.Ref_Y;

      return isAligned = ((tanVal_base == neutral && tanVal_temp == neutral) && (Math.Abs(ref_Y_base - ref_Y_temp) <= TolerateVal));
      
    }
    //public override Line AlignmentDraw()
    //{
    //  //return new Line(point_First, point_Second, Element_First.TanType);
    //  return new DrawAlignment(point_First, point_Second, Element_First.TanType).GenerateAlignmentLine();
    //}

  }

}

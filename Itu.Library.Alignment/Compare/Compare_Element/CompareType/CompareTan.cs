using Itu.Library.Alignment.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class CompareTan : AbstractCompare
  {
    public override PLElement Element_First { get; set; }
    public override PLElement Element_Second { get; set; }
    public override TangentType TangentType => TangentType.Tangent;
    public CompareTan(PLElement element_First, PLElement element_Second) : base(element_First, element_Second)
    {
    }

    public override Boolean CompareElement()
    {
      double tanVal_base = Element_First.TanVal_Rounded;
      double tanVal_temp = Element_Second.TanVal_Rounded;
      double ref_X_base = Element_First.Ref_X;
      double ref_X_temp = Element_Second.Ref_X;
      double ref_Y_base = Element_First.Ref_Y;
      double ref_Y_temp = Element_Second.Ref_Y;

      return isAligned = ((tanVal_base == tanVal_temp) &&
                        (Math.Abs(ref_X_base - ref_X_temp) <= TolerateVal) &&
                        (Math.Abs(ref_Y_base - ref_Y_temp) <= TolerateVal));

    }

  }
}

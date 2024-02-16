using Itu.Library.Alignment.Element;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class CompareFactory
  {
    PLElement Element_First { get; set; }
    PLElement Element_Second { get; set; }
    double TanVal { get; set; }
    double neutral = 0.0;
    Double Neutral { get { return neutral; } }
    AbstractCompare Compare { get; set; }

    public CompareFactory(PLElement element_First, PLElement element_Second)
    {
      Element_First = element_First;
      Element_Second = element_Second;
      TanVal = element_First.TanVal;

    }

    public AbstractCompare CompareType()
    {
      double tanVal = Math.Abs(Element_First.TanVal);
      TangentType tanType = Element_First.TanType;

      if (tanType.Equals(TangentType.Neutral))
        return new CompareNeutral(Element_First, Element_Second);
      else if (tanType.Equals(TangentType.Infinite))
        return new CompareInfinite(Element_First, Element_Second);
      else if (tanType.Equals(TangentType.Tangent))
        return new CompareTan(Element_First, Element_Second);

      return new CompareDefault();

    }
  }

}

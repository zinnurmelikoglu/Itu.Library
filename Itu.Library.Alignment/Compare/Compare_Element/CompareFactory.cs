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
    ICompare Compare { get; set; }

    public CompareFactory(PLElement element_First, PLElement element_Second)
    {
      Element_First = element_First;
      Element_Second = element_Second;
      //TanVal = plElement_Base.TanVal_Rounded;
      TanVal = element_First.TanVal;

    }

    public ICompare CompareType()
    {
      double neutral = 0.0;
      //double tanVal = PLElement_Base.TanVal_Rounded;
      double tanVal = Math.Abs(Element_First.TanVal);
      //double tanVal_Second = Math.Abs(Element_Second.TanVal);

      //if (tanVal == neutral)
      //  return new CompareNeutral(Element_First, Element_Second);
      //else if (Double.IsInfinity(tanVal))
      //  return new CompareInfinite(Element_First, Element_Second);
      ////else if(tanVal > neutral || tanVal < neutral)
      //else if (tanVal > neutral || tanVal < Double.PositiveInfinity)
      //  return new CompareTan(Element_First, Element_Second);
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

  //public enum TangentType
  //{
  //  [Description("Neutral")]
  //  Neutral,

  //  [Description("Infinite")]
  //  Infinite,

  //  [Description("Tangent")]
  //  Tangent,

  //  [Description("Default")]
  //  Default
  //}

}

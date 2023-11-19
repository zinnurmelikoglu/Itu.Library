using Itu.Library.Alignment.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
    public class CompareFactory
  {
    PLElement PLElement_Base { get; set; }
    PLElement PLElement_Temp { get; set; }
    double TanVal { get; set; }
    double neutral = 0.0;
    Double Neutral { get { return neutral; } }
    ICompare Compare { get; set; }

    public CompareFactory(PLElement plElement_Base, PLElement plElement_Temp)
    {
      PLElement_Base = plElement_Base;
      PLElement_Temp = plElement_Temp;
      //TanVal = plElement_Base.TanVal_Rounded;
      TanVal = plElement_Base.TanVal;

    }

    public ICompare CompareType()
    {
      double neutral = 0.0;
      //double tanVal = PLElement_Base.TanVal_Rounded;
      double tanVal = PLElement_Base.TanVal;

      if (Double.IsInfinity(tanVal))
        return new CompareInfinite(PLElement_Base, PLElement_Temp);
      else if (tanVal == neutral)
        return new CompareNeutral(PLElement_Base, PLElement_Temp);
      //else if(tanVal > neutral || tanVal < neutral)
      else
        return new CompareTan(PLElement_Base, PLElement_Temp);

    }

  }
}

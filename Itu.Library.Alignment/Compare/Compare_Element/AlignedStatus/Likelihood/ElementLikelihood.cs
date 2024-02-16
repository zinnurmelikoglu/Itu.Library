using Itu.Library.Alignment.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class ElementLikelihood
  {
    PLElement Element { get; set; }
    AlignedElementStatusList _AlignedElementStatusList { get; set; }
    public ElementLikelihood(PLElement element) => Element = element;

    public double GetElementLikehood(AlignedElementStatusList alignedElementStatusList)
    {

      _AlignedElementStatusList = alignedElementStatusList;
      double neutral = 0.00;
      double likelihood;

      var elementStatusByElement = _AlignedElementStatusList.GetAlignedElementStatusList(Element);
      if (elementStatusByElement.Count > 0)
      {
        AlignedElementStatus elementStatus = elementStatusByElement.Aggregate((i1, i2) => i1.AlignedStrength > i2.AlignedStrength ? i1 : i2);
        likelihood = elementStatus.AlignedStrength;
      }
      else
        likelihood = neutral;

      return likelihood;
    }
  }

}

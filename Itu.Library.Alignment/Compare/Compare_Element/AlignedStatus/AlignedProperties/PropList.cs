using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class PropList
  {
    public ElementCouple AlignedElementCouple { get; set; }
    public PropList(ElementCouple elementCouple) => AlignedElementCouple = elementCouple;
    public List<CommonProp> GetPropList() {

      return new List<CommonProp>() { new InBetweenProp(AlignedElementCouple)
        , new ClosenessProp(AlignedElementCouple)
        , new LineProp(AlignedElementCouple)
      };

    }
  }
}

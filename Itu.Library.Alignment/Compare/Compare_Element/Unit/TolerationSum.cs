using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class TolerationSum
  {
    double TolerateVal => 5.0;
    ElementCouple _ElementCouple { get; }
    //public double AlignedCloseness => new ClosenessProp(_ElementCouple).ClosenessFactor();
    public double AlignedCloseness => new ClosenessProp(_ElementCouple).AlignedCloseness();
    private const double conFact = 1.0;
    public TolerationSum( ElementCouple elementCouple) => _ElementCouple = elementCouple;

    public double GetTolerationVal() {

      double minVal = 1.0;
      double factor = conFact - AlignedCloseness;
      return (TolerateVal * factor) < minVal ? minVal : (TolerateVal * factor);

    }
  }
}

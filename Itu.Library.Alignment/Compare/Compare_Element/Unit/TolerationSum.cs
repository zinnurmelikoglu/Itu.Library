using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class TolerationSum
  {
    ElementCouple _ElementCouple { get; }
    public double AlignedCloseness => new ClosenessProp(_ElementCouple).AlignedCloseness();
    public double Distance => new ClosenessProp(_ElementCouple).GetDistance();
    double TolerateDegree => EntityBase.GetValue<double>(tolerance);
    
    private const double conFact = 1.0;
    string tolerance = "tolerance";
    public TolerationSum( ElementCouple elementCouple) => _ElementCouple = elementCouple;
    
    public double GetTolerationVal() {

      double distance = Distance;
      var tolerateDegree = TolerateDegree;
      var tanVal = Math.Tan(tolerateDegree * (Math.PI / 180));
      var tolerate = Math.Abs(tanVal) * distance;

      return tolerate;

    }
  }
}

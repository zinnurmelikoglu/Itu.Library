using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class StrengthProp : CommonProp
  {
    Double Sigma => 1.0;
    public StrengthProp(ElementCouple elementCouple) : base(elementCouple) => _ElementCouple = elementCouple;

    public double AlignmentStrengt()
    {
      var factorList = _ElementCouple._LikelihoodFactorList;
      return new LikelihoodOperation().AndOperation(factorList);
      
    }

  }
}

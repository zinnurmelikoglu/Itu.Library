using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class LikelihoodFactorList : IEnumerable<LikelihoodFactor>
  {
    public List<LikelihoodFactor> _LikelihoodFactorList { get; set; }

    public LikelihoodFactorList()
    {
      _LikelihoodFactorList = new List<LikelihoodFactor>();
    }

    public void AddLikelihoodFactor(LikelihoodFactor likelihood)
    {
      _LikelihoodFactorList.Add(likelihood);
    }

    public IEnumerator<LikelihoodFactor> GetEnumerator()
    {
      return _LikelihoodFactorList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public int Count()
    {
      return _LikelihoodFactorList.Count;
    }

  }
}

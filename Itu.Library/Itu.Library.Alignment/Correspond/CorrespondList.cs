using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Correspond
{
  internal class CorrespondList : IEnumerable<CorrespondGeometry>
  {
    public List<CorrespondGeometry> compareList;
    public CorrespondList()
    {
      compareList = new List<CorrespondGeometry>();
    }

    public void AddGeometry(CorrespondGeometry geometry)
    {
      compareList.Add(geometry);
    }
    public IEnumerator<CorrespondGeometry> GetEnumerator()
    {
      return compareList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

  }
}

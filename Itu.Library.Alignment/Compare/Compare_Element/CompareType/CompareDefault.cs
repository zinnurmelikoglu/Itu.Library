using Itu.Library.Alignment.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class CompareDefault : ICompare
  {
    public PLElement PLElement_First { get; set; }
    public PLElement PLElement_Second { get; set; }
    public double TolerateVal { get; set; }
    public TangentType TangentType => TangentType.Default;

    public double AlignmentStrength()
    {
      return Double.NaN;
    }

    public bool CompareElement()
    {
      return false;
    }

    public AlignedElement GetAlignedElement()
    {
      return null;
    }
  }
}

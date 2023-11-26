using Itu.Library.Alignment.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class CompareDefault : AbstractCompare
  {
    public override PLElement Element_First { get; set; }
    public override PLElement Element_Second { get; set; }
    public override double TolerateVal { get; set; }
    public override TangentType TangentType => TangentType.Default;
    public override bool isAligned { get; set; }
    
    public override bool CompareElement()
    {
      return isAligned = false;
    }

  }
}

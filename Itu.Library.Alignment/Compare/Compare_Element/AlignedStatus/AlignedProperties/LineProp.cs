using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class LineProp : CommonProp
  {
    public LineProp(ElementCouple elementCouple) : base(elementCouple) => _ElementCouple = elementCouple;
    public Line AlignmentLine() => _ElementCouple.AlignmentLine = new DrawAlignment(point_First, point_Second, _TangentType).GenerateAlignmentLine();

    
  }
}

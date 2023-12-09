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
    //ElementCouple _ElementCouple { get; }
    //public PLElement Element_First => _ElementCouple.Element_First;
    //public PLElement Element_Second => _ElementCouple.Element_Second;
    //public virtual Point3d point_First => Element_First.Element.ClosestPoint(Element_Second.PointFirst);
    //public virtual Point3d point_Second => Element_Second.Element.ClosestPoint(Element_First.PointFirst);
    //TangentType _TangentType => Element_First.TanType;
    public LineProp(ElementCouple elementCouple) : base(elementCouple) => _ElementCouple = elementCouple;
    public Line AlignmentLine() => _ElementCouple.AlignmentLine = new DrawAlignment(point_First, point_Second, _TangentType).GenerateAlignmentLine();

  }
}

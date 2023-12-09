using Itu.Library.Alignment.Element;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class CommonProp
  {
    public ElementCouple _ElementCouple { get; set; }
    public PLElement Element_First => _ElementCouple.Element_First;
    public PLElement Element_Second => _ElementCouple.Element_Second;
    public virtual Point3d point_First => Element_First.Element.ClosestPoint(Element_Second.PointFirst);
    public virtual Point3d point_Second => Element_Second.Element.ClosestPoint(Element_First.PointFirst);
    public TangentType _TangentType => Element_First.TanType;
    public GeometryCouple _GemoetryCouple => _ElementCouple.GetGeometryCouple();

    public CommonProp(ElementCouple elementCouple) => _ElementCouple = elementCouple;

  }
}

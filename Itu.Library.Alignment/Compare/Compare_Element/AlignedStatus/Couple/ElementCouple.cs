using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class ElementCouple
  {
    public PLElement Element_First { get; set; }
    public PLElement Element_Second { get; set; }
    public Line AlignmentLine { get; set; }

    public ElementCouple(PLElement element_First, PLElement element_Second)
    {
      Element_First = element_First;
      Element_Second = element_Second;
    }

    public GeometryCouple GetGeometryCouple()
    {
      var geometry_First = Element_First.Geometry;
      var geometry_Second = Element_Second.Geometry;

      return new GeometryCouple(geometry_First, geometry_Second);
    }

  }

  
}

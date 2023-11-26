using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Itu.Library.Alignment.Compare
{
  public class AlignedElement
  {
    PLElement Element_First { get; set; }
    PLElement Element_Second { get; set; }
    Point3d point_First => Element_First.Element.ClosestPoint(Element_Second.PointFirst);
    Point3d point_Second => Element_Second.Element.ClosestPoint(Element_First.PointFirst);
    public List<AlignedElement> AlignedElementList { get; set; }

    public AlignedElement(PLElement element_First, PLElement element_Second)
    {
      Element_First = element_First;
      Element_Second = element_Second;  
    }
    public double AlignmentStrength()
    {
      var length_base = Element_First.Element.Length;
      var length_temp = Element_Second.Element.Length;

      double distance = point_First.DistanceTo(point_Second);
      double plDistance = length_base + length_temp;
      return plDistance / (plDistance + Math.Abs(distance));
    }
    public Line AlignmentDraw()
    {
      //return new Line(point_First, point_Second, Element_First.TanType);
      return new DrawAlignment(point_First, point_Second, Element_First.TanType).GenerateAlignmentLine();

    }

  }
}

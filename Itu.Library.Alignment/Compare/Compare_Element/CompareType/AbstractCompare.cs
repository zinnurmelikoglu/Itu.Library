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
  public abstract class AbstractCompare
  {
    public virtual PLElement Element_First { get; set; }
    public virtual PLElement Element_Second { get; set; }
    public virtual double TolerateVal { get; set; }
    public abstract TangentType TangentType { get; }
    public abstract bool isAligned { get; set; }
    public virtual Point3d point_First => Element_First.Element.ClosestPoint(Element_Second.PointFirst);
    public virtual Point3d point_Second => Element_Second.Element.ClosestPoint(Element_First.PointFirst);
    public AbstractCompare() { }
    public AbstractCompare(PLElement element_First, PLElement element_Second)
    {
      Element_First = element_First;
      Element_Second = element_Second;
      TolerateVal = 5.0;
    }
    
    public abstract bool CompareElement();
    public virtual double AlignmentStrength()
    {
      var length_base = Element_First.Element.Length;
      var length_temp = Element_Second.Element.Length;

      Point3d point_First = Element_First.Element.ClosestPoint(Element_Second.PointFirst);
      Point3d point_Second = Element_Second.Element.ClosestPoint(Element_First.PointFirst);

      double distance = point_First.DistanceTo(point_Second);
      double plDistance = length_base + length_temp;
      return plDistance / (plDistance + Math.Abs(distance));
    }

    public virtual Line AlignmentDraw()
    {
      return new DrawAlignment(point_First, point_Second, TangentType).GenerateAlignmentLine();
    }

  }
}

using Itu.Library.Alignment.Element;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class ClosenessProp : CommonProp
  {
    public ClosenessFactor _ClosenessFactor { get; set; }
    public ClosenessProp(ElementCouple elementCouple) : base(elementCouple)
    {
      _ElementCouple = elementCouple;
      IsFactor = true;
      _ClosenessFactor = new ClosenessFactor();

    }

    public double AlignedCloseness()
    {
      var length_base = Element_First.Element.Length;
      var length_temp = Element_Second.Element.Length;

      Point3d point_First = Element_First.Element.ClosestPoint(Element_Second.PointFirst);
      Point3d point_Second = Element_Second.Element.ClosestPoint(Element_First.PointFirst);

      double distance = point_First.DistanceTo(point_Second);
      double plDistance = length_base + length_temp;
      return plDistance / (plDistance + Math.Abs(distance));
    }

    public override void AddLikelihoodFactor()
    {
      var closenessFactor = AlignedCloseness();
      _ClosenessFactor.Factor = closenessFactor;
      _ElementCouple._LikelihoodFactorList.AddLikelihoodFactor(_ClosenessFactor);

    }

  }
}

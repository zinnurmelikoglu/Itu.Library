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
    //ElementCouple _ElementCouple { get;}
    //public PLElement Element_First => _ElementCouple.Element_First;
    //public PLElement Element_Second => _ElementCouple.Element_Second;
    public ClosenessFactor _ClosenessFactor { get; set; }
    public ClosenessProp(ElementCouple elementCouple) : base(elementCouple)
    {
      _ElementCouple = elementCouple;
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

    public LikelihoodFactor ClosenessFactor()
    {
      var closenessFactor = AlignedCloseness();
      _ClosenessFactor.Factor = closenessFactor;
      _ElementCouple._LikelihoodFactorList.AddLikelihoodFactor(_ClosenessFactor);

      return _ClosenessFactor;

    }

  }
}

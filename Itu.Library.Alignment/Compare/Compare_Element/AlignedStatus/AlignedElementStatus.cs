using Itu.Library.Alignment.Geometry;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class AlignedElementStatus
  {
    //public GeometryCouple AlignedGeometryCouple => new GeometryCouple() { Geometry_First = AlignedElementCouple.Element_First.Geometry, Geometry_Second = AlignedElementCouple.Element_Second.Geometry };
    public GeometryCouple AlignedGeometryCouple { get; set; }
    public ElementCouple AlignedElementCouple { get; set; }
    public Line AlignedLine { get; set; }
    public double AlignedCloseness { get; set; }
    public virtual List<CurveIntersections> IntersectGeometryList { get; set; }
    public int IntersectGeometryCount => IntersectGeometryList.Count;
    double factor = 0.0;

  }
}

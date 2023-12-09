using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
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
    public GeometryCouple AlignedGeometryCouple => AlignedElementCouple.GetGeometryCouple();
    public ElementCouple AlignedElementCouple { get; set; }
    public Line AlignedLine => new LineProp(AlignedElementCouple).AlignmentLine();
    public double AlignedCloseness => new ClosenessProp(AlignedElementCouple).AlignmentCloseness();
    public List<CurveIntersections> IntersectGeometryList => new InBetweenProp(AlignedElementCouple).GetIntersectGeometryList();
    public int IntersectGeometryCount { get { var intersectGeometryList = IntersectGeometryList; return intersectGeometryList.Count; } }
    public AlignedElementStatus(ElementCouple alignedElementCouple) => AlignedElementCouple = alignedElementCouple;

  }
}

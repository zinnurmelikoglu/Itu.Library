using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Geometry;
using Rhino.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class CompareList : IEnumerable<CompareGeometry>
  {
    public List<CompareGeometry> compareList;
    public CompareList()
    {
      compareList = new List<CompareGeometry>();
    }

    public void AddGeometry(CompareGeometry geometry)
    {
      compareList.Add(geometry);
    }
    public IEnumerator<CompareGeometry> GetEnumerator()
    {
      return compareList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public List<PLGeometry> ComparedByGeometry(PLGeometry plGeometry)
    {
      List<PLGeometry> geometryList = new List<PLGeometry>();
      
      return (List<PLGeometry>)compareList.Where(s => s.isAligned && s.Geometry_First.Intersect(plGeometry).Any()).Select(s => s.Geometry_Second)
      .Concat(compareList.Where(s => s.isAligned && s.Geometry_Second.Intersect(plGeometry).Any()).Select(s => s.Geometry_First)).ToList();

    }

    public List<Line> GetLineList()
    {
      List<Line> lineList = new List<Line>();

      //return (List<PLGeometry>)compareList.Where(s => s.isAligned && s.Geometry_First.Intersect(plGeometry).Any()).Select(s => s.Geometry_Second)
      //.Concat(compareList.Where(s => s.isAligned && s.Geometry_Second.Intersect(plGeometry).Any()).Select(s => s.Geometry_First));

      return (List<Line>)compareList.Where(s => s.isAligned).Select(s => s.LineList);

    }

  }
}

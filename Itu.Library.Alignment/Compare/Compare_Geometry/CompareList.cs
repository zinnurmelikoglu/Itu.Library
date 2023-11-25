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

    public List<Line> GetLineList()
    {
      List<Line> lineList = new List<Line>();
      return (List<Line>)compareList.Where(s => s.isAligned).SelectMany(s => s.LineList).ToList();

    }

    public Double GetFactor()
    {
      double intersect = 0.0;
      int compareCount = this.Count();
      //foreach (CompareGeometry compareGeometry in this) {
      //  intersect += compareGeometry.Intersect;
      //}
      
      compareList.ForEach(m => { intersect += m.GetIntersectFactor(); });

      return intersect / compareCount;

    }

    //public double GetDistance(Line line) {
    
    //}

  }
}

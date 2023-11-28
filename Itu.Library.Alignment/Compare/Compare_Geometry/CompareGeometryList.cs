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
  internal class CompareGeometryList : IEnumerable<CompareGeometry>
  {
    public List<CompareGeometry> compareGeometryList;
    public CompareGeometryList()
    {
      compareGeometryList = new List<CompareGeometry>();
    }

    public void AddGeometry(CompareGeometry geometry)
    {
      compareGeometryList.Add(geometry);
    }
    public IEnumerator<CompareGeometry> GetEnumerator()
    {
      return compareGeometryList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public List<Line> GetLineList()
    {
      List<Line> lineList = new List<Line>();
      //return (List<Line>)compareGeometryList.Where(s => s.isAligned).SelectMany(s => s.LineList).ToList();
      return (List<Line>)this.Where(s => s.isAligned).SelectMany(s => s.alignedElementStatusList.alignedElementList).Select(s=> s.AlignedLine).ToList();

    }

    public List<Double> GetAlignedStrengthList()
    {
      List<Double> lineList = new List<Double>();
      //return (List<Double>)compareGeometryList.Where(s => s.isAligned).SelectMany(s => s.StrengthList).ToList();
      return (List<Double>)this.Where(s => s.isAligned).SelectMany(s => s.alignedElementStatusList.alignedElementList).Select(s => s.AlignedStrength).ToList();

    }

    public List<AlignedElementStatus> GetAlignedElementStatusList()
    {
      List<Double> lineList = new List<Double>();
      //return (List<Double>)compareGeometryList.Where(s => s.isAligned).SelectMany(s => s.StrengthList).ToList();
      return (List<AlignedElementStatus>)compareGeometryList.Where(s => s.isAligned).SelectMany(s => s.alignedElementStatusList).ToList();

    }

    public Double GetFactor()
    {
      double intersect = 0.0;
      int compareCount = this.Count();
      
      compareGeometryList.ForEach(m => { intersect += m.GetIntersectFactor(); });
      return intersect / compareCount;

    }


  }
}

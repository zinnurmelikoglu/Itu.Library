using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.DrawUp;
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
  internal class CompareGeometry
  {
    public PLGeometry Geometry_First { get; set; }
    public PLGeometry Geometry_Second { get; set; }
    public List<PLGeometry> GeometryList { get; set; }
    List<PLElement> ElementList_First => Geometry_First.GetElementList();
    List<PLElement> ElementList_Second => Geometry_Second.GetElementList();
    List<AbstractCompare> AlignedElementList { get; set; }
    public List<Line> LineList { get; set; }
    public List<Double> StrengthList { get; set; }
    public bool isAligned { get; set; }
    double factor => 0.5;
    double Intersect { get; set; }

    public CompareGeometry(PLGeometry geometry_First, PLGeometry geometry_Second)
    {
      Geometry_First = geometry_First;
      Geometry_Second = geometry_Second;
      LineList = new List<Line>();
      AlignedElementList = new List<AbstractCompare>();
      StrengthList = new List<Double>();

      Intersect = 0.0;
      isAligned = false;

    }

    public bool Compare()
    {
    
      foreach (var element in ElementList_First)
      {

        foreach (var temp in ElementList_Second)
        {
          CompareFactory compareFactory = new CompareFactory(element, temp);
          AbstractCompare compare = compareFactory.CompareType();

          if (compare.CompareElement())
          {
            AlignedElementList.Add(compare);
            StrengthList.Add(compare.AlignmentStrength());
            LineList.Add(compare.AlignmentDraw());

            Intersect += factor;
            isAligned = true;

          }
        }

      }

      return isAligned;
    }

    public Double GetIntersectFactor() => Intersect;


  }
}

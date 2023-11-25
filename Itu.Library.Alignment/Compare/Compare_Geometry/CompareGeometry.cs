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
    public List<Line> LineList { get; set; }
    public bool isAligned { get; set; }
    double factor => 0.5;
    public double Intersect { get; set; }

    public CompareGeometry(PLGeometry geometry_First, PLGeometry geometry_Second)
    {
      Geometry_First = geometry_First;
      Geometry_Second = geometry_Second;
      LineList = new List<Line>();

      Intersect = 0.0;
      isAligned = false;

    }

    public bool Compare()
    {
      //var elementList = PLGeometry_Base.GetElementList();
      
      foreach (var element in ElementList_First)
      {

        foreach (var temp in ElementList_Second)
        {
          CompareFactory compareFactory = new CompareFactory(element, temp);
          ICompare compare = compareFactory.CompareType();

          if (compare.CompareElement())
          {
            LineList.Add(new DrawAlignment(element).GenerateAlignment());
            var strength = compare.AlignmentStrength();
            //StrengthList.Add(tanVal);
            //StrengthList.Add(strength);

            Intersect += factor;

            isAligned = true;

          }
        }

      }

      return isAligned;
    }


  }
}

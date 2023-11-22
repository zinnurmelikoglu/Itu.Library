using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
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
    List<PLElement> ElementList_First { get { return Geometry_First.GetElementList(); } }
    List<PLElement> ElementList_Second { get { return Geometry_Second.GetElementList(); } }

    public bool isAligned { get; set; }
    double factor { get; set; }

    int intersect { get; set; }

    public CompareGeometry(PLGeometry plGeometry_Base, PLGeometry plGeometry_Temp)
    {
      Geometry_First = plGeometry_Base;
      Geometry_Second = plGeometry_Temp;

      intersect = 0;
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
            //lineList.Add(new DrawAlignment(element.PointFirst.X, element.PointFirst.Y, tanVal, facadeArea).GenerateAlignment());

            var strength = compare.AlignmentStrength();
            //StrengthList.Add(tanVal);
            //StrengthList.Add(strength);

            intersect++;

            isAligned = true;

          }
        }
      }

      return isAligned;
    }


  }
}

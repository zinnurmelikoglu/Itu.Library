using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Correspond
{
  internal class CorrespondGeometry
  {
    PLGeometry PLGeometry_Base { get; set; }
    PLGeometry PLGeometry_Temp { get; set; }
    List<PLElement> ElementList_Base { get { return PLGeometry_Base.GetElementList(); } }
    List<PLElement> ElementList_Temp { get { return PLGeometry_Temp.GetElementList(); } }

    Boolean isAligned { get; set; }
    Double factor { get; set; }

    int intersect { get; set; }

    public CorrespondGeometry(PLGeometry plGeometry_Base, PLGeometry plGeometry_Temp)
    {
      PLGeometry_Base = plGeometry_Base;
      PLGeometry_Temp = plGeometry_Temp;

      intersect = 0;
      isAligned = false;
    }

    public bool Compare()
    {
      //var elementList = PLGeometry_Base.GetElementList();
      foreach (var element in ElementList_Base)
      {
        foreach (var temp in ElementList_Temp)
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
            PLGeometry_Base.AlignedGeometry.Add(PLGeometry_Temp);
            PLGeometry_Temp.AlignedGeometry.Add(PLGeometry_Base);

          }
        }
      }

      return isAligned;
    }

  }
}

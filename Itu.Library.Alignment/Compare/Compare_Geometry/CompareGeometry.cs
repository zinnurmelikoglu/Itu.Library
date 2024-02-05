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
using static System.Net.Mime.MediaTypeNames;

namespace Itu.Library.Alignment.Compare
{
  public class CompareGeometry
  {
    public PLGeometry Geometry_First { get; set; }
    public PLGeometry Geometry_Second { get; set; }
    //public List<PLGeometry> Geometry_Remain { get; set; }
    //public List<PLGeometry> GeometryList { get; set; }
    List<PLElement> ElementList_First => Geometry_First.GetElementList();
    List<PLElement> ElementList_Second => Geometry_Second.GetElementList();
    public AlignedElementStatus _AlignedElementStatus { get; set; }
    public AlignedElementStatusList _AlignedElementStatusList { get; set; }
    //public List<Line> LineList { get; set; }
    //public List<Double> ClosenessList { get; set; }
    public bool isAligned { get; set; }
    double factor => 0.5;
    double Intersect { get; set; }

    public CompareGeometry(PLGeometry geometry_First, PLGeometry geometry_Second)
    {
      Geometry_First = geometry_First;
      Geometry_Second = geometry_Second;
      _AlignedElementStatusList = new AlignedElementStatusList();

      Intersect = 0.0;
      isAligned = false;

    }

    public bool Compare()
    {

      // Elements of the first geometry
      foreach (var element in ElementList_First)
      {

        // Elements of the second geometry
        foreach (var temp in ElementList_Second)
        {
          CompareFactory compareFactory = new CompareFactory(element, temp);
          AbstractCompare compareElement = compareFactory.CompareType();

          if (compareElement.CompareElement()) //if aligned
          {
            var alignedElementCouple = new ElementCouple(element, temp);
            var alignedStatus = new AlignedElementStatus(alignedElementCouple);

            //alignedStatus.AlignedStrength ==> likelihood

            //GetAligned

            //Bir elementin likelihood degerini diger elementlerde kazandigi likelihood degeriyle kiyaslanabilir

            var asd = _AlignedElementStatusList.AlignedElementStatusByElement(element);
            

            _AlignedElementStatusList.AddAlignedElement(alignedStatus);

            //Intersect += factor;
            isAligned = true;

          }
        }

      }

      return isAligned;
    }

    public Double GetIntersectFactor() => Intersect;

  }
}

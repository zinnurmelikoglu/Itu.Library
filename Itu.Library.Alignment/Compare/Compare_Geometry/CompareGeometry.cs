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
    public List<PLGeometry> Geometry_Remain { get; set; }
    //public List<PLGeometry> GeometryList { get; set; }
    List<PLElement> ElementList_First => Geometry_First.GetElementList();
    List<PLElement> ElementList_Second => Geometry_Second.GetElementList();
    List<AbstractCompare> AlignedElementList { get; set; }
    public AlignedElementStatusList alignedElementStatusList { get; set; }
    public List<Line> LineList { get; set; }
    public List<Double> ClosenessList { get; set; }
    public bool isAligned { get; set; }
    double factor => 0.5;
    double Intersect { get; set; }

    public CompareGeometry(PLGeometry geometry_First, PLGeometry geometry_Second, List<PLGeometry> geometry_Remain)
    {
      Geometry_First = geometry_First;
      Geometry_Second = geometry_Second;
      Geometry_Remain = geometry_Remain;
      //LineList = new List<Line>();
      //ClosenessList = new List<Double>();
      AlignedElementList = new List<AbstractCompare>();
      alignedElementStatusList = new AlignedElementStatusList();

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
          AbstractCompare compareElement = compareFactory.CompareType();

          if (compareElement.CompareElement()) //if is aligned
          {
            AlignedElementList.Add(compareElement);
            //ClosenessList.Add(compareElement.AlignmentCloseness());
            //LineList.Add(compareElement.AlignmentLine());

            //Aligment Status buranin uzerinden dolacaktir
            //compareElement.GetIntersectGeometryList(Geometry_Remain);
            var alignedElementCouple = new ElementCouple() { Element_First = element, Element_Second = temp };
            var alignedGeometryCouple = new GeometryCouple() { Geometry_First = Geometry_First, Geometry_Second = Geometry_Second, Geometry_Remain = Geometry_Remain };

            var alignedElementStatus = new AlignedElementStatus() { AlignedElementCouple = alignedElementCouple, AlignedGeometryCouple = alignedGeometryCouple };
            



            var alignedStatus = compareElement.GetAlignedElementStatus();
            alignedStatus.AlignedElementCouple = new ElementCouple() { Element_First = element, Element_Second = temp };

            

            alignedElementStatusList.AddAlignedElement(alignedStatus);

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

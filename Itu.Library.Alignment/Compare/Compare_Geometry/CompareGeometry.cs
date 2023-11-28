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
    internal class CompareGeometry
  {
    public PLGeometry Geometry_First { get; set; }
    public PLGeometry Geometry_Second { get; set; }
    public List<PLGeometry> Geometry_Remain { get; }
    public List<PLGeometry> GeometryList { get; set; }
    List<PLElement> ElementList_First => Geometry_First.GetElementList();
    List<PLElement> ElementList_Second => Geometry_Second.GetElementList();
    List<AbstractCompare> AlignedElementList { get; set; }
    public AlignedElementStatusList alignedElementStatusList { get; set; }
    public List<Line> LineList { get; set; }
    public List<Double> StrengthList { get; set; }
    public bool isAligned { get; set; }
    double factor => 0.5;
    double Intersect { get; set; }

    public CompareGeometry(PLGeometry geometry_First, PLGeometry geometry_Second, List<PLGeometry> geometry_Remain)
    {
      Geometry_First = geometry_First;
      Geometry_Second = geometry_Second;
      Geometry_Remain = geometry_Remain;
      LineList = new List<Line>();
      AlignedElementList = new List<AbstractCompare>();
      StrengthList = new List<Double>();

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
            //StrengthList.Add(compareElement.AlignmentStrength());
            //LineList.Add(compareElement.AlignmentLine());

            compareElement.GetIntersectGeometryList(Geometry_Remain);
            var alignedStatus = compareElement.GetAlignedElementStatus();
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

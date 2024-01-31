using Grasshopper.Kernel.Expressions;
using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class AlignedElementStatus
  {
    public GeometryCouple AlignedGeometryCouple => AlignedElementCouple.GetGeometryCouple();
    public ElementCouple AlignedElementCouple { get; set; }
    public Line AlignedLine => new LineProp(AlignedElementCouple).AlignmentLine();
    public double AlignedCloseness => new ClosenessProp(AlignedElementCouple).AlignedCloseness();
    //public LikelihoodFactor ClosenessFactor => Closeness.ClosenessFactor();
    
    /*******
     * 
     * This section is remarked before
     * 
    //public List<CurveIntersections> InBetweenGeometryList => new InBetweenProp(AlignedElementCouple).GetInBetweenGeometryList();
    //public int InBetweenGeometryCount { get { var inBetweenGeometryList = InBetweenGeometryList; return inBetweenGeometryList.Count; } }
    ******/


    //public List<CurveIntersections> InBetweenGeometryList => InBetween.InBetweenGeometryList;
    public int InBetweenGeometryCount => new InBetweenProp(AlignedElementCouple).InBetweenGeometryCount;
    //public double InBetweenFactor => InBetween.InBetweenFactor();
    public double AlignedStrength => new StrengthProp(AlignedElementCouple).AlignmentStrengt();
    //InBetweenProp InBetween { get; set; }
    //ClosenessProp Closeness { get; set; }
    //LineProp LineProp { get; set; }
    List<CommonProp> CommonPropList { get; set; }

    public AlignedElementStatus(ElementCouple alignedElementCouple)
    {
      AlignedElementCouple = alignedElementCouple;
      //InBetween = new InBetweenProp(AlignedElementCouple);
      //Closeness = new ClosenessProp(AlignedElementCouple);
      //LineProp = new LineProp(AlignedElementCouple);

      CommonPropList = new PropList(AlignedElementCouple).GetPropList().Where(s => s.IsFactor == true).ToList();
      PushLikeloodFactors();


      /*
       * Tum CommonProplari Listeye topladiktan sonra IsFactoru true olanlari cagirarak onlardaki tekbir pushFactor ile Factoru AligmentElementCouple daki
        LikelyhoodList'e cakmaya calisacagiz
       */

    }

    public void PushLikeloodFactors() => CommonPropList.ForEach(p => { p.PushFactor(); });
    

    //Push factor list to elementCouple 

    //List<LikelihoodFactor> 
    // Buradan object donmeli ve o objectlerin push yapan InBetweenFactor gibi methodlarini cagirmaliyim

  }
}

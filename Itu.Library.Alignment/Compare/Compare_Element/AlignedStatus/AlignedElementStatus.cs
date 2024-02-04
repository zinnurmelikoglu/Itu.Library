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
    public ElementCouple AlignedElementCouple { get; set; }
    public GeometryCouple AlignedGeometryCouple => AlignedElementCouple.GetGeometryCouple();
    public Line AlignedLine => new LineProp(AlignedElementCouple).AlignmentLine();
    public double AlignedCloseness => new ClosenessProp(AlignedElementCouple).AlignedCloseness();
    public double InBetweenFactor => new InBetweenProp(AlignedElementCouple).InBetweenFactor();
    public int InBetweenGeometryCount => new InBetweenProp(AlignedElementCouple).InBetweenGeometryCount;
    public double AlignedStrength => new StrengthProp(AlignedElementCouple).AlignmentStrengt();
    List<CommonProp> CommonPropList { get; set; }

    public AlignedElementStatus(ElementCouple alignedElementCouple)
    {
      AlignedElementCouple = alignedElementCouple;
      CommonPropList = new PropList(AlignedElementCouple).GetPropList().Where(s => s.IsFactor == true).ToList();
      PushLikeloodFactors();

    }

    public void PushLikeloodFactors() => CommonPropList.ForEach(p => { p.AddLikelihoodFactor(); });

  }
}

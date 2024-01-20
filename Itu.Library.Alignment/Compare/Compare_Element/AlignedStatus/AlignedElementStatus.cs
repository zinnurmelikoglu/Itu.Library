﻿using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class AlignedElementStatus
  {
    public GeometryCouple AlignedGeometryCouple => AlignedElementCouple.GetGeometryCouple();
    public ElementCouple AlignedElementCouple { get; set; }
    public Line AlignedLine => new LineProp(AlignedElementCouple).AlignmentLine();
    public double ClosenessFactor => new ClosenessProp(AlignedElementCouple).ClosenessFactor();
    public List<CurveIntersections> InBetweenGeometryList => new InBetweenProp(AlignedElementCouple).GetInBetweenGeometryList();
    public int InBetweenGeometryCount { get { var inBetweenGeometryList = InBetweenGeometryList; return inBetweenGeometryList.Count; } }
    public double InBetweenFactor => InBetween.InBetweenFactor();
    public double Strength => new StrengthProp(AlignedElementCouple).AlignmentStrengt();
    InBetweenProp InBetween { get; set; }
    public AlignedElementStatus(ElementCouple alignedElementCouple)
    {
      AlignedElementCouple = alignedElementCouple;
      InBetween = new InBetweenProp(AlignedElementCouple);
    }

  }
}

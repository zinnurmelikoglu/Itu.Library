﻿using Rhino.Geometry;
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
    public Line AlignedLine { get; set; }
    public double AlignedStrength { get; set; }
    public virtual List<CurveIntersections> IntersectGeometryList { get; set; }
    public int IntersectGeometryCount => IntersectGeometryList.Count;
    double factor = 0.0;

  }
}
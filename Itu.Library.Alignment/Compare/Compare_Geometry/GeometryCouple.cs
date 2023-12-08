﻿using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class GeometryCouple
  {
    public PLGeometry Geometry_First { get; set; }
    public PLGeometry Geometry_Second { get; set; }
    public List<PLGeometry> Geometry_Remain { get; set; }
  }
}

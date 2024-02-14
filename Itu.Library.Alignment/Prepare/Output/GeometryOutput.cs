using Itu.Library.Alignment.Geometry;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Prepare
{
  internal class GeometryOutput
  {
    public String GeometryName { get; set; }
    public Polyline Geometry { get; set; }
    public Point3d CenterPoint { get; set; }
    public double Likelihood { get; set; }

  }
}

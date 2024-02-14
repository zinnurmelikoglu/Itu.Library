using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Prepare
{
  internal class ElementOutput
  {
    public Polyline Element { get; set; }
    public Point3d CenterPoint { get; set; }
    public double Likelihood { get; set; }
  }
}

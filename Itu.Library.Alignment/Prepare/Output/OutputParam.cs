using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Prepare
{
  internal class OutputParam
  {
    public Line AlignedLine { get; set; }
    public double AlignedCloseness { get; set; }
    public double InBetweenFactor { get; set; }
    public int InBetweenGeometryCount { get; set; }
    public double AlignedStrengt { get; set; }

  }
}

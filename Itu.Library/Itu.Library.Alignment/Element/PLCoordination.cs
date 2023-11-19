using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Element
{
  public class PLCoordination
  {
    public double Coord_X { get; set; }
    public double Coord_Y { get; set; }

    public PLCoordination(double x, double y)
    {
      Coord_X = x;
      Coord_Y = y;
    }

  }
}

using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using Itu.Library.Alignment.Util;
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
    public List<PLGeometry> Geometry_Remain { get { var geometryList = Extension.GeometryList(); return geometryList.Except(new List<PLGeometry>() { Geometry_First, Geometry_Second }).ToList(); } }
    public GeometryCouple(PLGeometry geometry_First, PLGeometry geometry_Second)
    {
      Geometry_First = geometry_First;
      Geometry_Second = geometry_Second;
    }

  }
}

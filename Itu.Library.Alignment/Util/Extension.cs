using Grasshopper.Kernel;
using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.DrawUp;
using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Util
{
  public static class Extension
  {
    static GeometryCouple _GeometryCouple { get; set; }
    public static PLGeometry Geometry_First => _GeometryCouple.Geometry_First;
    public static PLGeometry Geometry_Second => _GeometryCouple.Geometry_Second;
    public static List<PLGeometry> GeometryList()
    {
      var geometryList = EntityBase.GetValue<List<PLGeometry>>("GeometryList");
      return geometryList;

    }
    //public static List<PLGeometry> RemainGeometry(this GeometryCouple geometryCouple)
    //{
    //  _GeometryCouple = geometryCouple;
    //  var geometryList = EntityBase.GetValue<List<PLGeometry>>("GeometryList");
    //  var remainList = geometryList.Except(new List<PLGeometry>() { Geometry_First, Geometry_Second }).ToList();
    //  return remainList;
    //}

  }

}

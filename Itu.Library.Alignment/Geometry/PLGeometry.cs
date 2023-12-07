using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.Element;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Geometry
{
  public class PLGeometry : Polyline
  {
    public bool isAligned { get; set; }
    public List<PLGeometry> AlignedGeometry { get; set; }
    public List<PLElement> ElementList => GetElementList();
    public String GeometryName { get { return geometryPrefix + "-" + geometryName; } set { geometryName = value; } }
    String geometryName;
    String geometryPrefix = "geometry";

    public PLGeometry(IEnumerable<Point3d> collection) : base(collection)
    {
      isAligned = false;
      AlignedGeometry = new List<PLGeometry>();
    }

    public Polyline[] GetElements()
    {
      var lineArray = this.BreakAtAngles(180.0);
      return lineArray;
    }

    public List<PLElement> GetElementList()
    {

      List<PLElement> elementList = new List<PLElement>();
      var lineArray = this.GetElements();

      foreach (var item in lineArray)
      {
        elementList.Add(new PLElement(item));
      }

      return elementList;
    }
    //internal List<PLGeometry> AlignedBy(CompareGeometryList compareList)
    //{
    //  return (List<PLGeometry>)compareList.Where(s => s.isAligned && s.Geometry_First.Intersect(this).Any()).Select(s => s.Geometry_Second)
    //  .Concat(compareList.Where(s => s.isAligned && s.Geometry_Second.Intersect(this).Any()).Select(s => s.Geometry_First)).ToList();
    //}

  }
}

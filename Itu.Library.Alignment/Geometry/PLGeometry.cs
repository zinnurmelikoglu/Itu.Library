using Itu.Library.Alignment.Element;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Geometry
{
  public class PLGeometry : Polyline
  {
    //private List<PLGeometry> aligned;
    public bool isAligned { get; set; }
    //public List<PLGeometry> AlignedGeometry {get{ return aligned;} set{ isAligned = true; aligned = value; }}
    //public List<PLGeometry> AlignedGeometry { get { return aligned; } set { aligned = value; } }
    public List<PLGeometry> AlignedGeometry { get; set; }
    public List<PLElement> ElementList => GetElementList();

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
  }
}

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
    public Polyline Geometry { get; set; }
    public bool isAligned { get; set; }
    public bool isSelected { get; set; }
    public Point3d CenterPoint => Geometry.CenterPoint();
    public Double Area => AreaMassProperties.Compute(this.Geometry.ToPolylineCurve()).Area;
    public List<PLGeometry> AlignedGeometry { get; set; }
    public List<PLElement> ElementList { get; }
    public double Likelihood { get; set; }
    public double Likelihood_Rounded => Math.Round(Likelihood, 3);
    public String GeometryName { get { return geometryPrefix + "-" + geometryName; } set { geometryName = value; } }
    String geometryName;
    String geometryPrefix = "geometry";

    public PLGeometry(IEnumerable<Point3d> collection) : base(collection)
    {
      this.isAligned = false;
      this.Geometry = new Polyline(collection);

      this.AlignedGeometry = new List<PLGeometry>();
      this.ElementList = GetElementList();
      
    }

    public Polyline[] GetElements()
    {
      var lineArray = this.BreakAtAngles(180.0);
      return lineArray;
    }

    List<PLElement> GetElementList()
    {

      List<PLElement> elementList = new List<PLElement>();
      var lineArray = this.GetElements();

      foreach (var item in lineArray)
      {
        elementList.Add(new PLElement(item) { Geometry = this });
      }

      return elementList;
    }

  }
}

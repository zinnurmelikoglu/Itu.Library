using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
    internal class CompareList : IEnumerable<CompareGeometry>
    {
        public List<CompareGeometry> compareList;
        public CompareList()
        {
            compareList = new List<CompareGeometry>();
        }

        public void AddGeometry(CompareGeometry geometry)
        {
            compareList.Add(geometry);
        }
        public IEnumerator<CompareGeometry> GetEnumerator()
        {
            return compareList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    public List<PLGeometry> CorrespondGeometryByGeometry(PLGeometry plGeometry)
    {
      //Geometriyi ve hangileriyle match oldugu bilgisini liste seklinde al al
      
      //return compareList.Where(s => s.isAligned && s.GeometryList.Contains(plGeometry)).SelectMany(s => s.GeometryList).ToList();

      //return compareList.Where(s => s.isAligned && (s.Geometry_First.Intersect(plGeometry).Any() || s.Geometry_Second.Intersect(plGeometry).Any()) ).SelectMany(s => s.GeometryList).ToList();

      List<PLGeometry> geometryList = new List<PLGeometry>();
      geometryList.AddRange(compareList.Where(s => s.isAligned && s.Geometry_First.Intersect(plGeometry).Any()).ToList());
      geometryList.AddRange(compareList.Where(s => s.isAligned && s.Geometry_Second.Intersect(plGeometry).Any()).SelectMany(s => s.Geometry_First).ToList());




    }

    }
}

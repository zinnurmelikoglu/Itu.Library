using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
using Itu.Library.Alignment.Prepare;
using Rhino.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class AlignedElementStatusList : IEnumerable<AlignedElementStatus>
  {
    public List<AlignedElementStatus> _AlignedElementStatusList { get; set; }

    public AlignedElementStatusList()
    {
      _AlignedElementStatusList = new List<AlignedElementStatus>();
    }

    public void AddAlignedElement(AlignedElementStatus alignedElement)
    {
      _AlignedElementStatusList.Add(alignedElement);
    }

    public void AddRangeAlignedElement(List<AlignedElementStatus> alignedElement)
    {
      _AlignedElementStatusList.AddRange(alignedElement);
    }
    public IEnumerator<AlignedElementStatus> GetEnumerator()
    {
      return _AlignedElementStatusList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    //internal List<PLElement> AlignedBy(PLElement element)
    //{
    //  return (List<PLElement>)this.Where(s => s._AbstractCompare.Element_First.Intersect(element).Any()).Select(s => s._AbstractCompare.Element_Second)
    //  .Concat(this.Where(s => s._AbstractCompare.Element_Second.Intersect(element).Any()).Select(s => s._AbstractCompare.Element_First)).ToList();
    //}

    internal List<PLGeometry> AlignedBy(PLGeometry geometry)
    {
      return (List<PLGeometry>)this.Where(s => s.AlignedGeometryCouple.Geometry_First.Intersect(geometry).Any()).Select(s => s.AlignedGeometryCouple.Geometry_Second)
      .Concat(this.Where(s => s.AlignedGeometryCouple.Geometry_Second.Intersect(geometry).Any()).Select(s => s.AlignedGeometryCouple.Geometry_First)).ToList();
    }

    internal List<PLElement> AlignedBy(PLElement element)
    {
      return (List<PLElement>)this.Where(s => s.AlignedElementCouple.Element_First.Intersect(element).Any()).Select(s => s.AlignedElementCouple.Element_Second)
      .Concat(this.Where(s => s.AlignedElementCouple.Element_Second.Intersect(element).Any()).Select(s => s.AlignedElementCouple.Element_First));

    }

    //internal List<PLGeometry> AlignedElementStatusByGeometry(PLGeometry geometry)
    //{
    //  return (List<PLGeometry>)this.Where(s => s.AlignedGeometryCouple.Geometry_First.Intersect(geometry).Any()).Select(s => s.AlignedGeometryCouple.Geometry_Second)
    //  .Concat(this.Where(s => s.AlignedGeometryCouple.Geometry_Second.Intersect(geometry).Any()).Select(s => s.AlignedGeometryCouple.Geometry_First)).ToList();
    //}

    internal List<AlignedElementStatus> AlignedElementStatusByElement(PLElement element)
    {
      return (List<AlignedElementStatus>)this.Where(s => s.AlignedElementCouple.Element_First.Element.Equals(element.Element))
      .Concat(this.Where(s => s.AlignedElementCouple.Element_Second.Element.Equals(element.Element))).ToList();

    }

  }
}

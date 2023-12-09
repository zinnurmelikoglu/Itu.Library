using Itu.Library.Alignment.Element;
using Itu.Library.Alignment.Geometry;
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
        public List<AlignedElementStatus> alignedElementStatusList { get; set; }

        public AlignedElementStatusList()
        {
            alignedElementStatusList = new List<AlignedElementStatus>();
        }

        public void AddAlignedElement(AlignedElementStatus alignedElement)
        {
            alignedElementStatusList.Add(alignedElement);
        }
        public IEnumerator<AlignedElementStatus> GetEnumerator()
        {
            return alignedElementStatusList.GetEnumerator();
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

  }
}

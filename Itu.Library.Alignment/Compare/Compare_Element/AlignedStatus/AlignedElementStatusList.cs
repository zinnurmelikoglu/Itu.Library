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
        public List<AlignedElementStatus> alignedElementList { get; set; }

        public AlignedElementStatusList()
        {
            alignedElementList = new List<AlignedElementStatus>();
        }

        public void AddAlignedElement(AlignedElementStatus alignedElement)
        {
            alignedElementList.Add(alignedElement);
        }
        public IEnumerator<AlignedElementStatus> GetEnumerator()
        {
            return alignedElementList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal List<PLElement> AlignedBy(PLElement element)
        {
          return (List<PLElement>)this.Where(s => s._AbstractCompare.Element_First.Intersect(element).Any()).Select(s => s._AbstractCompare.Element_Second)
          .Concat(this.Where(s => s._AbstractCompare.Element_Second.Intersect(element).Any()).Select(s => s._AbstractCompare.Element_First)).ToList();
        }

  }
}

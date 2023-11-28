using Rhino.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
    internal class AlignedElementStatusList : IEnumerable<AlignedElementStatus>
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

    }
}

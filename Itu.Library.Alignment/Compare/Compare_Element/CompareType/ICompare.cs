using Itu.Library.Alignment.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
    public interface ICompare
    {
        PLElement PLElement_First { get; set; }
        PLElement PLElement_Second { get; set; }
        double TolerateVal { get; set; }
        TangentType TangentType { get; }
        bool CompareElement();
        AlignedElement GetAlignedElement();
        double AlignmentStrength();
    }
}

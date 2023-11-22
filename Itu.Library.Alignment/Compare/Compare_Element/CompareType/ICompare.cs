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
        PLElement PLElement_Base { get; set; }
        PLElement PLElement_Temp { get; set; }
        double TolerateVal { get; set; }

        bool CompareElement();
        double AlignmentStrength();
    }
}

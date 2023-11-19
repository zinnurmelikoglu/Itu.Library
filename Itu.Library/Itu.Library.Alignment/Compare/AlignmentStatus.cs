using Itu.Library.Alignment.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class AlignmentStatus
  {
    public PLElement Element { get; set; }
    public Boolean Aligned { get; set; }
    public double AlignedStrength { get; set; }
    public double InBetweenObject { get; set; }

    public AlignmentStatus(PLElement element, Boolean aligned, double alignedStrength, double inBetweenObject)
    {
      Element = element;
      Aligned = aligned;
      AlignedStrength = alignedStrength;
      InBetweenObject = inBetweenObject;
    }
  }
}

using Itu.Library.Alignment.Compare;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Element
{
  internal class TangentStatus
  {
    public Double Tangent { get; set; }
    public TangentType TangentType { get; set; }
    double neutral = 0.0;
    public TangentStatus(Double tangent)=> Tangent = tangent;
    
    public TangentType GetTangentType()
    {
      if (Tangent == neutral)
        return TangentType.Neutral;
      else if (Double.IsInfinity(Tangent))
        return TangentType.Infinite;
      else if (Tangent > neutral || Tangent < Double.PositiveInfinity)
        return TangentType.Tangent;

      return TangentType.Default;
    }
  }
  public enum TangentType
  {
    [Description("Neutral")]
    Neutral,

    [Description("Infinite")]
    Infinite,

    [Description("Tangent")]
    Tangent,

    [Description("Default")]
    Default
  }
}

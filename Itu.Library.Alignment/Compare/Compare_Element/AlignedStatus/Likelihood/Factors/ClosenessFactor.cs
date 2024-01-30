using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class ClosenessFactor : LikelihoodFactor
  {
    public override string FactorName => "ClosenessFactor";
    public override double Weight => 0.5;
  }
}

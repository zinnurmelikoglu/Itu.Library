using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class ElementFactor : LikelihoodFactor
  {
    public override string FactorName => "ElementFactor";
    public override double Weight => 0.25;

    public override double GetLikelihood(double sigma, double weight)
    {
      Sigma = sigma;
      Weight = weight;

      return Likelihood = Math.Exp((-1 / (2 * Math.Pow(Sigma, 2))) * Math.Pow(Weight, 2) * Math.Pow(Factor, 2));

    }
  }
}

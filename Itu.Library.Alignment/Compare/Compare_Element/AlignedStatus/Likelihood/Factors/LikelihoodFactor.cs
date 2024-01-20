using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public abstract class LikelihoodFactor
  {
    public virtual string FactorName { get; set; }
    public virtual double Factor { get; set; }
    public double Likelihood { get; set; }
    public double Sigma { get; set; }
    public double Weight { get; set; }

    public virtual double GetLikelihood(double sigma, double weight)
    {
      Sigma = sigma;
      Weight = weight;
      return Likelihood = Math.Exp(-1 / (2 * Math.Pow(Sigma, 2)) * Math.Pow(Weight, 2) * Math.Pow(Factor - 1, 2));
      
    }

  }
}

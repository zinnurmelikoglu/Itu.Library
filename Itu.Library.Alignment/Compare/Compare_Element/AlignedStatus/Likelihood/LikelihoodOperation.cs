using Eto.Forms;
using Itu.Library.Alignment.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
    public class LikelihoodOperation
    {
        double Sigma;
        
        public double AndOperation(LikelihoodFactorList likelihoodList)
        {
        //Sigma = 0.296;

        double neutral = 0.0;
        Sigma = EntityBase.GetValue<double>("Sigma") != neutral ? EntityBase.GetValue<double>("Sigma") : new LikelihoodSigma(likelihoodList).Sigma;
        var retVal = likelihoodList.Select(s => s.GetLikelihood(Sigma, s.Weight)).Aggregate((n, m) => n * m);

          return retVal;
        }

        public double AndOperation(LikelihoodFactorList likelihoodList, double sigma)
        {

          Sigma = sigma;
          var retVal = likelihoodList.Select(s => s.GetLikelihood(Sigma, s.Weight)).Aggregate((n, m) => n * m);

          return retVal;
        }

    public double OrOperation(LikelihoodFactorList likelihoodList)
        {
          Sigma = 0.208;
          return likelihoodList.Select(s => s.GetLikelihood(Sigma, s.Weight)).Aggregate((n, m) => n * m);

        }

  }

  class LikelihoodProp
  {
    public virtual double Factor { get; set; }
    public double Likelihood { get; set; }
    public double Sigma { get; set; }
    public virtual double Weight { get; set; }

  }

}

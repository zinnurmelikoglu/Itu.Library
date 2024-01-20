using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
    public class LikelihoodOperation
    {
        public double Sigma { get; set; }
        public double Weight { get; set; }
        public LikelihoodOperation() { }

        //public double CalculateLikelihood(double sigma, double weight, double inputVal)
        //{

        //    double likelihood = 0.0;
        //    double result = Math.Exp(-1 / (2 * Math.Pow(sigma, 2)) * Math.Pow(weight, 2) * Math.Pow(y - 1, 2));

        //    return likelihood;
        //}

        public double AndOperation(List<Likelihood> likelihoodList)
        {
          Sigma = 0.296;
          Weight = 0.5;
          //var retVal = likelihoodList.Select(s => s.LikelihoodVal).Aggregate((s, t) => s * t);
          var retVal = likelihoodList.Select(s => s.GetLikelihoodVal(Sigma, Weight)).Aggregate((n, m) => n * m);

          double andVal = 1.0;
          foreach (var item in likelihoodList)
          {
            //andVal = item.LikelihoodVal * andVal;
            andVal = item.GetLikelihoodVal(Sigma, Weight) * andVal;
          }

          return andVal;
        }

        public double AndOperation(LikelihoodFactorList likelihoodList)
        {
          Sigma = 0.296;
          Weight = 0.5;
          //var retVal = likelihoodList.Select(s => s.LikelihoodVal).Aggregate((s, t) => s * t);
          var retVal = likelihoodList.Select(s => s.GetLikelihood(Sigma, Weight)).Aggregate((n, m) => n * m);

          double andVal = 1.0;
          foreach (var item in likelihoodList)
          {
            //andVal = item.LikelihoodVal * andVal;
            andVal = item.GetLikelihood(Sigma, Weight) * andVal;
          }

          return andVal;
        }

    public double OrOperation()
        {
          Sigma = 0.296;
          Weight = 0.5;

          return 0.0;
        }

        public double CalculateSigma(double sigma, double weight, double inputVal)
        {
          Range<int> range = new Range<int>(0,1);
            
          //I will calculate later, but now sigma will be accepted 0.296 for AND op, 0.*** for OR operation
          return 0.296;
        }

  }
}

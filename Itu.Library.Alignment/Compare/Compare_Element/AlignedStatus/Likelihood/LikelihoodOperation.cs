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
        //public double Sigma { get; set; }
        double Sigma;
        //public double Weight { get; set; }
        
        public double AndOperation(LikelihoodFactorList likelihoodList)
        {

          Sigma = 0.296;
          //Weight = 0.5;
          var retVal = likelihoodList.Select(s => s.GetLikelihood(Sigma, s.Weight)).Aggregate((n, m) => n * m);

          double andVal = 1.0;
          foreach (var item in likelihoodList)
          {
            var weight = item.Weight;
            andVal = item.GetLikelihood(Sigma, weight) * andVal;
          }

          return andVal;
        }

        public double OrOperation(AlignedElementStatusList alignedElementStatusList)
        {
          Sigma = 0.296;
          //Weight = 0.5;

          var retVal = likelihoodList.Select(s => s.GetLikelihood(Sigma, s.Weight)).Aggregate((n, m) => n * m);

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

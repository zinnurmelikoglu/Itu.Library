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
        double Sigma;
        
        public double AndOperation(LikelihoodFactorList likelihoodList)
        {

          Sigma = 0.296;
          var retVal = likelihoodList.Select(s => s.GetLikelihood(Sigma, s.Weight)).Aggregate((n, m) => n * m);

          return retVal;
        }

        public double OrOperation(LikelihoodFactorList likelihoodList)
        {
          Sigma = 0.208;
          return likelihoodList.Select(s => s.GetLikelihood(Sigma, s.Weight)).Aggregate((n, m) => n * m);

        }

        public double CreateSigma(double sigma, double weight, double inputVal)
        {
          Range<int> range = new Range<int>(0,1);
            
          //I will calculate later, but now sigma will be accepted 0.296 for AND op, 0.*** for OR operation
          return 0.296;
        }

  }
}

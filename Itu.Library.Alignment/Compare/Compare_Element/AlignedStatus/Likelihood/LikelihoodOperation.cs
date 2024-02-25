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

        var sigma = EntityBase.GetValue<double>("Sigma");
        Sigma = sigma == double.NaN ? new LikelihoodSigma(likelihoodList).Sigma : sigma;

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

    //public double CreateSigma(double sigma, double weight, double inputVal)
    //{
    //  Range<int> range = new Range<int>(0,1);

    //  //I will calculate later, but now sigma will be accepted 0.296 for AND op, 0.*** for OR operation
    //  return 0.296;
    //}


    /*****************************  I just tried  ************************************/
    /*
    public double CreateSigma(LikelihoodFactorList likelihoodList)
    {
      double sigma = 0.0;
      const int arrCount = 11;
      var listCount = likelihoodList.Count();

      double[] sigmaArr = Enumerable.Range(0, 10000).Select(s => s / 10000.0).ToArray();
      double[] factorArr = Enumerable.Range(0, arrCount).Select(s => s / 10.0).ToArray();



      double[,] likelihood =new double[listCount, arrCount];
      double[] multiArr = new double[arrCount];
      double[] subtractionArr = new double[arrCount];

      double result = 1.0;
      double _sigma = 1.0;

      double inc = 0.0001;

      //for (double i = 0.0; i < 1.0; i = i + inc)
      foreach (var i in sigmaArr)
      {
        //i = 0.296305;

        int factorCount = 0;
        foreach (var item in likelihoodList)
        {

          int y = 0;
          foreach (var factor in factorArr)
          {
            likelihood[factorCount, y] = Math.Exp(-1 / (2 * Math.Pow(i, 2)) * Math.Pow(item.Weight, 2) * Math.Pow(factor- 1, 2));
            //likelihood[factorCount, y] = item.GetLikelihood(i, factor);
            y++;
          }
          factorCount++;

        }

        int j = 0;
        for (int b = 0; b < likelihood.GetLength(1); b++)
        {
          double multi = 1.0;
          for (int a = 0; a < likelihood.GetLength(0); a++)
          {

            multi *= likelihood[a, b];

          }
          multiArr[j] = multi;
          j++;

        }


        for (int s = 0; s < factorArr.Length; s++)
        {
          subtractionArr[s] = factorArr[s] - multiArr[s];
        }

        var resultArr = subtractionArr.Select(s=> Math.Pow(s, 2)).ToArray();
        var sub = resultArr.Sum();

        if (sub < result)
        {
          result = sub;
          _sigma = i;
        }

      }

      sigma = _sigma;  //29684860000750612, 296848, 296687, 0.296799 
      return result;

      //Range<int> range = new Range<int>(0, 1);

      ////I will calculate later, but now sigma will be accepted 0.296 for AND op, 0.*** for OR operation
      //return 0.296;
    }
    */
    /*****************************  I just tried  ************************************/


  }

  class LikelihoodProp
  {
    public virtual double Factor { get; set; }
    public double Likelihood { get; set; }
    public double Sigma { get; set; }
    public virtual double Weight { get; set; }

  }

}

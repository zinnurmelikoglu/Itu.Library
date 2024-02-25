using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class LikelihoodSigma
  {
    LikelihoodFactorList _LikelihoodList;
    const int arrCount = 11;
    const double sigmaCount = 10000;

    public LikelihoodSigma(LikelihoodFactorList likelihoodList)
    {
      _LikelihoodList = likelihoodList;
    }

    public double CreateSigma()
    {
      //double sigma = 0.0;
      var factorCount = _LikelihoodList.Count();

      double[] sigmaArr = Enumerable.Range(0, 10000).Select(s => s / 10000.0).ToArray();
      double[] factorArr = Enumerable.Range(0, arrCount).Select(s => s / 10.0).ToArray();

      double[,] likelihoodArr = new double[factorCount, arrCount];
      double[][] likelihoodArr_ = new double[2][] ;
      double[] multiArr = new double[arrCount];
      double[] subtractionArr = new double[arrCount];

      double result = 1.0;
      double _sigma = 1.0;

      foreach (var sigma in sigmaArr)
      {

        int count = 0;
        foreach (var item in _LikelihoodList)
        {
          int y = 0;
          foreach (var factor in factorArr)
          {
            likelihoodArr[count, y] = item.GetLikelihood(sigma, item.Weight, factor);
            y++;
          }
          count++;

        }

        int j = 0;
        for (int b = 0; b < likelihoodArr.GetLength(1); b++)
        {
          double multi = 1.0;
          for (int a = 0; a < likelihoodArr.GetLength(0); a++)
          {

            multi *= likelihoodArr[a, b];

          }
          multiArr[j] = multi;
          j++;

        }


        for (int s = 0; s < factorArr.Length; s++)
        {
          subtractionArr[s] = factorArr[s] - multiArr[s];
        }

        var resultArr = subtractionArr.Select(s => Math.Pow(s, 2)).ToArray();
        var sub = resultArr.Sum();

        if (sub < result)
        {
          result = sub;
          _sigma = i;
        }



      }

      sigma = _sigma;  //29684860000750612, 296848, 296687, 0.296799 
      return result;



    }

  }
}

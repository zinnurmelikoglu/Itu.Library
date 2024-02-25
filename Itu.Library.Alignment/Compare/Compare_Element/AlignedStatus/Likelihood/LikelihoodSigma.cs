using Itu.Library.Alignment.Util;
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
    public double Sigma => CreateSigma();
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
      double[] multiArr = new double[arrCount];
      double[] subArr = new double[arrCount];

      double result = 1.0;
      double _sigma = 1.0;

      foreach (var sigma in sigmaArr)
      {

        #region Fill likelihood Array
        int count = 0;
        foreach (var item in _LikelihoodList)
        {
          int i = 0;
          foreach (var factor in factorArr)
          {
            likelihoodArr[count, i] = item.GetLikelihood(sigma, item.Weight, factor);
            i++;
          }
          count++;

        }
        #endregion

        #region Array Multiplication
        for (int line = 0; line < arrCount; line++)
        {
          double val = 1.0;
          for (int group = 0; group < factorCount; group++)
          {
            val *= likelihoodArr[group, line];
          }
          multiArr[line] = val;
        }
        #endregion

        #region Subtraction Between Multiplication and Factor Array
        for (int s = 0; s < arrCount; s++)
        {
          subArr[s] = factorArr[s] - multiArr[s];
        }
        #endregion

        var resultArr = subArr.Select(s => Math.Pow(s, 2)).ToArray();
        var sum = resultArr.Sum();

        if (sum < result)
        {
          result = sum;
          _sigma = sigma;
        }

      }

      //29684860000750612, 296848, 296687, 0.296799
      EntityBase.SetValue<double>("Sigma", _sigma);
      return _sigma;

    }

  }
}

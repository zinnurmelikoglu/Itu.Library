using Itu.Library.Alignment.Element;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  public class Likelihood
  {
    public double Sigma { get; set; }
    public double Weight { get; set; }
    public double InputVal { get; set; }
    public List<double> InputValList { get; set; }
    //public double LikelihoodVal => Math.Exp(-1 / (2 * Math.Pow(Sigma, 2)) * Math.Pow(Weight, 2) * Math.Pow(InputVal - 1, 2));
    public double LikelihoodVal { get; set; }
    public List<double> LikelihoodListVal { get; set; }
    public Likelihood() { }
    public Likelihood(double inputVal) => InputVal = inputVal;
    public Likelihood(double sigma, double weight, double inputVal)
    {
      Sigma = sigma;
      Weight = weight;
      InputVal = inputVal;

    }

    public double GetLikelihoodVal(double sigma, double weight)
    {
      Sigma = sigma;
      Weight = weight;
      return LikelihoodVal = Math.Exp(-1 / (2 * Math.Pow(Sigma, 2)) * Math.Pow(Weight, 2) * Math.Pow(InputVal - 1, 2));
    }

    public List<double> GetLikelihoodList(double sigma, double weight, List<double> list)
    {
      Sigma = sigma;
      Weight = weight;
      foreach (var item in list) {
        LikelihoodListVal.Add(Math.Exp(-1 / (2 * Math.Pow(Sigma, 2)) * Math.Pow(Weight, 2) * Math.Pow(item - 1, 2)));
      }

      return LikelihoodListVal;
    }


  }
}

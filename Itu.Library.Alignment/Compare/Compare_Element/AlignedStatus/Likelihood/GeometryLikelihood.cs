﻿using Itu.Library.Alignment.Geometry;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Compare
{
  internal class GeometryLikelihood
  {
    List<PLGeometry> GeometryList { get; set; }
    AlignedElementStatusList _AlignedElementStatusList { get; set; }
    public GeometryLikelihood(List<PLGeometry> geometryList)
    {
      GeometryList = geometryList;
    }

    public double CreateGeometryLikelihood(AlignedElementStatusList alignedElementStatusList)
    {
      _AlignedElementStatusList = alignedElementStatusList;
      double neutral = 0.00;

      foreach (var geometry in GeometryList)
      {
        var elementList = geometry.ElementList;
        LikelihoodFactorList likelihoodFactorList = new LikelihoodFactorList();
        
        foreach (var element in elementList)
        {
          var elementStatusByElement = _AlignedElementStatusList.GetAlignedElementStatusList(element);
          ElementFactor elementFactor = new ElementFactor();
          
          if (elementStatusByElement.Count > 0)
          {
            AlignedElementStatus elementStatus = elementStatusByElement.Aggregate((i1, i2) => i1.AlignedStrength > i2.AlignedStrength ? i1 : i2);
            elementFactor.Factor = elementStatus.AlignedStrength;

          }
          else
            elementFactor.Factor = neutral;

          likelihoodFactorList.AddLikelihoodFactor(elementFactor);

        }

        /* This code line below assigns geometry likelihood to likelihood property in geometry class */
        geometry.Likelihood = likelihoodFactorList._LikelihoodFactorList.Count > 0 ? 1 - new LikelihoodOperation().OrOperation(likelihoodFactorList) : neutral;

      }

      return CreateAverageLikelihood();

    }

    public double CreateAverageLikelihood()
    {
      var total = 0.0;
      double divider = 0.0;

      foreach (var item in GeometryList)
      {
        var likelihood = item.Likelihood;
        double area = item.Area;
        total += likelihood * area;
        divider += area;
      }
      return total / divider;
      
    }

  }
}

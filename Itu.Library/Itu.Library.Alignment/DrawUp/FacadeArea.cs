using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.DrawUp
{
  public class FacadeArea
  {
    public double Width { get; set; }
    public double Height { get; set; }

    public FacadeArea(double width, double height)
    {
      Width = width;
      Height = height;
    }
  }
}

using Grasshopper.Kernel;
using Rhino.Geometry;
using Rhino.Render.ChangeQueue;
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

    private Dictionary<string, FacadeArea> facadeDict = new Dictionary<string, FacadeArea>();

    public FacadeArea(double width, double height)
    {
      Width = width;
      Height = height;
    }

  }
}

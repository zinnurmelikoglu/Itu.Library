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

    //Spor olayi???

    //Kadikoy cok hos, isiklandirmalar insan olcegi

    //Baglam neden gelecekle baglanmali

    public FacadeArea(double width, double height)
    {
      Width = width;
      Height = height;
    }

    //public FacadeArea()
    //{
    //  Rectangle3d area = new Rectangle3d();
    //  IGH_DataAccess.GetData("area", ref area);
      
    //}

  }
}

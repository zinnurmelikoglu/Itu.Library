using Itu.Library.Alignment.Util;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.DrawUp
{
  public class DrawAlignment
  {

    private FacadeArea Area { get; set; }
    private double CoordX { get; set; }
    private double CoordY { get; set; }
    private double TanVal { get; set; }

    private double Width { get; set; }
    private double Height { get; set; }

    //    public double pointFirst;
    //    public double pointSecond;
    //    public double target;

    //public DrawAlignment(double coordX, double coordY, double tanVal, FacadeArea area)
    //{
    //  Area = area;
    //  Width = Area.Width;
    //  Height = Area.Height;
    //  CoordX = coordX;
    //  CoordY = coordY;
    //  TanVal = tanVal;
    //}

    public DrawAlignment(double coordX, double coordY, double tanVal)
    {
      Area = EntityBase.GetValue<FacadeArea>("area");
      Width = Area.Width;
      Height = Area.Height;
      CoordX = coordX;
      CoordY = coordY;
      TanVal = tanVal;
    }

    public Line GenerateAlignment()
    {
      Point3d pointFirst = new Point3d();
      Point3d pointSecond = new Point3d();
      Double neutral = Convert.ToDouble(0);

      double coordX = CoordX;
      double coordY = CoordY;
      double tanVal = TanVal;

      if (tanVal == neutral)
      {
        pointFirst = new Point3d(Width, coordY, neutral);
        pointSecond = new Point3d(neutral, coordY, neutral);
      }
      else if (Double.IsInfinity(tanVal))
      {
        pointFirst = new Point3d(coordX, neutral, neutral);
        pointSecond = new Point3d(coordX, Height, neutral);

      }
      else if (tanVal > neutral)
      {
        //        var x = (-( coordY / tanVal ) + coordX);
        //        var y = (-( tanVal * coordX ) + coordY);
        //
        //        pointFirst = new Point3d(x, neutral, neutral);
        //        pointSecond = new Point3d(neutral, y, neutral);

        var x1 = (-(coordY / tanVal) + coordX);
        var y1 = (tanVal * (-x1));

        var x2 = Width;
        var y2 = (tanVal * (Width - x1));



        pointFirst = new Point3d(x1 < neutral ? neutral : x1, y1 < neutral ? neutral : y1, neutral);
        //pointFirst = new Point3d(x, neutral, neutral);
        pointSecond = new Point3d(x2, y2, neutral);

      }
      else if (tanVal < neutral)
      {
        var x = -((coordY / tanVal) - coordX);
        var y = -((tanVal * coordX) - coordY);

        pointFirst = new Point3d(x, neutral, neutral);
        pointSecond = new Point3d(neutral, y, neutral);
      }

      return new Line(pointFirst, pointSecond);

    }

  }
}

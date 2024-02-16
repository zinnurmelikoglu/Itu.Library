using Itu.Library.Alignment.Element;
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

    public Point3d Point_First;
    public Point3d Point_Second;
    public TangentType TanType;


    public DrawAlignment(PLElement element)
    {
      Area = EntityBase.GetValue<FacadeArea>("area");
      Width = Area.Width;
      Height = Area.Height;
      CoordX = element.PointFirst.X;
      CoordY = element.PointFirst.Y;
      TanVal = element.TanVal;
    }

    public DrawAlignment(Point3d point_First, Point3d point_Second, TangentType tanType)
    {
      Point_First = point_First;
      Point_Second = point_Second;
      TanType = tanType;
    }

    public Line GenerateAlignmentLine()
    {
      if (TangentType.Neutral.Equals(TanType))
        return new Line(Point_First, new Point3d(Point_Second.X, Point_First.Y, 0.0));
      else if (TangentType.Infinite.Equals(TanType))
        return new Line(Point_First, new Point3d(Point_First.X, Point_Second.Y, 0.0));

      return new Line(Point_First, Point_Second);
    }

  }
}

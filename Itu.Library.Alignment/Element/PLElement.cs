using Itu.Library.Alignment.Compare;
using Itu.Library.Alignment.Geometry;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Element
{
  public class PLElement : Polyline
  {
    public Polyline Element { get; set; }
    public PLGeometry Geometry { get; set; }
    public List<PLCoordinate> Coord { get; set; }
    public List<Point3d> PointList { get; set; }

    public Point3d PointFirst { get { return PointList[0]; } }
    public Point3d PointSecond { get { return PointList[1]; } }
    public Point3d PointCenter { get { return getMiddlePoint(Element); } }

    public double TanVal { get { return calTanVal(PointFirst, PointSecond); } }
    public double TanVal_Rounded { get { return Math.Round(TanVal, 2); } }
    Func<Point3d, Point3d, double> calTanVal = GetTanVal;

    public TangentType TanType { get; }

    public double Ref_X { get { return getRefX(PointFirst, PointSecond, TanVal); } }
    public double Ref_Y { get { return getRefY(PointFirst, PointSecond, TanVal); } }

    Func<Point3d, Point3d, double, double> getRefX = GetReferenceX;
    Func<Point3d, Point3d, double, double> getRefY = GetReferenceY;
    Func<Polyline, Point3d> getMiddlePoint = GetMiddlePoint;

    public double Likelihood { get; set; }

    public PLElement(Polyline polyline)
    {
      Element = polyline;
      PointList = new List<Point3d>();
      polyline.ForEach(m => { PointList.Add(m); });
      TanType = new TangentStatus(TanVal).GetTangentType();
      
    }

    public static double GetTanVal(Point3d p1, Point3d p2)
    {
      double tanVal;

      tanVal = (p2.Y - p1.Y) / (p2.X - p1.X);
      return tanVal;
    }

    public static double GetReferenceX(Point3d p1, Point3d p2, double tanVal)
    {
      double reference;
      reference = double.IsInfinity(tanVal) ? p1.X : tanVal == 0 ? p1.X : tanVal > 0 ? -(p1.Y / tanVal) + p1.X : -(p1.Y / tanVal - p1.X);

      return Math.Round(reference, 2);
    }

    public static double GetReferenceY(Point3d p1, Point3d p2, double tanVal)
    {
      double reference;
      reference = tanVal > 0 ? -(tanVal * p1.X) + p1.Y : -(tanVal * p1.X - p1.Y);
      return Math.Round(reference);
    }

    //public static Point3d GetMiddlePoint(Point3d p1, Point3d p2)
    public static Point3d GetMiddlePoint(Polyline element)
    {
      //double neutral = 0.00;
      ////reference = tanVal > 0 ? -(tanVal * p1.X) + p1.Y : -(tanVal * p1.X - p1.Y);

      //var pX = Math.Abs(p1.X - p2.X);
      //var pY = Math.Abs(p1.Y - p2.Y);



      //return new Point3d(p1.X + pX, p1.Y + pY, neutral);

      return element.CenterPoint();

    }

  }
}

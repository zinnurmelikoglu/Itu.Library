using Itu.Library.Alignment.Element;
using Rhino.Geometry;

namespace Itu.Library.Alignment.Compare
{
  public abstract class AbstractCompare
  {
    public virtual PLElement Element_First { get; set; }
    public virtual PLElement Element_Second { get; set; }
    public virtual double TolerateVal => new TolerationSum(new ElementCouple(Element_First, Element_Second)).GetTolerationVal();
    public abstract TangentType TangentType { get; }
    public virtual bool isAligned
    {
      get { return _isAligned; }
      set { _isAligned = value; }
    }
    private bool _isAligned;
    public virtual Point3d point_First => Element_First.Element.ClosestPoint(Element_Second.PointFirst);
    public virtual Point3d point_Second => Element_Second.Element.ClosestPoint(Element_First.PointFirst);
    protected virtual AlignedElementStatus _AlignedElementStatus { get; set; }
    
    public AbstractCompare() { }
    public AbstractCompare(PLElement element_First, PLElement element_Second)
    {
      Element_First = element_First;
      Element_Second = element_Second;

    }
    public abstract bool CompareElement();
    public virtual AlignedElementStatus GetAlignedElementStatus()
    {
      var alignedElementCouple = new ElementCouple(Element_First, Element_Second);
      _AlignedElementStatus = new AlignedElementStatus(alignedElementCouple);
      return _AlignedElementStatus;
    }
    
  }
}

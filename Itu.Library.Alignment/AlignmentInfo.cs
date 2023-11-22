using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace Itu.Library.Alignment
{
  public class AlignmentInfo : GH_AssemblyInfo
  {
    public override string Name => "Itu.Library.Alignment";

    //Return a 24x24 pixel bitmap to represent this GHA library.
    public override Bitmap Icon => null;

    //Return a short string describing the purpose of this GHA library.
    public override string Description => "";

    public override Guid Id => new Guid("895de36e-21ce-4e4d-b68e-2de5df934b87");

    //Return a string identifying you or your company.
    public override string AuthorName => "Osman Zinnur Melikoglu";

    //Return a string representing your preferred contact details.
    public override string AuthorContact => "";
  }
}
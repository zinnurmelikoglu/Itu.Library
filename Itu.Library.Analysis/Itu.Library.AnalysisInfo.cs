using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace Itu.Library.Analysis
{
  public class Itu_Library_AnalysisInfo : GH_AssemblyInfo
  {
    public override string Name => "Itu.Library.Analysis";

    //Return a 24x24 pixel bitmap to represent this GHA library.
    public override Bitmap Icon => null;

    //Return a short string describing the purpose of this GHA library.
    public override string Description => "";

    public override Guid Id => new Guid("a42a6984-df2b-442e-98e0-0a9680a8400e");

    //Return a string identifying you or your company.
    public override string AuthorName => "";

    //Return a string representing your preferred contact details.
    public override string AuthorContact => "";
  }
}
using Grasshopper;
using Grasshopper.Kernel.Data;
using Itu.Library.Alignment.Compare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Prepare
{
  internal class PrepareDataTree<T>
  {
    List<T> ObjectList { get; set; }
    public PrepareDataTree(List<T> objectList)
    {
      ObjectList = objectList;
    }

    public DataTree<object> GetDataTree()
    {
      DataTree<object> alignedTree = new DataTree<object>();

      var j = 0;
      foreach (var item in ObjectList)
      {
        Type type = item.GetType();
        var properties = type.GetProperties();
        GH_Path path = new GH_Path(0, j);

        foreach (var property in properties) {
          alignedTree.Add(property.GetValue(item), path);
        }

        j++;
      }
      return alignedTree;
    }

  }
}

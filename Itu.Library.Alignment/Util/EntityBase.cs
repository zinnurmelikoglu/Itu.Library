using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Library.Alignment.Util
{
  public static class EntityBase
  {
    public static Dictionary<string, object> _dict = new Dictionary<string, object>();
    public static T GetValue<T>(string columnName)
    {
      return _dict.ContainsKey(columnName) ? (T)_dict[columnName] : (T)default;
    }
    public static void SetValue<T>(string columnName, T value)
    {
      if (!_dict.ContainsKey(columnName))
        _dict.Add(columnName, value);

    }

  }
}

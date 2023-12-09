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
    public static T GetValue<T>(string dictName)
    {
      return _dict.ContainsKey(dictName) ? (T)_dict[dictName] : (T)default;
    }
    public static void SetValue<T>(string dictName, T value)
    {
      if (!_dict.ContainsKey(dictName))
        _dict.Add(dictName, value);

    }
    public static void RemoveDictionary(string dictName) => _dict.Remove(dictName);
    public static void ClearDictionary() => _dict.Clear();

  }
}

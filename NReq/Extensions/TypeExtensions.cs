using System.Reflection;

namespace NReq.Extensions;

public static class TypeExtensions
{
  public static Type[] GetAllTypesInNamespace(this Type t)
  {
    string? targetNamespace = t.Namespace;
    var a = t.Assembly;

    return a.GetTypes()
      .Where(t => string.Equals(t.Namespace, targetNamespace, StringComparison.Ordinal))
      .Where(t => t.IsVisible) // Exclude display classes
      .ToArray();
  }
}
using System.Reflection;

namespace NReq.Extensions;

public static class AssemblyExtensions
{
  public static IList<Type> GetTypesDerivedFrom<T>(this Assembly assembly) => assembly.GetTypesDerivedFrom(typeof(T));

  /// <summary>
  /// Finds all types in the given assembly that are derived from the given base type.
  /// </summary>
  /// <param name="baseType">The base type to find derivatives of.</param>
  /// <returns>A list of derived types.</returns>
  public static IList<Type> GetTypesDerivedFrom(this Assembly assembly, Type baseType) => assembly
    .GetTypes()
    .Where(baseType.IsAssignableFrom)
    .ToList();

  /// <summary>
  /// Return all members in the given assembly that have the given attribute.
  /// </summary>
  public static IList<RequirementImplementation> FindAttributeUses(this Assembly assembly, Type attributeType) => assembly
    .GetTypes()
    .FindAttributeUses(attributeType);

  /// <summary>
  /// Return all members in the given types that have the given attribute.
  /// </summary>
  public static IList<RequirementImplementation> FindAttributeUses(this IEnumerable<Type> types, Type attributeType) => types 
    .Select(type => new { type, members = type.GetMembers() })
    .Select(pair => new
    {
      pair,
      attrs = pair.members
        .SelectMany(m => m.GetCustomAttributes(attributeType, true)).ToArray()
    })
    .Where(pair =>
    {
      return pair.attrs.Any(att => att.GetType().Name == attributeType.Name);
    })
    .Select(pair => new RequirementImplementation
    {
      ImplementingType = pair.pair.type,
      ImplementedRequirements = pair.attrs
        .Select(attr => attr.GetType().GenericTypeArguments.First())
        .Zip(pair.pair.members).ToDictionary(),
    })
    .ToList();
}
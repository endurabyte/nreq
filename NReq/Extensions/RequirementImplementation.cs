using System.Reflection;

namespace NReq.Extensions;

public class RequirementImplementation
{
  /// <summary>
  /// E.g. ICalculator
  /// </summary>
  public required Type ImplementingType { get; set; }

  /// <summary>
  /// Key = type of implemented requirement e.g. DivideRequirement.GetType(), 
  /// Value = implementing member, e.g. ICalculator.Divide
  /// </summary>
  public required Dictionary<Type, MemberInfo> ImplementedRequirements { get; set; }
}

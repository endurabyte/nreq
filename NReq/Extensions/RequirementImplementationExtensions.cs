namespace NReq.Extensions;

public static class RequirementImplementationExtensions
{
  public static string PrintAsMarkdown(this RequirementImplementation impl) =>
      $"# {impl.ImplementingType.Name}\n\n"
    + $"## Implements\n\n"
    + $"- {string.Join("\n- ", impl.ImplementedRequirements.Values.Select(v => $"{v.DeclaringType}.{v.Name}"))}";
}
using NReq.Spec;

namespace NReq.Extensions;

public static class RequirementExtensions
{
  public static string PrintAsMarkdown(this Requirement r) =>
      $"# {r.GetType().Name}\n\n"
    + $"## Description\n\n"
    + $"{r.Description}\n\n";
}

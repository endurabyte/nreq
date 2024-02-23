using NReq.Design;

namespace NReq.Extensions;

public static class DecisionExtensions
{
  public static string PrintAsMarkdown(this Decision d) =>
      $"# {d.GetType().Name}\n\n"
    + $"## Status\n\n"
    + $"{d.Status}\n\n"
    + $"## Context\n\n"
    + $"{d.Context}\n\n"
    + $"## Decision\n\n"
    + $"{d.Description}\n\n"
    + $"## Consequences\n\n"
    + $"- {string.Join("\n- ", d.Consequences)}";
}
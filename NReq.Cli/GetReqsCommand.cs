using System.Reflection;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using NReq.Analysis;
using NReq.Extensions;
using NReq.Spec;

[Command("reqs", Description = "Generate a report on all requirements in the given list of assemblies.")]
public class GetReqsCommand : ICommand
{
  [CommandOption("outDir", IsRequired = true)]
  public required string OutDir { get; set; }

  [CommandOption("assemblies", IsRequired = true)]
  public required IReadOnlyList<string> Assemblies { get; set; }

  public async ValueTask ExecuteAsync(IConsole console)
  {
    var cwd = Directory.GetCurrentDirectory();
    var assemblies = Assemblies
      .Select(path => Assembly.LoadFile(Path.GetFullPath(Path.Combine(cwd, path))))
      .ToList();

    var finder = new ReqsFinder();

    var reqTypes = finder.GetReqs(assemblies.First());
    var implementationAssemblies = finder.GetReqImpls(assemblies);

    await WriteReqs(reqTypes);
    await WriteImpls(implementationAssemblies);
  }

  private async Task WriteImpls(Dictionary<Assembly, IList<RequirementImplementation>> implementationAssemblies)
  {
    Directory.CreateDirectory(Path.Combine(OutDir, "Implementations"));
    foreach (var ass in implementationAssemblies)
    {
      foreach (RequirementImplementation impl in ass.Value)
      {
        string file = $"{Path.GetFullPath(Path.Combine(OutDir, "Implementations", impl.ImplementingType.Name))}.md";

        await File.WriteAllTextAsync(file, impl.PrintAsMarkdown());
      }
    }
  }

  private async Task WriteReqs(IList<Type> reqTypes)
  {
    Directory.CreateDirectory(Path.Combine(OutDir, "Reqs"));
    foreach (var req in reqTypes)
    {
      var instance = (Requirement)Activator.CreateInstance(req)!; // Convert Requirement type to Requirement instance
      string file = $"{Path.GetFullPath(Path.Combine(OutDir, "Reqs", instance.GetType().Name))}.md";

      await File.WriteAllTextAsync(file, instance.PrintAsMarkdown());
    }
  }
}

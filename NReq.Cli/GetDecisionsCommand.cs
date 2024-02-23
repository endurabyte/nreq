using System.Reflection;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using NReq.Design;
using NReq.Extensions;

[Command("decisions", Description = "Generate a report on all design decisions in the given list of assemblies.")]
public class GetDecisionsCommand : ICommand
{
  [CommandOption("outDir", IsRequired = true)]
  public required string OutDir { get; set; }

  [CommandOption("assemblies", IsRequired = true)]
  public required IReadOnlyList<string> Assemblies { get; set; }

  public async ValueTask ExecuteAsync(IConsole console)
  {
    Directory.CreateDirectory(OutDir);
    var cwd = Directory.GetCurrentDirectory();

    foreach (var path in Assemblies)
    {
      var a = Assembly.LoadFile(Path.Combine(cwd, path));
      var decisions = a.GetTypesDerivedFrom<Decision>().ToList();

      foreach (var d in decisions)
      {
        var instance = (Decision)Activator.CreateInstance(d)!;
        string file = $"{Path.GetFullPath(Path.Combine(OutDir, instance.GetType().Name))}.md";

        await File.WriteAllTextAsync(file, instance.PrintAsMarkdown());
      }
    }
  }
}
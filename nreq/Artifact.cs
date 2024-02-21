namespace NReq;

/// <summary>
/// An artifact is a generic term for the work output of a software project.
///
/// <para/>
/// There are two categories of artifacts: <see cref="TangibleArtifact"/> and <see cref="IntangibleArtifact"/>, i.e. visible and invisible.
/// 
/// <para/>
/// The tangible artifacts are <see cref="Design.Feature"/> and <see cref="Risk.Bug"/>.
/// 
/// <para/>
/// Analogously, the intangible artifacts are <see cref="Design.Decision"/> (for example, software architecture)
/// and <see cref="Risk.Debt"/> (tech debt).
/// 
/// <para/>
/// In summary:
/// 
/// <code>
///        Visible      Invisible
///       _________________________
///      |           |             |
/// Good |   Feature |    Design   |
///      |           |             |
/// Bad  |    Bug    |   Tech debt |
///      |___________|_____________|
/// </code>
/// </summary>
public abstract class Artifact
{
  public abstract string Description { get; }

  /// <summary>
  /// Use this if the artifact is tracked in an external tool e.g. GitHub Issues, JIRA, Azure DevOps, etc.
  /// </summary>
  public string? URL { get; }
}

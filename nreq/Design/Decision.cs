namespace NReq.Design;

public abstract class Decision : IntangibleArtifact
{
  public abstract DecisionStatus Status { get; }
  public abstract string Context { get; }
  public abstract string[] Consequences { get; }
}

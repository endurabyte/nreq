using NReq.Design;

namespace Calculators.Design.Decision;

public class DD2_ThreeExampleCalculatorsDecision : NReq.Design.Decision
{
  public override DecisionStatus Status => DecisionStatus.Accepted;

  public override string Context => $"The Calculators example project exists to show how use NReq " +
    $"to explicitly annotate features, bugs, tech debt, design decision, risks, mitigations, etc." +
    $"It would be good to show uses of all of these.";

  public override string Description => $"There shall be three calculators: a lenient one, a strict one, and a buggy one.";

  public override string[] Consequences => new[] 
  {
    "Users will have an easier time getting started"
  };
}
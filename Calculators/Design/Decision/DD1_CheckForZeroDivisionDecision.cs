using Calculators.Risk;
using NReq.Design;
using NReq.Risk;

namespace Calculators.Design.Decision;

[MitigatesRisk<DivideByZeroRisk>()]
public class DD1_CheckForZeroDivisionDecision : NReq.Design.Decision
{
  public override DecisionStatus Status => DecisionStatus.Accepted;

  public override string Context => $"It's possible to divide by zero. Without mitigation, a {nameof(DivideByZeroException)} will be thrown.";

  public override string Description => $"The implementation shall check for zero division. If so, it will return {0}.";

  public override string[] Consequences => new[]
  {
     $"The user will have to handle the possibility of {0} being returned",
     $"The user not have to catch {nameof(DivideByZeroException)}",
   };
}
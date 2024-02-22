using Calculators.Design.Decision;
using NReq.Design;

namespace Calculators.Design.Features;

[ImplementsDecision<DD1_CheckForZeroDivisionDecision>]
public class CheckForZeroDivisionFeature : Feature
{
  public override string Description => "Checks for division by zero";
}

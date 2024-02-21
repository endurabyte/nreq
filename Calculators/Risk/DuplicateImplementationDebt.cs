using NReq.Risk;

namespace Calculators.Risk;

public class DuplicateImplementationDebt : Debt
{
  public override string Description => $"The three calculators " +
    $"{nameof(LenientCalculator)}, {nameof(StrictCalculator)}, and {nameof(BuggyCalculator)} " +
    $"have identical implementations of " +
    $"{nameof(ICalculator.Add)}, {nameof(ICalculator.Subtract)}, and {nameof(ICalculator.Multiply)}. " +
    $"They can be consolidated.";
}
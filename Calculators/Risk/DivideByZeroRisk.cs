namespace Calculators.Risk;

public class DivideByZeroRisk : NReq.Risk.Risk
{
  public override string Description { get; } = "User attempts to divide by zero";
}
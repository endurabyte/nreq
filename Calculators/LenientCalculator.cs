using Calculators.Design.Features;
using Calculators.Logic;
using Calculators.Risk;
using NReq.Design;
using NReq.Logic;
using NReq.Risk;

namespace Calculators;

public class LenientCalculator : ICalculator
{
  public int Add(int a, int b) => a + b;
  public int Subtract(int a, int b) => a - b;
  public int Multiply(int a, int b) => a * b;

  /// <summary>
  /// This calculator mitigates the risk of division by zero by checking the divisor.
  /// Thus it can guarantee the postcondition the method will not throw <see cref="DivideByZeroException"/>
  /// </summary>
  [ImplementsFeature<DivideFeature>]
  [ImplementsFeature<CheckForZeroDivisionFeature>]
  [MitigatesRisk<DivideByZeroRisk>]
  [Postcondition<NoDivByZeroPostcondition>]
  public int Divide(int a, int b)
  {
    if (b == 0) return 0;
    return a / b; // Beware: Integer division
  }
}
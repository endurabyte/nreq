using Calculators.Design.Features;
using Calculators.Logic;
using NReq.Design;
using NReq.Logic;

namespace Calculators;

public class StrictCalculator : ICalculator
{
  public int Add(int a, int b) => a + b;
  public int Subtract(int a, int b) => a - b;
  public int Multiply(int a, int b) => a * b;

  /// <summary>
  /// This calculator does not mitgate the risk of division by zero.
  /// Instead, it explicitly declares with the precondition <see cref="NonzeroDivisorPrecondition"/> 
  /// that it assumes the divisor <paramref name="b"/> is nonzero.
  /// Unlike <see cref="LenientCalculator.Divide(int, int)"/> it does not guarantee the postcondition <see cref="NoDivByZeroPostcondition"/>
  /// </summary>
  [ImplementsFeature<DivideFeature>]
  [Precondition<NonzeroDivisorPrecondition>]
  public int Divide(int a, int b) => a / b; // Beware: Integer division
}
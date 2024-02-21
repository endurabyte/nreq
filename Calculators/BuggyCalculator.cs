using Calculators.Risk;
using NReq.Risk;

namespace Calculators;

public class BuggyCalculator : ICalculator
{
  [HasDebt<DuplicateImplementationDebt>]
  public int Add(int a, int b) => a + b;
  [HasDebt<DuplicateImplementationDebt>]
  public int Subtract(int a, int b) => a - b;
  [HasDebt<DuplicateImplementationDebt>]
  public int Multiply(int a, int b) => a * b;

  /// <summary>
  /// This calculator intentionally makes the mistake of dividing the divisor by the dividend i.e. b / a instead of a / b.
  /// We can mark it as such explicitly with <see cref="FlippedDivisorDividendBug"/>
  /// </summary>
  [HasBug<FlippedDivisorDividendBug>]
  public int Divide(int a, int b) => b / a; // Intentional bug for demonstration: Divisor and dividend are flipped
}

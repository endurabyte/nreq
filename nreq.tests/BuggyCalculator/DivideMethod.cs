using Calculators;
using Calculators.Risk;
using NReq.Risk;

namespace NReq.Tests.BuggyCalculator;

[TestFixture]
public class DivideMethod
{
  [TestCase(16, 2, 8)]
  public void Divide_IsFlipped(int a, int b, int want)
  {
    ICalculator calc = new Calculators.BuggyCalculator();

    int got = calc.Divide(a, b);
    int got2 = calc.Divide(b, a);
    Assert.That(got, Is.Not.EqualTo(want));
    Assert.That(got2, Is.EqualTo(want));
  }
}
  
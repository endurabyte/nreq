using Calculators;
using Calculators.Spec;
using Calculators.Risk;
using NReq.Risk;
using NReq.Spec;

namespace NReq.Tests.LenientCalculator;

[TestFixture]
public class DivideMethod
{
  [TestCase(16, 2, 8)]
  [VerifiesRequirement<DivideRequirement>]
  public void Divide_Divides(int a, int b, int want)
  {
    ICalculator calc = new Calculators.LenientCalculator();
    int got = calc.Divide(a, b);
    Assert.That(got, Is.EqualTo(want));
  }

  [TestCase(16, 0, 0)]
  [TestCase(0, 0, 0)]
  [VerifiesRiskMitigation<DivideByZeroRisk>]
  public void DivideByZero_DoesNotThrow(int a, int b, int want)
  {
    ICalculator calc = new Calculators.LenientCalculator();

    Assert.DoesNotThrow(() =>
    {
      int got = calc.Divide(a, b);
      Assert.That(got, Is.EqualTo(want));
    });
  }
}
  
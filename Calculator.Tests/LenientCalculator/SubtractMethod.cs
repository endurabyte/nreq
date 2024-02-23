using Calculators;
using Calculators.Spec;
using NReq.Spec;

namespace NReq.Tests.LenientCalculator;

[TestFixture]
public class SubtractMethod
{
  [Test]
  [VerifiesRequirement<SubtractRequirement>]
  public void Subtract_Subtracts()
  {
    ICalculator calc = new Calculators.LenientCalculator();
    int got = calc.Subtract(2, 1);
    Assert.That(got, Is.EqualTo(1));
  }
}

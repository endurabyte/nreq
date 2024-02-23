using Calculators;
using Calculators.Spec;
using NReq.Spec;

namespace NReq.Tests.LenientCalculator;

[TestFixture]
public class MultiplyMethod
{
  [Test]
  [VerifiesRequirement<MultiplyRequirement>]
  public void Multiply_Multiplies()
  {
    ICalculator calc = new Calculators.LenientCalculator();
    int got = calc.Multiply(2, 4);
    Assert.That(got, Is.EqualTo(8));
  }
}

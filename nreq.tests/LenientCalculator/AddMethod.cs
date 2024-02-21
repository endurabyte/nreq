using Calculators;
using Calculators.Spec;
using NReq.Spec;

namespace NReq.Tests.LenientCalculator;

[TestFixture]
public class AddMethod
{
  [Test]
  [VerifiesRequirement<AddRequirement>]
  public void Add_Adds()
  {
    ICalculator calc = new Calculators.LenientCalculator();
    int got = calc.Add(1, 2);
    Assert.That(got, Is.EqualTo(3));
  }
}  
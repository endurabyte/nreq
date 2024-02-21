using Calculators;
using Calculators.Risk;
using NReq.Risk;

namespace NReq.Tests.StrictCalculator;

[TestFixture]
public class DivideMethod
{
  [TestCase(16, 0)]
  [TestCase(0, 0)]
  [VerifiesRiskMitigation<DivideByZeroRisk>]
  public void DivideByZero_Throws(int a, int b)
  {
    ICalculator calc = new Calculators.StrictCalculator();
    Assert.Throws<DivideByZeroException>(() => _ = calc.Divide(a, b));
  }
}
  
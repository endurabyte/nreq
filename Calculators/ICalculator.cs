using Calculators.Spec;
using Calculators.Risk;
using NReq.Risk;
using NReq.Spec;

namespace Calculators;

public interface ICalculator
{
  [ImplementsRequirement<AddRequirement>]
  int Add(int a, int b);
  [ImplementsRequirement<SubtractRequirement>]
  int Subtract(int a, int b);
  [ImplementsRequirement<MultiplyRequirement>]
  int Multiply(int a, int b);

  /// <summary>
  /// Using integer division, divide the dividend <paramref name="a"/> by the divisor <paramref name="b"/>
  /// to produce the returned quotient.
  /// 
  /// <para/>
  /// With the implements attribute, we declare that the implementing class should implement the divide requirement.
  /// We could also put the implements attribute on the implementation methods.
  /// 
  /// <para/>
  /// With the risk attribute, we declare knowledge of the risk of throwing a divide by zero exception.
  /// It can be mitigated by the implementation.
  /// </summary> 
  [ImplementsRequirement<DivideRequirement>]
  [HasRisk<DivideByZeroRisk>] 
  int Divide(int a, int b);
}

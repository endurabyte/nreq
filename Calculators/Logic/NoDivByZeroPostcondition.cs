using NReq.Logic;

namespace Calculators.Logic;

class NoDivByZeroPostcondition : Postcondition
{
  public override string Description => $"Does not throw {nameof(DivideByZeroException)}";
}

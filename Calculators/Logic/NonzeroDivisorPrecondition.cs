using NReq.Logic;

namespace Calculators.Logic;

class NonzeroDivisorPrecondition : Precondition
{
    public override string Description => "The divisor shall be nonzero";
}

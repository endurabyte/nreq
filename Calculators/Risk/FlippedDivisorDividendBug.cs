using NReq.Risk;

namespace Calculators.Risk;

public class FlippedDivisorDividendBug : Bug
{
  public override string Description => "The divisor and dividend are flipped in this implementation. " +
    "Needs to be fixed but calling code can simply invert the return value e.g. 1/x. " +
    "Note, as a consequence, the dividend must be nonzero.";
}

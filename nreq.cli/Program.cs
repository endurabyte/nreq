namespace nreq.cli;

public static class Program
{
  public static void Main()
  {
    // Requirements:
    // Reflect over all instances of RequirementAttribute
    // For each field of a req, ensure there is a corresponding SatisfiesAttribute
    // All child reqs of a req must be satisfied, in addition to its own fields
    // Else throw compile time error

    // Risks:
    // Accumulate risks and throw compile time error if the value is too high

    // Notes:
    // Throw compile time error if any note does not have a corresponding description

    // Requirements must be satisfied; risks must be mitigated
  }
}

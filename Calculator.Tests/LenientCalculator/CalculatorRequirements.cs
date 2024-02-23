using Calculators;
using NReq.Analysis;
using NReq.Extensions;
using NReq.Spec;

namespace NReq.Tests.LenientCalculator;

[TestFixture]
public class CalculatorRequirements
{
  /// <summary>
  /// Verify that all requirements have an implementation
  /// </summary>
  [Test]
  public void AllReqsAreImplemented()
  { 
    var finder = new ReqsFinder();

    var type = typeof(ICalculator);
    var reqs = finder.GetReqs(type.Assembly);
    var reqImpls = finder.GetReqImpls(type);

    // Aggregate possible multiple implementations. We only care if there is at least one.
    var implementedReqs = reqImpls.SelectMany(i => i.ImplementedRequirements).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    foreach (var req in reqs)
    {
      Assert.That(implementedReqs.ContainsKey(req), Is.True);
    }
  }

  /// <summary>
  /// Verify that all requirement implementations have at least one test
  /// verifying the requirement is implemented, i.e. marked with <see cref="VerifiesRequirementAttribute{T}"/>.
  /// </summary>
  [Test]
  public void AllReqImplementationsAreVerified()
  {
    var finder = new ReqsFinder();

    var type = typeof(ICalculator);

    // Get all requirement verifications in this namespace (LenientCalculator)

    var testTypes = typeof(CalculatorRequirements).GetAllTypesInNamespace();
    var reqVers = finder.GetReqVerifications(testTypes);
    var reqImpls = finder.GetReqImpls(type);

    // Aggregate possible multiple verifications and implementations. We only care if there is at least one of each.
    var verifiedReqs = reqVers.SelectMany(v => v.ImplementedRequirements).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    var implementedReqs = reqImpls.SelectMany(i => i.ImplementedRequirements).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    // Verify each implementation has a verification
    foreach (var req in implementedReqs)
    {
      Assert.That(verifiedReqs.ContainsKey(req.Key), Is.True,
          $"Could not find any test in {typeof(CalculatorRequirements).Namespace} verifying that the type {type.Name} implements the requirement {req.Key.Name}."
        + $"\nAdd at least one test with [VerifiesRequirement<{req.Key.Name}>]");
    }
  }
}
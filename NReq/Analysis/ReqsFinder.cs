using System.Reflection;
using NReq.Extensions;
using NReq.Spec;

namespace NReq.Analysis;

public class ReqsFinder
{
  public Dictionary<Assembly, IList<Type>> GetReqs(IEnumerable<Assembly> assemblies) =>
    assemblies.ToDictionary(a => a, GetReqs);

  public Dictionary<Assembly, IList<RequirementImplementation>> GetReqImpls(IEnumerable<Assembly> assemblies) =>
    assemblies.ToDictionary(a => a, GetReqImpls);

  public IList<Type> GetReqs(Assembly a) => a.GetTypesDerivedFrom<Requirement>().ToList();

  public IList<RequirementImplementation> GetReqImpls(params Type[] types) => types.FindAttributeUses(typeof(ImplementsRequirementAttribute<>));
  public IList<RequirementImplementation> GetReqImpls(Assembly a) => a.FindAttributeUses(typeof(ImplementsRequirementAttribute<>));

  public IList<RequirementImplementation> GetReqVerifications(params Type[] types) => types.FindAttributeUses(typeof(VerifiesRequirementAttribute<>));
  public IList<RequirementImplementation> GetReqVerifications(Assembly a) => a.FindAttributeUses(typeof(VerifiesRequirementAttribute<>));
}
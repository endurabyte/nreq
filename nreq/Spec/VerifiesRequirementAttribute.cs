namespace NReq.Spec;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class VerifiesRequirementAttribute<T> : Attribute where T : Requirement
{
}

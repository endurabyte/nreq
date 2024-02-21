namespace NReq.Spec;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class ImplementsRequirementAttribute<T> : Attribute where T : Requirement
{
}
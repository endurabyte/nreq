namespace NReq.Risk;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class HasBugAttribute<T> : Attribute where T : Bug
{
}
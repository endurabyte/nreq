namespace NReq.Design;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class ImplementsDecisionAttribute<T> : Attribute where T : Decision
{
}

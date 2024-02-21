namespace NReq.Logic;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class PreconditionAttribute<T> : Attribute where T : Precondition
{
}

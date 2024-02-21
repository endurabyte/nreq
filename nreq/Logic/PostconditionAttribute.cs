namespace NReq.Logic;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class PostconditionAttribute<T> : Attribute where T : Postcondition
{
}

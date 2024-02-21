namespace NReq.Risk;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class HasDebtAttribute<T> : Attribute where T : Debt
{
}
namespace NReq.Risk;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class VerifiesRiskMitigationAttribute<T> : Attribute where T : Risk
{
}

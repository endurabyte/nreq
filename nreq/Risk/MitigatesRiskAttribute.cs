namespace NReq.Risk;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class MitigatesRiskAttribute<T> : Attribute where T : Risk
{
}
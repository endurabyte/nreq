namespace NReq.Risk;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class HasRiskAttribute<T> : Attribute where T : Risk
{
  public HasRiskAttribute() { }
}


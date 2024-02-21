namespace NReq.Design;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class ImplementsFeatureAttribute<T> : Attribute where T : Feature
{
}

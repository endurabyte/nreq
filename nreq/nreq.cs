namespace nreq;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class Satisfies : Attribute
{
  public object Target { get; set; }

  public Satisfies(object target)
  {
    Target = target;
  }
}

[AttributeUsage(AttributeTargets.All)]
public class Req : Attribute
{
  public Type? Parent { get; set; }
  public string Description { get; set; } = string.Empty;

  public Req() { }
  public Req(string description)
  {
    Description = description;
  }
}

[AttributeUsage(AttributeTargets.All)]
public class Note : Attribute
{
  public string Description { get; set; } = string.Empty;

  public Note(string description) 
  { 
    Description = description;
  }

  /// <summary>
  /// Link an annotation e.g. code comment to a call site
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="_"></param>
  public static void Link<T>(T _)
  {
  }
}


[AttributeUsage(AttributeTargets.All)]
public class Risk : Attribute
{
  public Type? Parent { get; set; }
  public string Description { get; set; } = string.Empty;

  public Risk() { }
  public Risk(string description)
  {
    Description = description;
  }
}

[AttributeUsage(AttributeTargets.All)]
public class Mitigates : Attribute
{
  public Type? Parent { get; set; }
  public string Description { get; set; } = string.Empty;

  public Mitigates() { }
  public Mitigates(string description)
  {
    Description = description;
  }
}

public class Precondition
{
  public static void Assert(Func<bool> predicate, params object[] requirements)
  {
    if (!predicate()) { throw new PreconditionFailedException(); }
  }
}

public class Postcondition
{
  public static void Assert(Func<bool> predicate, params object[] requirements)
  {
    if (!predicate()) { throw new PostconditionFailedException(); }
  }
}

[Req("All requirements")]
[Mitigates("All problems")]
public class AllReqs : Req
{
  [Req("Add should be safe on 8-bit machines")] public const int SafeOn8BitMachines = 0;
}

[Req("Child requirements")]
public class ChildReqs : AllReqs
{
  [Req("Left operand must be less than 4")] public const int LeftLessThan4 = 0;
  [Req("Right operand must be less than 4")] public const int RightLessThan4 = 1;
}

public enum Notes
{
  [Note("Just a simple test method to start brainstorming")]
  JustATest,
}

public class Adder
{
  [Risk("Overflows")]
  [Satisfies(AllReqs.SafeOn8BitMachines)]
  [Satisfies(ChildReqs.LeftLessThan4)]
  [Satisfies(ChildReqs.RightLessThan4)]
  public int Add(int a, int b)
  {
    Precondition.Assert(() => a < 4, ChildReqs.LeftLessThan4);
    Precondition.Assert(() => b < 4, ChildReqs.RightLessThan4);
    Note.Link(Notes.JustATest);
    int sum = a + b;
    Postcondition.Assert(() => sum < Math.Pow(2, 8), AllReqs.SafeOn8BitMachines);

    return sum;
  }
}

public class PreconditionFailedException : Exception
{
  public PreconditionFailedException()
  {
  }
}

public class PostconditionFailedException : Exception
{
  public PostconditionFailedException()
  {
  }
}

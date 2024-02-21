# NReq

NReq lets you keep track of software artifacts in your code. 

## Features

- Define natural language descriptions of your software artifacts, right in the code. 
  - Example software artifacts: features, bugs, design decisions, tech debt, requirements, risks, preconditions, postconditions.
- Use code navigation e.g. `Go To Definition/Implementation`, `Find All References` to navigate software artifacts.
- No runtime overhead. 

## Example

In the code, define your software artifacts with natural language:

```csharp

public class DivideRequirement : Requirement
{
  public override string Description { get; } = "Shall divide two integers";
}

public class DivideFeature : Feature
{
  public override string Description => "Divides two integers";
}

public class DivideByZeroRisk : Risk
{
  public override string Description { get; } = "User attempts to divide by zero";
}

public class CheckForZeroDivisionFeature : Feature
{
  public override string Description => "Checks for division by zero";
}

class NonzeroDivisorPrecondition : Precondition
{
    public override string Description => "The divisor shall be nonzero";
}

class NoDivByZeroPostcondition : Postcondition
{
  public override string Description => $"Does not throw {nameof(DivideByZeroException)}";
}

```

Declare a risk:

```csharp

public interface ICalculator
{
  [ImplementsRequirement<DivideRequirement>]
  [HasRisk<DivideByZeroRisk>] 
  int Divide(int a, int b);
}
```

Mitigate the risk with a feature:

```csharp
public class LenientCalculator : ICalculator
{
  /// <summary>
  /// This calculator mitigates the risk of division by zero by checking the divisor.
  /// Thus it can guarantee the postcondition the method will not throw <see cref="DivideByZeroException"/>
  /// </summary>
  [ImplementsFeature<DivideFeature>]
  [ImplementsFeature<CheckForZeroDivisionFeature>]
  [MitigatesRisk<DivideByZeroRisk>]
  [Postcondition<NoDivByZeroPostcondition>]
  public int Divide(int a, int b)
  {
    if (b == 0) return 0;
    return a / b; // Beware: Integer division
  }
}

[TestCase(16, 0, 0)]
[TestCase(0, 0, 0)]
[VerifiesRiskMitigation<DivideByZeroRisk>]
public void DivideByZero_DoesNotThrow(int a, int b, int want)
{
  ICalculator calc = new Calculators.LenientCalculator();

  Assert.DoesNotThrow(() =>
  {
    int got = calc.Divide(a, b);
    Assert.That(got, Is.EqualTo(want));
  });
}
```

Mitigate the risk with a precondition:

```sharp
public class StrictCalculator : ICalculator
{
  /// <summary>
  /// This calculator does not mitgate the risk of division by zero.
  /// Instead, it explicitly declares with the precondition <see cref="NonzeroDivisorPrecondition"/> 
  /// that it assumes the divisor <paramref name="b"/> is nonzero.
  /// Unlike <see cref="LenientCalculator.Divide(int, int)"/> it does not guarantee the postcondition <see cref="NoDivByZeroPostcondition"/>
  /// </summary>
  [ImplementsFeature<DivideFeature>]
  [Precondition<NonzeroDivisorPrecondition>]
  public int Divide(int a, int b) => a / b;
}

[TestCase(16, 0)]
[TestCase(0, 0)]
[VerifiesRiskMitigation<DivideByZeroRisk>]
public void DivideByZero_Throws(int a, int b)
{
  ICalculator calc = new Calculators.StrictCalculator();
  Assert.Throws<DivideByZeroException>(() => _ = calc.Divide(a, b));
}

```

## Example: Annotate buggy code

```charp

public class FlippedDivisorDividendBug : Bug
{
  public override string Description => "The divisor and dividend are flipped in this implementation. " +
    "Needs to be fixed but calling code can simply invert the return value e.g. 1/x. " +
    "Note, as a consequence, the dividend must be nonzero.";
}

public class BuggyCalculator : ICalculator
{
  [HasBug<FlippedDivisorDividendBug>]
  public int Divide(int a, int b) => b / a; // Intentional bug for demonstration: Divisor and dividend are flipped
}

```

## Software Artifacts

NReq recognizes four kinds of software artifacts. Artifacts can be positive or negative, and they can be visible or invisible:

|      | Visible | Invisible |
|      |---------|-----------|
| Good | Feature | Design    |
| Bad  | Bug     | Tech debt |

With NReq you can...

- Name and track positive artifacts: features and design decisions.
- Name and track negative artifacts: bugs and tech debt.
- Name and track requirements and risks.
- Name and track design decisions, much like an [ADR](https://adr.github.io/), but they are right next to your code.
- Describe program logic in the form of preconditions and postconditions

## Philosophy

Whatever isn't in the code gets lost. There are plenty of project management tools for stories, tasks, bugs, risks, requirements, etc. NReq lets you do all of that in the code.

People talk about implicit or tacit knowledge that gets lost when developers leave. When the code is all that is left, it's hard to change because you don't know the string of decisions.

By embedding the ([design in the code](https://www.pathsensitive.com/2018/01/the-design-of-software-is-thing-apart.html)), you can be confident the design will remain available.

I once worked at a company that got attacked by ransomware. A decade of completed stories, bug trackers, builds, and test runs were lost. All that remained were the `.git` folders on the developers' workstations. The company is still in business thanks to those folders.

I worked at another company that annotated unit tests with requirement attributes, e.g. `[Requirement(12345)]`. The number linked the unit test to a formal software requirement in Azure DevOps. Then, the CI scanned the test results declared whether all requirements were satisfied. I want that capability, but without an external tool or klugey CI scripts. 

## Future Work

- Calculate requirements coverage
- Generate reports, e.g. Markdown.
- Strip annotations from release
- Add analyzers. For example, warn of unimplemented requirements or unmitigated risks.
- When tests fail, make note of associated requirements or risks.
- Requirement tree
 
## Non goals

- This is not a test framework, e.g. not a replacement for NUnit

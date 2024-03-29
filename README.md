<img src="icon.png" style="width:400px"/>
Logo: A wrecked N. Get it?

[![Nuget Badge](https://buildstats.info/nuget/nreq)](https://www.nuget.org/packages/nreq)

# NReq

`NReq` is software library for QA (quality assurance).

Using attributes, `NReq` lets you define and manage software artifacts such as requirements in your code. This stands in contrast to using an external tracking tool like Azure DevOps or Jira. 

Then, in code you can map the requirements to the code that satifies them. Using your favorite test framework, you can run unit test that verify that the all requirements are met. Finally, you can export the artifacts as markdown.

## License

`NReq` is dual-licensed: either *community* or *commercial*.

### Community License (GPL)

If your project has a copyleft license, for example GPLv3, or your company grosses less than $1 million USD annually, you may use `NReq` free of charge under the GPLv3 license. 

### Commercial License (Paid)

If either the project does not have a copyleft license or your company grosses more than $1 million USD annually, you **must purchase a commercial license**.

We expect `NReq` to bring sigificant, quantifiable value to your enterprise projects. The largest projects benefit the most from the rigor of treating requirements, risks, design, and more as formal artifacts. Purchasing a license also enables us to offer you dedicated professional support.

Pricing table is below. Please [contact us](mailto:sales@endurabyte.com) to request a quote.

| Your License | Company Revenue | Our License | Support     | Price
|--------------|-----------------|-------------|-------------|--------------
| Copyleft     | < $1M/year      |  GPLv3      | Best Effort | $0
| Copyleft     | >= $1M/year     |  Commercial | Dedicated   | $200/year/seat
| Other        | Any             |  Commercial | Dedicated   | $200/year/seat


## Features

- Define natural language descriptions of your software artifacts, right in the code. 
  - Example software artifacts: features, bugs, design decisions, tech debt, requirements, risks, preconditions, postconditions.
- Use code navigation e.g. `Go To Definition/Implementation`, `Find All References` to navigate artifacts.
- Verify with unit tests that requirements are met and risks are mitigated
- Export artifacts to markdown
- No runtime overhead

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

```csharp
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

```csharp
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

## Export Design Decisions to Markdown

`nreq reqs --outDir .\DocGen\Specs\ --assemblies .\Calculators\bin\Debug\net8.0\Calculators.dll`

```powershell
> ls .\DocGen\Decisions\

    Directory: F:\repos\nreq\DocGen\Decisions

Mode                 LastWriteTime         Length Name
----                 -------------         ------ ----
-a---           2/23/2024    09:13            386 DD1_CheckForZeroDivisionDecision.md
-a---           2/23/2024    09:13            430 DD2_ThreeExampleCalculatorsDecision.md
```

```powershell
> cat .\DocGen\Decisions\DD1_CheckForZeroDivisionDecision.md
# DD1_CheckForZeroDivisionDecision

## Status

Accepted

## Context

It's possible to divide by zero. Without mitigation, a DivideByZeroException will be thrown.

## Decision

The implementation shall check for zero division. If so, it will return 0.

## Consequences

- The user will have to handle the possibility of 0 being returned
- The user not have to catch DivideByZeroException
```

## Export Requirements to Markdown

`nreq decisions --outDir .\DocGen\Decisions\ --assemblies .\Calculators\bin\Debug\net8.0\Calculators.dll`

```powershell
> ls .\DocGen\Specs\Reqs

    Directory: F:\repos\nreq\DocGen\Specs\Reqs

Mode                 LastWriteTime         Length Name
----                 -------------         ------ ----
-a---           2/23/2024    09:21             58 AddRequirement.md
-a---           2/23/2024    09:21             64 DivideRequirement.md
-a---           2/23/2024    09:21             68 MultiplyRequirement.md
-a---           2/23/2024    09:21             68 SubtractRequirement.md
```

```powershell 
> cat .\DocGen\Specs\Implementations\ICalculator.md
# ICalculator

## Implements

- Calculators.ICalculator.Add
- Calculators.ICalculator.Subtract
- Calculators.ICalculator.Multiply
- Calculators.ICalculator.Divide
```

## Software Artifacts

NReq recognizes four kinds of software artifacts. Artifacts can be positive or negative, and they can be visible or invisible:

|      | Visible | Invisible |
|:----:|:-------:|:---------:|
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

By embedding the [design in the code](https://www.pathsensitive.com/2018/01/the-design-of-software-is-thing-apart.html), you can be confident the design will remain available.

I once worked at a company that got attacked by ransomware. A decade of completed stories, bug trackers, builds, and test runs were lost. All that remained were the `.git` folders on the developers' workstations. The company is still in business thanks to those folders.

I worked at another company that annotated unit tests with requirement attributes, e.g. `[Requirement(12345)]`. The number linked the unit test to a formal software requirement in Azure DevOps. Then, the CI scanned the test results declared whether all requirements were satisfied. I want that capability, but without an external tool or klugey CI scripts. 

## Future Work

- Calculate requirements coverage
- Strip annotations from release
- Add analyzers. For example, warn of unimplemented requirements or unmitigated risks.
- When tests fail, make note of associated requirements or risks.
- Requirement tree
 
## Non goals

- This is not a test framework, e.g. not a replacement for NUnit

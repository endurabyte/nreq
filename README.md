# nreq

This repo is a placeholder for a code-quality tool I am brainstorming.

## Philosophy

The world needs more [code quality tools](https://www.pathsensitive.com/2021/03/developer-tools-can-be-magic-instead.html). The guiding principle is to "make the design apparent in the code" ([source](https://www.pathsensitive.com/2018/01/the-design-of-software-is-thing-apart.html)). Instead of using external tools  originate software specifications, risks, and requirements, the idea is to use in-code mechanisms perform compile-time or runtime verification of software requirements.

## Feature Ideas

- Use C# attributes to decorate methods and classes.
- The attributes construct a risk or requirement tree that is checked at compile time.
- Use Hoare triples from formal methods. 
- At runtime, precondition and postcondition methods can run which either assert/throw or return certain values. These can be linked to requirements.
- Annotations can also provide links from line(s) of code to detailed comments, keeping the call site clean. 

## Goals

- It should work alongside existing test frameworks like NUnit. This is not indended to be another test framework. 
- To the extent possible, this project should support all dotnet languages or even all programming languages. The tool should not depend on a specific IDE like Visual Studio, Visual Studio Code, or Rider, but it would be helpful

## Name ideas

These could become the project name or names used in the design.

- NReq - "requirement"
- NQuire - "enquire"
- NDesign - Adobe might not like it
- NTerpret ([Ladin](https://en.wiktionary.org/wiki/nterpret) word for interpreter)
- NFlect - "reflection"
- NOtate - "annotate"
- NState (taken, open source project)
- NRisk (taken, enterprise product)
- NSpec (taken, open source project)


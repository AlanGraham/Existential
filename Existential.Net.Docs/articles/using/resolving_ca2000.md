---
uid: resolving_ca2000.md
title: Resolving CA2000
---
# Resolving CA2000: Dispose objects before losing scope

## What's the problem?
If you're using 
[Code Analysis](https://docs.microsoft.com/en-us/visualstudio/code-quality/code-analysis-for-managed-code-overview)
to ensure that your code satisfies Microsoft's
[Framework Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/), 
sometimes you may encounter
[CA2000: Dispose objects before losing scope](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2000?view=vs-2019#example).
There are a number of ways it can occur, but there's one particular cause of this warning that can be resolved 
by a method in Existential.Net, reducing the amount of code you have to write to do so.

The scenario that Existential.Net can help with is when an object that implements the 
[IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable) interface has to be returned
from the method where it's created. The 
[correct way of doing that](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2000#example-1)
is to construct the object using a temporary variable inside a try/finally block before assigning it to 
the variable that will actually be returned. It's not a very intuitive solution, and takes several lines 
of code - or you can call an Existential.Net method to do it for you.

## How do I resolve it?
The type you want to return must implement 
[IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable),
and have a default constructor. If it meets those conditions, you can call the
* [Disposable.SafelyReturn&lt;T&gt;]()


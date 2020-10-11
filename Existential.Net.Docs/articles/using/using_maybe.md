---
uid: using_maybe.md
title: Using Maybe<T>
---
# Using Maybe&lt;T&gt;

**[Maybe&lt;T&gt;](xref:Existential.Maybe`1)** takes a more sophisticated 
approach than [Validate](xref:Existential.Validate) to dealing with whether a value exists or not.

---

* Enable cross-assembly checking of Code Analysis CA1062
* https://docs.microsoft.com/en-us/dotnet/api/microsoft.validatednotnullattribute
* https://github.com/dotnet/roslyn/issues/35104
* https://github.com/dotnet/roslyn-analyzers/blob/master/docs/Analyzer%20Configuration.md#null-check-validation-methods
* dotnet_code_qualityRe.null_check_validation_methods = ThrowIfNull|ThrowIfNullOrEmpty|ThrowIfNullOrWhiteSpace|ToMaybe|Some
* [CA1062: Validate arguments of public methods](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1062)

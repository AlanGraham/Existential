---
uid: resolving_ca1062.md
title: Resolving CA1062
---
# Resolving CA1062: Validate arguments of public methods

## What's the problem?
If you're using 
[Code Analysis](https://docs.microsoft.com/en-us/visualstudio/code-quality/code-analysis-for-managed-code-overview)
to ensure that your code satisfies Microsoft's
[Framework Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/), 
you've probably encountered warning
[CA1062: Validate arguments of public methods](https://docs.microsoft.com/en-gb/visualstudio/code-quality/ca1062).
You can use the methods in Existential.Net to resolve that warning consistently and
concisely, without having to sprinkle your code with lots of repetitive null-checking. The most
you'll need is a single line for each parameter that has to be checked.

## How do I resolve it?
### 1. Pick an approach
Existential.Net supports two alternate approaches to dealing with nulls, represented by the classes
[Validate](xref:using_validate.md) and [Maybe&lt;T&gt;](xref:using_maybe.md). 
The static [Validate](xref:using_validate.md) class contains methods that will throw an
[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)
if a null is encountered. They're simple to understand, ensure consistent handling of null-detection,
and save a few lines of code each time they're used, but you'll need to decide for yourself how to 
handle any exceptions that are thrown as a result.

[Maybe&lt;T&gt;](xref:using_maybe.md) takes a little more getting used to, but can help to make your 
code more robust, and won't throw exceptions. It's similar in concept to 
[Nullable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) - it may 
or may not contain a value - but takes the approach a little further. Take a little time to read 
"[Using Maybe&lt;T&gt;](xref:using_maybe.md)" and see if it's for you.

### 2. Call the relevant methods
The following methods on the Validate will resolve CA1062:
* Validate.ThrowIfNull&lt;T&gt;(T, String)
* Validate.ThrowIfNullOrEmpty(String, String)
* Validate.ThrowIfNullOrEmpty(String, String, Boolean)
* Validate.ThrowIfNullOrEmpty&lt;T&gt;(T, String)
* Validate.ThrowIfNullOrWhiteSpace(String, String)

Methods on Maybe&lt;T&gt;:
* Maybe.Some&lt;T&gt;(T)
* Maybe&lt;T&gt;.ToMaybe(T)
* Maybe&lt;T&gt;.ToMaybe(Maybe&lt;T&gt;)
### 3. Use .editorconfig to ensure they're recognised
Until cross-assembly checking becomes standard in Roslyn analysers you 
will need to add them to your
[null_check_validation_methods](https://docs.microsoft.com/en-gb/visualstudio/code-quality/ca1062#configurability)
setting in editor.config.


---
* Enable cross-assembly checking of Code Analysis CA1062
* https://docs.microsoft.com/en-us/dotnet/api/microsoft.validatednotnullattribute
* https://github.com/dotnet/roslyn/issues/35104
* https://github.com/dotnet/roslyn-analyzers/blob/master/docs/Analyzer%20Configuration.md#null-check-validation-methods
* dotnet_code_qualityRe.null_check_validation_methods = ThrowIfNull|ThrowIfNullOrEmpty|ThrowIfNullOrWhiteSpace|ToMaybe|Some

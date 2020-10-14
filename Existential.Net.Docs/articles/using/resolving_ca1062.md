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
There are three steps.

### 1. Set up .editorconfig (once per solution)
There are all sorts of other good reasons to set up a .editorconfig file if you don't
have one already, but I'll let you [read about](https://editorconfig.org/) those 
[elsewhere](https://docs.microsoft.com/en-us/visualstudio/ide/create-portable-custom-editor-options).

For our purposes, there's one line you need to add to an .editorconfig. It looks like this:
```
dotnet_code_quality.null_check_validation_methods = ThrowIfNull|ThrowIfNullOrEmpty|ThrowIfNullOrWhiteSpace|ToMaybe|Some|Existential.Maybe``.op_Implicit(``)~Existential.Maybe``
```

It lists methods in Existential.Net that Code Analysis should recognise as being valid
null-check validaton methods. You may have noticed that only one method is fully specified; 
that's to avoid any confusion over its definition, but if for any reason you need to do the
same for the other methods, you can fully specify them (and their overloads) in
[ID string format](https://github.com/dotnet/csharplang/blob/master/spec/documentation-comments.md#id-string-format).

You can read more about this in the documentation for the
[null_check_validation_methods setting](https://docs.microsoft.com/en-gb/visualstudio/code-quality/ca1062#configurability).
### 2. Pick the approach you prefer
Existential.Net supports two alternate approaches to dealing with nulls, represented by the classes
[Validate](xref:using_validate.md) and [Maybe&lt;T&gt;](xref:using_maybe.md). 
The static [Validate](xref:using_validate.md) class contains methods that will throw an
[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)
if a null is encountered. They're simple to understand, ensure consistent handling of null-detection,
and save a few lines of code each time they're used, but you'll need to decide for yourself how to 
handle any exceptions that are thrown as a result.

[Maybe&lt;T&gt;](xref:using_maybe.md) takes a little more getting used to, but doesn't throw exceptions,
so if used carefully can help to make your code more robust. It's similar in concept to 
[Nullable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) - it may 
or may not contain a value - but takes the approach a little further. Take a little time to read 
<em>[Using Maybe&lt;T&gt;](xref:using_maybe.md)</em> and see if it's for you.

### 3. Call the relevant methods
Using one of the following null-checking methods on the Validate class will resolve CA1062:
* [Validate.ThrowIfNull&lt;T&gt;(T, String)](xref:Existential.Validate#Existential_Validate_ThrowIfNull__1___0_System_String_)
* [Validate.ThrowIfNullOrEmpty(String, String)](xref:Existential.Validate#Existential_Validate_ThrowIfNullOrEmpty_System_String_System_String_)
* [Validate.ThrowIfNullOrEmpty(String, String, Boolean)](xref:Existential.Validate#Existential_Validate_ThrowIfNullOrEmpty_System_String_System_String_System_Boolean_)
* [Validate.ThrowIfNullOrEmpty&lt;T&gt;(T, String)](xref:Existential.Validate#Existential_Validate_ThrowIfNullOrEmpty__1___0_System_String_)
* [Validate.ThrowIfNullOrWhiteSpace(String, String)](xref:Existential.Validate#Existential_Validate_ThrowIfNullOrWhiteSpace_System_String_System_String_)

...but of course they'll throw an exception if a null is found, and you'll need to decide how to handle 
that. In each case, the first parameter is the value to be checked, and the second is a string containing
its name. In C#, you can get the name using the 
[nameof expression](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof).

Using one of the following methods to create a Maybe&lt;T&gt; will resolve CA1062:
* [The implicit operator](xref:Existential.Maybe`1#Existential_Maybe_1_op_Implicit__0__Existential_Maybe__0_) (can be used by assignment, e.g.: <code>Maybe&lt;T&gt; x = ...</code>)
* [Maybe&lt;T&gt;.ToMaybe(T)](xref:Existential.Maybe`1#Existential_Maybe_1_ToMaybe__0_)
* [Maybe.Some&lt;T&gt;(T)](xref:Existential.Maybe#Existential_Maybe_Some__1___0_)

Usually the most straightforward to use is the implicit operator; explicitly declare a 
variable of type Maybe&lt;T&gt;, where T is the type of the value that may be null, and 
assign the value to it:



---
* Enable cross-assembly checking of Code Analysis CA1062
* https://docs.microsoft.com/en-us/dotnet/api/microsoft.validatednotnullattribute
* https://github.com/dotnet/roslyn/issues/35104
* https://github.com/dotnet/roslyn-analyzers/blob/master/docs/Analyzer%20Configuration.md#null-check-validation-methods
* dotnet_code_qualityRe.null_check_validation_methods = ThrowIfNull|ThrowIfNullOrEmpty|ThrowIfNullOrWhiteSpace|ToMaybe|Some

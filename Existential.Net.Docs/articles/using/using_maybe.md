---
uid: using_maybe.md
title: Using Maybe<T>
---
# How to use Maybe&lt;T&gt;
Before deciding to use Maybe&lt;T&gt;, have a think about whether it's for you while reading
**[Should I use Maybe&lt;T&gt;?](xref:should_i_use_maybe.md)**

Once you've decided, here's how to get started:
## Creating a Maybe&lt;T&gt;
So you've decided to use Maybe&lt;T&gt;!

You can use one of the following methods to create a Maybe&lt;T&gt;:
* [The implicit operator](xref:Existential.Maybe`1#Existential_Maybe_1_op_Implicit__0__Existential_Maybe__0_)
* [Maybe&lt;T&gt;.ToMaybe(T)](xref:Existential.Maybe`1#Existential_Maybe_1_ToMaybe__0_)
* [Maybe.Some&lt;T&gt;(T)](xref:Existential.Maybe#Existential_Maybe_Some__1___0_)

Usually the most straightforward to use is the implicit operator; explicitly declare a 
variable of type Maybe&lt;T&gt;, where T is the type of the value that may be null, and 
assign the value to it:

```cs
public void Method(string inCouldBeNull)
{
    Maybe<string> theString = inCouldBeNull;

    // Other code goes here. There are safe methods 
    // to get a value or a default from theString 
    // if needed.
}
```
The value, whether it's null or not, will be silently converted to a Maybe of the same 
underlying type. Of course, Maybe&lt;T&gt; cannot be used for passthrough null-checking 
in the same was as the Validate methods, because it changes the type of the parameter;
but using Maybe&lt;T&gt; more widely through a codebase can have its benefits too, 
contributing to the overall robustness of the code.

## Getting a value from Maybe&lt;T&gt;

There are a number of methods on Maybe&ltT&gt; for retrieving a value from it safely:
* **[GetValueOr(T)](xref:Existential.Maybe`1#Existential_Maybe_1_GetValueOr__0_)** - 
you specify a default value to return if none exists.
* **[GetValueOr(Func&lt;T&gt;)](xref:Existential.Maybe`1#Existential_Maybe_1_GetValueOr_System_Func__0__)** - 
you specify a factory method that will return a default value if none exists.
* **[GetValueOrMaybe(Maybe&lt;T&gt;)](xref:Existential.Maybe`1#Existential_Maybe_1_GetValueOrMaybe_Existential_Maybe__0__)** - 
you specify an alternative Maybe&lt;T&gt; that will be returned if no value exists.
* **[GetValueOrMaybe(Func&lt;Maybe&lt;T&gt;&gt;)](xref:Existential.Maybe`1#Existential_Maybe_1_GetValueOrMaybe_System_Func_Existential_Maybe__0___)** - 
you specify a factory method that will return an alternative Maybe&lt;T&gt; if no value exists.
* **[GetValueOrThrow()](xref:Existential.Maybe`1#Existential_Maybe_1_GetValueOrThrow_System_String_)** - 
will throw an 
[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)
if no value exists.
* **[GetValueOrEmpty()](xref:Existential.MaybeExtensions#Existential_MaybeExtensions_GetValueOrEmpty_Existential_Maybe_System_Guid__)** - 
available when the underlying type has forms that can be considered "empty". There are 
overloads of this method for GUIDs, strings and collections.
* **[ToString()](xref:Existential.Maybe`1#Existential_Maybe_1_ToString)** - 
returns the result of calling ToString on the value or default value (if either exists),
or the empty string (if neither a value nor default exists).
* **[TryGetValue(out T)](xref:Existential.Maybe`1#Existential_Maybe_1_TryGetValue__0__)** - 
returns a Boolean indicating whether a value exists or not. If it does, the value of it will 
be assigned to the out parameter. If it doesn't, the out parameter will have the default value
for T (which may be null).

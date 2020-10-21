---
uid: should_i_use_maybe.md
title: Should I use Maybe<T>?
---
# Should I use Maybe&lt;T&gt;?
If you've already decided 
[Maybe&lt;T&gt;](xref:Existential.Maybe`1)
 is for you, have a look at the
**[Using Maybe&lt;T&gt;](xref:using_maybe.md)** article.

If not, here are some things to think about before making a choice:
## Pros:
[Maybe&lt;T&gt;](xref:Existential.Maybe`1) gives you a way to care less when nulls occur, and can help to
[resolve Code Analysis warning CA1062](xref:resolving_ca1062.md). Like 
[Nullable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1),
it can contain a value or a null and gives you safe ways of determining which it is. However, *unlike* Nullable&lt;T&gt;,
you don't necessarily need to unpack the contents of a Maybe&lt;T&gt; in order to work with it. You can act on it without
evaluating it first.

Methods can return a Maybe&lt;T&gt; rather than failing, and have other methods can be written to operate on that 
result without needing to know whether a null is actually there hidden away inside the Maybe&lt;T&gt;. You can chain
a series of such methods together safely without worrying about a failure occurring somewhere at the start or middle 
of the chain. Methods that would previously have converted a T to a U can instead convert a Maybe&lt;T&gt; to a 
Maybe&lt;U&gt; - so the underlying type changes - all without having to check whether a result actually exists before 
it's needed somewhere at the end of the process. 

## Cons:
Sounds amazing, doesn't it? Of course it doesn't come for free - it requires a change to your programming
style, so that methods that could fail to produce a result, possibly even throwing an exception, return a Maybe value
instead. And it's not a common idiom in C#, so the libraries you're using won't recognise it. It comes from the world
of functional languages, so it may not feel like a natural approach in C#, which isn't a functional language. Fellow
programmers who haven't come across Maybe may not understand what your code is doing. Adding Maybe
wrappers around a T - or removing them - is a breaking change when going in either direction, that may have
wide-ranging effects on your code.

## Comparison with Nullable&lt;T&gt;
As mentioned above, Maybe&lt;T&gt; gives you something that Nullable&lt;T&gt; doesn't - the ability to not only
pass a result around without explicitly evaluating it, but to *act* on it and perform transformations on it without 
explicitly evaluating it. It can make it safer to work with nulls, because explicit checks for null are far fewer
and may not be needed at all.

However, Nullable&lt;T&gt; has some significant advantages. It's built into the language, and it has support in 
analyzers that will help to ensure that you use it correctly and well; particularly if you enable the 
[nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references)
introduced in C# 8.0. Microsoft have provided some advice on 
[strategies for enabling nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-migration-strategies).

Nullable&lt;T&gt;, with nullable reference types, can give you a well-checked strategy for dealing with nulls.
Maybe&lt;T&gt; has the potential to go further, but requires a significant commitment to really get the best out of it.

If you're ready to jump into 
[Maybe&lt;T&gt;](xref:Existential.Maybe`1), then
**[Using Maybe&lt;T&gt;](xref:using_maybe.md)** is the best place to start.
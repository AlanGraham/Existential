---
uid: intro.md
title: Introduction to Existential.Net
---
# What can I do with Existential.Net?

## Resolve Code Analysis issues
Existential.Net provides functionality that helps to resolve a couple of common Code
Analysis issues.
* [Resolving CA1062: Validate arguments of public methods](xref:resolving_ca1062.md)
* [Resolving CA2000: Dispose objects before losing scope](xref:resolving_ca2000.md)

## Work with nulls
Existential.Net provides a couple of ways of dealing with the absence of data.
 
The static Validate class provides methods that'll throw an exception if data doesn't 
meet their requirements - most commonly if a null is provided. They're very simple; but 
fail fast in a controlled manner, and reduce the amount of boiler-plate code required 
to do so.

* **Validate**: [How To](xref:using_validate.md), [Documentation](xref:Existential.Validate)

Maybe&lt;T&gt; provides an alternate approach. It's similar to Nullable&lt;T&gt;, in that it
may or may not contain a value; but functions can be applied to it in a way that returns a 
Maybe of an independent type. That allows code to be written that postpones or eradicates 
the need to know whether there's a value.

* **Maybe&lt;T&gt;**: 
    * [Should I Use Maybe&lt;T&gt;?](xref:should_i_use_maybe.md)
    * [How To Use Maybe&lt;T&gt;](xref:using_maybe.md)
    * [Documentation](xref:Existential.Maybe`1)

## Convert to IEnumerable&lt;T&gt;
Occasionally it's useful to be able to treat a single value as an IEnumerable&lt;T&gt;, or 
it would have been useful to have an IEnumerable actually be an IEnumerable&lt;T&gt;.
Existential.Net provides conversions to enable that.
* GetGenericEnumerable
## Work with types
Existential provides a number of other small utility classes; to safely return an instance
of IDisposable from a method, to calculate hash codes, to provide the name of the current
method (without the need for reflection), and (using reflection) to report the name of a
generic type in the form it's usually written.
* **Disposable**: [How To](xref:resolving_ca2000.md), [Documentation](xref:Existential.Disposable)
* HashCodeHelper
* ThisMethod
* Type Extension Methods
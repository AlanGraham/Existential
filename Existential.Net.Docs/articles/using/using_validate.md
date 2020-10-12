---
uid: using_validate.md
title: Using Validate
---
# Using Validate
The static methods in **[Validate](xref:Existential.Validate)** throw an exception 
if the value doesn't exist, or sometimes if it doesn't exist in the expected form. 
They can be used to null-check a parameter in a single line of code without repeating 
the same few lines throughout your codebase.

If the value <em>does</em> exist, Validate's methods return it untouched - so they can be used
for pass-through validation where you wouldn't want to write lines of code at all, such as when 
passing arguments to a base or chained constructor.

Validate's null-checking methods can be used to 
[resolve Code Analysis warning CA1062: Validate arguments of public methods](xref:resolving_ca1062.md).

---


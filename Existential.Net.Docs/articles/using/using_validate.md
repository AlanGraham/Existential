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

* **[Validate.ThrowIfNull&lt;T&gt;(T, string)](xref:Existential.Validate#Existential_Validate_ThrowIfNull__1___0_System_String_)**
* **[Validate.ThrowIfNullOrEmpty&lt;T&gt;(T, string)](xref:Existential.Validate#Existential_Validate_ThrowIfNullOrEmpty__1___0_System_String_)**
* **[Validate.ThrowIfNullOrEmpty(string, string)](xref:Existential.Validate#Existential_Validate_ThrowIfNullOrEmpty_System_String_System_String_)**
* **[Validate.ThrowIfNullOrEmpty(string, string, bool)](xref:Existential.Validate#Existential_Validate_ThrowIfNullOrEmpty_System_String_System_String_System_Boolean_)**
* **[Validate.ThrowIfNullOrWhiteSpace(string, string)](xref:Existential.Validate#Existential_Validate_ThrowIfNullOrWhiteSpace_System_String_System_String_)**

The following methods won't resolve CA1062, but may also be useful:

* **[Validate.ThrowIfEmptyGuid(Guid, string)](xref:Existential.Validate#Existential_Validate_ThrowIfEmptyGuid_System_Guid_System_String_)**
* **[Validate.ThrowIfNotOfType&lt;T&gt;(object, string)](xref:Existential.Validate#Existential_Validate_ThrowIfNotOfType__1_System_Object_System_String_)**
* **[Validate.ThrowIfNotOfType(type, object, string)](xref:Existential.Validate#Existential_Validate_ThrowIfNotOfType_System_Type_System_Object_System_String_)**


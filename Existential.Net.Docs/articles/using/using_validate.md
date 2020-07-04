---
uid: using_validate.md
title: Using Validate
---
# Using Validate
The static methods in **[Validate](xref:Existential.Validate)** take a 
simplistic approach to validating whether a value exists or not and will 
throw an exception if the value doesn't exist at all, or doesn't exist in 
the expected form. They can be used to perform null-checking in a single line
of code without repeating the same few lines throughout the codebase.

If a value does exist, it's passed through - so these methods can be 
used in situations where you wouldn't want to have to write a few lines
of validation code at all, such as when passing parameters to a base 
constructor.

If you're using Code Analysis to ensure that your code satisfies Microsoft's
Framework Design Guidelines, these methods can be used to satisfy
[CA1062: Validate arguments of public methods](https://docs.microsoft.com/en-gb/visualstudio/code-quality/ca1062).
Until cross-assembly checking becomes standard in Roslyn analysers you 
will need to add them to your
[null_check_validation_methods](https://docs.microsoft.com/en-gb/visualstudio/code-quality/ca1062)
setting in editor.config.

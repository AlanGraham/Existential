# **Existential.Net**
# The library for things that may or may not exist

Existential contains utilities for dealing with very fundamental issues,
such as whether a value exists or not, or whether it exists
in the desired form.

The two main classes for this are **Validation** and **Maybe&lt;T&gt;**.

---
### Validation
The static methods in **[Validation](xref:Existential.Validation)** take a 
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
may need to add them to your
[null_check_validation_methods](https://docs.microsoft.com/en-gb/visualstudio/code-quality/ca1062)
setting in editor.config.

---
### Maybe&lt;T&gt;
**[Maybe&lt;T&gt;](xref:Existential.Maybe`1)** takes a more sophisticated 
approach to dealing with whether a value exists or not.


## About the icon
The icon for Existential contains the two symbols for 
[existential quantification](https://en.wikipedia.org/wiki/Existential_quantification),
in predicate logic: &#8707; (there exists) and &#8708; (there does not exist).


Refer to [Markdown](http://daringfireball.net/projects/markdown/) for how to write markdown files.
## Quick Start Notes:
1. Add images to the *images* folder if the file is referencing an image.

Doing more with DocFX:
https://skypointcloud.com/blog/docfx-azure-devops-github-integration-1/

https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html
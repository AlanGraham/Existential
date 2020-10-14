<img align="right" width="128" height="128" src="images/Existential128.png">

# **Existential.Net**

***The library for things that may or may not exist***


## Introduction
<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/ggreig/Existential/1/main">
<img alt="SonarCloud quality gate" src="https://sonarcloud.io/api/project_badges/measure?project=ggreig_Existential&metric=alert_status">
<img alt="Azure DevOps releases" src="https://img.shields.io/azure-devops/release/ggreig/9c4fc971-bef3-428a-ab81-cf30a24bea74/1/1">
<a href="https://www.nuget.org/packages/Existential.Net/"><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/Existential.Net"></a>

Existential is a utility library that contains parameter validation methods, a Maybe monad, 
and more. It can be used to work effectively with nulls, to efficiently resolve Code Analysis 
issues [CA1062](xref:resolving_ca1062.md) and [CA2000](xref:resolving_ca2000.md), to help generate 
a hash code, and to convert the names of generic types into a familiar string representation.

It reduces the code you need to write for basic null checks by providing [validation
techniques](xref:using_validate.md) or - even better - using the 
[Maybe&lt;T&gt;](xref:using_maybe.md) monad can just help you not to care!

Other classes in Existential help you to avoid constantly reinventing the wheel for 
common problems such as generating hash codes, safely returning disposable values
from methods, and converting an IEnumerable to an IEnumerable&lt;T&gt;.

These solutions aren't innovative in themselves - plenty code examples exist out
there - but Existential brings them together into one tested library so you don't
have to. 

*To get started:*

**[What can I do with Existential.Net?](xref:intro.md)** || **[API Documentation](xref:index.md)** || **[Access the code](https://dev.azure.com/ggreig/_git/Existential)**

## ![Existential icon](images/Existential32.png) About the icon

Existential's icon is made up of two symbols from 
[predicate logic](https://en.wikipedia.org/wiki/First-order_logic): 
* &#8707; (*there exists*) and 
* &#8708; (*there does not exist*)

## About the author
[Gavin Greig](http://www.ggreig.com/)'s been a professional software developer since 1992, 
and a .NET developer since 2004. Existential.Net's the crystallisation of his experience of
finding the bits he wishes weren't missing from .NET, often unearthed with the help of static
code analysis tools.

These are the two
[existential quantifiers](https://en.wikipedia.org/wiki/Existential_quantification)
, and capture the essence of what the Existential library is about.

## Acknowledgements
The implementation of the [Maybe&lt;T&gt;](xref:using_maybe.md) monad is based on a
couple of articles for 
[DotNetCurry](https://www.dotnetcurry.com/)
 by [Yacoub Massad](https://www.dotnetcurry.com/author/yacoub-massad):
* [The Maybe Monad (C#)](https://www.dotnetcurry.com/patterns-practices/1510/maybe-monad-csharp)
* [The Maybe Monad in C# More Methods](https://www.dotnetcurry.com/patterns-practices/1526/maybe-monad-csharp-examples)

I've been meaning to write a version of the Maybe monad and put it in a library wih some of
the other things here for years, but might not have got it done if it weren't for Yacoub's
previous work and the lockdown for COVID-19 giving me a little extra time!

My friend and former colleague Alan Graham helped with spotting some of the errors I'd made
so now there are fewer of them. Any that are left are my fault.


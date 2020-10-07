<img align="right" width="128" height="128" src="images/Existential128.png">

# **Existential.Net**

***The library for things that may or may not exist***


## Introduction
<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/ggreig/Existential/1/main">
<img alt="Azure DevOps releases" src="https://img.shields.io/azure-devops/release/ggreig/9c4fc971-bef3-428a-ab81-cf30a24bea74/1/1">
<a href="https://www.nuget.org/packages/Existential.Net/"><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/Existential.Net"></a?

Existential is a utility library for dealing with issues
such as whether a value exists or not, or whether it exists in the desired form. It
contains validation methods, a Maybe monad, and more.

It reduces the code you need to write for basic null checks by providing [validation
techniques](xref:using_validate.md) or - even better - using the 
[Maybe&lt;T&gt;](xref:using_maybe.md) monad can just help you not to care!

Other classes in Existential help you to avoid constantly reinventing the wheel for 
common problems such as generating hash codes, safely returning disposable values
from methods, and converting an IEnumerable to an IEnumerable&lt;T&gt;.

These solutions aren't innovative in themselves - plenty code examples exist out
there - but Existential brings them together into one tested library so you don't
have to. 

## Getting started
Install the package from NuGet, and import the Existential namespace where you want to
use the library.

**[Get the NuGet package](https://www.nuget.org/packages/Existential.Net/)** || **["Getting Started" Articles](xref:intro.md)** || **[API Documentation](xref:index.md)**

## About the author
[Gavin Greig](http://www.ggreig.com/)'s been a professional software developer since 1992, 
and a .NET developer since 2004. Existential.Net's the crystallisation of his experience of
finding the bits he wishes weren't missing from .NET, often unearthed with the help of static
code analysis tools.

## ![Existential icon](images/Existential32.png) About the icon

Existential's icon is made up of two symbols from 
[predicate logic](https://en.wikipedia.org/wiki/First-order_logic): 
* &#8707; (*there exists*) and 
* &#8708; (*there does not exist*)

These are the two
[existential quantifiers](https://en.wikipedia.org/wiki/Existential_quantification)
, and capture the essence of what the Existential library is about.


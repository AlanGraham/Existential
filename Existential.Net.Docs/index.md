<img align="right" width="128" height="128" src="images/Existential128.png">

# **Existential.Net**

***The library for things that may or may not exist***


## Introduction
Existential is a utility library for dealing with those very basic issues
such as whether a value exists or not, or whether it 
exists in the desired form.

It can reduce the number of lines you need to write for basic null checks by
detecting whether a value exists or not and acting on it or - even better - it 
can just help you not to care!

The two main classes for this are [Validate](xref:using_validate.md) 
(which will help you to check) and [Maybe&lt;T&gt;](xref:using_maybe.md) 
(which may mean you don't *have* to care).

Other classes in Existential help you to avoid constantly reinventing the wheel for 
common problems such as generating hash codes, returning disposable values
from methods, and converting an IEnumerable to an IEnumerable&lt;T&gt;.

These solutions are not innovative in themselves - plenty code examples exist out
there - but Existential brings them together into one tested library so you don't
have to. 

##  ![Existential icon](images/Existential32.png) About the icon

Existential's icon is made up of two symbols from 
[predicate logic](https://en.wikipedia.org/wiki/First-order_logic): 
* &#8707; (*there exists*) and 
* &#8708; (*there does not exist*)

These are the two
[existential quantifiers](https://en.wikipedia.org/wiki/Existential_quantification)
, and capture the essence of what the Existential library is about.


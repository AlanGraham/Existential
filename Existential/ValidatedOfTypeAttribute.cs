// <copyright file="ValidatedOfTypeAttribute.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    /// <summary>
    /// An attribute that requires that a parameter be validated as being of a particular type.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class ValidatedOfTypeAttribute : System.Attribute
    {
    }
}
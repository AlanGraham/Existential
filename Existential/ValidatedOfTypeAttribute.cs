// <copyright file="ValidatedOfTypeAttribute.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>
// <author>Dr. Gavin T.D. Greig</author>
// <date>2013-08-30</date>
// <summary>
// An attribute that requires that a parameter be validated as being of a particular type.
// </summary>

namespace GavinGreig
{
    /// <summary>
    /// An attribute that requires that a parameter be validated as being of a particular type.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class ValidatedOfTypeAttribute : global::System.Attribute
    {
    }
}
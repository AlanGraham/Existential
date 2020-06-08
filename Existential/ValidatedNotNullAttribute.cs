// <copyright file="ValidatedNotNullAttribute.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>
// <author>Dr. Gavin T.D. Greig</author>
// <date>2012-04-24</date>
// <summary>An attribute that requires that a parameter be validated as not null.</summary>

namespace GavinGreig
{
    /// <summary>
    /// An attribute that indicates to Code Analysis that a parameter is validated as not null, and
    /// does not need to be checked.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class ValidatedNotNullAttribute : global::System.Attribute
    {
    }
}
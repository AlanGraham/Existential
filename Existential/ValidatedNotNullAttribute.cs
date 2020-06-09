// <copyright file="ValidatedNotNullAttribute.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace GavinGreig
{
    /// <summary>
    /// An attribute that indicates to Code Analysis that a parameter is validated as not null, and
    /// does not need to be checked.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class ValidatedNotNullAttribute : System.Attribute
    {
    }
}
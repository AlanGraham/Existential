// <copyright file="ValidatedNotNullAttribute.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System;

    /// <summary>
    ///     <para>
    ///         This attribute indicates to Code Analysis that the value of the parameter it's associated with will be
    ///         validated as
    ///         non-null within the body of the method, and therefore it does not need additional validation outside that
    ///         method.
    ///     </para>
    ///     <para>
    ///         This attribute can only be applied to method parameters.
    ///     </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
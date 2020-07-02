// <copyright file="EnumerableHelper.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System.Collections.Generic;

    /// <summary>A class containing helper methods for working with Enumerables.</summary>
    public static class EnumerableHelper
    {
        /// <summary>Converts one or more items to an IEnumerable of the same type.</summary>
        /// <typeparam name="T">The type of the items to be converted to an IEnumerable.</typeparam>
        /// <param name="inItems">The item or items to be converted to an IEnumerable.</param>
        /// <returns>An IEnumerable containing the items.</returns>
        public static IEnumerable<T> ToEnumerable<T>(params T[] inItems) => inItems;
    }
}
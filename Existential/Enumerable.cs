﻿// <copyright file="Enumerable.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>A class containing helper methods for working with Enumerables.</summary>
    public static class Enumerable
    {
        /// <summary>Converts one or more items to an IEnumerable of the same type.</summary>
        /// <typeparam name="T">The type of the items to be converted to an IEnumerable.</typeparam>
        /// <param name="inItems">The item or items to be converted to an IEnumerable.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the items.</returns>
        public static IEnumerable<T> From<T>(params T[] inItems) => inItems;

        /// <summary>
        /// Converts an <see cref="IEnumerable"/> to an <see cref="IEnumerable{T}"/> (if possible).
        /// </summary>
        /// <typeparam name="T">The expected type of collection members.</typeparam>
        /// <param name="inEnumerable">A non-generic collection.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the items.</returns>
        public static IEnumerable<T> From<T>(IEnumerable inEnumerable) => new EnumerableOf<T>(inEnumerable);
    }
}
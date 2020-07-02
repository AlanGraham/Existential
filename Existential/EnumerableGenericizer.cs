﻿// <copyright file="EnumerableGenericizer.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A class for converting an <see cref="IEnumerable" /> to an <see cref="IEnumerable{T}" />.
    /// </summary>
    /// <typeparam name="T">The underlying type of the collection.</typeparam>
    public class EnumerableGenericizer<T> : IEnumerable<T>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EnumerableGenericizer{T}" /> class.
        /// </summary>
        /// <param name="target">
        /// The <see cref="IEnumerable" /> to be converted to an <see cref="IEnumerable{T}" />.
        /// </param>
        public EnumerableGenericizer(IEnumerable target) => Target = target;

        /// <summary>Gets or sets the <see cref="IEnumerable" /> collection.</summary>
        public IEnumerable Target { get; set; }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in Target)
            {
                yield return item;
            }
        }
    }
}
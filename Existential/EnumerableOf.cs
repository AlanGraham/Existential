// <copyright file="EnumerableOf.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Extensions;

    using Properties;

    /// <summary>
    /// A class for converting an <see cref="IEnumerable" /> to an <see cref="IEnumerable{T}" />.
    /// </summary>
    /// <typeparam name="T">The underlying type of the collection.</typeparam>
    public class EnumerableOf<T> : IEnumerable<T>
    {
        private readonly Lazy<int> myLength;

        /// <summary>
        ///     Initialises a new instance of the <see cref="EnumerableOf{T}" /> class.
        /// </summary>
        /// <param name="inEnumerable">
        ///     The <see cref="IEnumerable" /> to be converted to an <see cref="IEnumerable{T}" />.
        /// </param>
        /// <exception cref="ArgumentTypeException">
        ///     Thrown if the <see cref="IEnumerable" /> collection contains one or member
        ///     that is not of type <typeparamref name="T" />.
        /// </exception>
        public EnumerableOf(IEnumerable inEnumerable)
        {
            // Avoid multiple enumerations by converting to array.
            IEnumerable theObjects = inEnumerable as object[] ?? inEnumerable.Cast<object>().ToArray();

            foreach (object aMember in theObjects)
            {
                if (aMember != null && !(aMember is T))
                {
                    // Call GetType first to ensure we have the most derived type of the object.
                    string theUnexpectedType = aMember.GetType().GetGenericAwareTypeName();
                    string theMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.CollectionContainedAnUnexpectedType,
                        theUnexpectedType);
                    throw new ArgumentTypeException(theMessage, nameof(inEnumerable));
                }
            }

            Collection = theObjects;
            myLength = new Lazy<int>(() => new List<T>(this).Count);
        }

        /// <summary>Gets the number of members of the collection.</summary>
        public int Length => myLength.Value;

        /// <summary>Gets the <see cref="IEnumerable" /> collection.</summary>
        private IEnumerable Collection { get; }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator() => Collection.Cast<T>().GetEnumerator();
    }
}
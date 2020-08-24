// <copyright file="MaybeExtensions.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extensions to the <see cref="Maybe"/> class that enable additional functionality on selected types of Maybe.
    /// </summary>
    public static class MaybeExtensions
    {
        /// <summary>
        /// Returns the value, if it exists, or the empty string.
        /// </summary>
        /// <param name="inMaybe">The <see cref="Maybe{T}"/> (where T is <see cref="string"/>) that may or may not contain a value.</param>
        /// <returns>The value, if it exists, or the empty string.</returns>
        /// <remarks>Implemented as an extension method to keep it type-specific to strings.</remarks>
        public static string ValueOrEmpty(this Maybe<string> inMaybe) => inMaybe.ValueOr(string.Empty);

        /// <summary>
        /// Returns the value, if it exists, or <see cref="Guid.Empty"/>.
        /// </summary>
        /// <param name="inMaybe">The <see cref="Maybe{T}"/> (where T is <see cref="Guid"/>) that may or may not contain a value.</param>
        /// <returns>The value, if it exists, or <see cref="Guid.Empty"/>.</returns>
        /// <remarks>Implemented as an extension method to keep it type-specific to GUIDs.</remarks>
        public static Guid ValueOrEmpty(this Maybe<Guid> inMaybe) => inMaybe.ValueOr(Guid.Empty);

        /// <summary>
        /// Returns the value, if it exists, or an empty array of the same type.
        /// </summary>
        /// <typeparam name="T">The type contained by the array.</typeparam>
        /// <param name="inMaybe">The <see cref="Maybe{T}"/> that may or may not contain an array.</param>
        /// <returns>The value, if it exists, or an empty array of the same type.</returns>
        /// <remarks>Implemented as an extension method to keep it type-specific to arrays of T.</remarks>
        public static T[] ValueOrEmpty<T>(this Maybe<T[]> inMaybe) => inMaybe.ValueOr(Array.Empty<T>());

        /// <summary>
        /// Returns the value, if it exists, or an empty collection of the same type.
        /// </summary>
        /// <typeparam name="T">The type contained by the collection.</typeparam>
        /// <param name="inMaybe">The <see cref="Maybe{T}"/> that may or may not contain a generic collection.</param>
        /// <returns>The value, if it exists, or an empty collection of the same type.</returns>
        /// <remarks>Implemented as an extension method to keep it type-specific to generic collections of T.</remarks>
        /// <remarks>Wanted to make the type of collection generic (where IEnumerable, new()), but type constraints do not form
        /// part of a signature and so type parameters had to be specified explicitly, which removed the usefulness.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1002:Do not expose generic lists",
            Justification = "We're not really exposing a list, we're making sure we can return the same type by creating an empty one.")]
        public static List<T> ValueOrEmpty<T>(this Maybe<List<T>> inMaybe)
            => inMaybe.ValueOr(new List<T>());

        /// <summary>
        /// Gets a <see cref="Maybe{T}"/> containing the first matching member of the collection, if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the collection members.</typeparam>
        /// <param name="inCollection">A collection in which to find the first matching member.</param>
        /// <param name="inPredicate">The condition that a collection member must satisfy.</param>
        /// <returns>A <see cref="Maybe{T}"/> containing the first member if it exists.</returns>
        public static Maybe<T> MaybeFirstWhere<T>(this IEnumerable<T> inCollection, Func<T, bool> inPredicate)
            => inCollection?.ThatExist().Where(inPredicate).MaybeFirst() ?? default(Maybe<T>);

        /// <summary>
        /// Gets a <see cref="Maybe{T}"/> containing the first matching member of the collection, if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the collection members.</typeparam>
        /// <param name="inCollection">A collection in which to find the first matching member.</param>
        /// <returns>A <see cref="Maybe{T}"/> containing the first member if it exists.</returns>
        public static Maybe<T> MaybeFirstThatExists<T>(this IEnumerable<T> inCollection)
            => inCollection?.ThatExist().MaybeFirst() ?? default(Maybe<T>);

        /// <summary>
        /// Gets a <see cref="Maybe{T}"/> containing the first member of the collection if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the collection members.</typeparam>
        /// <param name="inCollection">A collection in which to find the first member.</param>
        /// <returns>A <see cref="Maybe{T}"/> containing the first member if it exists.</returns>
        public static Maybe<T> MaybeFirst<T>(this IEnumerable<T> inCollection)
        {
            if (inCollection != null)
            {
                if (inCollection is IList<T> theList)
                {
                    if (theList.Count > 0)
                    {
                        return theList[0];
                    }
                }
                else
                {
                    using (IEnumerator<T> theEnumerator = inCollection.GetEnumerator())
                    {
                        if (theEnumerator.MoveNext())
                        {
                            return theEnumerator.Current;
                        }
                    }
                }
            }

            return default(Maybe<T>);
        }

        /// <summary>
        /// Returns a collection containing only the members of the input collection that have values.
        /// </summary>
        /// <typeparam name="T">The type of members of the collection.</typeparam>
        /// <param name="inCollection">The collection that may contain values.</param>
        /// <returns>A collection containing only the members of the input collection that have values.</returns>
        /// <remarks>
        /// The implementation of this method looks identical to the more generic version
        /// <see cref="ThatExist{T}(IEnumerable{Maybe{T}})"/> but it is required to transform a collection
        /// of <see cref="Maybe{T}"/> to a collection of <typeparamref name="T"/>.
        /// </remarks>
        public static IEnumerable<T> ThatExist<T>(this IEnumerable<Maybe<T>> inCollection)
        {
            if (inCollection != null)
            {
                foreach (Maybe<T> aMaybe in inCollection)
                {
                    if (aMaybe.TryGetValue(out T aValue))
                    {
                        yield return aValue;
                    }
                }
            }

            // ...don't need an explicit return to mark the end of the collection when we're using "yield return".
        }

        /// <summary>
        /// Returns a collection containing only the members of the input collection that have values.
        /// </summary>
        /// <typeparam name="T">The type of members of the collection.</typeparam>
        /// <param name="inCollection">The collection that may contain values.</param>
        /// <returns>A collection containing only the members of the input collection that have values.</returns>
        public static IEnumerable<T> ThatExist<T>(this IEnumerable<T> inCollection)
        {
            if (inCollection != null)
            {
                foreach (Maybe<T> aMaybe in inCollection)
                {
                    if (aMaybe.TryGetValue(out T aValue))
                    {
                        yield return aValue;
                    }
                }
            }

            // ...don't need an explicit return to mark the end of the collection when we're using "yield return".
        }

        /// <summary>
        /// Returns a collection only if all the members of the input collection have values.
        /// </summary>
        /// <typeparam name="T">The type of members of the collection.</typeparam>
        /// <param name="inCollection">The collection that may contain values.</param>
        /// <returns>The input collection if all its members exist; otherwise no collection.</returns>
        public static Maybe<IEnumerable<T>> WhereAllExist<T>(this IEnumerable<Maybe<T>> inCollection)
        {
            if (inCollection != null)
            {
                var theResultBuilder = new List<T>();

                foreach (Maybe<T> aMaybe in inCollection)
                {
                    if (!aMaybe.TryGetValue(out T theValue))
                    {
                        return null;
                    }

                    theResultBuilder.Add(theValue);
                }

                return theResultBuilder;
            }

            return null;
        }
    }
}
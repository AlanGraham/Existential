﻿// <copyright file="Maybe.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Properties;

    /// <summary>
    ///     A container representing a value that may or may not exist. Additional support is provided by the types
    ///     <see cref="Maybe" /> and <see cref="MaybeExtensions" />.
    /// </summary>
    /// <typeparam name="T">The type of the object that may exist.</typeparam>
    /// <remarks>
    ///     Very strongly based on an example by Yacoub Massad:
    ///     https://www.dotnetcurry.com/patterns-practices/1510/maybe-monad-csharp. Defined as a struct
    ///     because structs themselves cannot be null.
    /// </remarks>
    public readonly struct Maybe<T> : IEquatable<Maybe<T>>
    {
        private readonly T myValue;

        private readonly bool myValueExists;

        /// <summary>Initialises a new instance of the <see cref="Maybe{T}" /> struct.</summary>
        /// <param name="inValue">The value for the maybe to contain.</param>
        /// <remarks>
        ///     Private so that it can only be used by methods within the class that check for null
        ///     before construction. If the provided value is null, they will use the default
        ///     constructor instead.
        /// </remarks>
        private Maybe(T inValue)
        {
            myValue = inValue;
            myValueExists = true;
        }

        /// <summary>
        ///     Performs an implicit conversion from a <typeparamref name="T" /> to <see cref="Maybe{T}" />.
        /// </summary>
        /// <param name="inValue">The value.</param>
        /// <returns>The result of the conversion.</returns>
        //// https://github.com/bridgedotnet/Bridge/issues/4046
        public static implicit operator Maybe<T>([ValidatedNotNull] T inValue)
            => inValue == null ? default : new Maybe<T>(inValue);

        /// <summary>Implements the == operator.</summary>
        /// <param name="inLeft">The left.</param>
        /// <param name="inRight">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Maybe<T> inLeft, Maybe<T> inRight)
            => inLeft.Equals(inRight);

        /// <summary>Implements the != operator.</summary>
        /// <param name="inLeft">The left.</param>
        /// <param name="inRight">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Maybe<T> inLeft, Maybe<T> inRight)
            => !inLeft.Equals(inRight);

#pragma warning disable CA1000 // Do not declare static members on generic types - these two methods help with readability.
        /// <summary>
        ///     Creates a <see cref="Maybe{T}" /> with the specified value.
        /// </summary>
        /// <param name="inValue">The value to assign to the <see cref="Maybe{T}" />.</param>
        /// <returns>A <see cref="Maybe{T}" /> with the value <paramref name="inValue" />.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the value, <paramref name="inValue" />, is null.</exception>
        public static Maybe<T> WithGuaranteedValue(T inValue)
        {
            _ = Validate.ThrowIfNull(inValue, nameof(inValue));
            return inValue;
        }

        /// <summary>
        ///     Creates a <see cref="Maybe{T}" /> with the specified value.
        /// </summary>
        /// <param name="inValue">The value to assign to the <see cref="Maybe{T}" />.</param>
        /// <returns>A <see cref="Maybe{T}" /> with the value <paramref name="inValue" />.</returns>
        public static Maybe<T> WithValue(T inValue) => inValue;
#pragma warning restore CA1000 // Do not declare static members on generic types

        /// <summary>
        ///     Performs a conversion from a <typeparamref name="T" /> to a <see cref="Maybe{T}" />.
        /// </summary>
        /// <param name="inValue">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <remarks>Required by Framework Design Guidelines as an alternate for the implicit operator.</remarks>
        public Maybe<T> ToMaybe([ValidatedNotNull] T inValue) => inValue;

        /// <summary>Returns a <see cref="Maybe{T}" /> when given one, to avoid double-wrapping.</summary>
        /// <param name="inValue">The value.</param>
        /// <returns>The original value, passed through.</returns>
        /// <remarks>Complements the method <see cref="ToMaybe(T)" />.</remarks>
        public Maybe<T> ToMaybe([ValidatedNotNull] Maybe<T> inValue) => inValue;

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        ///     true if <paramref name="obj" /> and this instance are the same type and represent the
        ///     same value; otherwise, false.
        /// </returns>
        [SuppressMessage(
            "Naming",
            "CA1725:Parameter names should match base declaration",
            Justification = "Base declaration is inconsistent between .NET and Bridge.")]
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Name matches Microsoft definition.")]
        public override bool Equals(object obj) => obj is Maybe<T> theOther && Equals(theOther);

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other">other</paramref>
        ///     parameter; otherwise, false.
        /// </returns>
        // ReSharper disable once InconsistentNaming
        public bool Equals(Maybe<T> other)
        {
            if (myValueExists)
            {
                if (other.myValueExists && myValue.Equals(other.myValue))
                {
                    return true;
                }
            }
            else
            {
                if (!other.myValueExists)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data
        ///     structures like a hash table.
        /// </returns>
        public override int GetHashCode() => myValueExists ? myValue.GetHashCode() : 0;

        /// <summary>
        ///     Returns the underlying value, or the default for its type, converted to a string.
        /// </summary>
        /// <returns>The string representation of the value, or a suitable fall-back value.</returns>
        /// <remarks>
        ///     Fall-backs: if no value exists, the string representation of the default value for the
        ///     type will be returned. If the default value for the type is null, the empty string will
        ///     be returned.
        /// </remarks>
        [SuppressMessage(
            "Style",
            "IDE0046:Convert to conditional expression",
            Justification = "Complex method - would reduce readability.")]
        public override string ToString()
        {
            if (myValueExists)
            {
                return myValue.ToString();
            }

            return default(T) != null ? default(T)?.ToString() : string.Empty;
        }

        /// <summary>Applies different functions depending on whether the value exists.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="inSome">The function to apply if a value exists.</param>
        /// <param name="inNone">The function to apply if no value exists.</param>
        /// <returns>The result of the function that was applied.</returns>
        public Maybe<TResult> Match<TResult>(Func<T, TResult> inSome, Func<TResult> inNone) => DoEither(inSome, inNone);

        /// <summary>Applies different actions depending on whether the value exists.</summary>
        /// <param name="inSome">The action to apply if a value exists.</param>
        /// <param name="inNone">The action to apply if no value exists.</param>
        public void Match(Action<T> inSome, Action inNone) => DoEither(inSome, inNone);

        /// <summary>Applies an action if the value exists.</summary>
        /// <param name="inSome">The action to apply if a value exists.</param>
        public void IfExists(Action<T> inSome)
        {
            _ = Validate.ThrowIfNull(inSome, nameof(inSome));

            if (myValueExists)
            {
                inSome(myValue);
            }
        }

        /// <summary>Applies different functions depending on whether the value exists.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="inSome">The function to apply if a value exists.</param>
        /// <param name="inNone">The function to apply if no value exists.</param>
        /// <returns>The result of the function that was applied.</returns>
        public Maybe<TResult> DoEither<TResult>(Func<T, TResult> inSome, Func<TResult> inNone)
        {
            _ = Validate.ThrowIfNull(inSome, nameof(inSome));
            _ = Validate.ThrowIfNull(inNone, nameof(inNone));

            return myValueExists
                ? inSome(myValue)
                : inNone();
        }

        /// <summary>Applies different actions depending on whether the value exists.</summary>
        /// <param name="inSome">The action to apply if a value exists.</param>
        /// <param name="inNone">The action to apply if no value exists.</param>
        public void DoEither(Action<T> inSome, Action inNone)
        {
            _ = Validate.ThrowIfNull(inSome, nameof(inSome));
            _ = Validate.ThrowIfNull(inNone, nameof(inNone));

            if (myValueExists)
            {
                inSome(myValue);
            }
            else
            {
                inNone();
            }
        }

        /// <summary>
        ///     Puts the actual value into <paramref name="inValue" />, if it exists, otherwise the
        ///     default value. Returns a Boolean indicating whether a value was found.
        /// </summary>
        /// <param name="inValue">The value, if it exists, or the default for that type.</param>
        /// <returns>A Boolean indicating whether a value was found.</returns>
        public bool TryGetValue(out T inValue)
        {
            if (myValueExists)
            {
                inValue = myValue;
                return true;
            }

            inValue = default;
            return false;
        }

        /// <summary>
        ///     Returns the current <see cref="Maybe{T}" /> if a value exists, or the specified alternative
        ///     <see cref="Maybe{T}" />.
        /// </summary>
        /// <param name="inAlternativeValue">An alternative <see cref="Maybe{T}" />.</param>
        /// <returns>
        ///     The current <see cref="Maybe{T}" /> if a value exists, or the specified alternative
        ///     <see cref="Maybe{T}" />.
        /// </returns>
        public Maybe<T> Or(Maybe<T> inAlternativeValue) => myValueExists ? this : inAlternativeValue;

        /// <summary>
        ///     Returns the current <see cref="Maybe{T}" /> if a value exists, or the specified alternative
        ///     <see cref="Maybe{T}" />.
        /// </summary>
        /// <param name="inAlternativeValueFactory">A method that returns an alternative <see cref="Maybe{T}" />.</param>
        /// <returns>
        ///     The current <see cref="Maybe{T}" /> if a value exists, or an alternative <see cref="Maybe{T}" />
        ///     provided by the specified method.
        /// </returns>
        public Maybe<T> Or(Func<Maybe<T>> inAlternativeValueFactory)
            => myValueExists
                ? this
                : Validate.ThrowIfNull(inAlternativeValueFactory, nameof(inAlternativeValueFactory)).Invoke();

        /// <summary>
        ///     Returns the result of applying the function to the value, or the default if no value exists.
        /// </summary>
        /// <typeparam name="TResult">The underlying type of the result.</typeparam>
        /// <param name="inConvert">The function to apply to the value.</param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        /// <remarks>Equivalent to Apply/Select; this is effectively an alias for comfort.</remarks>
        public Maybe<TResult> Map<TResult>(Func<T, TResult> inConvert) => Select(inConvert);

        /// <summary>
        ///     Returns the result of applying the function to the value, or the default if no value exists.
        /// </summary>
        /// <typeparam name="TResult">The underlying type of the result.</typeparam>
        /// <param name="inConvert">The function to apply to the value.</param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        /// <remarks>Required Monad method.</remarks>
        public Maybe<TResult> Apply<TResult>(Func<T, Maybe<TResult>> inConvert)
        {
            _ = Validate.ThrowIfNull(inConvert, nameof(inConvert));

            // Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
            return !myValueExists ? default : inConvert(myValue);
        }

        /// <summary>
        ///     Returns the result of applying the function to the value, or the default if no value exists.
        /// </summary>
        /// <typeparam name="TResult">The underlying type of the result.</typeparam>
        /// <param name="inConvert">The function to apply to the value.</param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        /// <remarks>Equivalent to Map/Select; this is effectively an alias for comfort.</remarks>
        public Maybe<TResult> Apply<TResult>(Func<T, TResult> inConvert) => Select(inConvert);

        /// <summary>
        ///     Returns the result of applying the function to the value, or the default if no value exists.
        /// </summary>
        /// <typeparam name="TResult">The underlying type of the result.</typeparam>
        /// <param name="inConvert">The function to apply to the value.</param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        /// <remarks>Equivalent to Apply/Map. Required for Linq compatibility.</remarks>
        public Maybe<TResult> Select<TResult>(Func<T, TResult> inConvert)
        {
            _ = Validate.ThrowIfNull(inConvert, nameof(inConvert));

            return !myValueExists ? default(Maybe<TResult>) : inConvert(myValue);
        }

        /// <summary>
        ///     Returns the result of applying the function to the value, or the default if no value exists.
        /// </summary>
        /// <typeparam name="TResult">The underlying type of the result.</typeparam>
        /// <param name="inConvert">The function to apply to the value.</param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        /// <remarks>
        ///     Required Monad method. Here, an alias of <see cref="Maybe{T}.Apply{TResult}(Func{T,Maybe{TResult}})" />
        ///     because Apply is thought to be more
        ///     readable.
        /// </remarks>
        public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> inConvert) => Apply(inConvert);

        /// <summary>
        ///     Allows Linq queries to be written that reference multiple properties of the type
        ///     <typeparamref name="T" /> underlying <see cref="Maybe{T}" />.
        /// </summary>
        /// <typeparam name="T2">The type of the intermediate result.</typeparam>
        /// <typeparam name="TResult">The type of the final result.</typeparam>
        /// <param name="inConvert">
        ///     A function that converts <typeparamref name="T" /> to an intermediate type.
        /// </param>
        /// <param name="inFinalSelect">
        ///     A function that produces a result using the original value and any value of the
        ///     intermediate type.
        /// </param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        /// <remarks>Unlikely to be used directly, but required to support Linq usage.</remarks>
        public Maybe<TResult> SelectMany<T2, TResult>(Func<T, Maybe<T2>> inConvert, Func<T, T2, TResult> inFinalSelect)
        {
            if (!myValueExists)
            {
                // Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
                return default;
            }

            _ = Validate.ThrowIfNull(inConvert, nameof(inConvert));
            _ = Validate.ThrowIfNull(inFinalSelect, nameof(inFinalSelect));
            Maybe<T2> theConverted = inConvert(myValue);

            // Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
            return !theConverted.myValueExists
                ? default(Maybe<TResult>)
                : inFinalSelect(myValue, theConverted.myValue);
        }

        /// <summary>Returns the value if the predicate applies, otherwise the default.</summary>
        /// <param name="inPredicate">A predicate applying to any value that exists.</param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        public Maybe<T> Where(Func<T, bool> inPredicate)
        {
            if (!myValueExists)
            {
                // Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
                return default;
            }

            _ = Validate.ThrowIfNull(inPredicate, nameof(inPredicate));

            //// Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
            return inPredicate(myValue)
                ? this
                : default;
        }

        /// <summary>Returns the value, if it exists, or the specified default value.</summary>
        /// <param name="inDefaultValue">The default value to return if no value exists.</param>
        /// <returns>The value, if it exists, or the specified default value.</returns>
        public T GetValueOr(T inDefaultValue)
        {
            if (myValueExists)
            {
                return myValue;
            }

            // Providing a null default value invalidates the purpose of Maybe<T>.
            _ = Validate.ThrowIfNull(inDefaultValue, nameof(inDefaultValue));
            return inDefaultValue;
        }

        /// <summary>Returns the value, if it exists, or the value provided by a factory function.</summary>
        /// <param name="inDefaultValueFactory">A factory function that will provide a default value.</param>
        /// <returns>The value, if it exists, or the value provided by a factory function.</returns>
        public T GetValueOr(Func<T> inDefaultValueFactory)
        {
            if (myValueExists)
            {
                return myValue;
            }

            // Providing a null factory invalidates the purpose of Maybe<T>.
            _ = Validate.ThrowIfNull(inDefaultValueFactory, nameof(inDefaultValueFactory));
            return inDefaultValueFactory();
        }

        /// <summary>Returns the value, if it exists, or the specified alternative value.</summary>
        /// <param name="inAlternativeValue">The alternative value.</param>
        /// <returns>
        ///     A container holding the value, if it exists, or the specified alternative value.
        /// </returns>
        public Maybe<T> GetValueOrMaybe(Maybe<T> inAlternativeValue)
        {
            if (myValueExists)
            {
                return this;
            }

            // Providing a null factory invalidates the purpose of Maybe<T>.
            return !inAlternativeValue.myValueExists
                ? throw new ArgumentException(
                    Resources.EmptyMaybeCannotBeDefaultForEmptyMaybe,
                    nameof(inAlternativeValue))
                : inAlternativeValue;
        }

        /// <summary>Returns the value, if it exists, or the value provided by a factory function.</summary>
        /// <param name="inAlternativeValueFactory">
        ///     A factory function that will provide an alternative value.
        /// </param>
        /// <returns>
        ///     A container holding the value, if it exists, or the value provided by a factory function.
        /// </returns>
        public Maybe<T> GetValueOrMaybe(Func<Maybe<T>> inAlternativeValueFactory)
        {
            if (myValueExists)
            {
                return this;
            }

            // Providing a null factory invalidates the purpose of Maybe<T>.
            _ = Validate.ThrowIfNull(inAlternativeValueFactory, nameof(inAlternativeValueFactory));
            return inAlternativeValueFactory();
        }

        /// <summary>Returns the value, if it exists, or throws an exception.</summary>
        /// <param name="inErrorMessage">The error message to include in any exception thrown.</param>
        /// <returns>The value, if it exists.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no value exists.</exception>
        public T GetValueOrThrow(string inErrorMessage) =>
            myValueExists ? myValue : throw new InvalidOperationException(inErrorMessage);
    }

    /// <summary>Class Maybe. Provides additional support for the main class, <see cref="Maybe{T}" />.</summary>
    public static class Maybe
    {
        /// <summary>
        ///     Converts the provided value of type <typeparamref name="T" /> to a <see cref="Maybe{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of the value to be converted.</typeparam>
        /// <param name="inValue">The value to be converted.</param>
        /// <returns>A container holding the value, if it exists.</returns>
        public static Maybe<T> Some<T>([ValidatedNotNull] T inValue) => inValue ?? default(Maybe<T>);

        /// <summary>
        ///     Ensures that a <see cref="Maybe{T}" /> doesn't get double-wrapped to become a Maybe&lt;Maybe&lt;T&gt;&gt;.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="inValue">The <see cref="Maybe{T}" />.</param>
        /// <returns>The value.</returns>
        public static Maybe<T> Some<T>([ValidatedNotNull] Maybe<T> inValue) => inValue;
    }
}
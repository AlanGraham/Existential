// <copyright file="Maybe.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace GavinGreig
{
    using System;

    /// <summary>A container representing a value that may or may not exist.</summary>
    /// <typeparam name="T">The type of the object that may exist.</typeparam>
    /// <remarks>
    /// Very strongly based on an example by Yacoub Massad: https://www.dotnetcurry.com/patterns-practices/1510/maybe-monad-csharp.
    /// TODO: Add support for nullable? values.
    /// </remarks>
    public struct Maybe<T> : IEquatable<Maybe<T>>
    {
        private readonly T myValue;

        private readonly bool myValueExists;

        private Maybe(T value)
        {
            myValue = value;
            myValueExists = true;
        }

        /// <summary>
        /// Performs an implicit conversion from a <typeparamref name="T"/> to <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="inValue">The value.</param>
        /// <returns>The result of the conversion.</returns>
        //// Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
        //// https://github.com/bridgedotnet/Bridge/issues/4046
        public static implicit operator Maybe<T>(T inValue)
            => (inValue == null) ? default(Maybe<T>) : new Maybe<T>(inValue);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Maybe.MaybeNone"/> to <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="_">The value, which will be ignored. Its type alone is used to select this method.</param>
        /// <returns>The result of the conversion.</returns>
        //// Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "StyleCop.CSharp.NamingRules",
            "SA1313:Parameter names should begin with lower-case letter",
            Justification = "The underscore is a conventional name for values that will be discarded.")]
        public static implicit operator Maybe<T>(Maybe.MaybeNone _)
            => default(Maybe<T>);

        /// <summary>Implements the == operator.</summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Maybe<T> left, Maybe<T> right)
            => left.Equals(right);

        /// <summary>Implements the != operator.</summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Maybe<T> left, Maybe<T> right)
            => !left.Equals(right);

        /// <summary>
        /// Performs a conversion from a <typeparamref name="T"/> to a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="inValue">The value.</param>
        /// <returns>The result of the conversion.</returns>
        //// Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
        //// TODO: Decide whether "ToMaybe" makes sense in this context. Have to have a Maybe first?
        public Maybe<T> ToMaybe(T inValue) => inValue == null || inValue is Maybe.MaybeNone
               ? default(Maybe<T>)
                 : new Maybe<T>(inValue);

        /// <summary>Returns a <see cref="Maybe{T}" /> when given one, to avoid double-wrapping.</summary>
        /// <param name="inValue">The value.</param>
        /// <returns>The original value, passed through.</returns>
        //// TODO: Decide whether "ToMaybe" makes sense in this context. Have to have a Maybe first?
        public Maybe<T> ToMaybe(Maybe<T> inValue) => inValue;

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// true if <paramref name="obj" /> and this instance are the same type and represent the
        /// same value; otherwise, false.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming",
            "CA1725:Parameter names should match base declaration",
            Justification = "Base declaration is inconsistent between .NET and Bridge.")]
        public override bool Equals(object obj)
            => obj is Maybe<T> other && Equals(other);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
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
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => myValueExists ? myValue.GetHashCode() : 0;

        /// <summary>
        /// Returns the underlying value, or the default for its type, converted to a string.
        /// </summary>
        /// <returns>The string representation of the value, or a suitable fall-back value.</returns>
        /// <remarks>
        /// Fall-backs: if no value exists, the string representation of the default value for the
        /// type will be returned. If the default value for the type is null, the empty string will
        /// be returned.
        /// </remarks>
        public override string ToString()
            => myValueExists ?
                myValue.ToString() :
                default(T) != null ?
                    default(T).ToString() :
                    string.Empty;

        /// <summary>Applies different functions depending on whether the value exists.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="some">The function to apply if a value exists.</param>
        /// <param name="none">The function to apply if no value exists.</param>
        /// <returns>The result of the function that was applied.</returns>
        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            _ = ParameterValidation.EnsureNotNull(some, nameof(some));
            _ = ParameterValidation.EnsureNotNull(none, nameof(none));

            return myValueExists
                       ? some(myValue)
                       : none();
        }

        /// <summary>Applies different actions depending on whether the value exists.</summary>
        /// <param name="some">The action to apply if a value exists.</param>
        /// <param name="none">The action to apply if no value exists.</param>
        public void Match(Action<T> some, Action none)
        {
            _ = ParameterValidation.EnsureNotNull(some, nameof(some));
            _ = ParameterValidation.EnsureNotNull(none, nameof(none));

            if (myValueExists)
            {
                some(myValue);
            }
            else
            {
                none();
            }
        }

        /// <summary>
        /// Puts the actual value into <paramref name="value" />, if it exists, otherwise the
        /// default value. Returns a Boolean indicating whether a value was found.
        /// </summary>
        /// <param name="value">The value, if it exists, or the default for that type.</param>
        /// <returns>A Boolean indicating whether a value was found.</returns>
        public bool TryGetValue(out T value)
        {
            if (myValueExists)
            {
                value = myValue;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Returns the result of applying the function to the value, or the default if no value exists.
        /// </summary>
        /// <typeparam name="TResult">The underlying type of the result.</typeparam>
        /// <param name="convert">The function to apply to the value.</param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        /// <remarks>Equivalent to Select; this is effectively an alias for comfort.</remarks>
        public Maybe<TResult> Map<TResult>(Func<T, TResult> convert) => Select(convert);

        /// <summary>
        /// Returns the result of applying the function to the value, or the default if no value exists.
        /// </summary>
        /// <typeparam name="TResult">The underlying type of the result.</typeparam>
        /// <param name="convert">The function to apply to the value.</param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        /// <remarks>Equivalent to Map. Required for Linq compatibility.</remarks>
        public Maybe<TResult> Select<TResult>(Func<T, TResult> convert)
        {
            _ = ParameterValidation.EnsureNotNull(convert, nameof(convert));

            return !myValueExists ? default(Maybe<TResult>) : convert(myValue);
        }

        /// <summary>
        /// Returns the result of applying the function to the value, or the default if no value exists.
        /// </summary>
        /// <typeparam name="TResult">The underlying type of the result.</typeparam>
        /// <param name="convert">The function to apply to the value.</param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        /// <remarks>Required Monad method.</remarks>
        public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> convert)
        {
            _ = ParameterValidation.EnsureNotNull(convert, nameof(convert));

            // Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
            return !myValueExists ? default(Maybe<TResult>) : convert(myValue);
        }

        /// <summary>
        /// Allows Linq queries to be written that reference multiple properties of the type
        /// <typeparamref name="T" /> underlying <see cref="Maybe{T}" />.
        /// </summary>
        /// <typeparam name="T2">The type of the intermediate result.</typeparam>
        /// <typeparam name="TResult">The type of the final result.</typeparam>
        /// <param name="convert">
        /// A function that converts <typeparamref name="T" /> to an intermediate type.
        /// </param>
        /// <param name="finalSelect">
        /// A function that produces a result using the original value and any value of the
        /// intermediate type.
        /// </param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        /// <remarks>Unlikely to be used directly, but required to support Linq usage.</remarks>
        public Maybe<TResult> SelectMany<T2, TResult>(Func<T, Maybe<T2>> convert, Func<T, T2, TResult> finalSelect)
        {
            if (!myValueExists)
            {
                // Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
                return default(Maybe<TResult>);
            }

            _ = ParameterValidation.EnsureNotNull(convert, nameof(convert));
            Maybe<T2> converted = convert(myValue);

            // Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
            return !converted.myValueExists
                       ? default(Maybe<TResult>)
                       : finalSelect(myValue, converted.myValue);
        }

        /// <summary>Returns the value if the predicate applies, otherwise the default.</summary>
        /// <param name="predicate">A predicate applying to any value that exists.</param>
        /// <returns>A container representing a result that may or may not exist.</returns>
        public Maybe<T> Where(Func<T, bool> predicate)
        {
            if (!myValueExists)
            {
                // Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
                return default(Maybe<T>);
            }

            _ = ParameterValidation.EnsureNotNull(predicate, nameof(predicate));

            //// Bridge.NET doesn't support simplifying this use of "default" (2020/03/22).
            return predicate(myValue)
                       ? this
                       : default(Maybe<T>);
        }

        /// <summary>Returns the value, if it exists, or the specified default value.</summary>
        /// <param name="defaultValue">The default value to return if no value exists.</param>
        /// <returns>The value, if it exists, or the specified default value.</returns>
        public T ValueOr(T defaultValue) => myValueExists ? myValue : defaultValue;

        /// <summary>Returns the value, if it exists, or the value provided by a factory function.</summary>
        /// <param name="defaultValueFactory">A factory function that will provide a default value.</param>
        /// <returns>The value, if it exists, or the value provided by a factory function.</returns>
        public T ValueOr(Func<T> defaultValueFactory)
        {
            _ = ParameterValidation.EnsureNotNull(defaultValueFactory, nameof(defaultValueFactory));
            return myValueExists ? myValue : defaultValueFactory();
        }

        /// <summary>Returns the value, if it exists, or the specified alternative value.</summary>
        /// <param name="alternativeValue">The alternative value.</param>
        /// <returns>
        /// A container holding the value, if it exists, or the specified alternative value.
        /// </returns>
        public Maybe<T> ValueOrMaybe(Maybe<T> alternativeValue) => myValueExists ? this : alternativeValue;

        /// <summary>Returns the value, if it exists, or the value provided by a factory function.</summary>
        /// <param name="alternativeValueFactory">
        /// A factory function that will provide an alternative value.
        /// </param>
        /// <returns>
        /// A container holding the value, if it exists, or the value provided by a factory function.
        /// </returns>
        public Maybe<T> ValueOrMaybe(Func<Maybe<T>> alternativeValueFactory)
        {
            _ = ParameterValidation.EnsureNotNull(alternativeValueFactory, nameof(alternativeValueFactory));
            return myValueExists ? this : alternativeValueFactory();
        }

        /// <summary>Returns the value, if it exists, or throws an exception.</summary>
        /// <param name="errorMessage">The error message to include in any exception thrown.</param>
        /// <returns>The value, if it exists.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no value exists.</exception>
        public T ValueOrThrow(string errorMessage)
        {
            if (myValueExists)
            {
                return myValue;
            }

            throw new InvalidOperationException(errorMessage);
        }
    }

    /// <summary>Class Maybe.</summary>
    public static class Maybe
    {
        /// <summary>
        /// Gets a value (implicitly convertible to <see cref="Maybe{T}" />) indicating the absence
        /// of a value.
        /// </summary>
        /// <value>A value indicating the absence of a value.</value>
        public static MaybeNone None { get; } = new MaybeNone();

        /// <summary>
        /// Converts the provided value of type <typeparamref name="T" /> to a <see cref="Maybe{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of the value to be converted.</typeparam>
        /// <param name="value">The value to be converted.</param>
        /// <returns>A container holding the value, if it exists.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the value provided is null.</exception>
        public static Maybe<T> Some<T>(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value;
        }

        /// <summary>
        /// Ensures that a <see cref="Maybe{T}" /> doesn't get double-wrapped to become a Maybe{Maybe{T}}.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The <see cref="Maybe{T}" />.</param>
        /// <returns>The value.</returns>
        public static Maybe<T> Some<T>(Maybe<T> value) => value;

        /// <summary>Ensures that a <see cref="MaybeNone" /> doesn't get double-wrapped.</summary>
        /// <param name="value">The <see cref="Maybe{T}" />.</param>
        /// <returns>The value.</returns>
        public static MaybeNone Some(MaybeNone value) => value;

        /// <summary>A class representing the absence of a value in a <see cref="Maybe{T}" />.</summary>
#pragma warning disable CA1034 // Nested types should not be visible

        public class MaybeNone

#pragma warning restore CA1034 // Nested types should not be visible
        {
        }
    }
}
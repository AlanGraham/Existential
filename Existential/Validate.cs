// <copyright file="Validate.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using Existential.Extensions;
    using Existential.Properties;

    // ReSharper disable once StyleCop.SA1650 - 'nameof' is correctly spelled in the documentation header.

    /// <summary>
    ///     <para>
    ///         The static <see cref="Validate" /> class contains a collection of methods for validating arguments; mainly to
    ///         ensure they're not null or empty. The name of the value being validated must be passed in to ensure that it
    ///         can be included in any exception message.
    ///     </para>
    ///     <para>
    ///         There are several practical advantages of these methods.
    ///         <ul>
    ///             <li>The name of the parameter that failed validation will be included in the exception message.</li>
    ///             <li>They're expressive, but succinct - they'll save you several lines of code every time you need a check.</li>
    ///             <li>
    ///                 If the value validates, the method returns it - so pass-through validation is possible in places like
    ///                 chained constructor calls, where you just can't write several lines of code to do this.
    ///             </li>
    ///         </ul>
    ///     </para>
    ///     <para>
    ///         Since the
    ///         <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a>
    ///         operator was introduced in C# 6.0, it's been possible for callers of the methods in this class to use
    ///         <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a>
    ///         to retrieve the value's name in a way that will not be broken by refactoring.
    ///         When using older versions of C#, it was necessary to pass an explicit string value.
    ///     </para>
    /// </summary>
    public static class Validate
    {
        // ReSharper disable once StyleCop.SA1650 - 'nameof' and 'Guid' are correctly spelled in the documentation header.

        /// <summary>
        ///     <em>
        ///         <see cref="ThrowIfEmptyGuid" />
        ///     </em>
        ///     will throw an <see cref="ArgumentNullException" /> if passed an empty GUID
        ///     (<see cref="Guid.Empty" />) as the parameter <paramref name="inValue" />.
        /// </summary>
        /// <param name="inValue">
        ///     A value of type <see cref="Guid" /> to be checked for equality with the empty value
        ///     <see cref="Guid.Empty" />. The name of the variable containing it should be passed as the
        ///     <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inValue" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass as this parameter.
        /// </param>
        /// <returns>The value passed as parameter <paramref name="inValue" /> will be returned if it's not the empty Guid.</returns>
        /// <exception cref="ArgumentException">
        ///     A <see cref="ArgumentException" /> will be thrown if the GUID <paramref name="inValue" /> has the value
        ///     <see cref="Guid.Empty" />.
        ///     If the parameter <paramref name="inName" /> has been correctly populated, the name of the value that was
        ///     unexpectedly found to be empty will be included in the exception message.
        /// </exception>
        [SuppressMessage(
            "Globalization",
            "CA1303:Do not pass literals as localized parameters",
            Justification = "Exception messages should not be localized.")]
        public static Guid ThrowIfEmptyGuid(Guid inValue, string inName)
        {
            if (inValue == Guid.Empty)
            {
                string theMessage = Resources.GuidHasTheEmptyValue;
                throw new ArgumentException(theMessage, inName);
            }

            return inValue;
        }

        // ReSharper disable once StyleCop.SA1650 - 'nameof' is correctly spelled in the documentation header.

        /// <summary>
        ///     <em>
        ///         <see cref="ThrowIfNotOfType{T}" />
        ///     </em>
        ///     will throw an <see cref="ArgumentTypeException" /> if the object passed as the parameter
        ///     <paramref name="inActual" /> isn't of either the type <typeparamref name="T" /> or a
        ///     type derived from it.
        /// </summary>
        /// <typeparam name="T">
        ///     The type parameter <typeparamref name="T" /> specifies that the <paramref name="inActual" /> should
        ///     be of either that type or one derived from it.
        /// </typeparam>
        /// <param name="inActual">
        ///     An object that should be of type <typeparamref name="T" /> or a type derived from it. The name of the variable
        ///     containing it should be passed as the
        ///     <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inActual" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass as this parameter.
        /// </param>
        /// <remarks>
        ///     If a type such as <typeparamref name="T" /> is not known at compile time, then the more dynamic version of
        ///     this method, <see cref="ThrowIfNotOfType" /> can be used instead.
        /// </remarks>
        /// <returns>
        ///     The object passed as parameter <paramref name="inActual" /> will be returned if it's of the expected type or a
        ///     derived one.
        /// </returns>
        /// <exception cref="ArgumentTypeException">
        ///     A <see cref="ArgumentTypeException" /> will be thrown if the object <paramref name="inActual" /> is neither of the
        ///     type <typeparamref name="T" /> nor of a type derived from it.
        ///     If the parameter <paramref name="inName" /> has been correctly populated, the name of the value that was
        ///     found to be of an unexpected type will be included in the exception message.
        /// </exception>
        public static T ThrowIfNotOfType<T>([ValidatedOfType] object inActual, string inName)
            where T : class
        {
            if (!(inActual is T theReturnValue))
            {
                string theMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    "An argument of type {0} was provided (expected {1}).",
                    ThrowIfNull(inActual, inName).GetType().GetGenericAwareTypeName(),
                    typeof(T).GetGenericAwareTypeName());
                throw new ArgumentTypeException(theMessage, inName);
            }

            return theReturnValue;
        }

        // ReSharper disable once StyleCop.SA1650 - 'nameof' is correctly spelled in the documentation header.

        /// <summary>
        ///     <em>
        ///         <see cref="ThrowIfNotOfType{T}" />
        ///     </em>
        ///     will throw an <see cref="ArgumentTypeException" /> if the object passed as the parameter
        ///     <paramref name="inActual" /> isn't of either the type <paramref name="inType" /> or a
        ///     type derived from it.
        /// </summary>
        /// <param name="inType">
        ///     The type <paramref name="inType" /> specifies that the <paramref name="inActual" /> should
        ///     be of either that type or one derived from it.
        /// </param>
        /// <param name="inActual">
        ///     An object that should be of type <paramref name="inType" /> or a type derived from it. The name of the variable
        ///     containing it should be passed as the
        ///     <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inActual" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass as this parameter.
        /// </param>
        /// <remarks>
        ///     If the type of <paramref name="inType"/> is known at compile time, then the generic version of
        ///     this method, <see cref="ThrowIfNotOfType{T}"/> may be preferred.
        /// </remarks>
        /// <returns>
        ///     The object passed as parameter <paramref name="inActual" /> will be returned if it's of the expected type or a
        ///     derived one.
        /// </returns>
        /// <exception cref="ArgumentTypeException">
        ///     A <see cref="ArgumentTypeException" /> will be thrown if the object <paramref name="inActual" /> is neither of the
        ///     type <paramref name="inType" /> nor of a type derived from it.
        ///     If the parameter <paramref name="inName" /> has been correctly populated, the name of the value that was
        ///     found to be of an unexpected type will be included in the exception message.
        /// </exception>
        public static object ThrowIfNotOfType(Type inType, [ValidatedOfType] object inActual, string inName)
        {
            // ReSharper disable once UseMethodIsInstanceOfType - can't be directly substituted.
            if (!ThrowIfNull(inType, nameof(inType)).IsAssignableFrom(ThrowIfNull(inActual, inName).GetType()))
            {
                string theMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    "An argument of type {0} was provided (expected {1}).",
                    inActual.GetType().GetGenericAwareTypeName(),
                    inType.GetGenericAwareTypeName());
                throw new ArgumentTypeException(theMessage, inName);
            }

            return inActual;
        }

        /// <summary>Ensures the specified value is not null; otherwise throws an ArgumentNullException.</summary>
        /// <typeparam name="T">The type of the value being checked for null.</typeparam>
        /// <param name="inValue">The value to check for null.</param>
        /// <param name="inName">The name of the value being checked, as a string.</param>
        /// <returns>The validated value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the object value is null.</exception>
        public static T ThrowIfNull<T>([ValidatedNotNull] T inValue, string inName) =>
            inValue == null ? throw new ArgumentNullException(inName) : inValue;

        /// <summary>
        ///     Throws an ArgumentNullException if the collection is null, or an ArgumentException if it
        ///     exists but is empty.
        /// </summary>
        /// <typeparam name="T">Type of object in the collection.</typeparam>
        /// <param name="inCollection">The collection to check.</param>
        /// <param name="inName">The name of the parameter.</param>
        /// <returns>The validated value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the collection is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the collection exists but is empty.</exception>
        public static T ThrowIfNullOrEmpty<T>(T inCollection, string inName)
            where T : class, IEnumerable =>
            inCollection == null
                ?
                throw new ArgumentNullException(inName, FormatArgumentMessage("The collection \"{0}\" is null", inName))
                : !inCollection.GetEnumerator().MoveNext()
                    ? throw new ArgumentException(
                          FormatArgumentMessage("The collection \"{0}\" is empty", inName),
                          inName)
                    : inCollection;

        /// <summary>Throws an ArgumentNullException if the specified string is null or empty.</summary>
        /// <param name="inValue">The parameter being checked for null.</param>
        /// <param name="inName">The name of the parameter.</param>
        /// <param name="inTrim">Trim string before check.</param>
        /// <returns>The validated value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the string is null or empty.</exception>
        public static string ThrowIfNullOrEmpty([ValidatedNotNull] string inValue, string inName, bool inTrim)
        {
            if (inValue == null)
            {
                throw new ArgumentNullException(inName);
            }

            string theTestValue = inTrim ? inValue.Trim() : inValue;

            return string.IsNullOrEmpty(theTestValue)
                       ? throw (inTrim
                                    ? new ArgumentException(Resources.StringCannotBeEmptyOrWhiteSpaceWithTrim, inName)
                                    : new ArgumentException(Resources.StringCannotBeEmpty, inName))
                       : inValue;
        }

        /// <summary>Throws an ArgumentNullException if the specified string is null or empty.</summary>
        /// <param name="inValue">The parameter being checked for null.</param>
        /// <param name="inName">The name of the parameter.</param>
        /// <returns>The validated value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the string is null or empty.</exception>
        public static string ThrowIfNullOrEmpty([ValidatedNotNull] string inValue, string inName) =>
            ThrowIfNullOrEmpty(inValue, inName, false);

        /// <summary>Throws an ArgumentNullException if the specified string is null or empty.</summary>
        /// <param name="inValue">The parameter being checked for null.</param>
        /// <param name="inName">The name of the parameter.</param>
        /// <returns>The validated value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the string is null or empty.</exception>
        public static string ThrowIfNullOrWhiteSpace([ValidatedNotNull] string inValue, string inName)
        {
            if (inValue == null)
            {
                throw new ArgumentNullException(inName);
            }

            return string.IsNullOrWhiteSpace(inValue)
                       ? throw new ArgumentException(Resources.StringCannotBeEmptyOrWhiteSpace, inName)
                       : inValue;
        }

        /// <summary>Inserts a name into an exception message.</summary>
        /// <param name="inMessage">The message into which the name should be inserted.</param>
        /// <param name="inArgumentName">The name to be inserted in the exception message.</param>
        /// <returns>The message with the name inserted into it.</returns>
        private static string FormatArgumentMessage(string inMessage, string inArgumentName)
        {
            string theEmptyErrorMessage = string.Format(CultureInfo.InvariantCulture, inMessage, inArgumentName);
            return theEmptyErrorMessage;
        }
    }
}
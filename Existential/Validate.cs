// <copyright file="Validate.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using Properties;

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
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Spelling was reviewed. Suppression is required because of keywords (e.g. 'nameof').")]
    public static class Validate
    {
        /// <summary>
        ///     <em>
        ///         <see cref="ThrowIfEmptyGuid" />
        ///     </em>
        ///     will throw an <see cref="ArgumentNullException" /> if passed an empty GUID
        ///     (<see cref="Guid.Empty" />) as the parameter <paramref name="inValue" />.
        /// </summary>
        /// <param name="inValue">
        ///     A value of type <see cref="Guid" /> to be checked for equality with the empty value <see cref="Guid.Empty" />. The
        ///     name of the variable containing the value should be passed to this method as the
        ///     <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inValue" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass here.
        /// </param>
        /// <returns>The value passed as parameter <paramref name="inValue" /> will be returned if it's not the empty Guid.</returns>
        /// <exception cref="ArgumentException">
        ///     <para>
        ///         A <see cref="ArgumentException" /> will be thrown if the GUID <paramref name="inValue" /> has the value
        ///         <see cref="Guid.Empty" />.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the value that was
        ///         unexpectedly found to be empty will be included in the exception message.
        ///     </para>
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
        ///     containing the object should be passed to this method as the
        ///     <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inActual" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass here.
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
        ///     <para>
        ///         A <see cref="ArgumentTypeException" /> will be thrown if the object <paramref name="inActual" /> is neither of
        ///         the
        ///         type <typeparamref name="T" /> nor of a type derived from it.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the value that was
        ///         found to be of an unexpected type will be included in the exception message.
        ///     </para>
        /// </exception>
        public static T ThrowIfNotOfType<T>(object inActual, string inName)
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
        ///     containing the object should be passed to this method as the <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inActual" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass here.
        /// </param>
        /// <remarks>
        ///     If the type of <paramref name="inType" /> is known at compile time, then the generic version of
        ///     this method, <see cref="ThrowIfNotOfType{T}" /> may be preferred.
        /// </remarks>
        /// <returns>
        ///     The object passed as parameter <paramref name="inActual" /> will be returned if it's of the expected type or a
        ///     derived one.
        /// </returns>
        /// <exception cref="ArgumentTypeException">
        ///     <para>
        ///         A <see cref="ArgumentTypeException" /> will be thrown if the object <paramref name="inActual" /> is neither of
        ///         the
        ///         type <paramref name="inType" /> nor of a type derived from it.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the value that was
        ///         found to be of an unexpected type will be included in the exception message.
        ///     </para>
        /// </exception>
        public static object ThrowIfNotOfType(Type inType, object inActual, string inName)
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

        /// <summary>
        ///     <em>
        ///         <see cref="ThrowIfNull{T}" />
        ///     </em>
        ///     will throw an <see cref="ArgumentNullException" /> if the value passed as the parameter <paramref name="inValue" />
        ///     is null.
        /// </summary>
        /// <typeparam name="T">
        ///     The type parameter <typeparamref name="T" /> captures the type of the parameter
        ///     <paramref name="inValue" /> so that the value can be returned as the same type if it validates as not being null.
        ///     The type should not have to be specified explicitly when calling the method.
        /// </typeparam>
        /// <param name="inValue">
        ///     A value that will be validated to ensure that it's non-null. The name of the variable containing the value should
        ///     be passed to this method as the
        ///     <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inValue" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass here.
        /// </param>
        /// <returns>
        ///     The value passed as parameter <paramref name="inValue" /> will be returned if it validates as non-null.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <para>
        ///         A <see cref="ArgumentNullException" /> will be thrown if the object <paramref name="inValue" /> is null.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the value that was
        ///         found to be unexpectedly null will be included in the exception message.
        ///     </para>
        /// </exception>
        public static T ThrowIfNull<T>([ValidatedNotNull] T inValue, string inName) =>
            inValue == null ? throw new ArgumentNullException(inName) : inValue;

        /// <summary>
        ///     <em>
        ///         <see cref="ThrowIfNullOrEmpty{T}" />
        ///     </em>
        ///     will throw an <see cref="ArgumentNullException" /> if a null value is passed as the parameter
        ///     <paramref name="inCollection" />, or an <see cref="ArgumentException" /> if the collection exists but is empty.
        /// </summary>
        /// <typeparam name="T">
        ///     The type parameter <typeparamref name="T" /> captures the type of the collection passed to the
        ///     parameter <paramref name="inCollection" /> so that the value can be returned as exactly the same type if it
        ///     validates successfully. The type should not have to be specified explicitly when calling the method. There are few
        ///     constraints on the type <typeparamref name="T" />; it must be a class that implements the interface
        ///     <see cref="IEnumerable" />.
        /// </typeparam>
        /// <param name="inCollection">
        ///     A collection that will be validated to ensure that it's neither null nor empty. The name of the variable containing
        ///     the collection should be passed to this method as the
        ///     <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inCollection" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass here.
        /// </param>
        /// <returns>
        ///     The collection passed as parameter <paramref name="inCollection" /> will be returned if it validates as being
        ///     neither null nor empty.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <para>
        ///         A <see cref="ArgumentNullException" /> will be thrown if the value of parameter
        ///         <paramref name="inCollection" /> is null.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the collection that was
        ///         found to be unexpectedly null will be included in the exception message.
        ///     </para>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <para>
        ///         A <see cref="ArgumentException" /> will be thrown if the collection passed as parameter
        ///         <paramref name="inCollection" /> is empty.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the collection that was
        ///         found to be unexpectedly empty will be included in the exception message.
        ///     </para>
        /// </exception>
        [SuppressMessage(
            "Style",
            "IDE0046:Convert to conditional expression",
            Justification = "Complex method - would reduce readability.")]
        public static T ThrowIfNullOrEmpty<T>([ValidatedNotNull] T inCollection, string inName)
            where T : class, IEnumerable
        {
            if (inCollection == null)
            {
                throw new ArgumentNullException(
                    inName,
                    FormatArgumentMessage("The collection \"{0}\" is null", inName));
            }

            return !inCollection.GetEnumerator().MoveNext()
                ? throw new ArgumentException(
                    FormatArgumentMessage("The collection \"{0}\" is empty", inName),
                    inName)
                : inCollection;
        }

        /// <summary>
        ///     <em>ThrowIfNullOrEmpty</em>
        ///     will throw an <see cref="ArgumentNullException" /> if a null value is passed as the parameter
        ///     <paramref name="inValue" />, or an <see cref="ArgumentException" /> if the string exists but is empty.
        /// </summary>
        /// <param name="inValue">
        ///     A string that will be validated to ensure that it's neither null nor empty. The name of the variable containing
        ///     the string should be passed to this method as the
        ///     <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inValue" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass here.
        /// </param>
        /// <returns>
        ///     The string passed as parameter <paramref name="inValue" /> will be returned if it validates as being
        ///     neither null nor empty.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <para>
        ///         A <see cref="ArgumentNullException" /> will be thrown if the value of parameter
        ///         <paramref name="inValue" /> is null.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the string that was
        ///         found to be unexpectedly null will be included in the exception message.
        ///     </para>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <para>
        ///         A <see cref="ArgumentException" /> will be thrown if the string passed as parameter
        ///         <paramref name="inValue" /> is empty.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the string that was
        ///         found to be unexpectedly empty will be included in the exception message.
        ///     </para>
        /// </exception>
        /// <seealso cref="ThrowIfNullOrWhiteSpace" />
        public static string ThrowIfNullOrEmpty([ValidatedNotNull] string inValue, string inName) =>
            ThrowIfNullOrEmpty(inValue, inName, false);

        /// <summary>
        ///     <em>ThrowIfNullOrEmpty</em>
        ///     will throw an <see cref="ArgumentNullException" /> if a null value is passed as the parameter
        ///     <paramref name="inValue" />, or an <see cref="ArgumentException" /> if the string exists but is empty.
        /// </summary>
        /// <param name="inValue">
        ///     A string that will be validated to ensure that it's neither null nor empty. The name of the variable containing
        ///     the string should be passed to this method as the
        ///     <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inValue" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass here.
        /// </param>
        /// <param name="inTrim">
        ///     A Boolean value indicating whether or not white space should be trimmed from each end of the string. This can
        ///     result in a string that contains <em>only</em> white space characters being treated as empty. However the behaviour
        ///     of this method is not identical to <see cref="ThrowIfNullOrWhiteSpace" />. This method can change the value of the
        ///     string passed as <paramref name="inValue" /> before returning it, while <see cref="ThrowIfNullOrWhiteSpace" /> has
        ///     no side-effects.
        /// </param>
        /// <returns>
        ///     The string passed as parameter <paramref name="inValue" /> will be returned if it validates as being
        ///     neither null nor empty. If the value of <paramref name="inTrim" /> was <c>true</c>, then white space characters
        ///     will be trimmed from either end of the string before it is returned.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <para>
        ///         A <see cref="ArgumentNullException" /> will be thrown if the value of parameter
        ///         <paramref name="inValue" /> is null.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the string that was
        ///         found to be unexpectedly null will be included in the exception message.
        ///     </para>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <para>
        ///         A <see cref="ArgumentException" /> will be thrown if the string passed as parameter
        ///         <paramref name="inValue" /> is empty.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the string that was
        ///         found to be unexpectedly empty will be included in the exception message.
        ///     </para>
        /// </exception>
        /// <seealso cref="ThrowIfNullOrWhiteSpace" />
        [SuppressMessage(
            "Style",
            "IDE0046:Convert to conditional expression",
            Justification = "Complex method - would reduce readability.")]
        public static string ThrowIfNullOrEmpty([ValidatedNotNull] string inValue, string inName, bool inTrim)
        {
            if (inValue == null)
            {
                throw new ArgumentNullException(inName);
            }

            string theTestValue = inTrim ? inValue.Trim() : inValue;

            if (string.IsNullOrEmpty(theTestValue))
            {
                throw inTrim
                    ? new ArgumentException(Resources.StringCannotBeEmptyOrWhiteSpaceWithTrim, inName)
                    : new ArgumentException(Resources.StringCannotBeEmpty, inName);
            }

            return inValue;
        }

        /// <summary>
        ///     <em>
        ///         <see cref="ThrowIfNullOrWhiteSpace" />
        ///     </em>
        ///     will throw an <see cref="ArgumentNullException" /> if a null value is passed as the parameter
        ///     <paramref name="inValue" />, or an <see cref="ArgumentException" /> if the string exists but is either empty or
        ///     contains only white space characters.
        /// </summary>
        /// <param name="inValue">
        ///     A string that will be validated to ensure that it's neither null nor empty, nor contains only white space
        ///     characters. The name of the variable containing the string should be passed to this method as the
        ///     <paramref name="inName" /> parameter.
        /// </param>
        /// <param name="inName">
        ///     The name of the variable passed to the <paramref name="inValue" /> parameter. Since C# 6.0, the
        ///     <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</a> operator
        ///     can be used to retrieve a value to pass here.
        /// </param>
        /// <returns>
        ///     The string passed as parameter <paramref name="inValue" /> will be returned if it validates as being
        ///     neither null nor empty, nor containing only whitespace characters.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <para>
        ///         A <see cref="ArgumentNullException" /> will be thrown if the value of parameter
        ///         <paramref name="inValue" /> is null.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the string that was
        ///         found to be unexpectedly null will be included in the exception message.
        ///     </para>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <para>
        ///         A <see cref="ArgumentException" /> will be thrown if the string passed as parameter
        ///         <paramref name="inValue" /> is empty or contains only white space characters.
        ///     </para>
        ///     <para>
        ///         If the parameter <paramref name="inName" /> has been correctly populated, the name of the string that was
        ///         found to be unexpectedly empty or containing only white space will be included in the exception message.
        ///     </para>
        /// </exception>
        [SuppressMessage(
            "Style",
            "IDE0046:Convert to conditional expression",
            Justification = "Complex method - would reduce readability.")]
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
// <copyright file="Validate.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System;
    using System.Globalization;

    using Existential.Extensions;

    /// <summary>Contains validation methods.</summary>
    public static class Validate
    {
        /// <summary>Throws an ArgumentNullException if the specified GUID is the empty GUID.</summary>
        /// <param name="inValue">The GUID being checked for emptiness.</param>
        /// <param name="inName">The name of the parameter.</param>
        /// <returns>The validated value.</returns>
        /// <exception cref="ArgumentException">Thrown if the GUID has the empty value.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Globalization",
            "CA1303:Do not pass literals as localized parameters",
            Justification = "Exception messages should not be localized.")]
        public static Guid ThrowIfEmptyGuid(Guid inValue, string inName)
        {
            if (inValue == Guid.Empty)
            {
                const string Message = "The GUID has the empty value, {00000000-0000-0000-0000-000000000000}.";
                throw new ArgumentException(Message, inName);
            }

            return inValue;
        }

        /// <summary>
        /// Ensures the specified value is of the expected type; otherwise throws an ArgumentException.
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="inActual">The value to check for an expected type.</param>
        /// <param name="inName">The name of the value being checked, as a string.</param>
        /// <exception cref="ArgumentException">Thrown if the object type is not as expected.</exception>
        /// <returns>The original value.</returns>
        public static T ThrowIfNotOfType<T>([ValidatedOfType] object inActual, string inName)
            where T : class
        {
            if (!(inActual is T))
            {
                string theMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    "An argument of type {0} was provided (expected {1}).",
                    ThrowIfNull(inActual, inName)
                        .GetType().GetGenericAwareTypeName(),
                    typeof(T).GetGenericAwareTypeName());
                throw new ArgumentTypeException(theMessage, inName);
            }

            return inActual as T;
        }

        /// <summary>Ensures the specified value is not null; otherwise throws an ArgumentNullException.</summary>
        /// <typeparam name="T">The type of the value being checked for null.</typeparam>
        /// <param name="inValue">The value to check for null.</param>
        /// <param name="inName">The name of the value being checked, as a string.</param>
        /// <returns>The validated value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the object value is null.</exception>
        public static T ThrowIfNull<T>([ValidatedNotNull] T inValue, string inName)
        {
            if (inValue == null)
            {
                throw new ArgumentNullException(inName);
            }

            return inValue;
        }

        /// <summary>
        /// Throws an ArgumentNullException if the collection is null, or an ArgumentException if it
        /// exists but is empty.
        /// </summary>
        /// <typeparam name="T">Type of object in the collection.</typeparam>
        /// <param name="inCollection">The collection to check.</param>
        /// <param name="inName">The name of the parameter.</param>
        /// <returns>The validated value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the collection is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the collection exists but is empty.</exception>
        public static T ThrowIfNullOrEmpty<T>(T inCollection, string inName)
            where T : class, System.Collections.IEnumerable
        {
            if (inCollection == null)
            {
                throw new ArgumentNullException(
                    inName,
                    InsertNameIntoExceptionMessage("The collection \"{0}\" is null", inName));
            }

            if (!inCollection.GetEnumerator().MoveNext())
            {
                throw new ArgumentException(
                    InsertNameIntoExceptionMessage("The collection \"{0}\" is empty", inName),
                    inName);
            }

            return inCollection;
        }

        /// <summary>Throws an ArgumentNullException if the specified string is null or empty.</summary>
        /// <param name="inValue">The parameter being checked for null.</param>
        /// <param name="inName">The name of the parameter.</param>
        /// <param name="trim">Trim string before check.</param>
        /// <returns>The validated value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the string is null or empty.</exception>
        public static string ThrowIfNullOrEmpty([ValidatedNotNull] string inValue, string inName, bool trim)
        {
            if (inValue == null)
            {
                throw new ArgumentNullException(inName);
            }

            string theTestValue = trim ? inValue.Trim() : inValue;
            if (string.IsNullOrEmpty(theTestValue))
            {
                throw new ArgumentException("String cannot be empty.", inName);
            }

            return inValue;
        }

        /// <summary>Throws an ArgumentNullException if the specified string is null or empty.</summary>
        /// <param name="inValue">The parameter being checked for null.</param>
        /// <param name="inName">The name of the parameter.</param>
        /// <returns>The validated value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the string is null or empty.</exception>
        public static string ThrowIfNullOrEmpty([ValidatedNotNull] string inValue, string inName)
            => ThrowIfNullOrEmpty(inValue, inName, trim: true);

        /// <summary>Inserts a name into an exception message.</summary>
        /// <param name="inMessage">The message into which the name should be inserted.</param>
        /// <param name="inArgumentName">The name to be inserted in the exception message.</param>
        /// <returns>The message with the name inserted into it.</returns>
        private static string InsertNameIntoExceptionMessage(string inMessage, string inArgumentName)
        {
            string theEmptyErrorMessage = string.Format(
                CultureInfo.InvariantCulture,
                inMessage,
                inArgumentName);
            return theEmptyErrorMessage;
        }

        /*
        /// <summary>Determines whether the specified value is of the expected type.</summary>
        /// <param name="inExpectedType">The expected type of the value..</param>
        /// <param name="inActual">The value to check for an expected type.</param>
        /// <returns>
        /// Value is <see langword="true" /> if the specified value is of the expected type;
        /// otherwise <see langword="false" />.
        /// </returns>
        private static bool IsOfType(System.Type inExpectedType, object inActual)
        {
            ParameterValidation.EnsureNotNull(inActual, nameof(inActual));

            return inActual.GetType().IsInstanceOfType(inExpectedType);
        }
        */
    }
}
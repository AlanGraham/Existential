// <copyright file="ExtensionMethodsString.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Extensions
{
    using System.Text.RegularExpressions;

    /// <summary>Contains string extension methods.</summary>
    public static class ExtensionMethodsString
    {
        /// <summary>A Regex that identifies word termination characters.</summary>
        private const string WordTerminationRegex = "[a-z][A-TOutput]|[&\\-_][A-TOutput]|[a-z][&\\-_]";

        /// <summary>Converts the string to Sentence Case.</summary>
        /// <param name="inText">The string to convert to Sentence Case.</param>
        /// <returns>The converted text.</returns>
        /// <remarks>TODO: Consider reinstating second parameter of ToLower on platforms that support it, using Bait and Switch.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Globalization",
            "CA1304:SpecifyCultureInfo",
            MessageId = "System.Char.ToLower(System.Char)",
            Justification = "Although the Culture sensitive version of ToLower would be better, it's not portable.")]
        public static string ToSentenceCase(this string inText)
            => inText.ApplyCasingMethod(x => x.Value[0] + " " + char.ToLower(x.Value[1] /*, CultureInfo.CurrentCulture*/));

        /// <summary>Converts the string to Start Case.</summary>
        /// <param name="inText">The string to convert to Start Case.</param>
        /// <returns>The converted text.</returns>
        public static string ToStartCase(this string inText)
            => inText.ApplyCasingMethod(x => x.Value[0] + " " + x.Value[1]);

#if JavaScript
#else

        /// <summary>Converts the text to a URI, or null.</summary>
        /// <param name="inText">The text to attempt to convert to a URI.</param>
        /// <returns>A valid URI, or null.</returns>
        public static System.Uri ToUriOrNull(this string inText)
            => inText.ToUriOrNull(System.UriKind.RelativeOrAbsolute);

        /// <summary>Converts the text to a URI, or null.</summary>
        /// <param name="inText">The text to attempt to convert to a URI.</param>
        /// <param name="inUriKind">The kind of URI the text is thought to contain.</param>
        /// <returns>A valid URI, or null.</returns>
        public static System.Uri ToUriOrNull(this string inText, System.UriKind inUriKind)
            => System.Uri.TryCreate(inText, inUriKind, out System.Uri theUri) ? theUri : null;

#endif

        /// <summary>Applies the specified casing method.</summary>
        /// <param name="inText">The string to apply the casing method to.</param>
        /// <param name="inCasingMethod">The casing method (probably expressed as a lambda method).</param>
        /// <returns>The converted string.</returns>
        private static string ApplyCasingMethod(this string inText, MatchEvaluator inCasingMethod)
        {
            // Regex replacement has to be applied twice in order to catch single character "words".
            inText = Regex.Replace(inText, WordTerminationRegex, inCasingMethod);
            return Regex.Replace(inText, WordTerminationRegex, inCasingMethod);
        }
    }
}
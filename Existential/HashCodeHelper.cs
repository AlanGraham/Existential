// <copyright file="HashCodeHelper.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System;

    /// <summary>A class containing methods to help with hash codes.</summary>
    public static class HashCodeHelper
    {
        // This is the value suggested for the FNV algorithm.
        private const int FnvPrimeNumber = 16777619;

        // This value is used as a fallback in the unlikely event that a random offset can't be assigned. The original uint
        // value is the one suggested for the FNV algorithm. The conversion from uint to int is not ideal, but should help to
        // retain its effect on the distribution of the hash function. As it's only ever used as a compromise in an unlikely
        // situation, we'll live with it.
        private const int FallbackOffsetBasis = unchecked((int)2166136261);

        private static readonly int OffsetBasis = GetOffsetBasis();

        /// <summary>
        ///     Generates a hash code from the provided values. A hash code can be generated for any type of object, by passing
        ///     its fields as arguments to this method.
        /// </summary>
        /// <param name="inObjectArray">
        ///     An array of values from which to calculate a hash code.
        /// </param>
        /// <returns>A hash code generated from the provided values.</returns>
        /// <remarks>
        ///     This method is a modification of a method
        ///     <a href="https://stackoverflow.com/a/263416">documented on Stack Overflow</a> by Jon Skeet, which in turn
        ///     is derived from the
        ///     <a href="https://web.archive.org/web/20190214151827/http://www.isthe.com/chongo/tech/comp/fnv/#FNV-param">
        ///         FNV (Fowler/Noll/Vo) hash algorithm
        ///     </a>
        ///     . This method differs in that it uses a randomly assigned number as the offset basis. Doing so means that a hash
        ///     value for the same object will be different when calculated in a different process. This behaviour is a better
        ///     match for default hash code behaviour as of .NET 5.0 (and optional behaviour that can be enabled in the .NET
        ///     Framework by enabling the use of the
        ///     <a
        ///         href="https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/runtime/userandomizedstringhashalgorithm-element">
        ///         randomised string hash algorithm
        ///     </a>
        ///     ).
        /// </remarks>
        public static int CalculateHashCode(params object[] inObjectArray)
        {
            int theHashCode = 0;

            if (inObjectArray != null && inObjectArray.Length != 0)
            {
                // Overflow is fine, just wrap
                unchecked
                {
                    theHashCode = OffsetBasis;

                    foreach (object aFieldValue in inObjectArray)
                    {
                        theHashCode = (theHashCode * FnvPrimeNumber) ^
                                      (aFieldValue?.GetHashCode()).GetValueOrDefault(0);
                    }
                }
            }

            return theHashCode;
        }

        /// <summary>
        ///     Ensure the hash is unique to this process by using a static, non-zero random offset.
        /// </summary>
        /// <returns>A static, non-zero random offset.</returns>
        private static int GetOffsetBasis()
        {
            var theRandom = new Random();
            int theOffsetBasis = theRandom.Next();

            // It's very unlikely the random number will be zero, but if it happens we'll allow a number of
            // retries to get a different result.
            const int LoopLimit = 101;
            int theCounter = 0;
            while (theOffsetBasis == 0 && theCounter < LoopLimit)
            {
                theOffsetBasis = theRandom.Next();
                theCounter++;
            }

            // Return the offset basis. If a random one has not been created, we'll use a fallback value,
            // which is the one recommended for the FNV algorithm.
            return theCounter == LoopLimit - 1
                ? FallbackOffsetBasis
                : theOffsetBasis;
        }
    }
}
// <copyright file="HashCodeHelper.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace GavinGreig
{
    /// <summary>A class containing methods to help with hash codes.</summary>
    public static class HashCodeHelper
    {
        private const int PrimeNumberA = 17;
        private const int PrimeNumberB = 23;

        /// <summary>A method to calculate hash codes fairly reliably.</summary>
        /// <param name="inFieldValues">
        /// An array of field values from which to calculate the hash code.
        /// </param>
        /// <returns>A fairly reliably unique hash code.</returns>
        /// <remarks>Method documented here: https://stackoverflow.com/a/263416.</remarks>
        public static int CalculateHashCode(params object[] inFieldValues)
        {
            // Overflow is fine, just wrap
            unchecked
            {
                int theHashCode = PrimeNumberA;

                foreach (object aFieldValue in inFieldValues)
                {
                    theHashCode = (theHashCode * PrimeNumberB) + (aFieldValue?.GetHashCode()).GetValueOrDefault(0);
                }

                return theHashCode;
            }
        }
    }
}
// <copyright file="ThisMethod.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace GavinGreig
{
    using System.Runtime.CompilerServices;

    /// <summary>A utility class for caller methods.</summary>
    public static class ThisMethod
    {
        /// <summary>
        /// Gets the name of the calling method at compile time (i.e. not requiring reflection).
        /// </summary>
        /// <param name="inCaller">
        /// No value should be provided for this parameter. The value will be populated by the
        /// CallerMemberName attribute.
        /// </param>
        /// <returns>The name of the calling method.</returns>
        /// <remarks>Depends on the use of the CallerMemberName attribute: https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.callermembernameattribute?view=netframework-4.8.</remarks>
        public static string GetName([CallerMemberName] string inCaller = null) => inCaller;
    }
}
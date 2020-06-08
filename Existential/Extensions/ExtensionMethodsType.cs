// <copyright file="ExtensionMethodsType.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>
// <author>Dr. Gavin T.D. Greig</author>
// <date>2013-08-31</date>
// <summary>Contains Type extension methods.</summary>

namespace GavinGreig.Extensions
{
    using System;
    using System.Linq;

    /// <summary>Contains Type extension methods for better support of generics.</summary>
    public static class ExtensionMethodsType
    {
        /// <summary>Gets the full name of the type, including any generic type parameters.</summary>
        /// <param name="inType">The type for which to get the full name.</param>
        /// <returns>The full name of the type, including any generic type parameters.</returns>
        public static string GetGenericAwareFullTypeName(this System.Type inType)
        {
            ParameterValidation.EnsureNotNull(inType, "inType");
            return inType.GetGenericAwareTypeName(inType.FullName);
        }

        /// <summary>Gets the full name of the type, including any generic type parameters.</summary>
        /// <typeparam name="T">The type of the instance (will be inferred).</typeparam>
        /// <param name="inInstance">The instance for which to get the type name.</param>
        /// <returns>The full name of the type, including any generic type parameters.</returns>
        public static string GetGenericAwareFullTypeName<T>(this T inInstance)
        {
            ParameterValidation.EnsureNotNull(inInstance, "inInstance");
            Type theType = typeof(T);
            return theType.GetGenericAwareTypeName(theType.FullName);
        }

        /// <summary>Gets the name of the type, including any generic type parameters.</summary>
        /// <param name="inType">The type for which to get the name.</param>
        /// <returns>The name of the type, including any generic type parameters.</returns>
        public static string GetGenericAwareTypeName(this System.Type inType)
        {
            ParameterValidation.EnsureNotNull(inType, "inType");
            return inType.GetGenericAwareTypeName(inType.Name);
        }

        /// <summary>Gets the name of the type, including any generic type parameters.</summary>
        /// <typeparam name="T">The type of the instance (will be inferred).</typeparam>
        /// <param name="inInstance">The instance for which to get the type name.</param>
        /// <returns>The name of the type, including any generic type parameters.</returns>
        public static string GetGenericAwareTypeName<T>(this T inInstance)
        {
            ParameterValidation.EnsureNotNull(inInstance, "inInstance");
            Type theType = typeof(T);
            return theType.GetGenericAwareTypeName(theType.Name);
        }

        /// <summary>Determines whether this type is of the expected type.</summary>
        /// <param name="inType">This type.</param>
        /// <param name="inExpectedType">The expected type.</param>
        /// <returns>True if the types are same; otherwise false.</returns>
        public static bool IsInstanceOfType(this System.Type inType, System.Type inExpectedType)
            => inType == inExpectedType;

        /// <summary>Gets the name of the type, including any generic type parameters.</summary>
        /// <param name="inType">The type for which to get the name.</param>
        /// <param name="inTypeNameRoot">
        /// The preferred format of the non-generic part of the type name.
        /// </param>
        /// <returns>The name of the type, including any generic type parameters.</returns>
        private static string GetGenericAwareTypeName(this System.Type inType, string inTypeNameRoot)
        {
            string theTypeNameRoot = inTypeNameRoot;
            if (!inType.IsGenericType)
            {
                return theTypeNameRoot;
            }

            var theTypeNameBuilder = new System.Text.StringBuilder();
            theTypeNameBuilder.Append(theTypeNameRoot.Substring(0, theTypeNameRoot.IndexOf('`')));
            theTypeNameBuilder.Append("<");
            theTypeNameBuilder.Append(string.Join(
                ", ",
                GetTypeParameters(inType)));
            theTypeNameBuilder.Append(">");
            return theTypeNameBuilder.ToString();
        }

        /// <summary>Gets the specified type's generic type parameters as an array of strings.</summary>
        /// <param name="inType">The type for which to get the type parameters.</param>
        /// <returns>The generic type parameters as an array of strings.</returns>
        private static string[] GetTypeParameters(this System.Type inType)
            => inType.GetGenericArguments().Select(t => GetGenericAwareTypeName(t)).ToArray();
    }
}
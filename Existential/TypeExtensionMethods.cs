﻿// <copyright file="TypeExtensionMethods.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>Contains Type extension methods for better support of generics.</summary>
    public static class TypeExtensionMethods
    {
        /// <summary>Gets the full name of the type, including any generic type parameters.</summary>
        /// <param name="inType">The type for which to get the full name.</param>
        /// <returns>The full name of the type, including any generic type parameters.</returns>
        public static string GetGenericAwareFullTypeName(this Type inType)
        {
            _ = Validate.ThrowIfNull(inType, "inType");
            return inType.GetGenericAwareTypeName(inType.FullName);
        }

        /// <summary>Gets the full name of the type, including any generic type parameters.</summary>
        /// <typeparam name="T">The type of the instance (will be inferred).</typeparam>
        /// <param name="inInstance">The instance for which to get the type name.</param>
        /// <returns>The full name of the type, including any generic type parameters.</returns>
        public static string GetGenericAwareFullTypeName<T>(this T inInstance)
        {
            _ = Validate.ThrowIfNull(inInstance, "inInstance");
            Type theType = typeof(T);
            return theType.GetGenericAwareTypeName(theType.FullName);
        }

        /// <summary>Gets the name of the type, including any generic type parameters.</summary>
        /// <param name="inType">The type for which to get the name.</param>
        /// <returns>The name of the type, including any generic type parameters.</returns>
        public static string GetGenericAwareTypeName(this Type inType)
        {
            _ = Validate.ThrowIfNull(inType, "inType");
            return inType.GetGenericAwareTypeName(inType.Name);
        }

        /// <summary>Gets the name of the type, including any generic type parameters.</summary>
        /// <typeparam name="T">The type of the instance (will be inferred).</typeparam>
        /// <param name="inInstance">The instance for which to get the type name.</param>
        /// <returns>The name of the type, including any generic type parameters.</returns>
        public static string GetGenericAwareTypeName<T>(this T inInstance)
        {
            _ = Validate.ThrowIfNull(inInstance, "inInstance");
            Type theType = typeof(T);
            return theType.GetGenericAwareTypeName(theType.Name);
        }

        /// <summary>Determines whether this type is of the expected type.</summary>
        /// <typeparam name="T">The type being extended.</typeparam>
        /// <param name="inInstance">The instance to compare with the expected type.</param>
        /// <param name="inExpectedType">The expected type.</param>
        /// <returns>True if the types are same; otherwise false.</returns>
        public static bool IsInstanceOfType<T>(this T inInstance, Type inExpectedType)
        {
            _ = Validate.ThrowIfNull(inInstance, "inInstance");
            Type theType = typeof(T);
            return theType == inExpectedType;
        }

        /// <summary>Gets the name of the type, including any generic type parameters.</summary>
        /// <param name="inType">The type for which to get the name.</param>
        /// <param name="inTypeNameRoot">
        /// The preferred format of the non-generic part of the type name.
        /// </param>
        /// <returns>The name of the type, including any generic type parameters.</returns>
        private static string GetGenericAwareTypeName(this Type inType, string inTypeNameRoot)
        {
            string theTypeNameRoot = inTypeNameRoot;

            if (!inType.IsGenericType)
            {
                return theTypeNameRoot;
            }

            StringBuilder theTypeNameBuilder = new StringBuilder()
                .Append(theTypeNameRoot.Substring(0, theTypeNameRoot.IndexOf('`')))
                .Append('<')
                .Append(string.Join(
                    ", ",
                    GetTypeParameters(inType)))
                .Append('>');
            return theTypeNameBuilder.ToString();
        }

        /// <summary>Gets the specified type's generic type parameters as an array of strings.</summary>
        /// <param name="inType">The type for which to get the type parameters.</param>
        /// <returns>The generic type parameters as an array of strings.</returns>
        private static string[] GetTypeParameters(this Type inType)
            => inType.GetGenericArguments().Select(GetGenericAwareTypeName).ToArray();
    }
}
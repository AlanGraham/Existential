// <copyright file="ExtensionMethodsTypeTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Extension.Tests
{
    using System;
    using System.Collections.Generic;

    using Existential.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public static class ExtensionMethodsTypeTest
    {
        [Test]
        public static void GetGenericAwareFullTypeName_WithGenericType_Succeeds()
        {
            // Act
            string theResult = typeof(List<int>).GetGenericAwareFullTypeName();

            // Assert
            Assert.That(theResult, Is.EqualTo("System.Collections.Generic.List<Int32>"));
        }

        [Test]
        public static void GetGenericAwareFullTypeName_WithGenericTypeInstance_Succeeds()
        {
            // Act
            string theResult = new List<int>().GetGenericAwareFullTypeName();

            // Assert
            Assert.That(theResult, Is.EqualTo("System.Collections.Generic.List<Int32>"));
        }

        [Test]
        public static void GetGenericAwareFullTypeName_WithNullType_Throws() =>
            Assert.That(
                () =>
                {
                    // Act
                    _ = ((Type)null).GetGenericAwareFullTypeName();
                },
                Throws.ArgumentNullException
                    .With.Message.Contains("Value cannot be null")
                    .And.Message.Contains("inType"));

        [Test]
        public static void GetGenericAwareFullTypeName_WithNullInstance_Throws() =>
            Assert.That(
                () =>
                {
                    // Act
                    _ = ((List<int>)null).GetGenericAwareFullTypeName();
                },
                Throws.ArgumentNullException
                    .With.Message.Contains("Value cannot be null")
                    .And.Message.Contains("inInstance"));

        [Test]
        public static void GetGenericAwareTypeName_WithGenericType_Succeeds()
        {
            // Act
            string theResult = typeof(List<int>).GetGenericAwareTypeName();

            // Assert
            Assert.That(theResult, Is.EqualTo("List<Int32>"));
        }

        [Test]
        public static void GetGenericAwareTypeName_WithMultipleTypeParameters_Succeeds()
        {
            // Act
            string theResult = typeof(Dictionary<int, string>).GetGenericAwareTypeName();

            // Assert
            Assert.That(theResult, Is.EqualTo("Dictionary<Int32, String>"));
        }

        [Test]
        public static void GetGenericAwareTypeName_WithGenericTypeInstance_Succeeds()
        {
            // Act
            string theResult = new List<int>().GetGenericAwareTypeName();

            // Assert
            Assert.That(theResult, Is.EqualTo("List<Int32>"));
        }

        [Test]
        public static void IsInstanceOfType_WithMatchingType_Succeeds()
        {
            // Act
            bool theResult = new List<int>().IsInstanceOfType(typeof(List<int>));

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void IsInstanceOfType_WithDifferentType_Fails()
        {
            // Act
            bool theResult = new List<int>().IsInstanceOfType(typeof(List<short>));

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void IsInstanceOfType_WithNull_Throws() =>
            Assert.That(
                () =>
                {
                    _ = ((List<int>)null).IsInstanceOfType(typeof(List<int>));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains("Value cannot be null.")
                    .With.Message.Contains("inInstance"));

        [Test]
        public static void GetGenericAwareTypeName_WithNullType_Throws() =>
            Assert.That(
                () =>
                {
                    // Act
                    _ = ((Type)null).GetGenericAwareTypeName();
                },
                Throws.ArgumentNullException
                    .With.Message.Contains("Value cannot be null")
                    .And.Message.Contains("inType"));

        [Test]
        public static void GetGenericAwareTypeName_WithNullInstance_Throws() =>
            Assert.That(
                () =>
                {
                    // Act
                    _ = ((List<int>)null).GetGenericAwareTypeName();
                },
                Throws.ArgumentNullException
                    .With.Message.Contains("Value cannot be null")
                    .And.Message.Contains("inInstance"));
    }
}

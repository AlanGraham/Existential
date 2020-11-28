// <copyright file="HashCodeHelperTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

// ReSharper disable StyleCop.SA1600
namespace Existential.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using AssertPropertyThat = FsCheck.Prop;

    [TestFixture]
    public static class HashCodeHelperTest
    {
        [FsCheck.NUnit.Property]
        public static void HashCodeRepeatable() =>
            AssertPropertyThat.ForAll<int[]>(
                inNumber =>
                {
                    int theFirstHashCode = HashCodeHelper.CalculateHashCode(inNumber);
                    int theSecondHashCode = HashCodeHelper.CalculateHashCode(inNumber);
                    return theFirstHashCode == theSecondHashCode;
                });

        [FsCheck.NUnit.Property]
        public static void ReorderedValuesGiveDifferentHashCode() =>
            AssertPropertyThat.ForAll<int[]>(
                inNumber =>
                    HashCodeHelper.CalculateHashCode(inNumber) != HashCodeHelper.CalculateHashCode(inNumber.Reverse()));

        [Test]
        public static void HashCode_ForEmptyArray_IsZero()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode();

            // Assert
            Assert.That(theResult, Is.EqualTo(0));
        }

        [Test]
        public static void HashCode_ForExplicitEmptyArray_IsZero()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode(Array.Empty<object>());

            // Assert
            Assert.That(theResult, Is.EqualTo(0));
        }

        [Test]
        public static void HashCode_ForOtherEmptyCollection_IsNonZero()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode(new List<object>());

            // Assert
            Assert.That(theResult, Is.Not.EqualTo(0));
        }

        [Test]
        public static void HashCode_ForNull_IsZero()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode(null);

            // Assert
            Assert.That(theResult, Is.EqualTo(0));
        }

        [Test]
        public static void HashCode_WithAnInvalidValueInCollection_IsNonZero()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode(1, null);

            // Assert
            Assert.That(theResult, Is.Not.EqualTo(0));
        }

        [Test]
        public static void HashCode_WithAllInvalidValueInCollection_IsNonZero()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode(null, null);

            // Assert
            Assert.That(theResult, Is.Not.EqualTo(0));
        }

        [Test]
        public static void HashCode_WithSomeInvalidValueSInCollection_IsDifferentFromNoInvalid()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode(1, null);

            // Assert
            Assert.That(theResult, Is.Not.EqualTo(HashCodeHelper.CalculateHashCode(null, null)));
        }
    }
}
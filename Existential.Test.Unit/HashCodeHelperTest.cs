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
    using PropertyAttribute = FsCheck.NUnit.PropertyAttribute;

    [TestFixture]
    public static class HashCodeHelperTest
    {
        [Property]
        public static void HashCodeRepeatable() =>
            AssertPropertyThat.ForAll<int[]>(
                inNumber =>
                {
                    int theFirstHashCode = HashCodeHelper.CalculateHashCode(inNumber);
                    int theSecondHashCode = HashCodeHelper.CalculateHashCode(inNumber);
                    return theFirstHashCode == theSecondHashCode;
                });

        [Property]
        public static void ReorderedValuesGiveDifferentHashCode() =>
            AssertPropertyThat.ForAll<int[]>(
                (inNumber) =>
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
        public static void HashCode_HasExpectedValue()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode(1, 1, 1);

            // Assert
            Assert.That(theResult, Is.EqualTo(207392));
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
        public static void HashCode_WithAnInvalidValueInCollection_UsesDefault()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode(1, null);

            // Assert
            Assert.That(theResult, Is.EqualTo(9016));
        }

        [Test]
        public static void HashCode_WithAllInvalidValueInCollection_UsesDefault()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode(null, null);

            // Assert
            Assert.That(theResult, Is.EqualTo(8993));
        }
    }
}
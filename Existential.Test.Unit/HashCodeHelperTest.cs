// <copyright file="HashCodeHelperTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

// ReSharper disable StyleCop.SA1600
namespace Existential.Test
{
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
        public static void HashCode_WithNothing_IsZero()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode();

            // Assert
            Assert.That(theResult, Is.EqualTo(0));
        }

        [Test]
        public static void HashCode_HasExpectedValue()
        {
            // Act
            int theResult = HashCodeHelper.CalculateHashCode(1, 1, 1);

            // Assert
            Assert.That(theResult, Is.EqualTo(207392));
        }
    }
}
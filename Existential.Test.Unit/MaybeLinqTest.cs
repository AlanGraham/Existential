// <copyright file="MaybeLinqTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System.Globalization;

    using NUnit.Framework;

    [TestFixture]
    public static class MaybeLinqTest
    {
        [Test]
        public static void Select_WithValue_ReturnsCorrectValue()
        {
            // Arrange
            Maybe<string> theMaybe = "1";

            // Act
            Maybe<int> theResult = from value in theMaybe
                                   select value.Length;

            // Assert
            Assert.That(theResult.ValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void Select_WithNone_ReturnsNone()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Act
            Maybe<int> theResult = from value in theMaybe
                                   select value.Length;

            // Assert
            Assert.That(theResult.ValueOr(7), Is.EqualTo(7));
        }
    }
}

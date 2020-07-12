// <copyright file="MaybeLinqTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
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
            Maybe<int> theResult = from theText in theMaybe
                                   select theText.Length;

            // Assert
            Assert.That(theResult.ValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void Select_WithNone_ReturnsNone()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Act
            Maybe<int> theResult = from theText in theMaybe
                                   select theText.Length;

            // Assert
            Assert.That(theResult.ValueOr(7), Is.EqualTo(7));
        }

        [Test]
        public static void Where_WithValue_ReturnsCorrectValue()
        {
            // Arrange
            Maybe<string> theMaybe = "1";

            // Act
            Maybe<int> theResult = from theText in theMaybe
                                   select theText.Length;

            // Assert
            Assert.That(theResult.ValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void SelectMany_WithValues_ReturnsCorrectValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";
            Maybe<string> theOtherMaybe = "Another test string";

            // Act
            Maybe<(int, int)> theResult = from theText in theMaybe
                                          from theOtherText in theOtherMaybe
                                          select (Length1: theText.Length, Length2: theOtherText.Length);

            // Assert
            Assert.That(theResult.ValueOr((0, 0)), Is.EqualTo((13, 19)));
        }

        [Test]
        public static void SelectMany_WithANone_ReturnsNone()
        {
            // Arrange
            Maybe<string> theMaybe = null;
            Maybe<string> theOtherMaybe = "Another test string";

            // Act
            Maybe<(int, int)> theResult = from theText in theMaybe
                                          from theOtherText in theOtherMaybe
                                          select (Length1: theText.Length, Length2: theOtherText.Length);

            // Assert
            Assert.That(theResult.ValueOr((0, 0)), Is.EqualTo((0, 0)));
        }

        [Test]
        public static void SelectMany_WithADifferentNone_ReturnsNone()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";
            Maybe<string> theOtherMaybe = null;

            // Act
            Maybe<(int, int)> theResult = from theText in theMaybe
                                          from theOtherText in theOtherMaybe
                                          select (Length1: theText.Length, Length2: theOtherText.Length);

            // Assert
            Assert.That(theResult.ValueOr((0, 0)), Is.EqualTo((0, 0)));
        }

        [Test]
        public static void SelectMany_WithAllNone_ReturnsNone()
        {
            // Arrange
            Maybe<string> theMaybe = null;
            Maybe<string> theOtherMaybe = null;

            // Act
            Maybe<(int, int)> theResult = from theText in theMaybe
                                          from theOtherText in theOtherMaybe
                                          select (Length1: theText.Length, Length2: theOtherText.Length);

            // Assert
            Assert.That(theResult.ValueOr((0, 0)), Is.EqualTo((0, 0)));
        }
    }
}

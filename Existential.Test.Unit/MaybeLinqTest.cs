// <copyright file="MaybeLinqTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public static class MaybeLinqTest
    {
        [Test]
        public static void SelectLinq_WithValue_ReturnsCorrectValue()
        {
            // Arrange
            Maybe<string> theMaybe = "1";

            // Act
            Maybe<int> theResult = from theText in theMaybe
                                   select theText.Length;

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void SelectLinq_WithNone_ReturnsNone()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Act
            Maybe<int> theResult = from theText in theMaybe
                                   select theText.Length;

            // Assert
            Assert.That(theResult.GetValueOr(7), Is.EqualTo(7));
        }

        [Test]
        public static void SelectMethod_WithNoValue_ReturnsMaybeWithNoValue()
        {
            // Arrange
            Maybe<string> theNullString = null;

            // Act
            Maybe<int> theResult = theNullString.Select(inText => inText.Length);

            // Assert
            Assert.That(theResult.GetValueOr(17), Is.EqualTo(17));
        }

        [Test]
        public static void SelectMethod_WithValue_ReturnsExpectedValue()
        {
            // Arrange
            Maybe<string> theString = "Call me Maybe";

            // Act
            Maybe<int> theResult = theString.Select(inText => inText.Length);

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(13));
        }

        [Test]
        public static void SelectManyLinq_WithValues_ReturnsCorrectValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";
            Maybe<string> theOtherMaybe = "Another test string";

            // Act
            Maybe<(int, int)> theResult = from theText in theMaybe
                                          from theOtherText in theOtherMaybe
                                          select (Length1: theText.Length, Length2: theOtherText.Length);

            // Assert
            Assert.That(theResult.GetValueOr((0, 0)), Is.EqualTo((13, 19)));
        }

        [Test]
        public static void SelectManyLinq_WithANone_ReturnsNone()
        {
            // Arrange
            Maybe<string> theMaybe = null;
            Maybe<string> theOtherMaybe = "Another test string";

            // Act
            Maybe<(int, int)> theResult = from theText in theMaybe
                                          from theOtherText in theOtherMaybe
                                          select (Length1: theText.Length, Length2: theOtherText.Length);

            // Assert
            Assert.That(theResult.GetValueOr((0, 0)), Is.EqualTo((0, 0)));
        }

        [Test]
        public static void SelectManyLinq_WithADifferentNone_ReturnsNone()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";
            Maybe<string> theOtherMaybe = null;

            // Act
            Maybe<(int, int)> theResult = from theText in theMaybe
                                          from theOtherText in theOtherMaybe
                                          select (Length1: theText.Length, Length2: theOtherText.Length);

            // Assert
            Assert.That(theResult.GetValueOr((0, 0)), Is.EqualTo((0, 0)));
        }

        [Test]
        public static void SelectManyLinq_WithAllNone_ReturnsNone()
        {
            // Arrange
            Maybe<string> theMaybe = null;
            Maybe<string> theOtherMaybe = null;

            // Act
            Maybe<(int, int)> theResult = from theText in theMaybe
                                          from theOtherText in theOtherMaybe
                                          select (Length1: theText.Length, Length2: theOtherText.Length);

            // Assert
            Assert.That(theResult.GetValueOr((0, 0)), Is.EqualTo((0, 0)));
        }

        [Test]
        public static void SelectManyMethod_WithValues_ReturnsExpectedValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";
            Maybe<string> theOtherMaybe = "Another test string";

            // Act
            Maybe<(int, int)> theResult = theMaybe.SelectMany(
                inText2 => theOtherMaybe,
                (inText1, inText2) => (Length1: inText1.Length, Length2: inText2.Length));

            // Assert
            Assert.That(theResult.GetValueOr((0, 0)), Is.EqualTo((13, 19)));
        }

        [Test]
        public static void WhereLinq_WithValue_ReturnsCorrectValue()
        {
            // Arrange
            Maybe<string> theMaybe = "1";

            // Act
            Maybe<int> theResult = from theText in theMaybe
                                   where theText.Contains('1', StringComparison.Ordinal)
                                   select theText.Length;

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void WhereLinq_WithNoValue_ReturnsCorrectValue()
        {
            // Arrange
            Maybe<string> theMaybe = "2";

            // Act
            Maybe<int> theResult = from theText in theMaybe
                                   where theText.Contains('1', StringComparison.Ordinal)
                                   select theText.Length;

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(0));
        }

        [Test]
        public static void WhereMethod_WithValue_ReturnsCorrectValue()
        {
            // Arrange
            Maybe<string> theMaybe = "1";

            // Act
            Maybe<int> theResult = theMaybe
                .Where(inText => inText.Contains('1', StringComparison.Ordinal))
                .Select(inText => inText.Length);

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void WhereMethod_WithNoValue_ReturnsCorrectValue()
        {
            // Arrange
            Maybe<string> theMaybe = "2";

            // Act
            Maybe<int> theResult = theMaybe
                .Where(inText => inText.Contains('1', StringComparison.Ordinal))
                .Select(inText => inText.Length);

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(0));
        }
    }
}

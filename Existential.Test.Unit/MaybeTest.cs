// <copyright file="MaybeTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace GavinGreig.Test
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public static class MaybeTest
    {
        [Test]
        public static void Create_WithInt_HasExpectedValue()
        {
            // Act
            Maybe<int> theResult = 1;

            // Assert
            Assert.That(theResult.ValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void Create_WithInt_Succeeds()
        {
            // Act
            Maybe<int> theResult = 1;

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<int>>());
        }

        [Test]
        public static void Create_WithString_HasExpectedValue()
        {
            // Act
            Maybe<string> theResult = "Test";

            // Assert
            Assert.That(theResult.ValueOr(string.Empty), Is.EqualTo("Test"));
        }

        [Test]
        public static void Create_WithString_Succeeds()
        {
            // Act
            Maybe<string> theResult = "Test";

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<string>>());
        }

        [Test]
        public static void DoesNotEqualOperator_WithDifferentValue_ReturnsTrue()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = "A different string";

            // Act
            bool theResult = theFirstMaybe != theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void DoesNotEqualOperator_WithIdenticalValue_ReturnsFalse()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = "A test string";

            // Act
            bool theResult = theFirstMaybe != theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void DoesNotEqualOperator_WithNoneAndValue_ReturnsTrue()
        {
            // Arrange
            string theNullString = null;
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = theNullString;

            // Act
            bool theResult = theFirstMaybe != theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void DoesNotEqualOperator_WithNones_ReturnsFalse()
        {
            // Arrange
            string theNullString1 = null;
            string theNullString2 = null;
            Maybe<string> theFirstMaybe = theNullString1;
            Maybe<string> theSecondMaybe = theNullString2;

            // Act
            bool theResult = theFirstMaybe != theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void EqualsOperator_WithDifferentValue_ReturnsFalse()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = "A different string";

            // Act
            bool theResult = theFirstMaybe == theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void EqualsOperator_WithIdenticalValue_ReturnsTrue()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = "A test string";

            // Act
            bool theResult = theFirstMaybe == theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void EqualsOperator_WithNoneAndValue_ReturnsFalse()
        {
            // Arrange
            string theNullString = null;
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = theNullString;

            // Act
            bool theResult = theFirstMaybe == theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void EqualsOperator_WithNones_ReturnsTrue()
        {
            // Arrange
            string theNullString1 = null;
            string theNullString2 = null;
            Maybe<string> theFirstMaybe = theNullString1;
            Maybe<string> theSecondMaybe = theNullString2;

            // Act
            bool theResult = theFirstMaybe == theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void HashCode_WithInvalid_HasExpectedValue()
        {
            // Arrange
            string theNullString = null;

            // Act
            Maybe<string> theResult = theNullString;

            // Assert
            Assert.That(theResult.GetHashCode(), Is.EqualTo(0));
        }

        [Test]
        public static void HashCode_WithValid_HasExpectedValue()
        {
            // Arrange
            string theValidString = "A valid string";

            // Act
            Maybe<string> theResult = theValidString;

            // Assert
            Assert.That(
                theResult.GetHashCode(),
                Is.EqualTo(theValidString.GetHashCode(StringComparison.Ordinal)));
        }

        [Test]
        public static void Some_WithInvalid_Throws()
        {
            // Arrange
            string theNullString = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    var theResult = Maybe.Some(theNullString);
                },
                Throws.ArgumentNullException);
        }

        [Test]
        public static void Some_WithMaybe_IsMaybe()
        {
            // Arrange
            string theValidString = "A valid string";
            Maybe<string> theMaybe = theValidString;

            // Act
            var theResult = Maybe.Some(theMaybe);

            // Assert
            Assert.That(theResult.ValueOr("A default string"), Is.EqualTo("A valid string"));
        }

        [Test]
        public static void Some_WithNone_IsNone()
        {
            // Arrange
            string theInvalidString = null;
            Maybe<string> theNone = theInvalidString;

            // Act
            var theResult = Maybe.Some(theNone);

            // Assert
            Assert.That(theResult.ValueOr("A default string"), Is.EqualTo("A default string"));
        }

        [Test]
        public static void Some_WithValid_HasExpectedValue()
        {
            // Arrange
            string theValidString = "A valid string";

            // Act
            var theResult = Maybe.Some(theValidString);

            // Assert
            Assert.That(theResult.ValueOr("A default string"), Is.EqualTo("A valid string"));
        }

        [Test]
        public static void ValueOr_WithNull_HasExpectedValue()
        {
            // Arrange
            string theNullString = null;

            // Act
            Maybe<string> theResult = theNullString;

            // Assert
            Assert.That(theResult.ValueOr("A default string"), Is.EqualTo("A default string"));
        }
    }
}
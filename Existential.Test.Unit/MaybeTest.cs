﻿// <copyright file="MaybeTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public static class MaybeTest
    {
        [Test]
        public static void Bind_WithNoValue_ReturnsMaybeWithNoValue()
        {
            // Arrange
            Maybe<string> theNullString = null;

            // Act
            Maybe<int> theResult = theNullString.Bind(x =>
            {
                Maybe<int> theMaybe = x.Length;
                return theMaybe;
            });

            // Assert
            Assert.That(theResult.ValueOr(12), Is.EqualTo(12));
        }

        [Test]
        public static void Bind_WithValue_ReturnsExpectedValue()
        {
            // Arrange
            Maybe<string> theString = "Call me Maybe";

            // Act
            Maybe<int> theResult = theString.Bind(x =>
            {
                Maybe<int> theMaybe = x.Length;
                return theMaybe;
            });

            // Assert
            Assert.That(theResult.ValueOr(0), Is.EqualTo(13));
        }

        [Test]
        public static void DefaultCreate_WithInt_HasNoValue()
        {
            // Act
            var theResult = default(Maybe<int>);

            // Assert
            Assert.That(theResult.ValueOr(1), Is.EqualTo(1));
        }

        [Test]
        public static void DefaultCreate_WithInt_Succeeds()
        {
            // Act
            var theResult = default(Maybe<int>);

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<int>>());
        }

        [Test]
        public static void DefaultCreate_WithString_HasNoValue()
        {
            // Act
            var theResult = default(Maybe<string>);

            // Assert
            Assert.That(theResult.ValueOr("Default value"), Is.EqualTo("Default value"));
        }

        [Test]
        public static void DefaultCreate_WithString_Succeeds()
        {
            // Act
            var theResult = default(Maybe<string>);

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
        public static void ImplicitCreate_WithInt_HasExpectedValue()
        {
            // Act
            Maybe<int> theResult = 1;

            // Assert
            Assert.That(theResult.ValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void ImplicitCreate_WithInt_Succeeds()
        {
            // Act
            Maybe<int> theResult = 1;

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<int>>());
        }

        [Test]
        public static void ImplicitCreate_WithString_HasExpectedValue()
        {
            // Act
            Maybe<string> theResult = "Test";

            // Assert
            Assert.That(theResult.ValueOr(string.Empty), Is.EqualTo("Test"));
        }

        [Test]
        public static void ImplicitCreate_WithString_Succeeds()
        {
            // Act
            Maybe<string> theResult = "Test";

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<string>>());
        }

        [Test]
        public static void Some_WithInt_HasExpectedValue()
        {
            // Act
            var theResult = Maybe.Some(1);

            // Assert
            Assert.That(theResult.ValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void Some_WithInt_Succeeds()
        {
            // Act
            var theResult = Maybe.Some(1);

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<int>>());
        }

        [Test]
        public static void Some_WithString_HasExpectedValue()
        {
            // Act
            var theResult = Maybe.Some("Test");

            // Assert
            Assert.That(theResult.ValueOr(string.Empty), Is.EqualTo("Test"));
        }

        [Test]
        public static void Some_WithString_Succeeds()
        {
            // Act
            var theResult = Maybe.Some("Test");

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<string>>());
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
        public static void Some_WithNull_IsNone()
        {
            // Arrange
            string theNullString = null;

            // Act
            var theResult = Maybe.Some(theNullString);

            // Assert
            Assert.That(theResult.ValueOr("A default string"), Is.EqualTo("A default string"));
        }

        [Test]
        public static void Map_WithNoValue_ReturnsMaybeWithNoValue()
        {
            // Arrange
            Maybe<string> theNullString = null;

            // Act
            Maybe<int> theResult = theNullString.Map(x => x.Length);

            // Assert
            Assert.That(theResult.ValueOr(17), Is.EqualTo(17));
        }

        [Test]
        public static void Map_WithValue_ReturnsExpectedValue()
        {
            // Arrange
            Maybe<string> theString = "Call me Maybe";

            // Act
            Maybe<int> theResult = theString.Map(x => x.Length);

            // Assert
            Assert.That(theResult.ValueOr(0), Is.EqualTo(13));
        }

        [Test]
        public static void ToMaybe_WithMaybe_IsMaybe()
        {
            // Arrange
            var theDefaultMaybe = default(Maybe<string>);
            var theMaybeWithValue = theDefaultMaybe.ToMaybe("A test string");

            // Act
            var theResult = theDefaultMaybe.ToMaybe(theMaybeWithValue);

            // Assert
            Assert.That(theResult, Is.EqualTo(theMaybeWithValue));
        }

        [Test]
        public static void ToMaybe_WithNone_IsNone()
        {
            // Arrange
            var theDefaultMaybe = default(Maybe<string>);

            // Act
            var theResult = theDefaultMaybe.ToMaybe(theDefaultMaybe);

            // Assert
            Assert.That(theResult, Is.EqualTo(theDefaultMaybe));
        }

        [Test]
        public static void ToMaybe_WithNull_IsNone()
        {
            // Arrange
            string theNullString = null;
            var theDefaultMaybe = default(Maybe<string>);

            // Act
            var theResult = theDefaultMaybe.ToMaybe(theNullString);

            // Assert
            Assert.That(theResult, Is.EqualTo(theDefaultMaybe));
        }

        [Test]
        public static void ToMaybe_WithValue_ContainsValue()
        {
            // Arrange
            string theTestValue = "A test string";
            var theDefaultMaybe = default(Maybe<string>);

            // Act
            var theResult = theDefaultMaybe.ToMaybe(theTestValue);

            // Assert
            Assert.That(theResult.ValueOr("A default string"), Is.EqualTo(theTestValue));
        }

        [Test]
        public static void ToString_WithNullString_ReturnsStringOfDefault()
        {
            // Arrange
            string theNullString = null;
            Maybe<string> theMaybe = theNullString;

            // Act
            string theResult = theMaybe.ToString();

            // Assert
            Assert.That(theResult, Is.EqualTo(string.Empty));
        }

        [Test]
        public static void ToString_WithValue_ReturnsStringOfValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.ToString();

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void TryGetValue_WithNoValue_HasDefaultOutValue()
        {
            // Arrange
            string theNullString = null;
            Maybe<string> theTestValue = theNullString;

            // Act
            _ = theTestValue.TryGetValue(out string theOutValue);

            // Assert
            Assert.That(theOutValue, Is.EqualTo(default(string)));
        }

        [Test]
        public static void TryGetValue_WithNoValue_ReturnsFalse()
        {
            // Arrange
            string theNullString = null;
            Maybe<string> theTestValue = theNullString;

            // Act
            bool theResult = theTestValue.TryGetValue(out _);

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void TryGetValue_WithValue_HasCorrectOutValue()
        {
            // Arrange
            Maybe<string> theTestValue = "A test string";

            // Act
            _ = theTestValue.TryGetValue(out string theOutValue);

            // Assert
            Assert.That(theOutValue, Is.EqualTo("A test string"));
        }

        [Test]
        public static void TryGetValue_WithValue_ReturnsTrue()
        {
            // Arrange
            Maybe<string> theTestValue = "A test string";

            // Act
            bool theResult = theTestValue.TryGetValue(out _);

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void ValueOr_WithNull_ReturnsDefaultValue()
        {
            // Arrange
            string theNullString = null;
            Maybe<string> theMaybe = theNullString;

            // Act
            string theResult = theMaybe.ValueOr("A default string");

            // Assert
            Assert.That(theResult, Is.EqualTo("A default string"));
        }

        [Test]
        public static void ValueOr_WithNullAndFactory_ReturnsFactoryValue()
        {
            // Arrange
            string theNullString = null;
            Maybe<string> theMaybe = theNullString;

            // Act
            string theResult = theMaybe.ValueOr(() => "A factory string");

            // Assert
            Assert.That(theResult, Is.EqualTo("A factory string"));
        }

        [Test]
        public static void ValueOr_WithValue_ReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.ValueOr("A default string");

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void ValueOr_WithValueAndFactory_ReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.ValueOr(() => "A factory string");

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void ValueOrMaybe_WithNoneAndAlternativeMaybe_ReturnsAlternativeMaybe()
        {
            // Arrange
            Maybe<string> theDefault = "A different default string";
            string theNullString = null;
            Maybe<string> theMaybe = theNullString;

            // Act
            Maybe<string> theResult = theMaybe.ValueOrMaybe(theDefault);

            // Assert
            Assert.That(theResult, Is.EqualTo(theDefault));
        }

        [Test]
        public static void ValueOrMaybe_WithNoneAndMaybeFactory_ReturnsFactoryValue()
        {
            // Arrange
            Maybe<string> theDefault = "A factory string";
            string theNullString = null;
            Maybe<string> theMaybe = theNullString;

            // Act
            Maybe<string> theResult = theMaybe.ValueOrMaybe(() => theDefault);

            // Assert
            Assert.That(theResult, Is.EqualTo(theDefault));
        }

        [Test]
        public static void ValueOrMaybe_WithValueAndAlternativeMaybe_ReturnsValue()
        {
            // Arrange
            Maybe<string> theDefault = "A different default string";
            Maybe<string> theMaybe = "A test string";

            // Act
            Maybe<string> theResult = theMaybe.ValueOrMaybe(theDefault);

            // Assert
            Assert.That(theResult, Is.EqualTo(theMaybe));
        }

        [Test]
        public static void ValueOrMaybe_WithValueAndMaybeFactory_ReturnsValue()
        {
            // Arrange
            Maybe<string> theDefault = "A factory string";
            Maybe<string> theMaybe = "A test string";

            // Act
            Maybe<string> theResult = theMaybe.ValueOrMaybe(() => theDefault);

            // Assert
            Assert.That(theResult, Is.EqualTo(theMaybe));
        }

        [Test]
        public static void ValueOrThrow_WithNone_Throws()
        {
            // Arrange
            string theNullString = null;
            Maybe<string> theMaybe = theNullString;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    _ = theMaybe.ValueOrThrow("An error message.");
                },
                Throws.InvalidOperationException.With.Message.Contains("An error message"));
        }

        [Test]
        public static void ValueOrThrow_WithValue_ReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.ValueOrThrow("An error message.");

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void StringValueOrEmpty_WithValue_ReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.ValueOrEmpty();

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void StringValueOrEmpty_WithNone_ReturnsEmpty()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Act
            string theResult = theMaybe.ValueOrEmpty();

            // Assert
            Assert.That(theResult, Is.EqualTo(string.Empty));
        }

        [Test]
        public static void ListOfStringValueOrEmpty_WithValue_ReturnsValue()
        {
            // Arrange
            Maybe<List<string>> theMaybe = new List<string>
            {
                "A test string",
            };

            // Act
            List<string> theResult = theMaybe.ValueOrEmpty();

            // Assert
            Assert.That(theResult, Has.Count.EqualTo(1).And.Contains("A test string"));
        }

        [Test]
        public static void ListOfStringValueOrEmpty_WithNone_ReturnsEmpty()
        {
            // Arrange
            Maybe<List<string>> theMaybe = null;

            // Act
            List<string> theResult = theMaybe.ValueOrEmpty();

            // Assert
            Assert.That(theResult, Has.Count.EqualTo(0));
        }

        [Test]
        public static void ArrayOfStringValueOrEmpty_WithValue_ReturnsValue()
        {
            // Arrange
            Maybe<string[]> theMaybe = new string[]
            {
                "A test string",
            };

            // Act
            string[] theResult = theMaybe.ValueOrEmpty();

            // Assert
            Assert.That(theResult, Has.Length.EqualTo(1).And.Contains("A test string"));
        }

        [Test]
        public static void ArrayOfStringValueOrEmpty_WithNone_ReturnsEmpty()
        {
            // Arrange
            Maybe<string[]> theMaybe = null;

            // Act
            string[] theResult = theMaybe.ValueOrEmpty();

            // Assert
            Assert.That(theResult, Has.Length.EqualTo(0));
        }

        [Test]
        public static void ThatExist_WithOneValue_ReturnsExpectedResult()
        {
            // Arrange
            var theMaybeCollection = new List<Maybe<string>>
            {
                null,
                "Test string",
            };

            // Act
            IEnumerable<string> theResult = theMaybeCollection.ThatExist();

            // Assert
            Assert.That(Enumerable.Count(theResult), Is.EqualTo(1));
            Assert.That(theResult, Contains.Item("Test string"));
        }

        [Test]
        public static void ThatExist_WithMultipleValues_ReturnsExpectedResult()
        {
            // Arrange
            var theMaybeCollection = new List<Maybe<string>>
            {
                null,
                "Test string",
                null,
                "Test string 2",
                null,
                null,
            };

            // Act
            IEnumerable<string> theResult = theMaybeCollection.ThatExist();

            // Assert
            Assert.That(Enumerable.Count(theResult), Is.EqualTo(2));
            Assert.That(theResult, Contains.Item("Test string"));
            Assert.That(theResult, Contains.Item("Test string 2"));
        }

        [Test]
        public static void ThatExist_WithNoValues_ReturnsEmptyCollection()
        {
            // Arrange
            var theMaybeCollection = new List<Maybe<string>>
            {
                null,
                null,
                null,
            };

            // Act
            IEnumerable<string> theResult = theMaybeCollection.ThatExist();

            // Assert
            Assert.That(Enumerable.Count(theResult), Is.EqualTo(0));
        }
    }
}
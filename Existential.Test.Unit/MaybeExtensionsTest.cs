// <copyright file="MaybeExtensionsTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    public static class MaybeExtensionsTest
    {
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
        public static void GuidValueOrEmpty_WithValue_ReturnsValue()
        {
            // Arrange
            Maybe<Guid> theMaybe = new Guid("{B583114D-9632-4B9B-A70A-F05B41E2FCA3}");

            // Act
            Guid theResult = theMaybe.ValueOrEmpty();

            // Assert
            Assert.That(theResult.ToString(), Is.EqualTo("b583114d-9632-4b9b-a70a-f05b41e2fca3"));
        }

        /* TODO: Doesn't make sense for Guid, but revisit for Guid?.
        [Test]
        public static void GuidValueOrEmpty_WithNoValue_ReturnsEmptyGuid()
        {
            // Arrange
            Maybe<Guid> theMaybe = Maybe.Some<Guid>(null);

            // Act
            Guid theResult = theMaybe.ValueOrEmpty();

            // Assert
            Assert.That(theResult, Is.EqualTo(Guid.Empty));
        }
        */

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
            Maybe<string[]> theMaybe = new[]
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
            // Specifying ToList() ensures the result is only evaluated once.
            IEnumerable<string> theResult = theMaybeCollection.ThatExist().ToList();

            // Assert
            Assert.That(theResult.Count(), Is.EqualTo(1));
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
            // Specifying ToList() ensures the result is only evaluated once.
            IEnumerable<string> theResult = theMaybeCollection.ThatExist().ToList();

            // Assert
            Assert.That(theResult.Count(), Is.EqualTo(2));
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
            Assert.That(theResult.Count(), Is.EqualTo(0));
        }

        [Test]
        public static void WhereAllExist_IsTrue_ReturnsOriginalCollection()
        {
            // Arrange
            var theCollection = new List<Maybe<string>>
            {
                "Test string 1",
                "Test string 2",
            };

            // Act
            Maybe<IEnumerable<string>> theResult = theCollection.WhereAllExist();

            // Assert
            Assert.That(theResult.ValueOr(() => null), Contains.Item("Test string 1"));
            Assert.That(theResult.ValueOr(() => null), Contains.Item("Test string 2"));
        }

        [Test]
        public static void WhereAllExist_IsNotTrue_ReturnsNoCollection()
        {
            // Arrange
            var theCollection = new List<Maybe<string>>
            {
                "Test string 1",
                null,
            };

            // Act
            Maybe<IEnumerable<string>> theResult = theCollection.WhereAllExist();

            // Assert
            Assert.That(theResult.ValueOr(() => null), Is.EqualTo(null));
        }

        [Test]
        public static void FirstOrNone_WithNullCollection_ReturnsNoValue()
        {
            // Act
            // Need to test extension method on a null.
            Maybe<string> theResult = ((List<string>)null).MaybeFirst();

            // Assert
            Assert.That(theResult.ValueOrEmpty(), Is.EqualTo(string.Empty));
        }

        [Test]
        public static void FirstOrNone_WithNullFirst_ReturnsNoValue()
        {
            // Arrange
            var theCollection = new List<string>
            {
                null,
                "A test string",
            };

            // Act
            Maybe<string> theResult = theCollection.MaybeFirst();

            // Assert
            Assert.That(theResult.ValueOrEmpty(), Is.EqualTo(string.Empty));
        }

        [Test]
        public static void FirstOrNone_WithValueFirst_ReturnsExpectedValue()
        {
            // Arrange
            var theCollection = new List<string>
            {
                "A test string",
                null,
            };

            // Act
            Maybe<string> theResult = theCollection.MaybeFirst();

            // Assert
            Assert.That(theResult.ValueOrEmpty(), Is.EqualTo("A test string"));
        }

        [Test]
        public static void FirstThatExists_WithNullCollection_ReturnsNoValue()
        {
            // Act
            // Need to test extension method on a null.
            Maybe<string> theResult = ((List<string>)null).MaybeFirstThatExists();

            // Assert
            Assert.That(theResult.ValueOrEmpty(), Is.EqualTo(string.Empty));
        }

        [Test]
        public static void FirstThatExists_WithNullFirst_ReturnsNoValue()
        {
            // Arrange
            var theCollection = new List<string>
            {
                null,
                "A test string",
            };

            // Act
            Maybe<string> theResult = theCollection.MaybeFirstThatExists();

            // Assert
            Assert.That(theResult.ValueOrEmpty(), Is.EqualTo("A test string"));
        }

        [Test]
        public static void FirstThatExists_WithValueFirst_ReturnsExpectedValue()
        {
            // Arrange
            var theCollection = new List<string>
            {
                "A test string",
                null,
            };

            // Act
            Maybe<string> theResult = theCollection.MaybeFirstThatExists();

            // Assert
            Assert.That(theResult.ValueOrEmpty(), Is.EqualTo("A test string"));
        }

        [Test]
        public static void FirstWhere_WithNullCollection_ReturnsNoValue()
        {
            // Act
            // Need to test extension method on a null.
            Maybe<string> theResult = ((List<string>)null).MaybeFirstWhere((inText) => true);

            // Assert
            Assert.That(theResult.ValueOrEmpty(), Is.EqualTo(string.Empty));
        }

        [Test]
        public static void FirstWhere_WithNoMatch_ReturnsNoValue()
        {
            // Arrange
            var theCollection = new List<string>
            {
                null,
                "A test string",
            };

            // Act
            Maybe<string> theResult = theCollection.MaybeFirstWhere(
                (inText) => inText != null && inText.Contains("elephant", StringComparison.Ordinal));

            // Assert
            Assert.That(theResult.ValueOrEmpty(), Is.EqualTo(string.Empty));
        }

        [Test]
        public static void FirstWhere_WithNullFirst_ReturnsCorrectValue()
        {
            // Arrange
            var theCollection = new List<string>
            {
                null,
                "A test string",
            };

            // Act
            Maybe<string> theResult = theCollection.MaybeFirstWhere(
                (inText) => inText != null && inText.Contains("test", StringComparison.Ordinal));

            // Assert
            Assert.That(theResult.ValueOrEmpty(), Is.EqualTo("A test string"));
        }

        [Test]
        public static void FirstWhere_WithValueFirst_ReturnsExpectedValue()
        {
            // Arrange
            var theCollection = new List<string>
            {
                "A test string",
                null,
            };

            // Act
            Maybe<string> theResult = theCollection.MaybeFirstWhere(
                (inText) => inText.Contains("test", StringComparison.Ordinal));

            // Assert
            Assert.That(theResult.ValueOrEmpty(), Is.EqualTo("A test string"));
        }
    }
}
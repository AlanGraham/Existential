// <copyright file="ParameterValidationTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace GavinGreig.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using FsCheck;

    using GavinGreig.Extensions;

    using NUnit.Framework;

    // using AssertPropertyThat = FsCheck.Prop;
    using PropertyAttribute = FsCheck.NUnit.PropertyAttribute;

    /// <summary>Unit tests for parameter validation methods.</summary>
    [TestFixture]
    public static class ParameterValidationTest
    {
        /// <summary>
        /// Checks that validation fails when an IEnumerable collection is non-null but empty.
        /// </summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenIEnumerableIsEmpty()
        {
            // Arrange
            string theExpectedMessage = "The collection \"theCollection\" is empty";
            IEnumerable theCollection = new List<int> { };

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfCollectionNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>Checks that validation fails when an IEnumerable collection is null.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenIEnumerableIsNull()
        {
            // Arrange
            string theExpectedMessage = "The collection \"theCollection\" is null";
            IEnumerable theCollection = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfCollectionNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>
        /// Checks that validation fails when an IEnumerable{T} collection is non-null but empty.
        /// </summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenIEnumerableTIsEmpty()
        {
            // Arrange
            string theExpectedMessage = "The collection \"theCollection\" is empty";
            IEnumerable<int> theCollection = new List<int> { };

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfCollectionNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>Checks that validation fails when an IEnumerable{T} collection is null.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenIENumerableTIsNull()
        {
            // Arrange
            string theExpectedMessage = "The collection \"theCollection\" is null";
            IEnumerable<int> theCollection = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfCollectionNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>Checks that validation fails when a List{T} collection is non-null but empty.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenListTIsEmpty()
        {
            // Arrange
            string theExpectedMessage = "The collection \"theCollection\" is empty";
            var theCollection = new List<int> { };

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfCollectionNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>Checks that validation fails when a List{T} collection is null.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenListTIsNull()
        {
            // Arrange
            string theExpectedMessage = "The collection \"theCollection\" is null";
            List<int> theCollection = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfCollectionNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>Checks that validation succeeds when an IEnumerable collection has content.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_ReturnsCorrectType_WhenIEnumerableIsPopulated()
        {
            // Arrange
            IEnumerable theCollection = new List<int> { 1 };

            // Act
            IEnumerable theResult = Validation.ThrowIfCollectionNullOrEmpty(theCollection, nameof(theCollection));

            // Assert
            Assert.AreSame(theResult, theCollection);
        }

        /// <summary>Checks that validation succeeds when an IEnumerable{T} collection has content.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_ReturnsCorrectType_WhenIEnumerableTIsPopulated()
        {
            // Arrange
            IEnumerable<int> theCollection = new List<int> { 1 };

            // Act
            IEnumerable<int> theResult = Validation.ThrowIfCollectionNullOrEmpty(theCollection, nameof(theCollection));

            // Assert
            Assert.AreSame(theResult, theCollection);
        }

        /// <summary>Checks that validation succeeds when a List{T} collection has content.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_ReturnsCorrectType_WhenListTIsPopulated()
        {
            // Arrange
            var theCollection = new List<int> { 1 };

            // Act
            List<int> theResult = Validation.ThrowIfCollectionNullOrEmpty(theCollection, nameof(theCollection));

            // Assert
            Assert.AreSame(theResult, theCollection);
        }

        /// <summary>Checks that validation fails when a GUID is empty.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureGuidNotEmpty_WithEmptyGuid_ThrowsException()
        {
            // Arrange
            Guid theGuid = Guid.Empty;
            string theExpectedMessage = "The GUID has the empty value, {00000000-0000-0000-0000-000000000000}.";

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfEmptyGuid(theGuid, nameof(theGuid));
                },
                Throws.ArgumentException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theGuid)));
        }

        /// <summary>Checks that validation succeeds when a GUID is not-empty.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureGuidNotEmpty_WithValidGuid_ReturnsValue()
        {
            // Arrange
            var theGuid = new Guid("1d572f1a-c8e9-4ff8-8ec6-9e585aa64e74");

            // Act
            Guid theResult = Validation.ThrowIfEmptyGuid(theGuid, nameof(theGuid));

            // Assert
            Assert.AreEqual(theResult, theGuid);
        }

        [Property]
        public static Property EnsureNotNull_WithNonWhiteSpaceString_ReturnsValue(NonEmptyString x, bool y)
        {
            Func<bool> theProperty = () => Validation.ThrowIfStringNullOrEmpty(x.Get, nameof(x), y) == x.Get;
            return theProperty.When(!string.IsNullOrWhiteSpace(x.Get));
        }

        /// <summary>Checks that validation fails when a string is null.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureNotNull_WithNullData_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "Value cannot be null.";
            string theNullText = null; // Need to use a variable, as type can't be inferred from a raw null.

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfNull(theNullText, nameof(theNullText));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theNullText)));
        }

        [Test]
        public static void EnsureOfType_WithDerivedType_ReturnsValue()
        {
            // Arrange
            var theParameter = new DerivedType();

            // Act
            ExpectedType theResult = Validation.ThrowIfNotOfType<ExpectedType>(theParameter, nameof(theParameter));

            // Assert
            Assert.AreSame(theResult, theParameter);
        }

        [Test]
        public static void EnsureOfType_WithExpectedType_ReturnsValue()
        {
            // Arrange
            var theParameter = new ExpectedType();

            // Act
            ExpectedType theResult = Validation.ThrowIfNotOfType<ExpectedType>(theParameter, nameof(theParameter));

            // Assert
            Assert.AreSame(theResult, theParameter);
        }

        [Test]
        public static void EnsureOfType_WithNull_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "Value cannot be null.";
            ExpectedType theNullReference = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfNotOfType<ExpectedType>(theNullReference, nameof(theNullReference));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theNullReference)));
        }

        [Test]
        public static void EnsureOfType_WithOtherType_ThrowsException()
        {
            // Arrange
            var theUnexpectedType = new UnexpectedType();
            string theExpectedMessage = "An argument of type " +
                theUnexpectedType.GetGenericAwareTypeName() +
                " was provided (expected " +
                new ExpectedType().GetGenericAwareTypeName() +
                ").";

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfNotOfType<ExpectedType>(theUnexpectedType, nameof(theUnexpectedType));
                },
                Throws.Exception.TypeOf<ArgumentTypeException>()
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theUnexpectedType)));
        }

        [Test]
        public static void EnsureStringNotNullOrEmpty_WithEmptyString_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "String cannot be empty.";
            string theEmptyString = string.Empty;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfStringNullOrEmpty(theEmptyString, nameof(theEmptyString));
                },
                Throws.ArgumentException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theEmptyString)));
        }

        [Test]
        public static void EnsureStringNotNullOrEmpty_WithNull_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "Value cannot be null.";
            string theNullString = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfStringNullOrEmpty(theNullString, nameof(theNullString));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theNullString)));
        }

        [Test]
        public static void EnsureStringNotNullOrEmpty_WithString_ReturnsValue()
        {
            // Arrange
            const string ExpectedValue = "Test";

            // Act
            string theResult = Validation.ThrowIfStringNullOrEmpty(ExpectedValue, nameof(ExpectedValue));

            // Assert
            Assert.AreSame(theResult, ExpectedValue);
        }

        [Test]
        public static void EnsureStringNotNullOrEmpty_WithWhiteSpaceStringAndNoTrim_ReturnsValue()
        {
            string theWhiteSpaceText = "    ";

            // Act
            string theResult = Validation.ThrowIfStringNullOrEmpty(
                theWhiteSpaceText,
                nameof(theWhiteSpaceText),
                trim: false);

            // Assert
            Assert.AreSame(theResult, theWhiteSpaceText);
        }

        public static void EnsureStringNotNullOrEmpty_WithWhiteSpaceStringAndTrim_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "Value cannot be empty.";
            string theWhiteSpaceText = "    ";

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    _ = Validation.ThrowIfStringNullOrEmpty(theWhiteSpaceText, nameof(theWhiteSpaceText), trim: true);
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theWhiteSpaceText)));
        }

        public static void EnsureStringNotNullOrEmpty_WithWhiteSpaceStringAndUnspecifiedTrim_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "Value cannot be empty.";
            string theWhiteSpaceText = "    ";

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validation.ThrowIfStringNullOrEmpty(theWhiteSpaceText, nameof(theWhiteSpaceText));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theWhiteSpaceText)));
        }

        private class DerivedType : ExpectedType
        {
        }

        private class ExpectedType
        {
        }

        private class UnexpectedType
        {
        }
    }
}
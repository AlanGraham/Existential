// <copyright file="ValidateTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Existential.Extensions;

    using FsCheck;

    using NUnit.Framework;

    // using AssertPropertyThat = FsCheck.Prop;
    using PropertyAttribute = FsCheck.NUnit.PropertyAttribute;

    /// <summary>Unit tests for parameter validation methods.</summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Tests don't need doc comments.")]
    public static class ValidateTest
    {
        /// <summary>
        /// Checks that validation fails when an IEnumerable collection is non-null but empty.
        /// </summary>
        [Test]
        public static void ThrowIfNullOrEmpty_Fails_WhenIEnumerableIsEmpty()
        {
            // Arrange
            const string ExpectedMessage = "The collection \"theCollection\" is empty";
            IEnumerable theCollection = new List<int>();

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentException
                    .With.Message.Contains(ExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>Checks that validation fails when an IEnumerable collection is null.</summary>
        [Test]
        public static void ThrowIfNullOrEmpty_Fails_WhenIEnumerableIsNull()
        {
            // Arrange
            const string ExpectedMessage = "The collection \"theCollection\" is null";
            IEnumerable theCollection = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(ExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>
        /// Checks that validation fails when an IEnumerable{T} collection is non-null but empty.
        /// </summary>
        [Test]
        public static void ThrowIfNullOrEmpty_Fails_WhenIEnumerableTIsEmpty()
        {
            // Arrange
            const string ExpectedMessage = "The collection \"theCollection\" is empty";
            IEnumerable<int> theCollection = new List<int>();

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentException
                    .With.Message.Contains(ExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>Checks that validation fails when an IEnumerable{T} collection is null.</summary>
        [Test]
        public static void ThrowIfNullOrEmpty_Fails_WhenIENumerableTIsNull()
        {
            // Arrange
            const string ExpectedMessage = "The collection \"theCollection\" is null";
            IEnumerable<int> theCollection = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(ExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>Checks that validation fails when a List{T} collection is non-null but empty.</summary>
        [Test]
        public static void ThrowIfNullOrEmpty_Fails_WhenListTIsEmpty()
        {
            // Arrange
            const string ExpectedMessage = "The collection \"theCollection\" is empty";
            var theCollection = new List<int>();

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentException
                    .With.Message.Contains(ExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>Checks that validation fails when a List{T} collection is null.</summary>
        [Test]
        public static void ThrowIfNullOrEmpty_Fails_WhenListTIsNull()
        {
            // Arrange
            const string ExpectedMessage = "The collection \"theCollection\" is null";
            List<int> theCollection = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrEmpty(theCollection, nameof(theCollection));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(ExpectedMessage)
                    .And.Message.Contains(nameof(theCollection)));
        }

        /// <summary>Checks that validation succeeds when an IEnumerable collection has content.</summary>
        [Test]
        public static void ThrowIfNullOrEmpty_ReturnsCorrectType_WhenIEnumerableIsPopulated()
        {
            // Arrange
            IEnumerable theCollection = new List<int> { 1 };

            // Act
            IEnumerable theResult = Validate.ThrowIfNullOrEmpty(theCollection, nameof(theCollection));

            // Assert
            Assert.AreSame(theResult, theCollection);
        }

        /// <summary>Checks that validation succeeds when an IEnumerable{T} collection has content.</summary>
        [Test]
        public static void ThrowIfNullOrEmpty_ReturnsCorrectType_WhenIEnumerableTIsPopulated()
        {
            // Arrange
            IEnumerable<int> theCollection = new List<int> { 1 };

            // Act
            IEnumerable<int> theResult = Validate.ThrowIfNullOrEmpty(theCollection, nameof(theCollection));

            // Assert
            Assert.AreSame(theResult, theCollection);
        }

        /// <summary>Checks that validation succeeds when a List{T} collection has content.</summary>
        [Test]
        public static void ThrowIfNullOrEmpty_ReturnsCorrectType_WhenListTIsPopulated()
        {
            // Arrange
            var theCollection = new List<int> { 1 };

            // Act
            List<int> theResult = Validate.ThrowIfNullOrEmpty(theCollection, nameof(theCollection));

            // Assert
            Assert.AreSame(theResult, theCollection);
        }

        /// <summary>Checks that validation fails when a GUID is empty.</summary>
        [Test]
        public static void ThrowIfEmptyGuid_WithEmptyGuid_ThrowsException()
        {
            // Arrange
            Guid theGuid = Guid.Empty;
            string theExpectedMessage = "The GUID has the empty value, {00000000-0000-0000-0000-000000000000}.";

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfEmptyGuid(theGuid, nameof(theGuid));
                },
                Throws.ArgumentException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theGuid)));
        }

        /// <summary>Checks that validation succeeds when a GUID is not-empty.</summary>
        [Test]
        public static void ThrowIfEmptyGuid_WithValidGuid_ReturnsValue()
        {
            // Arrange
            var theGuid = new Guid("1d572f1a-c8e9-4ff8-8ec6-9e585aa64e74");

            // Act
            Guid theResult = Validate.ThrowIfEmptyGuid(theGuid, nameof(theGuid));

            // Assert
            Assert.AreEqual(theResult, theGuid);
        }

        [Property]
        public static Property ThrowIfNullOrEmpty_WithNonWhiteSpaceString_ReturnsValue(NonEmptyString inText, bool inTrim)
        {
            Func<bool> theProperty = () => Validate.ThrowIfNullOrEmpty(inText.Get, nameof(inText), inTrim) == inText.Get;
            return theProperty.When(!string.IsNullOrWhiteSpace(inText.Get));
        }

        /// <summary>Checks that validation fails when a string is null.</summary>
        [Test]
        public static void ThrowIfNull_WithNullData_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "Value cannot be null.";
            string theNullText = null; // Need to use a variable, as type can't be inferred from a raw null.

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNull(theNullText, nameof(theNullText));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theNullText)));
        }

        [Test]
        public static void GenericThrowIfNotOfType_WithDerivedType_ReturnsValue()
        {
            // Arrange
            var theParameter = new DerivedType();

            // Act
            ExpectedType theResult = Validate.ThrowIfNotOfType<ExpectedType>(theParameter, nameof(theParameter));

            // Assert
            Assert.AreSame(theResult, theParameter);
        }

        [Test]
        public static void GenericThrowIfNotOfType_WithExpectedType_ReturnsValue()
        {
            // Arrange
            var theParameter = new ExpectedType();

            // Act
            ExpectedType theResult = Validate.ThrowIfNotOfType<ExpectedType>(theParameter, nameof(theParameter));

            // Assert
            Assert.AreSame(theResult, theParameter);
        }

        [Test]
        public static void GenericThrowIfNotOfType_WithNull_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "Value cannot be null.";
            ExpectedType theNullReference = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNotOfType<ExpectedType>(theNullReference, nameof(theNullReference));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theNullReference)));
        }

        [Test]
        public static void GenericThrowIfNotOfType_WithOtherType_ThrowsException()
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
                    Validate.ThrowIfNotOfType<ExpectedType>(theUnexpectedType, nameof(theUnexpectedType));
                },
                Throws.Exception.TypeOf<ArgumentTypeException>()
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theUnexpectedType)));
        }

        [Test]
        public static void ThrowIfNotOfType_WithDerivedType_ReturnsValue()
        {
            // Arrange
            var theParameter = new DerivedType();

            // Act
            object theResult = Validate.ThrowIfNotOfType(typeof(ExpectedType), theParameter, nameof(theParameter));

            // Assert
            Assert.AreSame(theResult, theParameter);
        }

        [Test]
        public static void ThrowIfNotOfType_WithExpectedType_ReturnsValue()
        {
            // Arrange
            var theParameter = new ExpectedType();

            // Act
            object theResult = Validate.ThrowIfNotOfType(typeof(ExpectedType), theParameter, nameof(theParameter));

            // Assert
            Assert.AreSame(theResult, theParameter);
        }

        [Test]
        public static void ThrowIfNotOfType_WithNull_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "Value cannot be null.";
            ExpectedType theNullReference = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNotOfType(typeof(ExpectedType), theNullReference, nameof(theNullReference));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theNullReference)));
        }

        [Test]
        public static void ThrowIfNotOfType_WithOtherType_ThrowsException()
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
                    Validate.ThrowIfNotOfType(typeof(ExpectedType), theUnexpectedType, nameof(theUnexpectedType));
                },
                Throws.Exception.TypeOf<ArgumentTypeException>()
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theUnexpectedType)));
        }

        [Test]
        public static void ThrowIfNullOrEmpty_WithEmptyString_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "The string cannot be empty.";
            string theEmptyString = string.Empty;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrEmpty(theEmptyString, nameof(theEmptyString));
                },
                Throws.ArgumentException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theEmptyString)));
        }

        [Test]
        public static void ThrowIfNullOrEmpty_WithNull_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "Value cannot be null.";
            string theNullString = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrEmpty(theNullString, nameof(theNullString));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theNullString)));
        }

        [Test]
        public static void ThrowIfNullOrEmpty_WithString_ReturnsValue()
        {
            // Arrange
            const string ExpectedValue = "Test";

            // Act
            string theResult = Validate.ThrowIfNullOrEmpty(ExpectedValue, nameof(ExpectedValue));

            // Assert
            Assert.AreSame(theResult, ExpectedValue);
        }

        [Test]
        public static void ThrowIfNullOrEmpty_WithWhiteSpaceStringAndNoTrim_ReturnsValue()
        {
            string theWhiteSpaceText = "    ";

            // Act
            string theResult = Validate.ThrowIfNullOrEmpty(
                theWhiteSpaceText,
                nameof(theWhiteSpaceText),
                inTrim: false);

            // Assert
            Assert.AreSame(theResult, theWhiteSpaceText);
        }

        [Test]
        public static void ThrowIfNullOrEmpty_WithWhiteSpaceStringAndTrim_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "The string cannot be empty (or contain only whitespace characters when the inTrim parameter is true).";
            string theWhiteSpaceText = "    ";

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    _ = Validate.ThrowIfNullOrEmpty(theWhiteSpaceText, nameof(theWhiteSpaceText), inTrim: true);
                },
                Throws.ArgumentException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theWhiteSpaceText)));
        }

        [Test]
        public static void ThrowIfNullOrEmpty_WithWhiteSpaceStringAndUnspecifiedTrim_ReturnsValue()
        {
            // Arrange
            string theWhiteSpaceText = "    ";

            // Assert
            string theResult = Validate.ThrowIfNullOrEmpty(theWhiteSpaceText, nameof(theWhiteSpaceText));

            // Assert
            Assert.AreSame(theResult, theWhiteSpaceText);
        }

        [Test]
        public static void ThrowIfNullOrWhiteSpace_WithEmptyString_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "The string cannot be empty or contain only whitespace characters.";
            string theEmptyString = string.Empty;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrWhiteSpace(theEmptyString, nameof(theEmptyString));
                },
                Throws.ArgumentException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theEmptyString)));
        }

        [Test]
        public static void ThrowIfNullOrWhiteSpace_WithNull_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "Value cannot be null.";
            string theNullString = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrWhiteSpace(theNullString, nameof(theNullString));
                },
                Throws.ArgumentNullException
                    .With.Message.Contains(theExpectedMessage)
                    .And.Message.Contains(nameof(theNullString)));
        }

        [Test]
        public static void ThrowIfNullOrWhiteSpace_WithNonWhiteSpaceString_ReturnsValue()
        {
            // Arrange
            const string ExpectedValue = "Test";

            // Act
            string theResult = Validate.ThrowIfNullOrWhiteSpace(ExpectedValue, nameof(ExpectedValue));

            // Assert
            Assert.AreSame(theResult, ExpectedValue);
        }

        [Test]
        public static void ThrowIfNullOrWhiteSpace_WithWhiteSpaceString_ThrowsException()
        {
            // Arrange
            string theExpectedMessage = "The string cannot be empty or contain only whitespace characters.";
            string theWhiteSpaceText = "    ";

            // Act
            string theResult = Validate.ThrowIfNullOrEmpty(
                theWhiteSpaceText,
                nameof(theWhiteSpaceText),
                inTrim: false);

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    Validate.ThrowIfNullOrWhiteSpace(theWhiteSpaceText, nameof(theWhiteSpaceText));
                },
                Throws.ArgumentException
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
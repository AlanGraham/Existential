// <copyright file="ParameterValidationTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace GavinGreig.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using NUnit.Framework;

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
            string theExpectedMessage = "The collection \"theTest\" is empty";
            Assert.Throws<ArgumentException>(
                () =>
                {
                    IEnumerable theCollection = new List<int> { };
                    ParameterValidation.EnsureCollectionNotEmpty(theCollection, "theTest");
                },
                theExpectedMessage);
        }

        /// <summary>Checks that validation fails when an IEnumerable collection is null.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenIEnumerableIsNull()
        {
            string theExpectedMessage = "The collection \"theCollection\" is null";
            ArgumentNullException theException = Assert.Throws<System.ArgumentNullException>(
                () =>
                {
                    IEnumerable theCollection = null;
                    ParameterValidation.EnsureCollectionNotEmpty(theCollection, nameof(theCollection));
                },
                theExpectedMessage);
            ////assert.AreEqual(theExpectedMessage, theException.Message);
            ////NUnit.Framework.Assert.That(theException.Message, Contains.Substring(theExpectedMessage));
        }

        /// <summary>
        /// Checks that validation fails when an IEnumerable{T} collection is non-null but empty.
        /// </summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenIEnumerableTIsEmpty()
        {
            string theExpectedMessage = "The collection \"theTest\" is empty";
            Assert.Throws<ArgumentException>(
                () =>
                {
                    IEnumerable<int> theCollection = new List<int> { };
                    ParameterValidation.EnsureCollectionNotEmpty(theCollection, "theTest");
                },
                theExpectedMessage);
        }

        /// <summary>Checks that validation fails when an IEnumerable{T} collection is null.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenIENumerableTIsNull()
        {
            string theExpectedMessage = "The collection \"theCollection\" is null";
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    IEnumerable<int> theCollection = null;
                    ParameterValidation.EnsureCollectionNotEmpty(theCollection, nameof(theCollection));
                },
                theExpectedMessage);
        }

        /// <summary>Checks that validation fails when a List{T} collection is non-null but empty.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenListTIsEmpty()
        {
            string theExpectedMessage = "The collection \"theTest\" is empty";
            Assert.Throws<ArgumentException>(
                () =>
                {
                    var theCollection = new List<int> { };
                    ParameterValidation.EnsureCollectionNotEmpty(theCollection, "theTest");
                },
                theExpectedMessage);
        }

        /// <summary>Checks that validation fails when a List{T} collection is null.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_Fails_WhenListTIsNull()
        {
            string theExpectedMessage = "The collection \"theTest\" is null";
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    List<int> theCollection = null;
                    ParameterValidation.EnsureCollectionNotEmpty(theCollection, "theTest");
                },
                theExpectedMessage);
        }

        /// <summary>Checks that validation succeeds when an IEnumerable collection has content.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_ReturnsCorrectType_WhenIEnumerableIsPopulated()
        {
            // Arrange
            IEnumerable theCollection = new List<int> { 1 };

            // Act
            IEnumerable theResult = ParameterValidation.EnsureCollectionNotEmpty(theCollection, "theTest");

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable>());
        }

        /// <summary>Checks that validation succeeds when an IEnumerable{T} collection has content.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_ReturnsCorrectType_WhenIEnumerableTIsPopulated()
        {
            // Arrange
            IEnumerable<int> theCollection = new List<int> { 1 };

            // Act
            IEnumerable<int> theResult = ParameterValidation.EnsureCollectionNotEmpty(theCollection, "theTest");

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<int>>());
        }

        /// <summary>Checks that validation succeeds when a List{T} collection has content.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureCollectionNotEmpty_ReturnsCorrectType_WhenListTIsPopulated()
        {
            // Arrange
            var theCollection = new List<int> { 1 };

            // Act
            List<int> theResult = ParameterValidation.EnsureCollectionNotEmpty(theCollection, "theTest");

            // Assert
            Assert.That(theResult, Is.InstanceOf<List<int>>());
        }

        /// <summary>Checks that validation fails when a GUID is empty.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureGuidNotEmpty_WithEmptyGuid_ThrowsException()
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    ParameterValidation.EnsureGuidNotEmpty(Guid.Empty, "testparam");
                });
        }

        /// <summary>Checks that validation succeeds when a GUID is not-empty.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureGuidNotEmpty_WithValidGuid_ReturnsValue()
        {
            // Arrange
            var theParameter = new Guid("1d572f1a-c8e9-4ff8-8ec6-9e585aa64e74");

            // Act
            Guid theResult = ParameterValidation.EnsureGuidNotEmpty(theParameter, "testparam");

            Assert.That(theResult, Is.EqualTo(theParameter));
        }

        /// <summary>Checks that validation fails when a string is null.</summary>
        /// <param name="assert">Required for compatibility with QUnit's assert instances.</param>
        [Test]
        public static void EnsureNotNull_WithNullData_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    string theNullText = null; // Need to use a variable, as type can't be inferred from a raw null.
                    ParameterValidation.EnsureNotNull(theNullText, "testparam");
                });
        }

        [Test]
        public static void EnsureOfType_WithExpectedType_ReturnsValue()
        {
            // Arrange
            var theParameter = new ExpectedType();

            // Act
            ExpectedType theResult = ParameterValidation.EnsureOfType<ExpectedType>(theParameter, "testparam");

            Assert.That(theResult, Is.EqualTo(theParameter));
        }

        [Test]
        public static void EnsureOfType_WithNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    ParameterValidation.EnsureOfType<ExpectedType>(null, "testparam");
                });
        }

        [Test]
        public static void EnsureOfType_WithOtherType_ThrowsException()
        {
            Assert.Throws<ArgumentTypeException>(
                () =>
                {
                    var theParameter = new UnexpectedType();
                    ParameterValidation.EnsureOfType<ExpectedType>(theParameter, "testparam");
                });
        }

        [Test]
        public static void EnsureStringNotNullOrEmpty_WithEmptyString_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    ParameterValidation.EnsureStringNotNullOrEmpty(string.Empty, "testparam");
                });
        }

        [Test]
        public static void EnsureStringNotNullOrEmpty_WithNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    string theParameter = null; // Need a variable to enforce the type of the null.
                    ParameterValidation.EnsureStringNotNullOrEmpty(theParameter, "testparam");
                });
        }

        [Test]
        public static void EnsureStringNotNullOrEmpty_WithString_ReturnsValue()
        {
            const string ExpectedValue = "Test";
            string theResult = ParameterValidation.EnsureStringNotNullOrEmpty(ExpectedValue, "testparam");

            Assert.That(theResult, Is.EqualTo(ExpectedValue));
        }

        private class ExpectedType
        {
        }

        private class UnexpectedType
        {
        }
    }
}
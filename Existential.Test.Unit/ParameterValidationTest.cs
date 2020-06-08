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

        /// <summary>A deliberately failing test to check build behaviour.</summary>
        [Test]
        public static void Fail()
        {
            Assert.Fail();
        }
    }
}
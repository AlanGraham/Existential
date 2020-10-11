// <copyright file="CreateEnumerableTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    public static class CreateEnumerableTest
    {
        [Test]
        public static void From_Null_ReturnsCollection()
        {
            // Arrange
            string theNull = null;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            // ReSharper disable once PossiblyMistakenUseOfParamsMethod
            IEnumerable<string> theResult = CreateEnumerable.From(theNull);

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<string>>()
                .With.Length.EqualTo(1)
                .And.Contains(null));
        }

        [Test]
        public static void From_SingleInt_ReturnsCollection()
        {
            // Act
            IEnumerable<int> theResult = CreateEnumerable.From(1);

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<int>>()
                .With.Length.EqualTo(1)
                .And.Contains(1));
        }

        [Test]
        public static void From_SingleString_ReturnsCollection()
        {
            // Act
            // ReSharper disable once PossiblyMistakenUseOfParamsMethod
            IEnumerable<string> theResult = CreateEnumerable.From("Test");

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<string>>()
                .With.Length.EqualTo(1)
                .And.Contains("Test"));
        }

        [Test]
        public static void From_TwoIntegers_ReturnsCollection()
        {
            // Act
            IEnumerable<int> theResult = CreateEnumerable.From(1, 2);

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<int>>()
                .With.Length.EqualTo(2)
                .And.Contains(1)
                .And.Contains(2));
        }

        [Test]
        public static void From_EnumerableOfNull_ReturnsCollection()
        {
            // Arrange
            var theEnumerable = new ArrayList { null };

            // Act
            IEnumerable<string> theResult = CreateEnumerable.From<string>(theEnumerable)
                .GetValueOr(new List<string>());

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<string>>()
                .With.Length.EqualTo(1)
                .And.Contains(null));
        }

        [Test]
        public static void From_EnumerableOfSingleInt_ReturnsCollection()
        {
            var theEnumerable = new ArrayList { 1 };

            // Act
            IEnumerable<int> theResult = CreateEnumerable.From<int>(theEnumerable)
                .GetValueOr(new List<int>());

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<int>>()
                .With.Length.EqualTo(1)
                .And.Contains(1));
        }

        [Test]
        public static void From_EnumerableOfSingleString_ReturnsCollection()
        {
            var theEnumerable = new ArrayList { "Test" };

            // Act
            IEnumerable<string> theResult = CreateEnumerable.From<string>(theEnumerable)
                .GetValueOr(new List<string>());

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<string>>()
                .With.Length.EqualTo(1)
                .And.Contains("Test"));
        }

        [Test]
        public static void From_EnumerableOfTwoIntegers_ReturnsCollection()
        {
            var theEnumerable = new ArrayList { 1, 2 };

            // Act
            IEnumerable<int> theResult = CreateEnumerable.From<int>(theEnumerable)
                .GetValueOr(new List<int>());

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<int>>()
                .With.Length.EqualTo(2)
                .And.Contains(1)
                .And.Contains(2));
        }

        [Test]
        public static void From_CollectionOfExpectedType_ReturnsIEnumerableWithCorrectMembers()
        {
            // Arrange
            object[] theObjects = { 1, 2 };

            // Act
            IEnumerable<int> theResult = CreateEnumerable.From<int>(theObjects)
                .GetValueOr(new List<int>());

            // Assert
            Assert.That(theResult, Has.Member(1).And.Member(2));
        }

        [Test]
        public static void From_CollectionWithMixedTypes_ReturnsDefault()
        {
            // Arrange
            object[] theObjects = { 1, "A test string" };

            // Act
            IEnumerable<int> theResult = CreateEnumerable.From<int>(theObjects)
                .GetValueOr(new List<int>());

            // Assert
            Assert.That(theResult.Count(), Is.EqualTo(0));
        }

        [Test]
        public static void From_CollectionWithMixedTypesIncludingString_ReturnsDefault()
        {
            // Check that other types aren't accidentally flattened into strings by a ToString operation.

            // Arrange
            object[] theObjects = { 1, "A test string" };

            // Act
            IEnumerable<string> theResult = CreateEnumerable.From<string>(theObjects)
                .GetValueOr(new List<string>());

            // Assert
            Assert.That(theResult.Count(), Is.EqualTo(0));
        }
    }
}
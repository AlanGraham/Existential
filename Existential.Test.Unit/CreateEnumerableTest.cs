// <copyright file="CreateEnumerableTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System.Collections;
    using System.Collections.Generic;

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
            IEnumerable<string> theResult = CreateEnumerable.From<string>(theEnumerable);

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
            IEnumerable<int> theResult = CreateEnumerable.From<int>(theEnumerable);

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
            IEnumerable<string> theResult = CreateEnumerable.From<string>(theEnumerable);

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
            IEnumerable<int> theResult = CreateEnumerable.From<int>(theEnumerable);

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<int>>()
                .With.Length.EqualTo(2)
                .And.Contains(1)
                .And.Contains(2));
        }
    }
}
// <copyright file="EnumerableTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System.Collections.Generic;

    using NUnit.Framework;

    public static class EnumerableTest
    {
        [Test]
        public static void From_Null_ReturnsCollection()
        {
            // Arrange
            string theNull = null;

            // Act
            IEnumerable<string> theResult = Enumerable.From(theNull);

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<string>>()
                .With.Length.EqualTo(1)
                .And.Contains(null));
        }

        [Test]
        public static void From_SingleInt_ReturnsCollection()
        {
            // Act
            IEnumerable<int> theResult = Enumerable.From(1);

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<int>>()
                .With.Length.EqualTo(1)
                .And.Contains(1));
        }

        [Test]
        public static void From_SingleString_ReturnsCollection()
        {
            // Act
            IEnumerable<string> theResult = Enumerable.From("Test");

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<string>>()
                .With.Length.EqualTo(1)
                .And.Contains("Test"));
        }

        [Test]
        public static void From_TwoIntegers_ReturnsCollection()
        {
            // Act
            IEnumerable<int> theResult = Enumerable.From(1, 2);

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<int>>()
                .With.Length.EqualTo(2)
                .And.Contains(1)
                .And.Contains(2));
        }
    }
}
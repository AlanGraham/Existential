// <copyright file="EnumerableHelperTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public static class EnumerableHelperTest
    {
        [Test]
        public static void EnumerableHelper_WithNull_ReturnsCollection()
        {
            // Arrange
            string theNull = null;

            // Act
            IEnumerable<string> theResult = EnumerableHelper.ToEnumerable(theNull);

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<string>>()
                  .With.Length.EqualTo(1)
                  .And.Contains(null));
        }

        [Test]
        public static void EnumerableHelper_WithSingleInt_ReturnsCollection()
        {
            // Act
            IEnumerable<int> theResult = EnumerableHelper.ToEnumerable(1);

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<int>>()
                .With.Length.EqualTo(1)
                .And.Contains(1));
        }

        [Test]
        public static void EnumerableHelper_WithSingleString_ReturnsCollection()
        {
            // Act
            IEnumerable<string> theResult = EnumerableHelper.ToEnumerable("Test");

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<string>>()
                .With.Length.EqualTo(1)
                .And.Contains("Test"));
        }

        [Test]
        public static void EnumerableHelper_WithTwoInts_ReturnsCollection()
        {
            // Act
            IEnumerable<int> theResult = EnumerableHelper.ToEnumerable(1, 2);

            // Assert
            Assert.That(theResult, Is.InstanceOf<IEnumerable<int>>()
                .With.Length.EqualTo(2)
                .And.Contains(1)
                .And.Contains(2));
        }
    }
}
// <copyright file="EnumerableOfTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using NUnit.Framework;

    [TestFixture]
    public static class EnumerableOfTest
    {
        [Test]
        public static void Constructor_WithValidCollection_ReturnsIEnumerableWithCorrectMembers()
        {
            // Arrange
            object[] theObjects = { 1, 2 };

            // Act
            var theResult = new EnumerableOf<int>(theObjects);

            // Assert
            Assert.That(theResult, Has.Member(1).And.Member(2));
        }

        [Test]
        public static void Constructor_WithMixedTypeCollection_Throws()
        {
            // Arrange
            object[] theObjects = { 1, "A test string" };

            // Assert
            Assert.That(
                () =>
                    {
                        // Act
                        _ = new EnumerableOf<int>(theObjects);
                    },
                Throws.TypeOf<ArgumentTypeException>()
                    .With.Message.Contains("The collection contained a member of an unexpected type (String)."));
        }

        [Test]
        public static void Constructor_ExpectingStringWithMixedTypeCollection_Throws()
        {
            // Arrange
            object[] theObjects = { 1, "A test string" };

            // Assert (ensure that there isn't an unexpected "ToString" operation.)
            Assert.That(
                () =>
                    {
                        // Act
                        _ = new EnumerableOf<string>(theObjects);
                    },
                Throws.TypeOf<ArgumentTypeException>()
                    .With.Message.Contains("The collection contained a member of an unexpected type (Int32)."));
        }
    }
}

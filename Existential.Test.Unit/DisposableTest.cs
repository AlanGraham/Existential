// <copyright file="DisposableTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System.IO;
    using System.IO.Compression;

    using NUnit.Framework;

    [TestFixture]
    public static class DisposableTest
    {
        [Test]
        public static void Disposable_SafelyReturnWithNull_RetainsDefaultManagedValueOutsideCreationScope()
        {
            // Act
            TestDummyDisposable theResult = Disposable.SafelyReturn<TestDummyDisposable>();

            // Assert
            Assert.That(theResult.ManagedObject, Is.Not.Null.And.TypeOf<MemoryStream>());
        }

        [Test]
        public static void Disposable_SafelyReturn_RetainsDefaultManagedValueOutsideCreationScope()
        {
            // Arrange
            static void EmptyInitialiser(TestDummyDisposable inDisposable)
            {
                /* Do nothing */
            }

            // Act
            TestDummyDisposable theResult = Disposable.SafelyReturn<TestDummyDisposable>(EmptyInitialiser);

            // Assert
            Assert.That(theResult.ManagedObject, Is.Not.Null.And.TypeOf<MemoryStream>());
        }

        [Test]
        public static void Disposable_SafelyReturn_RetainsInitialisedManagedValueOutsideCreationScope()
        {
            // Arrange
            static void Initialiser(TestDummyDisposable inDisposable) =>
                inDisposable.ManagedObject = new BrotliStream(new MemoryStream(), CompressionMode.Compress);

            // Act
            TestDummyDisposable theResult = Disposable.SafelyReturn<TestDummyDisposable>(Initialiser);

            // Assert
            Assert.That(theResult.ManagedObject, Is.Not.Null.And.TypeOf<BrotliStream>());
        }

        [Test]
        public static void Disposable_SafelyReturn_RetainsUnmanagedValueOutsideCreationScope()
        {
            // Act
            TestDummyDisposable theResult = Disposable.SafelyReturn<TestDummyDisposable>(
                inDisposable => inDisposable.UnmanagedObject = "A test string");

            // Assert
            Assert.That(theResult.UnmanagedObject, Is.EqualTo("A test string"));
        }
    }
}
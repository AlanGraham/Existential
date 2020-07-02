// <copyright file="ThisMethodTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using NUnit.Framework;

    [TestFixture]
    public static class ThisMethodTest
    {
        [Test]
        public static void ThisMethod_IsCorrectlyIdentified()
        {
            // Act
            string theMethodName = ThisMethod.GetName();

            // Assert
            Assert.That(theMethodName, Is.EqualTo("ThisMethod_IsCorrectlyIdentified"));
        }
    }
}
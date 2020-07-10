// <copyright file="MaybeLinqTest.cs" company="Gavin Greig">
//   Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public static class MaybeLinqTest
    {
        [Test]
        public static void Select_Works()
        {
            // Arrange
            var theStrings = new List<Maybe<string>>
            {
                "value 1",
                "value 2",
            };

            // Act
            IEnumerable<Maybe<string>> theResult = from maybe in theStrings
                                .Where((x) => x.ValueOr("Default text").Contains('1', StringComparison.Ordinal))
                                                   select maybe;

            // Assert
            Assert.That(theResult.Count() == 1);
            Assert.That(theResult.Contains("value 1"));
        }
    }
}

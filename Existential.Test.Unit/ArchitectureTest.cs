﻿// <copyright file="ArchitectureTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test.Architecture
{
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using NetArchTest.Rules;

    using NUnit.Framework;

    [TestFixture]
    public static class ArchitectureTest
    {
        [Test]
        public static void RootNamespace_IsCorrect()
        {
            // Arrange
            const string RootNamespace = "Existential";

            var theTargetAssembly = Assembly.GetAssembly(typeof(Validate));
            ConditionList theRootNamespaceRule = Types.InAssembly(theTargetAssembly)
                .That().AreNotNested()
                .And().DoNotResideInNamespaceStartingWith("Coverlet")
                .Should().ResideInNamespaceStartingWith(RootNamespace);

            // Act
            TestResult theResult = theRootNamespaceRule.GetResult();

            // Assert
            Assert.That(theResult.IsSuccessful, () =>
            {
                StringBuilder theBuilder = new StringBuilder()
                    .Append(theResult.FailingTypeNames.Count)
                    .Append(" type(s) not under the correct root namespace \"")
                    .Append(RootNamespace)
                    .Append("\":\n\t")
                    .Append(
                        theResult
                            .FailingTypeNames
                            .Aggregate((x, y) => string.Format(CultureInfo.CurrentCulture, "{0}\n\t{1}", x, y)));

                return theBuilder.ToString();
            });
        }
    }
}
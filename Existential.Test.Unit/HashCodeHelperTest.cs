// <copyright file="HashCodeHelperTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace GavinGreig.Test
{
    using System.Linq;

    using FsCheck;

    using NUnit.Framework;

    using AssertPropertyThat = FsCheck.Prop;
    using PropertyAttribute = FsCheck.NUnit.PropertyAttribute;

    [TestFixture]
    public static class HashCodeHelperTest
    {
        [Property]
        public static void HashCodeRepeatable()
        {
            AssertPropertyThat.ForAll<int[]>(
                (x) =>
                {
                    return HashCodeHelper.CalculateHashCode(x) == HashCodeHelper.CalculateHashCode(x);
                });
        }

        [Property]
        public static void ReorderedValuesGiveDifferentHashCode()
        {
            AssertPropertyThat.ForAll<int[]>(
                (x) =>
                {
                    return HashCodeHelper.CalculateHashCode(x) != HashCodeHelper.CalculateHashCode(x.Reverse());
                });
        }
    }
}
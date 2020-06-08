// <copyright file="TestRunner.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

#if JavaScript
// TODO: Target reflection more tightly when possible.
[assembly: Bridge.Reflectable("Bridge.*")]

namespace GavinGreig.Test
{
    using System.Reflection;
    using NUnitForBridge;

    internal class TestRunner
    {
        /// <summary>Runs the tests.</summary>
        [Bridge.Ready]
        public static void RunTests() => TestScanner.RunTests(Assembly.GetExecutingAssembly());
    }
}

#endif
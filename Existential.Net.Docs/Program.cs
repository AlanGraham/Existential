// <copyright file="Program.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Net.Docs
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>The entry point for the website program.</summary>
    /// <remarks>Inspired by: https://www.tpeczek.com/2017/05/generating-documentation-with-docfx-as.html.</remarks>
    public static class Program
    {
        /// <summary>The host builder for the web site.</summary>
        /// <param name="args">
        ///     Command line arguments passed through from <see cref="Main(string[])" />.
        /// </param>
        /// <returns>The builder for the web site.</returns>
        // ReSharper disable once InconsistentNaming, IdentifierTypo
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(inWebBuilder =>
                {
                    _ = inWebBuilder.UseStartup<Startup>();
                });

        /// <summary>The main method of the website.</summary>
        /// <param name="args">Command line arguments.</param>
        // ReSharper disable once InconsistentNaming, IdentifierTypo
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();
    }
}
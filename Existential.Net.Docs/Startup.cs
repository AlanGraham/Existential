// <copyright file="Startup.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Net.Docs
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>Start-up actions for the web site.</summary>
#pragma warning disable S1118 // Utility classes should not have public constructors - can't be static due to the way it's used in Program.
    public sealed class Startup
#pragma warning restore S1118 // Utility classes should not have public constructors
    {
        /// <summary>Configures the application.</summary>
        /// <param name="inApplication">The application builder.</param>
        /// <param name="inEnvironment">The web host environment.</param>
        /// <remarks>
        ///     This method gets called by the run-time. Use this method to configure the HTTP request pipeline.
        /// </remarks>
        public static void Configure(IApplicationBuilder inApplication, IWebHostEnvironment inEnvironment)
        {
            if (inEnvironment.IsDevelopment())
            {
                _ = inApplication.UseDeveloperExceptionPage();
            }

            _ = inApplication
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseRouting()
                .UseEndpoints(inEndpoints =>
                {
                    _ = inEndpoints.MapGet("/", async inContext =>
                    {
                        await inContext.Response.WriteAsync("Hello World!").ConfigureAwait(false);
                    });
                });
        }

        /// <summary>Configures services for the application.</summary>
        /// <param name="_">The service collection is currently unused in this application.</param>
        /// <remarks>
        ///     This method gets called by the run-time. Use this method to add services to the
        ///     container. For more information on how to configure your application, visit
        ///     https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </remarks>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
        public static void ConfigureServices(IServiceCollection _)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
        {
            // Haven't needed to add anything here yet.
        }
    }
}
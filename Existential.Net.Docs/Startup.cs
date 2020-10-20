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
    // ReSharper disable once ConvertToStaticClass - can't be static due to the way it's used in Program.
    public sealed class Startup
    {
        /// <summary>
        ///     Initialises a new instance of the <see cref="Startup" /> class.
        /// </summary>
        private Startup()
        {
        }

        /// <summary>Configures the application.</summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The web host environment.</param>
        /// <remarks>
        ///     This method gets called by the run-time. Use this method to configure the HTTP request pipeline.
        /// </remarks>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }

            _ = app
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseRouting()
                .UseEndpoints(endpoints =>
                    {
                        _ = endpoints.MapGet("/", async context =>
                        {
                            await context.Response.WriteAsync("Hello World!").ConfigureAwait(false);
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
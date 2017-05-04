using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmptyApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Konfigurationsdatei laden.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Das wird für die DI benötigt.
            services.AddOptions();

            // Aus einer Konfigurationsdatei laden.
            services.Configure<ReinRausOptions>(Configuration.GetSection("ReinRaus"));

            // DI manuell hinzufügen.
            services.AddReinRaus((options) =>
            {
                // options.Number = "Fuenf";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Middleware reinhängen.
            app.UseMiddleware<ReinRausMiddleware>();

            // 2. Middleware reinhängen, ohne eigne Klasse.
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Zwei rein<br />");
                await next();
                await context.Response.WriteAsync("Zwei raus<br />");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello Workshop!<br />");
            });
        }
    }
}

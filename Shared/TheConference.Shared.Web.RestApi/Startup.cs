using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MediatR;
using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using TheConference.InfoBooth.Core;
using TheConference.InfoBooth.Data;

namespace TheConference.Shared.Web.RestApi {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services) {
            // Add framework services.                        
            services.AddMediatR(
                typeof(Startup),
                typeof(IInfoBoothContext), typeof(InfoBoothContext)
            );
            services.AddMvc();

            // DI AutoFac
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.Register(_ => new InfoBoothContextFactory().Create()).As<IInfoBoothContext>().InstancePerLifetimeScope();
            //services.AddTransient<IInfoBoothContext, InfoBoothContext>(_ => new InfoBoothContextFactory().Create());
            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseMvc();
        }
    }
}

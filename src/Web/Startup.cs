namespace Web
{
    using System;
    using Autofac;
    using Domain;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Autofac.Extensions.DependencyInjection;
    using Domain.ValuesProvider;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json");

            if (env.IsDevelopment())
                builder.AddApplicationInsightsSettings(true);

            builder.AddEnvironmentVariables();
            Configuration = builder
                .Build()
                .ReloadOnChanged("appsettings.json")
                .ReloadOnChanged($"appsettings.{env.EnvironmentName}.json");
        }

        private IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);

            services
                .AddOptions()
                .Configure<ValuesControllerConfiguration>(Configuration);

            services.AddMvc(); //services.AddMvc(x=>x.Conventions.Insert(0, new RouteConvention(Configuration.GetSection("AppSettings")["ApiPreffix"])));

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<SimpleValuesProvider>().As<IValuesProvider>().SingleInstance();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory
                .AddConsole(Configuration.GetSection("Logging"))
                .AddDebug();

            app
                .UseIISPlatformHandler()
                .UseApplicationInsightsRequestTelemetry()
                .UseApplicationInsightsExceptionTelemetry();

            if (env.IsDevelopment())
                app
                    .UseDeveloperExceptionPage()
                    .UseRuntimeInfoPage()
                    .UseBrowserLink();
            else
                app.UseExceptionHandler("/Errors/Error500");

            app.UseStatusCodePagesWithReExecute("/Errors/Error{0}");

            app.UseMvc();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

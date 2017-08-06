using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tripeace.IoC;

namespace Tripeace.Application
{
    public class Startup
    {
        private IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceManager.InjectContext(services, _configuration);
            ServiceManager.InjectIdentity(services);
            ServiceManager.InjectMvc(services);
            ServiceManager.InjectLocalization(services);
            ServiceManager.InjectNugetPackages(services);
            ServiceManager.InjectLifestyleServices(services, _configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            ConfigureManager.ConfigureNLog(app, env, loggerFactory, _configuration);
            ConfigureManager.ConfigureLocalization(app);
            ConfigureManager.ConfigureEtc(app);

            if (env.IsDevelopment())
            {
                ConfigureManager.ConfigureDebugEnv(app);
            }
            else
            {
                ConfigureManager.ConfigureReleaseEnv(app);
            }
            
            ConfigureManager.ConfigureFolders(env);
            ConfigureManager.ConfigureRoutes(app);

            // Database configuration
            DatabaseManager.SeedRoles(app).Wait();
        }
    }
}

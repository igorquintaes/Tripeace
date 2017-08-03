using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tripeace.EF;
using Tripeace.Service.Services.Contracts;
using Tripeace.Service.Services;
using Tripeace.Domain.Contracts.Repositories;
using Tripeace.EF.Repository.Server;
using Tripeace.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Tripeace.Domain.Consts;
using Microsoft.AspNetCore.Http;
using NLog.Web;
using NLog.Extensions.Logging;
using NLog.Config;
using NLog.Web.LayoutRenderers;
using System.Runtime.InteropServices;
using Tripeace.Application.Helpers.Mappers.Contracts;
using Tripeace.Application.Helpers.Mappers;
using System.IO;

namespace Tripeace.Application
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ServerContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add Identity (Membership Provider)
            services.AddIdentity<AccountIdentity, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = AccountInfo.PasswordMinLength;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<ServerContext>()
                .AddDefaultTokenProviders();

            // Add the localization services to the services container
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Add HTTP Context
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add framework services.
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                .AddDataAnnotationsLocalization();

            // Add application services.
            services.AddScoped<IServerRepository, ServerRepository>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<ICharacterService, CharacterService>();
            services.AddSingleton<IAccountMapper, AccountMapper>();

            // Configuration
            services.Add(new ServiceDescriptor(typeof(IConfiguration),
                     provider => new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json",
                                        optional: false,
                                        reloadOnChange: true)
                        .Build(),
                     ServiceLifetime.Singleton));

            // Pagination 
            services.AddCloudscribePagination();

            // Configure supported cultures and localization options
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pt-BR")
                };

                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.FallBackToParentCultures = false;
                options.FallBackToParentUICultures = false;
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new CookieRequestCultureProvider { CookieName = CookieRequestCultureProvider.DefaultCookieName },
                    new AcceptLanguageHeaderRequestCultureProvider { }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            ConfigurationItemFactory.Default.LayoutRenderers
                .RegisterDefinition("aspnet-request-ip", typeof(AspNetRequestIpLayoutRenderer));

            if (IsLinux())
                env.ConfigureNLog("linux_nlog.config");
            else
                env.ConfigureNLog("windows_nlog.config");

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.AddNLogWeb();
            app.UseIdentity();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute", 
                    template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            Helpers.RolesData.SeedRoles(app.ApplicationServices).Wait();

            if (!Directory.Exists(Path.Combine(env.ContentRootPath, ServerInfo.PlayerAvatarDir)))
                Directory.CreateDirectory(Path.Combine(env.ContentRootPath, ServerInfo.PlayerAvatarDir));
        }

        private static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }
    }
}

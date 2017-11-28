using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tripeace.Domain.Contracts;
using System.Reflection;
using System.Linq;
using Tripeace.EF.Repository.Server;
using Tripeace.Service.Services.Server;
using Tripeace.EF;
using Tripeace.Domain.Entities;
using Tripeace.Domain.Consts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Tripeace.IoC
{
    public static class ServiceManager
    {
        public static void InjectContext(IServiceCollection services, IConfigurationRoot configBuilder)
        {
            services.AddDbContext<ServerContext>(options =>
            {
                options.UseMySql(configBuilder.GetConnectionString("DefaultConnection"));
            });

            // Database seeder
            services.AddScoped<IDatabaseManager, DatabaseManager>();
        }

        public static void InjectIdentity(IServiceCollection services)
        {
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
        }

        public static void InjectMvc(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                .AddDataAnnotationsLocalization();
        }

        public static void InjectLocalization(IServiceCollection services)
        {
            // Add the localization services to the services container
            services.AddLocalization(options => options.ResourcesPath = "Resources");

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
        
        public static void InjectNugetPackages(IServiceCollection services)
        {
            // Pagination 
            services.AddCloudscribePagination();
        }

        public static void InjectLifestyleServices(IServiceCollection services, IConfigurationRoot configBuilder)
        {
            // HTTP Context
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Configuration on .json
            services.Add(new ServiceDescriptor(typeof(IConfiguration),
                     provider => configBuilder,
                     ServiceLifetime.Singleton));
            
            // All Repositories
            var repositoryAssembly = typeof(AccountRepository).GetTypeInfo().Assembly;
            var repositoryRegistrations =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace == "Tripeace.EF.Repository.Server"
                where type.GetInterfaces().Any()
                select new
                {
                    Contract = type.GetInterfaces().First(x => x.Name != typeof(IRepository<>).Name),
                    Implementation = type
                };

            foreach (var reg in repositoryRegistrations)
            {
                services.AddScoped(reg.Contract, reg.Implementation);
            }

            // All Services
            var serviceAssembly = typeof(AccountService).GetTypeInfo().Assembly;
            var serviceRegistrations =
                from type in serviceAssembly.GetExportedTypes()
                where type.Namespace == "Tripeace.Service.Services.Server"
                where type.GetInterfaces().Any()
                select new
                {
                    Contract = type.GetInterfaces().First(x => x.Name != typeof(IService<>).Name),
                    Implementation = type
                };

            foreach (var reg in serviceRegistrations)
            {
                services.AddScoped(reg.Contract, reg.Implementation);
            }
        }
    }
}

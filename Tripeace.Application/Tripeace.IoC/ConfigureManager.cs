using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Web;
using NLog.Web.LayoutRenderers;
using System.Runtime.InteropServices;
using System.IO;
using Tripeace.Domain.Consts;

namespace Tripeace.IoC
{
    public static class ConfigureManager
    {
        public static void ConfigureNLog(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            ILoggerFactory loggerFactory, 
            IConfigurationRoot configBuilder)
        {
            loggerFactory.AddNLog();
            loggerFactory.AddConsole(configBuilder.GetSection("Logging"));
            loggerFactory.AddDebug();
            ConfigurationItemFactory.Default.LayoutRenderers
                .RegisterDefinition("aspnet-request-ip", typeof(AspNetRequestIpLayoutRenderer));

            if (IsLinux())
                env.ConfigureNLog("linux_nlog.config");
            else
                env.ConfigureNLog("windows_nlog.config");

            app.AddNLogWeb();
        }

        public static void ConfigureLocalization(IApplicationBuilder app)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
        }

        public static void ConfigureFolders(IHostingEnvironment env)
        {
            if (!Directory.Exists(Path.Combine(env.ContentRootPath, ServerInfo.PlayerAvatarDir)))
                Directory.CreateDirectory(Path.Combine(env.ContentRootPath, ServerInfo.PlayerAvatarDir));
        }

        public static void ConfigureRoutes(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static void ConfigureDebugEnv(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
        }

        public static void ConfigureReleaseEnv(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Home/Error");
        }

        public static void ConfigureEtc(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        #region Helpers

        private static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        #endregion
    }
}

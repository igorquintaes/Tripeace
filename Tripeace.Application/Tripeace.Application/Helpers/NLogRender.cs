using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Web.Internal;
using NLog.Web.LayoutRenderers;
using NLog;

namespace Tripeace.Application.Helpers
{
    /// <summary>
    /// Render the request IP for ASP.NET Core
    /// </summary>
    /// <example>
    /// <code lang="NLog Layout Renderer">
    /// ${aspnet-request-ip}
    /// </code>
    /// </example>
    [LayoutRenderer("aspnet-request-ip")]
    public class AspNetRequestIpLayoutRenderer : AspNetLayoutRendererBase
    {

        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var httpContext = HttpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return;
            }
            builder.Append(httpContext.Connection.RemoteIpAddress);
        }
    }
}

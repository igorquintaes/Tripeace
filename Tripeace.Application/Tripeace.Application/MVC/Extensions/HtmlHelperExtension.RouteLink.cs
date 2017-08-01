using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.MVC.Extensions
{
    public static partial class HtmlHelperExtension
    {
        public static IHtmlContent RouteLink(this IHtmlHelper html, LocalizedHtmlString text, object routeValues)
        {
            return html.RouteLink(text.Value, routeValues);
        }

        public static IHtmlContent RouteLink(this IHtmlHelper html, LocalizedHtmlString text, string routeName)
        {
            return html.RouteLink(text.Value, routeName);
        }

        public static IHtmlContent RouteLink(this IHtmlHelper html, LocalizedHtmlString text, object routeValues, object htmlAttributes)
        {
            return html.RouteLink(text.Value, routeValues, htmlAttributes);
        }

        public static IHtmlContent RouteLink(this IHtmlHelper html, LocalizedHtmlString text, string routeName, object routeValues)
        {
            return html.RouteLink(text.Value, routeName, routeValues);
        }

        public static IHtmlContent RouteLink(this IHtmlHelper html, LocalizedHtmlString text, string routeName, object routeValues, object htmlAttributes)
        {
            return html.RouteLink(text.Value, routeName, routeValues, htmlAttributes);
        }

        public static IHtmlContent RouteLink(this IHtmlHelper html, LocalizedHtmlString text, string routeName, string protocol, string hostname, string fragment, object routeValues, object htmlAttributes)
        {
            return html.RouteLink(text.Value, routeName, protocol, hostname, fragment, routeValues, htmlAttributes);
        }
    }
}

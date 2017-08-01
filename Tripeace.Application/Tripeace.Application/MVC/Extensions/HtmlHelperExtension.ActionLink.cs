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
        public static IHtmlContent ActionLink(this IHtmlHelper html, LocalizedHtmlString text, string actionName)
        {
            return html.ActionLink(text.Value, actionName);
        }

        public static IHtmlContent ActionLink(this IHtmlHelper html, LocalizedHtmlString text, string actionName, object routeValues)
        {
            return html.ActionLink(text.Value, actionName, routeValues);
        }

        public static IHtmlContent ActionLink(this IHtmlHelper html, LocalizedHtmlString text, string actionName, string controllerName)
        {
            return html.ActionLink(text.Value, actionName, controllerName);
        }

        public static IHtmlContent ActionLink(this IHtmlHelper html, LocalizedHtmlString text, string actionName, object routeValues, object htmlAttributes)
        {
            return html.ActionLink(text.Value, actionName, routeValues, htmlAttributes);
        }

        public static IHtmlContent ActionLink(this IHtmlHelper html, LocalizedHtmlString text, string actionName, string controllerName, object routeValues)
        {
            return html.ActionLink(text.Value, actionName, controllerName, routeValues);
        }

        public static IHtmlContent ActionLink(this IHtmlHelper html, LocalizedHtmlString text, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            return html.ActionLink(text.Value, actionName, controllerName, routeValues, htmlAttributes);
        }

        public static IHtmlContent ActionLink(this IHtmlHelper html, LocalizedHtmlString text, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            return html.ActionLink(text.Value, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes);
        }
    }
}

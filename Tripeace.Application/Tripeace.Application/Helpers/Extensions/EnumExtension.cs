using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tripeace.Application.Helpers.Extensions
{
    public static class EnumExtension
    {
        public static string ToCamelCaseString(this Enum value)
        {
            return Regex.Replace(
                Regex.Replace(
                    value.ToString(),
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }
    }
}

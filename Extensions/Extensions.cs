using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace capital_index.Extensions
{
    public static class Extensions
    {
        public static string GetEnumDisplayName(this Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>().Name;

        }
    }
}
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace System;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var type = enumValue.GetType();
        var member = type.GetMember(enumValue.ToString());
        if (member.Length > 0)
        {
            var displayAttr = member[0].GetCustomAttribute<DisplayAttribute>();
            if (displayAttr != null)
                return displayAttr.Name ?? enumValue.ToString();
        }
        return enumValue.ToString();
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareerWay.Web.Helpers;

public class EnumHelper
{
    public static List<SelectListItem> ToSelectList<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<Enum>()
            .Select(e => new SelectListItem
            {
                Value = Convert.ToInt32(e).ToString(),
                Text = e.GetDisplayName()
            })
            .ToList();
    }
}

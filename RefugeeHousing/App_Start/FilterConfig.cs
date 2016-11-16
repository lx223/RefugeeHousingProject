using System.Web.Mvc;
using RefugeeHousing.Translations;

namespace RefugeeHousing
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LocalizationAttribute());
        }
    }
}

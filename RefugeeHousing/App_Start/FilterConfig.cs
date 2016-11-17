using System.Web.Mvc;
using RefugeeHousing.Filters;
using RefugeeHousing.Translations;

namespace RefugeeHousing
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionHandlingAttribute());
            filters.Add(new LocalizationAttribute());
        }
    }
}

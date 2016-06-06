using System.Web.Mvc;
using outdoor.rocks.Filters;
namespace outdoor.rocks
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

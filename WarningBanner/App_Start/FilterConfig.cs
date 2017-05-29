using System.Web;
using System.Web.Mvc;
using WarningBanner.Attribute;

namespace WarningBanner
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequireAcceptPolicy());
        }
    }
}

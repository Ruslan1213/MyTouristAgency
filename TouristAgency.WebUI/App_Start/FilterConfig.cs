using System.Web;
using System.Web.Mvc;
using TouristAgency.Domain.Filters;

namespace TouristAgency.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new AdminActionAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}

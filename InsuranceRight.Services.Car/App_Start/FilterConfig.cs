using System.Web;
using System.Web.Mvc;

namespace InsuranceRight.Services.Car
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

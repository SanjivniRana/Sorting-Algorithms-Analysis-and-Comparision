using System.Web;
using System.Web.Mvc;

namespace _5311_Project_sxr0277
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
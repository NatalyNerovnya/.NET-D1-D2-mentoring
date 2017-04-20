using System.Web.Mvc;
using BadServise.Exceptions;

namespace ExceptionHandling
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleErrorAttribute
            {
                View = "~/Views/Shared/Error",
                ExceptionType = typeof(GlobalException)
            });
        }
    }
}
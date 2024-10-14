using System.Web;
using System.Web.Mvc;

namespace OPERACION_AMHCH_5_OK
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

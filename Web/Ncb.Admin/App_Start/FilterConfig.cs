using System.Web;
using System.Web.Mvc;
using Framework.Auth;

namespace Ncb.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new NoCacheAttribute());
            filters.Add(new ActionAuthorizeAttribute());
        }
    }
}

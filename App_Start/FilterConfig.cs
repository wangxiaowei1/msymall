using System.Web;
using System.Web.Mvc;
using WebApplication7.Services.Filters;

namespace WebApplication7
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());    //全局异常过滤器

            filters.Add(new IEFilterAttribute());   //将自定义IE过滤器应用到了全局网站
        }
    }
}

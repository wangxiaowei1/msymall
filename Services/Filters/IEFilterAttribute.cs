using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication7.Services.Filters
{
    public class IEFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["IEWarning"] != "false")
            {
                if (filterContext.HttpContext.Request.Browser.Browser == "InternetExplorer")
                {
                    string returnUrl = filterContext.HttpContext.Request.RawUrl;    //获取访问页面的URL
                    string s = "<div class=\"container\"><div class=\"alert alert-warning\">"
                        + "<a class=\"close\" data-dismiss=\"alert\" style=\"padding-left:20px;\">&times;</a>"
                        + "<a href=\"Home/Notip?returnUrl=" + returnUrl + "\" class=\"pull-right\" data-dismiss=\"alert\">不再提示</a>"
                        + "<strong>警告！</strong>不建议使用IE浏览器访问，推荐使用谷歌或火狐浏览器访问本网站。</div></div>";
                    filterContext.HttpContext.Response.Write(s);
                }
            }
            
        }
    }
}
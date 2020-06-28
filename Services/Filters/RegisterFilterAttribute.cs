using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication7.Services.Filters
{
    public class RegisterFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session["Registed"] == "true")
            {
                string returnUrl = filterContext.HttpContext.Request.RawUrl;    //获取访问页面的URL
                string style = "position:fixed; top:0; width:100%; margin-top:0; z-index:9999;";
                string s = "<div class=\"alert alert-success\" style=\"" + style + "\">"
                    + "<a class=\"close\" data-dismiss=\"alert\" style=\"padding-left:20px;\">&times;</a>"
                    + "<span class=\"glyphicon glyphicon-ok\">&nbsp;</span><strong>提示！</strong>恭喜，您注册本网站成功，请登录。</div>";
                filterContext.HttpContext.Response.Write(s);
                filterContext.HttpContext.Session["Registed"] = "false";
            }
        }
    }
}
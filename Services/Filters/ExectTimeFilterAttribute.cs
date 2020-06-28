using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace WebApplication7.Services.Filters
{
    public class ExectTimeFilterAttribute : ActionFilterAttribute
    {
        //定义一个定时器
        Stopwatch  timer;
        public ExectTimeFilterAttribute ()
	    {
            timer = new Stopwatch();
	    }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer.Reset();
            timer.Start();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();
            string time = string.Format("{0:f6}", timer.Elapsed.Seconds);
            if (filterContext.HttpContext.Session["TimeAlert"] != "false")
            {
                string returnUrl = filterContext.HttpContext.Request.RawUrl;    //获取访问页面的URL
                string style = "position:fixed; bottom:0; width:100%; margin-bottom:0; paddig:8px 30px;";
                string s = "<div class=\"alert alert-info\" style=\"" + style + "\">"
                    + "<a class=\"close\" data-dismiss=\"alert\" style=\"padding-left:20px;\">&times;</a>"
                    + "<a href=\"/Home/Notime?returnUrl=" + returnUrl + "\" class=\"pull-right\" data-dismiss=\"alert\">不再提示</a>"
                    + "<strong>提示！</strong>本次查询执行耗时 " + time + " 秒</div>";
                filterContext.HttpContext.Response.Write(s);
            }
            
        }
    }
}
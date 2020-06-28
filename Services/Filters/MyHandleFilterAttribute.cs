using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApplication7.Services.Filters
{
    public class MyHandleFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is StackOverflowException)  //判断异常是否为栈溢出异常
            {
                filterContext.Result = new ViewResult() { ViewName = "StackOverflowError" };  //如果是栈溢出异常，转入StackOverflowError页面
                filterContext.ExceptionHandled = true;  //将异常设置为已处理
            }
            else if (filterContext.Exception is ArgumentException)  //判断是否为参数异常
            {
                filterContext.Result = new ViewResult() { ViewName = "ArgumentError" };  //如果是参数异常，转入ArgumentError页面
                filterContext.ExceptionHandled = true;  //将异常设置为已处理
            }
            else if (filterContext.Exception is FileNotFoundException)  //判断是否为找不到文件异常
            {
                filterContext.Result = new ViewResult() { ViewName = "FileNotFoundError" };  //如果是找不到文件异常，转入FileNotFoundError页面
                filterContext.ExceptionHandled = true;  //将异常设置为已处理
            }
            else
            {
                filterContext.Result = new ViewResult() { ViewName = "Error" };  //其余异常，转入Error页面
                filterContext.ExceptionHandled = true;  //将异常设置为已处理
            }
        }
    }
}
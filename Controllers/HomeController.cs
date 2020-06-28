using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Services.Filters;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Notip(string returnUrl)
        {
            Session["IEWarning"] = "false";
            return Redirect(returnUrl);
        }

        public ActionResult Notime(string returnUrl)
        {
            Session["TimeAlert"] = "false";
            return Redirect(returnUrl);
        }
    }
}
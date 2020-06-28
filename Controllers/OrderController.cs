using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;
using Microsoft.AspNet.Identity;
using WebApplication7.Services.Filters;

namespace WebApplication7.Controllers
{
    [Authorize(Roles="普通顾客")]
    public class OrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Order/
        [ExectTimeFilter]
        public ActionResult Index(string date, string Category)
        {
            var userID = User.Identity.GetUserId();
            var orders = db.Orders.Where(x => x.ApplicationUserID == userID).ToList();

            if (!string.IsNullOrEmpty(date))
            {
                var list = date.Split('-').ToList();
                var dt = string.Join("/", list).ToString();
                orders = orders.Where(x => x.Date.ToShortDateString() == dt).ToList();
            }

            if (!string.IsNullOrEmpty(Category))
            {
                orders = orders.Where(x => x.Product.Category == Category).ToList();
            }
            return View(orders);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var order = db.Orders.Find(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Delete(int? id, Order model)
        {
            var order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
	}
}
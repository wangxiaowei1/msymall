using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;
using Microsoft.AspNet.Identity;
using WebApplication7.Services.Filters;
using PagedList;

namespace WebApplication7.Controllers
{
    
    public class ProductController : Controller
    {
        //添加数据库上下文对象，用于本控制器对数据库表进行增删改查
        ApplicationDbContext db = new ApplicationDbContext();

        //
        // GET: /Product/
        [ExectTimeFilter]
        //[MyHandleFilter]
        public ActionResult Index(string Category, int? productId, string ProductName,decimal? StartPrice, decimal? EndPrice, int? page,
            string sortOrder="id")
        {
            ViewBag.Categories = db.Products.Select(x => x.Category).Distinct().ToList();
            ViewBag.SelectedCategory = Category;
            List<Product> products = new List<Product>();
            if (string.IsNullOrEmpty(Category))
            {
                products = db.Products.ToList();
            }
            else
            {
                ViewBag.Category = Category;
                products = db.Products.Where(x => x.Category == Category).ToList();
            }

            if (productId != null)
            {
                products = db.Products.Where(x => x.ProductID == productId.Value).ToList();
            }

            if (!string.IsNullOrEmpty(ProductName))
            {
                products = db.Products.Where(x => x.ProductName == ProductName).ToList();
            }

            if (StartPrice != null && EndPrice != null && StartPrice.Value <= EndPrice.Value)
            {
                ViewBag.StartPrice = StartPrice.Value;
                ViewBag.EndPrice = EndPrice.Value;
                products = products.Where(x => x.Price >= StartPrice.Value && x.Price <= EndPrice.Value).ToList();
            }

            //排序
            ViewBag.SortOrder = sortOrder;
            ViewBag.NameSort = sortOrder == "productName" ? "productName_desc" : "productName";
            ViewBag.PriceSort = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.CategorySort = sortOrder == "category" ? "category_desc" : "category";
            switch (sortOrder)
            {
                case "productName": products = products.OrderBy(x => x.ProductName).ToList(); break;
                case "productName_desc": products = products.OrderByDescending(x => x.ProductName).ToList(); break;
                case "price": products = products.OrderBy(x => x.Price).ToList(); break;
                case "price_desc": products = products.OrderByDescending(x => x.Price).ToList(); break;
                case "category": products = products.OrderBy(x => x.Category).ToList(); break;
                case "category_desc": products = products.OrderByDescending(x => x.Category).ToList(); break;
                default: products = products.OrderBy(x=>x.ProductID).ToList(); break;
            }

            int pageSize = 3;  //每页显示三条记录
            int pageNumber = page ?? 1; //如果URL没传递页码，那就默认第一页

            return View(products.ToPagedList(pageNumber,pageSize));  //数据库上下文中Products属性，也就是映射到数据库的Products表，这里获取表全部记录传给视图
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");   //如果传递的参数id为空，则不知道显示哪个商品，返回商品列表
            var product = db.Products.Find(id);
            return View(product);
        }

        [Authorize(Roles="普通顾客")]
        public ActionResult AddToCart(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            Order order = new Order();
            order.Count = 1;
            order.Date = DateTime.Now;
            order.ProductID = id.Value;
            
            //获取当前登录用户ID
            var userID = User.Identity.GetUserId();
            order.ApplicationUserID = userID;
            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index","Order");
        }
	}
}
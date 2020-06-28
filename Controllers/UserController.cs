using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication7.Controllers
{
    [Authorize(Roles = "管理员")]
    public class UserController : Controller
    {
        ApplicationDbContext db;
        UserManager<ApplicationUser> userManager;

        public UserController()
        {
            db = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        //
        // GET: /User/
        public ActionResult Index()
        {
            var users = new List<UserViewModel>();
            db.Users.ToList().ForEach(x => users.Add(new UserViewModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Roles = x.Roles.Select(t => t.Role.Name).ToList()
            }));
            return View(users);
        }

        //
        // GET: /User/Details/5
        public ActionResult Details(string id)
        {
            var user = db.Users.Find(id);
            UserViewModel model = new UserViewModel() { UserName = user.UserName, Roles = user.Roles.Select(x=>x.Role.Name).ToList() };
            return View(model);
        }

        //
        // GET: /User/Create
        public ActionResult Create()
        {
            return RedirectToAction("Register","Account"); //转到注册页面
        }


        //
        // GET: /User/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Roles = db.ApplicationRoles.Select(x => x.Name).ToList();
            var user = db.Users.Find(id);
            UserViewModel model = new UserViewModel() { UserName = user.UserName, Roles = user.Roles.Select(x => x.Role.Name).ToList() };
            return View(model);
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UserViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                var user = db.Users.Find(id);
                user.UserName = model.UserName;
                user.Roles.Clear();
                foreach (var item in db.ApplicationRoles.Select(x=>x.Name).ToList())
                {
                    var isChecked = Request[item].ToString().Split(',')[0];
                    if (Convert.ToBoolean(isChecked) && !userManager.IsInRole(user.Id, item))
                    {
                        userManager.AddToRole(user.Id, item);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /User/Delete/5
        public ActionResult Delete(string id)
        {
            var user = db.Users.Find(id);
            UserViewModel model = new UserViewModel() { UserName = user.UserName, Roles = user.Roles.Select(x => x.Role.Name).ToList() };
            return View(model);
        }

        //
        // POST: /User/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, UserViewModel model)
        {
            try
            {
                // TODO: Add delete logic here
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

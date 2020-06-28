using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Authorize(Roles="管理员")]
    public class RoleController : Controller
    {
        ApplicationDbContext db;
        public RoleController()
        {
            db = new ApplicationDbContext();
        }
        //
        // GET: /Role/
        public ActionResult Index()
        {
            var roles = new List<RoleViewModel>();
            db.ApplicationRoles.ToList().ForEach(x => roles.Add(new RoleViewModel() {  Id=x.Id, Name=x.Name, CreateTime=x.CreateTime}));
            return View(roles);
        }

        //
        // GET: /Role/Details/5
        public ActionResult Details(string id)
        {
            var role = db.ApplicationRoles.Find(id);
            RoleViewModel model = new RoleViewModel() { Id=role.Id, Name=role.Name, CreateTime=role.CreateTime };
            return View(model);
        }

        //
        // GET: /Role/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Role/Create
        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                db.ApplicationRoles.Add(new ApplicationRole() { Name = model.Name, CreateTime = DateTime.Now });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Role/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.ApplicationRoles.Find(id);
            RoleViewModel model = new RoleViewModel() { Id = role.Id, Name = role.Name, CreateTime = role.CreateTime };
            return View(model);
        }

        //
        // POST: /Role/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, RoleViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                db.ApplicationRoles.Find(id).Name = model.Name;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Role/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.ApplicationRoles.Find(id);
            RoleViewModel model = new RoleViewModel() { Id = role.Id, Name = role.Name, CreateTime = role.CreateTime };
            return View(model);
        }

        //
        // POST: /Role/Delete/5
        [HttpPost]
        public ActionResult Delete(string  id, RoleViewModel model)
        {
            try
            {
                // TODO: Add delete logic here
                var role = db.ApplicationRoles.Find(id);
                db.ApplicationRoles.Remove(role);
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

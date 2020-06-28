namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication7.Models;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication7.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WebApplication7.Models.ApplicationDbContext";
        }

        protected override void Seed(WebApplication7.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var products = new List<Product>() { 
                new Product { ProductName = "外套", Image = "/Content/Images/1.jpg", Price = 86.8M, Category = "服装" },
                new Product { ProductName = "T恤", Image = "/Content/Images/2.jpg", Price = 32.5M, Category = "服装" },
                new Product { ProductName = "牛仔裤", Image = "/Content/Images/3.jpg", Price = 168.5M, Category = "服装" },
                new Product { ProductName = "笔记本", Image = "/Content/Images/4.jpg", Price = 3598M, Category = "电子产品" },
                new Product { ProductName = "IPad", Image = "/Content/Images/5.jpg", Price = 2350M, Category = "电子产品" },
                new Product { ProductName = "手机", Image = "/Content/Images/6.jpg", Price = 4260M, Category = "电子产品" },
                new Product { ProductName = "篮球", Image = "/Content/Images/7.jpg", Price = 75.8M, Category = "球类" },
                new Product { ProductName = "足球", Image = "/Content/Images/8.jpg", Price = 62.5M, Category = "球类" },
                new Product { ProductName = "排球", Image = "/Content/Images/9.jpg", Price = 55.3M, Category = "球类" }
            };
            context.Products.AddOrUpdate(x => x.ProductName, products.ToArray()); //该语句作用是将product列表每条记录添加到数据库中

            var roles = new List<ApplicationRole>()
            {
                new ApplicationRole() { Name = "管理员", CreateTime = DateTime.Now },
                new ApplicationRole() { Name = "普通顾客", CreateTime = DateTime.Now }
            };
            context.Roles.AddOrUpdate(x => x.Name, roles.ToArray());
            context.SaveChanges();

            if (context.Users.Where(x => x.UserName == "陈雪").Count() <= 0)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser() { UserName = "ADMINUSER" };
                var result = userManager.Create(user, "123456");

                if (result.Succeeded)
                {
                    context.Users.Find(user.Id).UserName = "陈雪";
                    context.SaveChanges();
                    userManager.AddToRole(user.Id, "管理员");
                }
            }
        }
    }
}

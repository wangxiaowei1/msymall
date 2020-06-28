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
                new Product { ProductName = "����", Image = "/Content/Images/1.jpg", Price = 86.8M, Category = "��װ" },
                new Product { ProductName = "T��", Image = "/Content/Images/2.jpg", Price = 32.5M, Category = "��װ" },
                new Product { ProductName = "ţ�п�", Image = "/Content/Images/3.jpg", Price = 168.5M, Category = "��װ" },
                new Product { ProductName = "�ʼǱ�", Image = "/Content/Images/4.jpg", Price = 3598M, Category = "���Ӳ�Ʒ" },
                new Product { ProductName = "IPad", Image = "/Content/Images/5.jpg", Price = 2350M, Category = "���Ӳ�Ʒ" },
                new Product { ProductName = "�ֻ�", Image = "/Content/Images/6.jpg", Price = 4260M, Category = "���Ӳ�Ʒ" },
                new Product { ProductName = "����", Image = "/Content/Images/7.jpg", Price = 75.8M, Category = "����" },
                new Product { ProductName = "����", Image = "/Content/Images/8.jpg", Price = 62.5M, Category = "����" },
                new Product { ProductName = "����", Image = "/Content/Images/9.jpg", Price = 55.3M, Category = "����" }
            };
            context.Products.AddOrUpdate(x => x.ProductName, products.ToArray()); //����������ǽ�product�б�ÿ����¼��ӵ����ݿ���

            var roles = new List<ApplicationRole>()
            {
                new ApplicationRole() { Name = "����Ա", CreateTime = DateTime.Now },
                new ApplicationRole() { Name = "��ͨ�˿�", CreateTime = DateTime.Now }
            };
            context.Roles.AddOrUpdate(x => x.Name, roles.ToArray());
            context.SaveChanges();

            if (context.Users.Where(x => x.UserName == "��ѩ").Count() <= 0)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser() { UserName = "ADMINUSER" };
                var result = userManager.Create(user, "123456");

                if (result.Succeeded)
                {
                    context.Users.Find(user.Id).UserName = "��ѩ";
                    context.SaveChanges();
                    userManager.AddToRole(user.Id, "����Ա");
                }
            }
        }
    }
}

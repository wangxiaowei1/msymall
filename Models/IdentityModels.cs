using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections.Generic;
using System;
using WebApplication7.Migrations;

namespace WebApplication7.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //用户数据模型
    public class ApplicationUser : IdentityUser
    {
        //用户名是常用的属性，将其设置对应中文为顾客姓名
        [Display(Name="顾客姓名")]
        public override string UserName
        {
            get
            {
                return base.UserName;
            }
            set
            {
                base.UserName = value;
            }
        }

        public virtual ICollection<Order> Orders { get; set; }  //导航属性
    }

    //创建角色模型
    public class ApplicationRole : IdentityRole
    { 
        [Display(Name="角色创建时间")]
        public DateTime CreateTime { get; set; }
    }

    //数据上下文，负责管理数据库，对数据库表进行增删改查
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new
                MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    }

    public class DBInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> 
    {
        protected override void Seed(ApplicationDbContext context)
        {
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
            products.ForEach(x => context.Products.Add(x)); //该语句作用是将product列表每条记录添加到数据库中

            base.Seed(context);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Routing.Constraints;

namespace WebApplication7
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //根据商品类型
            routes.MapRoute(
                name: "CategoryRoute",
                url: "{Category}",
                defaults: new { controller = "Product", action = "Index" },
                constraints: new { Category="服装|电子产品|球类"}
            );

            //根据商品ID
            routes.MapRoute(
                name: "ProductIdRoute",
                url: "Products/{productId}",
                defaults: new { controller = "Product", action = "Index" },
                constraints: new { productId = @"\d{1}" }
            );

            //根据商品名称
            routes.MapRoute(
                name: "ProductNameRoute",
                url: "Products/{ProductName}",
                defaults: new { controller = "Product", action = "Index" }
            );

            //根据订单日期
            routes.MapRoute(
                name: "OrderDateRoute",
                url: "Orders/{date}",
                defaults: new { controller = "Order", action = "Index" },
                constraints: new { date=new DateTimeRouteConstraint()}
            );

            //根据订单商品类型
            routes.MapRoute(
                name: "OrderCategoryRoute",
                url: "Orders/{Category}",
                defaults: new { controller = "Order", action = "Index" },
                constraints: new { Category = "服装|电子产品|球类" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        [Display(Name="购买数量")]
        public int Count { get; set; }
        [Display(Name = "购买日期")]
        public DateTime Date { get; set; }
        [Display(Name = "商品编号")]
        public int ProductID { get; set; }  //外键
        public virtual Product Product { get; set; } //导航属性

        public string ApplicationUserID { get; set; } //外键
        public virtual ApplicationUser ApplicationUser { get; set; }    //导航属性
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Display(Name="商品名称")]
        public string ProductName { get; set; }
        [Display(Name = "图片")]
        public string Image { get; set; }
        [Display(Name = "价格")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Display(Name = "商品分类")]
        public string Category { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Display(Name="角色")]
        [Required]
        [StringLength(50,MinimumLength=2)]
        public string Name { get; set; }

        [Display(Name="角色创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
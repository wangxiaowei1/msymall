using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name="用户名")]
        [Required]
        [StringLength(50,MinimumLength=2)]
        public string UserName { get; set; }

        [Display(Name="角色")]
        public IList<string> Roles { get; set; }
    }
}
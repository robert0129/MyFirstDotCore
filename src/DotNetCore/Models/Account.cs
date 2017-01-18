using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.Models
{
    public class Account
    {
        public int id { get; set; }

        [Required, MaxLength(30)]
        [Display(Name = "User Name")]
        public string username { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }

        [Required]
        public string password { get; set; }
    }
}

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
        public string username { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
    }
}

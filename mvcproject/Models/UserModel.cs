using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcproject.Models
{
    public class UserModel
    {
        [Required]
        public string name { get; set; }

        [Required]
        [DataType("Password")]
        public string password { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace SimpleBlog.ViewModels
{
    public class AuthLogin
    {
        [Required]
        public String Username { get; set; }

        [Required, DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
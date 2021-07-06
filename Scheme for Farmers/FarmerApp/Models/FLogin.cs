using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmerApp.Models
{
    public class FLogin
    {
        [Key]
        
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeepCart.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string UserName { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Password { get; set; }
    }
}
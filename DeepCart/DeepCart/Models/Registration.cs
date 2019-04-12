using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeepCart.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string UserName { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        public string Gender { get; set; }
        [Phone]
        public string Phone { get; set; }

    }
}
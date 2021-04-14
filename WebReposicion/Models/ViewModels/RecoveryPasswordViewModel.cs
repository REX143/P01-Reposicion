using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebReposicion.Models.ViewModels
{
    public class RecoveryPasswordViewModel
    {
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        public string Password2 { get; set; }
    }
}
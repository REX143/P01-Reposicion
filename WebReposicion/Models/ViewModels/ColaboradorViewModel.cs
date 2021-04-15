using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebReposicion.Models.ViewModels
{
    public class ColaboradorViewModel
    {
        [Required]
        public int IDColaborador { get; set; }

        [Display(Name = "NOMBRES")]
        [Required]
        public string Nombres { get; set; }


        [Display(Name = "APELLIDOS")]
        [Required]
        public string Apellidos { get; set; }


        [Required]
        public string DNI { get; set; }


        [Display(Name = "Teléfono")]
        [Required]
        public string Telefono { get; set; }

        [EmailAddress]

        [Display(Name = "E-mail")]
        public string Correo { get; set; }


        [Display(Name = "CARGO ASIGNADO")]
        [Required]
        public string Cargo_Asignado { get; set; }

        public string Token_Security { get; set; }
        /*Info Rol*/
        [Display(Name ="ROL")]
        [Required]
        public int IDRol { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public string Observacion { get; set; }
        /*Info usuario*/
        public int IDUsuario { get; set; }
        //public int IDColaboradorU { get; set; }
        //public int IDRol { get; set; }
        [Display(Name = "USUARIO")]
        [Required]
        public string Nombre_usuario { get; set; }

        [Display(Name = "CONTRASEÑA")]
        [Required]
        public string Password { get; set; }
        public string Observaciones { get; set; }
    }
}
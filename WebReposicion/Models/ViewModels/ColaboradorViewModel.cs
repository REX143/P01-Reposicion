using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebReposicion.Models.ViewModels
{
    public class ColaboradorViewModel
    {
        public int IDColaborador { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }

        [EmailAddress]
        public string Correo { get; set; }
        public string Cargo_Asignado { get; set; }
        public string Token_Security { get; set; }
        /*Info Rol*/
        public int IDRol { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        /*Info usuario*/
        public int IDUsuario { get; set; }
        //public int IDColaboradorU { get; set; }
        //public int IDRol { get; set; }
        public string Nombre_usuario { get; set; }
        public string Password { get; set; }
        public string Observaciones { get; set; }
    }
}
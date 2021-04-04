using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebReposicion.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public int IDUsuario { get; set; }
        public int IDColaborador { get; set; }
        public int IDRol { get; set; }
        public string Nombre_usuario { get; set; }
        public string Contraseña { get; set; }
        public string Observaciones { get; set; }


    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebReposicion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        public int IDUsuario { get; set; }
        public Nullable<int> IDColaborador { get; set; }
        public Nullable<int> IDRol { get; set; }
        public string Nombre_usuario { get; set; }
        public string Contraseña { get; set; }
        public string Observaciones { get; set; }
    }
}

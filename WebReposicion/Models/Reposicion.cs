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
    
    public partial class Reposicion
    {
        public int NroReposicion { get; set; }
        public string NombreReponedor { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Estado { get; set; }
        public Nullable<int> PrioridadAtencion { get; set; }
        public Nullable<System.DateTime> FechaConfirmacion { get; set; }
        public Nullable<System.DateTime> FechaRecepcion { get; set; }
        public string UsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebReposicion.Models.ViewModels
{
    public class ReposicionViewModel
    {
        public int NroReposicion { get; set; }
        public string NombreReponedor { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int PrioridadAtencion { get; set; }
        /*****************************************/
        //public int IdDetalle { get; set; }
        //public int PkArticulo { get; set; }
        //public int Almacen { get; set; }
        //public string CodigoArticulo { get; set; }
        //public string NombreArticulo { get; set; }
        //public string Categoria { get; set; }
        //public int Cantidad { get; set; }
    }

}
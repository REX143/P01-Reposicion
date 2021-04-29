using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebReposicion.Models.ViewModels
{
    public class StockDisponibleViewModel
    {
        public int Nro { get; set; }
        public int Fk_ubicacion { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string Almacen { get; set; }
        public int Stock { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebReposicion.Models.ViewModels
{
    public class ArticuloDevViewModel
    {
        public int Pk_ma { get; set; }
        public int Pk_ubc { get; set;}
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int StockTDA { get; set; }
        public string Almacen { get; set; }
        public int Und { get; set; }
        public int Stock { get; set; }
    }
}
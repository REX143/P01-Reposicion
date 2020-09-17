using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebReposicion.Models.ViewModels
{
    public class StockViewModel
    {
        public int Pk { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public Nullable<int> Stock { get; set; }
        public Nullable<int> Comprometido { get; set; }
        public Nullable<int> Disponible { get; set; }
        public string ABC { get; set; }
        public string XYZ { get; set; }
        public Nullable<int> Predictivo { get; set; }
        public Nullable<int> Minimo { get; set; }
        public Nullable<int> Maximo { get; set; }
        public string PrioridadReposicion { get; set; }
        public string Ruta { get; set; }
    }
}
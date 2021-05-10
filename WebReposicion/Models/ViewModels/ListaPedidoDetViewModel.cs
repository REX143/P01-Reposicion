using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebReposicion.Models.ViewModels
{
    public class ListaPedidoDetViewModel
    {
        public int Nro { get; set; }
        public int NroReposicion { get; set; }
        public string CodigoArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public string Almacen { get; set; }
    }
}
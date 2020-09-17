using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReposicion.Models.ViewModels;
using WebReposicion.Models;

namespace WebReposicion.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        
        public ActionResult Index(string cadena)
        {
            List<StockViewModel> articulos;

            using (DBPREDICTIVOEntities db=new DBPREDICTIVOEntities())
            {
                if (cadena !="")
                {
                    articulos = (from d in db.sp_obtenerConsultaStock(cadena)
                                 select new StockViewModel
                                 {
                                     Pk = d.Pk,
                                     Codigo = d.Codigo,
                                     Nombre = d.Nombre,
                                     Categoria = d.Categoria,
                                     Stock = d.Stock,
                                     Comprometido = d.Comprometido,
                                     Disponible = d.Disponible,
                                     ABC = d.ABC,
                                     XYZ = d.XYZ,
                                     Predictivo = d.Predictivo,
                                     Minimo = d.Minimo,
                                     Maximo = d.Maximo,
                                     PrioridadReposicion = d.PrioridadReposicion,
                                     Ruta = d.Ruta

                                 }).ToList();
                             return View(articulos);
                }

                return Redirect("~/Home/Index");
            }
                
        }
    }
}
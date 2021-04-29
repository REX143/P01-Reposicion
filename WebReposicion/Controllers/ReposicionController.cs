using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReposicion.Models;
using WebReposicion.Models.ViewModels;

namespace WebReposicion.Controllers
{
    public class ReposicionController : Controller
    {
        // GET: Reposicion
        public ActionResult Index()
        {
            return View();
        }

       
        // CONTROLADORES: Generar pedido de reposición
        public ActionResult GenerarPedido()
        {
            List<StockDisponibleViewModel> articulos;
            articulos = null;
            using (DBPREDICTIVOEntities db=new DBPREDICTIVOEntities())
            {
                articulos = (from d in db.sp_ObtenerDisponiblexAlmacen("")
                             select new StockDisponibleViewModel
                             {
                                 Nro = Convert.ToInt32(d.Nro),
                                 Fk_ubicacion = d.FK_UBICACION,
                                 Codigo = d.Codigo,
                                 Descripcion = d.Descripcion,
                                 Categoria = d.Categoria,
                                 Almacen = d.Almacen,
                                 Stock = Convert.ToInt32(d.Stock)

                             }).ToList();

                return View(articulos);
            }
            return Redirect("~/Reposicion/GenerarPedido");
        }

        [HttpGet]
        public ActionResult GenerarPedido(string cadena)
        {
            List<StockDisponibleViewModel> articulos;
            using (DBPREDICTIVOEntities db = new DBPREDICTIVOEntities())
            {
                if (cadena != "" || cadena != null)
                {
                    articulos = (from d in db.sp_ObtenerDisponiblexAlmacen(cadena)
                                 select new StockDisponibleViewModel
                                 {
                                     Nro=Convert.ToInt32(d.Nro),
                                     Fk_ubicacion=d.FK_UBICACION,
                                     Codigo=d.Codigo,
                                     Descripcion=d.Descripcion,
                                     Categoria=d.Categoria,
                                     Almacen=d.Almacen,
                                     Stock= Convert.ToInt32(d.Stock)

                                 }).ToList();

                    ViewBag.exists = "Cargada la disponibilidad de artículos por almacén ...";
                    return View(articulos);
                }

                return Redirect("~/Reposicion/GenerarPedido");
            }

           
        }


        [HttpGet]
        public ActionResult BuscarArticulo()
        {
            ViewBag.Confirmacion = "Se realizó exitosamente la descarga del reporte.";
            return View();
        }


        // GET: Editar pedido de reposición
        public ActionResult EditarPedido()
        {
            return View();
        }


        
        // GET: Recepcionar pedido de reposición
        public ActionResult RecepcionarPedido()
        {
            return View();
        }



        // GET: Devolver pedido de reposición
        public ActionResult DevolverPedido()
        {
            return View();
        }




    }
}
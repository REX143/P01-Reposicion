using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReposicion.Models;
using WebReposicion.Models.ViewModels;
using X.PagedList;


namespace WebReposicion.Controllers
{
    public class KardexController : Controller
    {
        // GET: Kardex
        public ActionResult Index()
        {
            List<KardexViewModel> articulos = new List<KardexViewModel>();
            return View(articulos);
        }

        [HttpPost]
        public ActionResult Index(string Cadena,DateTime FechaMov)

        {
            //List<KardexViewModel> articulos = new List<KardexViewModel>();
            
            using (DBPREDICTIVOEntities db = new DBPREDICTIVOEntities())
                          
            {
                db.Database.CommandTimeout = 300;
                int total = 0;
                int totalDia = 0;

                if (Cadena != null)
                {
                 var articulos = (from d in db.sp_ObtenerHistorialKardex(FechaMov, Cadena)
                                 select new KardexViewModel
                                 {
                                     FECHA_MOV = d.FECHA_MOV,
                                     COD_ART_IMPORT = d.COD_ART_IMPORT,
                                     NUMERO_DOCUMENTO = d.NUMERO_DOCUMENTO,
                                     CANTIDADES = d.CANTIDADES,
                                     TOTAL_AL_DIA = Convert.ToInt32(d.TOTAL_AL_DIA),
                                     DESCRIP_MOV = d.DESCRIP_MOV,
                                     PC_CLIENTE = d.PC_CLIENTE


                                 }).ToList();

                    if (articulos.Count()>0)
                    {
                        total = articulos.ElementAt(0).TOTAL_AL_DIA;
                      
                        foreach (var item in articulos)
                        {
                            totalDia = totalDia + item.CANTIDADES;
                        }
                    }
                    totalDia = totalDia + total;
                    ViewBag.StockActual = total;
                    ViewBag.totalDia = totalDia;

                    return View(articulos);
                }

            }
            ViewBag.StockActual = 0;

            return Redirect("~/Kardex/Index");
        }
    }
}
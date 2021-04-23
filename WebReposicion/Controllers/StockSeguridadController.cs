using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReposicion.Models;

namespace WebReposicion.Controllers
{
    public class StockSeguridadController : Controller
    {
        // GET: StockSeguridad
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Index(bool response = false)
        {
            using (DBPREDICTIVOEntities db = new DBPREDICTIVOEntities())
            {
                db.Database.CommandTimeout = 1000;

                // Se realiza la asignación de stock de seguridad
                db.sp_AsignarStockSeguridad();

                response = true;
            }
            if (response == true)
            {
                ViewBag.Confirmacion = "Se realizo la asignación del stock de seguridad de los artículos vigentes.";

            }


            return View();
        }




        [HttpGet]
        public ActionResult btnExcelDescargar()
        {

            DataTable dt = new DataTable();
            dt = Transversal.GeneradorDataTable.dtProcedimientoAlmacenado("sp_ObtenerStockSeguridad");
            Stream s = Transversal.Reporteador.DataTableToExcel(dt);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("StockSeguridad") + ".xlsx"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
                ms.Close();
                ms.Dispose();
            }
            return View();
        }



    }
}
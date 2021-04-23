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
    public class ClasificacionController : Controller
    {
        // GET: Clasificacion
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(bool response = false)
        {
            using (DBPREDICTIVOEntities db = new DBPREDICTIVOEntities())
            {
                db.Database.CommandTimeout = 300;
                // Extracción y preparación de la data  
                db.sp_ExtraerDataHistorica();
                db.sp_PrepararDataHistorica();

                // Se realiza la clasificación a la data obtenida
                db.sp_AsignarClasificacion();

                response = true;
            }
            if (response == true)
            {
                ViewBag.Confirmacion = "Se realizo la clasificación del stock vigente.";

            }


            return View();
        }


        [HttpGet]
        public ActionResult btnExcelDescargar()
        {

            DataTable dt = new DataTable();
            dt = Transversal.GeneradorDataTable.dtProcedimientoAlmacenado("sp_ObtenerStockClasificado");
            Stream s = Transversal.Reporteador.DataTableToExcel(dt);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("StockClasificado") + ".xlsx"));
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
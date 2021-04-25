using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReposicion.Models;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace WebReposicion.Controllers
{
    public class PrediccionController : Controller
    {
        // GET: Prediccion
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(bool response=false)
        {
            using (DBPREDICTIVOEntities db=new DBPREDICTIVOEntities())
            {
                // Extracción y preparación de la data ver condicion 
                db.Database.CommandTimeout = 300;
                // Se realiza la clasificación a la data obtenida
               // db.sp_AsignarClasificacion();

                response = true;
            }
            if (response==true)
            {
                Process myProcess = Process.Start("C:\\Users\\REX\\source\\repos\\WebReposicion\\ExtraerPrediccion\\ExtraerPrediccion\\bin\\Debug\\ExtraerPrediccion.exe");

                            
            }

               
            ViewBag.Confirmacion = "Se esta ejecutando el proceso de actualización del stock predictivo.";

            return View();
        }


        [HttpGet]
        public ActionResult btnExcelDescargar()
        {

            DataTable dt = new DataTable();
            dt = Transversal.GeneradorDataTable.dtProcedimientoAlmacenado("sp_ObtenerStockPredictivo");
            Stream s = Transversal.Reporteador.DataTableToExcel(dt);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("StockPredictivo") + ".xlsx"));
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
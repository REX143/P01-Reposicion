using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReposicion.Controllers
{
    public class StockSeguridadController : Controller
    {
        // GET: StockSeguridad
        public ActionResult Index()
        {
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
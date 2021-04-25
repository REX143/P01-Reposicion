using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReposicion.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }



        // GET: Reporte Stock 0
        public ActionResult ReporteStockCero()
        {
            return View();
        }

        [HttpGet]
        public ActionResult btnExcelReporteStocCero()
        {

            DataTable dt = new DataTable();
            dt = Transversal.GeneradorDataTable.dtProcedimientoAlmacenado("sp_ObtenerArticulosStockCero");
            Stream s = Transversal.Reporteador.DataTableToExcel(dt);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("ArticulosStockCero") + ".xlsx"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
                ms.Close();
                ms.Dispose();
            }

            ViewBag.Confirmacion = "Se realizó exitosamente la descarga del reporte.";
            return View();
        }

        // GET: Reporte Stock sin rotación
        public ActionResult ReporteStockSinRotacion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult btnExcelReporteStockSinRotacion()
        {

            DataTable dt = new DataTable();
            dt = Transversal.GeneradorDataTable.dtProcedimientoAlmacenado("sp_ObtenerArticulosSinRotacion");
            Stream s = Transversal.Reporteador.DataTableToExcel(dt);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("ArticulosSinRotacion") + ".xlsx"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
                ms.Close();
                ms.Dispose();
            }

            ViewBag.Confirmacion = "Se realizó exitosamente la descarga del reporte.";
            return View();
        }

        // GET: Reporte clasificación de stock
        public ActionResult ReporteStockClasificacion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult btnExcelReporteStockClasificacion()
        {

            DataTable dt = new DataTable();
            dt = Transversal.GeneradorDataTable.dtProcedimientoAlmacenado("sp_ObtenerArticulosClasificado");
            Stream s = Transversal.Reporteador.DataTableToExcel(dt);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("ArticulosClasificados") + ".xlsx"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
                ms.Close();
                ms.Dispose();
            }

            ViewBag.Confirmacion = "Se realizó exitosamente la descarga del reporte.";
            return View();
        }

    }
}
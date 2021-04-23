﻿using System;
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
    public class PriorizacionController : Controller
    {
        // GET: Priorizacion
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
              
                // Se realiza la asignación de priorización
                db.sp_AsignarPrioridadReposicion();

                response = true;
            }
            if (response == true)
            {
                ViewBag.Confirmacion = "Se realizo la asignación de prioridad al stock vigente.";

            }


            return View();
        }



        [HttpGet]
        public ActionResult btnExcelDescargar()
        {

            DataTable dt = new DataTable();
            dt = Transversal.GeneradorDataTable.dtProcedimientoAlmacenado("sp_ObtenerStockPrioridad");
            Stream s = Transversal.Reporteador.DataTableToExcel(dt);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("StockPrioridad") + ".xlsx"));
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
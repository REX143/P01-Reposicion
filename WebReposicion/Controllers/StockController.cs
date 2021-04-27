using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReposicion.Models.ViewModels;
using WebReposicion.Models;
using System.IO;
using System.Net.Http;
//using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WebReposicion.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        public ActionResult Index(string cadena)
        {
            List<StockViewModel> articulos;

            using (DBPREDICTIVOEntities db=new DBPREDICTIVOEntities())
            {
                if (cadena !="" || cadena !=null)
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
                              
                return Redirect("~/Stock/Index");
            }
                
        }

        // GET: Artículos a reponer
        public ActionResult ArticulosaReponer()
        {
            return View();
        }

        [HttpGet]
        public ActionResult btnExcelReporteaReponer()
        {

            DataTable dt = new DataTable();
            dt = Transversal.GeneradorDataTable.dtProcedimientoAlmacenado("sp_ObtenerArticulosaReponer");
            Stream s = Transversal.Reporteador.DataTableToExcel(dt);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("ArticulosaReponer") + ".xlsx"));
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



        // GET: Listar pedidos de reposición realizados
        public ActionResult ListarPedidos()
        {
            return View();
        }




    }





    //    static async Task InvokeRequestResponseService()
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            var s = "125555";
    //            var scoreRequest = new
    //            {

    //                Inputs = new Dictionary<string, StringTable>() {
    //                        {

    //                            "input1",
    //                            new StringTable()
    //                            {
    //                                ColumnNames = new string[] {"CODIGO;", ";DIA1;", ";DIA2;", ";DIA3;", ";DIA4;", ";DIA5;", ";DIA6;", ";DIA7;", ";DIA8;", ";DIA9;", ";DIA10;", ";DIA11"},
    //                                Values = new string[,] {  { s, "0", "2", "2", "1", "8", "8", "8", "8", "8", "8", "8" }}
    //                            }
    //                        },
    //                    },
    //                GlobalParameters = new Dictionary<string, string>() {
    //        { "Account name", "" },
    //}
    //            };
    //            const string apiKey = "nOBO1gD67I9AaAN0I25pRXzCJF+nNanb4kOnxMS6zU403hJxITfWt4Y/UfFK2Eyp/Zwlkb10zJhBEexuhiqLXQ=="; // Replace this with the API key for the web service
    //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

    //            client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/3c39f696d1ff4002b750eeef43304837/services/d8170eca54304ce0b9865cbc42c5ca2c/execute?api-version=2.0&details=true");

    //            // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
    //            // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
    //            // For instance, replace code such as:
    //            //      result = await DoSomeTask()
    //            // with the following:
    //            //      result = await DoSomeTask().ConfigureAwait(false)


    //            HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

    //            if (response.IsSuccessStatusCode)
    //            {
    //                string result = await response.Content.ReadAsStringAsync();
    //                Console.WriteLine("Result: {0}", result);
    //            }
    //            else
    //            {
    //                Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

    //                // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
    //                Console.WriteLine(response.Headers.ToString());

    //                string responseContent = await response.Content.ReadAsStringAsync();
    //                Console.WriteLine(responseContent);
    //            }
    //        }
    //    }




    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

}
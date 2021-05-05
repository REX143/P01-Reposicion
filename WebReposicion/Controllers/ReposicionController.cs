using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReposicion.Models;
using WebReposicion.Models.ViewModels;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

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
                db.Database.CommandTimeout = 300;
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

               
            }

            return View(articulos);
            //return Redirect("~/Reposicion/GenerarPedido");
        }


        // GET: Reposicion
        public ActionResult BuscarArticulo()
        {
            return View();
        }





        [HttpGet]
        public ActionResult GenerarPedido(string cadena)
        {
          
            List<StockDisponibleViewModel> articulos;
            using (DBPREDICTIVOEntities db = new DBPREDICTIVOEntities())
            {
                db.Database.CommandTimeout = 300;
                if (cadena != "" || cadena != null)
                {
                    articulos = (from d in db.sp_ObtenerStockDisponiblesxAlmacen(cadena)
                                 select new StockDisponibleViewModel
                                 {
                                     Nro=Convert.ToInt32(d.Nro),
                                     Fk_ubicacion=d.FK_UBICACION,
                                     PkMA=d.FK_MAESTRO_ARTICULOS,
                                     Codigo=d.Codigo,
                                     Descripcion=d.Descripcion,
                                     Categoria=d.Categoria,
                                     Almacen=d.Almacen,
                                     Stock= Convert.ToInt32(d.Stock),
                                     StockSolicitar=0

                                 }).ToList();
                    if (articulos.Count>0)
                    {
                        ViewBag.Confirmacion = "Cargada la disponibilidad de artículos en almacén ...";
                    }

                    // Lista temporal de pedidos
                    List<ReposicionDetViewModel> detallePedido;
                    using (DBPREDICTIVOEntities dba=new DBPREDICTIVOEntities())
                    {
                        detallePedido = (from d in dba.DetalleReposicion
                                         select new ReposicionDetViewModel
                                         {
                                             IdDetalle=d.IdDetalle,
                                             NroReposicion=(d.NroReposicion.Value),
                                             PkArticulo= (d.PkArticulo.Value),
                                             Almacen=d.Almacen.Value,
                                             CodigoArticulo=d.CodigoArticulo,
                                             NombreArticulo=d.NombreArticulo,
                                             Categoria=d.Categoria,
                                             Cantidad= (d.Cantidad.Value)

                                         }).ToList();

                        if (detallePedido.Count>0)
                        {

                            ViewBag.NroReposicion = detallePedido.FirstOrDefault().NroReposicion;
                        }

                    }

                    ViewBag.detallePedido = detallePedido;

                    return View(articulos);
                }

                return Redirect("~/Reposicion/GenerarPedido");
            }

           
        }



        [HttpGet]
        public  ActionResult GenerarTemporal(int PkArticulo, int Fk_ubicacion,string Codigo,string Descripcion,string Categoria, int Stock, int stockSol)
        {

            if (stockSol <= Stock)
            {
                using (TransactionScope registro=new TransactionScope())
                { // Apertura del rollback en el contexto
                   

                    try
                    {
                        using (SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["BDPREDICTIVO"].ToString()))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                // Sp creado para  cargar los pedidos temporales 
                                cmd.CommandText = "sp_GenerarPedidotemporal";
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@Reponedor", Session["NameUser"].ToString());
                                cmd.Parameters.AddWithValue("@Estado", "TEMPORAL");
                                cmd.Parameters.AddWithValue("@PrioridadAtencion", 1);
                                cmd.Parameters.AddWithValue("@PkArticulo", PkArticulo);
                                cmd.Parameters.AddWithValue("@PkAlmacen", Fk_ubicacion);
                                cmd.Parameters.AddWithValue("@CodigoArticulo", Codigo);
                                cmd.Parameters.AddWithValue("@NombreArticulo", Descripcion);
                                cmd.Parameters.AddWithValue("@Categoria", Categoria);
                                cmd.Parameters.AddWithValue("@Cantidad", stockSol);
                                cmd.Connection = sqlConnection1;
                                sqlConnection1.Open();
                                cmd.ExecuteNonQuery();

                                registro.Complete();
                            }
                        }

                        return Redirect("~/Reposicion/GenerarPedido");

                    }
                    catch (Exception)
                    {
                        registro.Dispose();
                        throw;
                    }
      
                }
            }

            else
            {
                ViewBag.Confirmacion = "Verifique la cantidad ingresada no puede ser mayor al stock disponible";
            }
               
           //List<StockDisponibleViewModel> articulos=null;

            return Redirect("~/Reposicion/GenerarPedido");

        }


        [HttpPost]
        public ActionResult ConfirmarPedido(int NroReposicion)
        {

            ViewBag.Confirmacion = "Se ha generado el pedido en almacenes.";
            return Redirect("~/Reposicion/GenerarPedido");
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
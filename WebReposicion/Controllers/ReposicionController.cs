using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReposicion.Controllers
{
    public class ReposicionController : Controller
    {
        // GET: Reposicion
        public ActionResult Index()
        {
            return View();
        }


        // GET: Generar pedido de reposición
        public ActionResult GenerarPedido()
        {
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
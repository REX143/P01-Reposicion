using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReposicion.Models;

namespace WebReposicion.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string User, string Pass)
        {
            try
            {
                using (DBPREDICTIVOEntities db = new DBPREDICTIVOEntities())
                {
                    var oUser = (from d in db.Usuario
                                 where d.Nombre_usuario == User.Trim() && d.Contraseña == Pass.Trim()
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "¡Acceso incorrecto! Asegurese de ingresar correctamente sus credenciales.";
                        return View();
                    }
                    Session["User"] = oUser.Nombre_usuario;
                    ViewBag.user = oUser;

                }
                //return Redirect("~/Tabla/");
                return RedirectToAction("Index", "Stock");
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }

        }


        // GET: Acceso
        public ActionResult Error()
        {
            return View();
        }

    }
}
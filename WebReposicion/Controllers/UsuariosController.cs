using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReposicion.Models;
using WebReposicion.Models.ViewModels;

namespace WebReposicion.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            List<ColaboradorViewModel> lst;
            using (DBPREDICTIVOEntities db=new DBPREDICTIVOEntities())
            {
                lst = (from d in db.sp_GetUsuarios()
                       select new ColaboradorViewModel
                       {
                           IDColaborador = d.IDColaborador,
                           Nombres = d.Nombres,
                           Apellidos = d.Apellidos,
                           DNI = d.DNI,
                           Telefono = d.Telefono,
                           Correo = d.Correo,
                           Cargo_Asignado = d.Cargo_Asignado,
                           Descripcion=d.Descripcion,
                           Observacion=d.Observacion,
                           Nombre_usuario=d.Nombre_usuario,
                           Password=d.Password,
                           Observaciones=d.Observaciones
                           
                       }).ToList();
            }
            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(ColaboradorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                 using (DBPREDICTIVOEntities db= new DBPREDICTIVOEntities())
                {
                    var oColaborador = new Colaborador();
                    oColaborador.Nombres = model.Nombres;
                    oColaborador.Apellidos = model.Apellidos;
                    oColaborador.DNI = model.DNI;
                    oColaborador.Telefono = model.Telefono;
                    oColaborador.Correo = model.Correo;
                    oColaborador.Cargo_Asignado = model.Cargo_Asignado;
                    db.Colaborador.Add(oColaborador);
                    db.SaveChanges();

                    int IDColaborador = oColaborador.IDColaborador;
                    var oUser = new Usuario();
                    oUser.IDColaborador = IDColaborador;
                    oUser.IDRol = model.IDRol;
                    oUser.Nombre_usuario = model.Nombre_usuario;
                    oUser.Password = model.Password;
                    oUser.Observaciones = model.Observaciones;
                    db.Usuario.Add(oUser);
                    db.SaveChanges();
                }
                    return Redirect("~/Usuarios/");
                }

                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }


    }
}
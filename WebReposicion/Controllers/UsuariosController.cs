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
                           Observaciones=d.Observaciones,
                           IDUsuario=d.IDUsuario,
                           
                       }).ToList();
            }
            return View(lst);
        }

        public ActionResult Nuevo()
        {
            List<RolViewModel> lst = null;
            using (DBPREDICTIVOEntities db=new DBPREDICTIVOEntities())
            {
                    lst=(from d in db.Rol
                     select new RolViewModel
                     { IDRol=d.IDRol,
                     Descripcion=d.Descripcion
                     }).ToList();
            }

            List<SelectListItem> items = lst.ConvertAll(d =>
              {
                  return new SelectListItem()
                  {
                      Text = d.Descripcion.ToString(),
                      Value = d.IDRol.ToString(),
                     // Selected = false
                  };
              });


            ViewBag.items = items;
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(ColaboradorViewModel model)
        {
            
            try
            {
                //if (ModelState.IsValid)
                //{

                 using (DBPREDICTIVOEntities db= new DBPREDICTIVOEntities())
                {
                    var oColaborador = new Colaborador();
                    oColaborador.Nombres = model.Nombres.ToUpper();
                    oColaborador.Apellidos = model.Apellidos.ToUpper();
                    oColaborador.DNI = model.DNI;
                    oColaborador.Telefono = model.Telefono;
                    oColaborador.Correo = model.Correo.ToUpper();
                    oColaborador.Cargo_Asignado = model.Cargo_Asignado.ToUpper();
                    db.Colaborador.Add(oColaborador);
                    db.SaveChanges();

                    int IDColaborador = oColaborador.IDColaborador;
                    var oUser = new Usuario();
                    oUser.IDColaborador = IDColaborador;
                    oUser.IDRol = Convert.ToInt32(model.IDRol);
                    oUser.Nombre_usuario = model.Nombre_usuario.ToUpper();
                    oUser.Password = model.Password;
                    oUser.Observaciones = model.Observaciones.ToUpper();
                    db.Usuario.Add(oUser);
                    db.SaveChanges();
                }
                    return Redirect("~/Usuarios/");
                //}

                //return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }


        public ActionResult Editar(int Id)
        {

            ColaboradorViewModel model = new ColaboradorViewModel();
            using (DBPREDICTIVOEntities db=new DBPREDICTIVOEntities())
            {
                var oColaborador = db.Colaborador.Find(Id);

                model.Nombres = oColaborador.Nombres;
                model.Apellidos = oColaborador.Apellidos;
                model.DNI = oColaborador.DNI;
                model.Telefono = oColaborador.Telefono;
                model.Correo = oColaborador.Correo;
                model.Cargo_Asignado = oColaborador.Cargo_Asignado;
                model.IDColaborador = oColaborador.IDColaborador;

               var oUser = db.Usuario.FirstOrDefault(d => (int)d.IDColaborador == model.IDColaborador);// . Select("").FirstOrDefault(x => (int)x["RowNo"] == 1)
                model.IDUsuario = oUser.IDUsuario;
                model.IDRol = oUser.IDRol.Value;
                model.Nombre_usuario = oUser.Nombre_usuario;


            }


            List<RolViewModel> lst = null;
            using (DBPREDICTIVOEntities db = new DBPREDICTIVOEntities())
            {
                lst = (from d in db.Rol
                       select new RolViewModel
                       {
                           IDRol = d.IDRol,
                           Descripcion = d.Descripcion
                       }).ToList();
            }

            List<SelectListItem> items = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion.ToString(),
                    Value = d.IDRol.ToString(),
                    // Selected = false
                };
            });


            ViewBag.items = items;




            return View(model);
        }



        [HttpPost]
        public ActionResult Editar(ColaboradorViewModel model)
        {

            try
            {
                //if (ModelState.IsValid)
                //{

                using (DBPREDICTIVOEntities db = new DBPREDICTIVOEntities())
                {
                    var oColaborador = db.Colaborador.Find(model.IDColaborador);
                    oColaborador.Nombres = model.Nombres.ToUpper();
                    oColaborador.Apellidos = model.Apellidos.ToUpper();
                    oColaborador.DNI = model.DNI;
                    oColaborador.Telefono = model.Telefono;
                    oColaborador.Correo = model.Correo.ToUpper();
                    oColaborador.Cargo_Asignado = model.Cargo_Asignado.ToUpper();
                    db.Entry(oColaborador).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    var oUser = db.Usuario.FirstOrDefault(d => (int)d.IDColaborador == model.IDColaborador);
                    //oUser.IDColaborador = IDColaborador;
                    oUser.IDRol = Convert.ToInt32(model.IDRol);
                    oUser.Nombre_usuario = model.Nombre_usuario.ToUpper();
                    //oUser.Password = model.Password;
                    oUser.Observaciones = model.Observaciones.ToUpper();
                    db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Redirect("~/Usuarios/");
                //}

                //return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


        [HttpGet]
        public ActionResult Eliminar(int Id)
        {

            ColaboradorViewModel model = new ColaboradorViewModel();
            using (DBPREDICTIVOEntities db = new DBPREDICTIVOEntities())
            {
                var oColaborador = db.Colaborador.Find(Id);
                model.IDColaborador = oColaborador.IDColaborador;

                var oUser = db.Usuario.FirstOrDefault(d => (int)d.IDColaborador == model.IDColaborador);// . Select("").FirstOrDefault(x => (int)x["RowNo"] == 1)
                model.IDUsuario = oUser.IDUsuario;

                db.Usuario.Remove(oUser);
                db.SaveChanges();

                db.Colaborador.Remove(oColaborador);
                db.SaveChanges();

            }
            return Redirect("~/Usuarios/");
        }





    }
}
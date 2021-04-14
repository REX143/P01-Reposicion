using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebReposicion.Models;
using WebReposicion.Models.ViewModels;

namespace WebReposicion.Controllers
{
    public class AccesoController : Controller
    {
        string UrlDomain = "http://localhost:52656/";
        // GET: Acceso
        public ActionResult Index()
        {

            //Session["User"] = "";
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
                                 where d.Nombre_usuario == User.Trim() && d.Password == Pass.Trim()
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "¡Acceso incorrecto! Asegurese de ingresar correctamente sus credenciales.";
                        return View();
                    }
                    
                    Session["User"] = oUser;
                    Session["NameUser"] = oUser.Nombre_usuario;
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
        public ActionResult UpdatePassword()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePassword(string User, string Pass, string NewPass, string NewPassConfirm)
        {
            UsuarioViewModel model = new UsuarioViewModel();

            try
            {
                using (var db = new DBPREDICTIVOEntities())
                {

                    var oUser = (from d in db.Usuario
                                 where d.Nombre_usuario == User.Trim() && d.Password == Pass.Trim()
                                 select d).FirstOrDefault();

                    if (oUser.Nombre_usuario == User && oUser.Password == Pass && NewPass == NewPassConfirm)
                    {
                        var oUserID = db.Usuario.Find(oUser.IDUsuario);
                        model.IDUsuario = oUserID.IDUsuario;
                        model.Password = NewPass;

                        ViewBag.confirm = "Credenciales actualizadas con exito";
                       return View(Content(Update(model)));

                    }
                    else
                    {
                        ViewBag.Error = "¡Hay datos que no corresponden a la cuenta y/o las contraseñas no coinciden. No se realizo la actualización.";
                        return View();
                    }



                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "¡Hay datos que no corresponden a la cuenta y/o las contraseñas no coinciden. No se realizo la actualización.";
                return View();
            }
          

           
        }


        public string Update(UsuarioViewModel model)
        {
            try
            {

                using (var db=new DBPREDICTIVOEntities())
                {
                    var oUser = db.Usuario.Find(model.IDUsuario);
                    oUser.Password = model.Password;

                    db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return ("Contraseña actualizada con exito");//RedirectToAction("Index", "Stock");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return ViewBag.Error;
            }
        }

        //GET: Acceso
        public ActionResult Error()
        {
            return View();
        }

        //Para recuperar contraseñas
        [HttpGet]
        public ActionResult StartRecovery()
        {
            RecoveryViewModel model = new RecoveryViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult StartRecovery(RecoveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string token = GetSha256(Guid.NewGuid().ToString());

            using (Models.DBPREDICTIVOEntities db=new DBPREDICTIVOEntities())
            {
                var oUser = db.Colaborador.Where(d => d.Correo == model.Email).FirstOrDefault();
                if (oUser!=null)
                {
                    oUser.Token_Security = token;
                
                    db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    //Enviar correo
                    SendEmail(oUser.Correo,token);
                }
            }

            ViewBag.Message = "Recibira un correo para poder actualizar su contraseña.";
            return View();
        }

        [HttpGet]
        public ActionResult Recovery(string Token)
        {
            RecoveryPasswordViewModel model = new RecoveryPasswordViewModel();
            using (Models.DBPREDICTIVOEntities db=new DBPREDICTIVOEntities())
            {
                model.Token = Token;

                if (model.Token == null || model.Token.Trim().Equals(""))
                {
                    return View("Index");
                }
                var oUser = db.Colaborador.Where(d => d.Token_Security == model.Token).FirstOrDefault();
                if (oUser==null)
                {
                    ViewBag.Error = "Token expirado. Vuelva a enviar su solicitud.";
                    return View("Index");
                }
            }


          
            return View(model);
        }

        [HttpPost]
        public ActionResult Recovery(RecoveryPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                using (Models.DBPREDICTIVOEntities db=new DBPREDICTIVOEntities())
                {
                    var oUser = db.Colaborador.Where(d=>d.Token_Security==model.Token).FirstOrDefault();

                    if (oUser!=null)
                    {
                        var oUserSystem = db.Usuario.Where(d => d.IDColaborador == oUser.IDColaborador).FirstOrDefault();
                        oUserSystem.Password = model.Password;
                        oUser.Token_Security = null;
                        db.Entry(oUserSystem).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            ViewBag.Message = "Ya puedes ingresar al sistema con tu nueva contraseña.";
            return View("Index");
        }

        #region HELPERS
        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void SendEmail(string EmailDestino, string Token)
        {
            string EmailOrigen = "HERALDAVA@GMAIL.COM";
            string Contraseña = "/HERBERTSRS";
            string url = UrlDomain+"/Acceso/Recovery/?token="+ Token;
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de contraseña",
                                     "<p>Correo para recuperación de contraseña de usuario - Sistema de Reposición de Stock</p>" +
                                     "<a href='"+url+"'>Clic para recuperar</a>");

            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen,Contraseña);

            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();
        }
        #endregion

    }
}
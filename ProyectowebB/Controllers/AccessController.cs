using ProyectowebB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectowebB.Controllers
{
    public class AccessController : Controller
    {
       
        RecoveryModel model = new RecoveryModel();

        [HttpGet]
        public ActionResult StartRecovery()
        {
            Models.RecoveryModel model = new Models.RecoveryModel();
            return View(model);
        }

        // Enviar el token generado a BD
        [HttpPost]
        public ActionResult StartRecovery(Models.RecoveryModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string token = model.GetSha256(Guid.NewGuid().ToString());

                using (ModeloBD.ProyectoFinal_KN_BDEntities db = new ModeloBD.ProyectoFinal_KN_BDEntities())
                {
                    var vUsuario = db.User_tb.Where(d => d.email == model.Email).FirstOrDefault();
                    if (vUsuario != null)
                    {
                        vUsuario.token_recovery = token;
                        db.Entry(vUsuario).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        //Enviar el Correo 
                        model.SendEmail(vUsuario.email, token);
                    }
                }

                return View();

            }catch(Exception ex)
            {
                UsuarioModel UsuarioModel = new UsuarioModel();
                UsuarioModel.RegistrarBitacora("Access-StartRecovery", ex.Message);
                return View("/Views/Home/Index.cshtml");

            }

            }
        
    // Este Action va a recibir el token

    [HttpGet]
        public ActionResult Recovery(string token)
        {
            Models.RecoveryPassword model = new Models.RecoveryPassword();
            model.token = token;

            using (ModeloBD.ProyectoFinal_KN_BDEntities db = new ModeloBD.ProyectoFinal_KN_BDEntities())
            {

                if (model.token == null || model.token.Trim().Equals(""))
                {
                    return View("/Views/Home/Index.cshtml");
                }
                var vUsuario = db.User_tb.Where(d => d.token_recovery == model.token).FirstOrDefault();

                if (vUsuario == null)
                {
                    ViewBag.Error = "Tu token ha expirado";
                    return View("/Views/Home/Index.cshtml");
                }
            }

            return View(model);
        }

        // Hacer el cambio de password en la BD

        [HttpPost]
        public ActionResult Recovery(Models.RecoveryPassword model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using (ModeloBD.ProyectoFinal_KN_BDEntities db = new ModeloBD.ProyectoFinal_KN_BDEntities())
                {
                    var vUsuario = db.User_tb.Where(d => d.token_recovery == model.token).FirstOrDefault();

                    if (vUsuario != null)
                    {
                        vUsuario.password = model.Password;
                        vUsuario.token_recovery = null;
                        db.Entry(vUsuario).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }catch(Exception ex)
            {
                UsuarioModel usuarioModel = new UsuarioModel();
                usuarioModel.RegistrarBitacora("Access-Recovery", ex.Message);
                return View("/Views/Home/Index.cshtml");
            }

            ViewBag.Message = "Contraseña recuperada exitosamente";
            return View("/Views/Home/Index.cshtml");
        }


        [HttpPost]
        public ActionResult BuscarCorreo(string correo)
        {
            try
            {
                var resultado = model.BuscarCorreo(correo);
                return Json(resultado, JsonRequestBehavior.AllowGet);

            }catch(Exception ex)
            {
                UsuarioModel UsuarioModel = new UsuarioModel();
                UsuarioModel.RegistrarBitacora("Access-BuscarCorreo", ex.Message);
                return Json(null, JsonRequestBehavior.DenyGet);
            }
            
        }

    }

}
using ProyectowebB.Entities;
using ProyectowebB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProyectowebB.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel UsuarioModel = new UsuarioModel();

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch(Exception ex) 
            {
                UsuarioModel.RegistrarBitacora("Home-Index", ex.Message);
                return View("Index");
            }
            
        }

        // Se crean las variables de sesion y se redirige
        [HttpPost]
        public ActionResult Principal(UsuarioEnt entidad) {
            try
            {
                var resultado = UsuarioModel.ValidarUsuario(entidad); 
                if (resultado != null)
                {
                    Session["IdUsuario"] = resultado.IdUsuario;
                    Session["CorreoUsuario"] = resultado.CorreoElectronico; 
                    return View();  
                }
                else{
                    ViewBag.mensajeError = "Sus credenciales son incorrectas";
                    return View("Index");
                }

            }catch(Exception ex)
            {
                UsuarioModel.RegistrarBitacora("Home-Principal", ex.Message);
                ViewBag.mensajeError = "Sus credenciales no fueron validadas";
                return View("Index");
            }
        
        }

        // Faltan metodos para registrar y buscar correo

    }

}
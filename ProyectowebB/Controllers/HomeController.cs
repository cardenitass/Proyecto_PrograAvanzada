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
        [HttpGet]
        public ActionResult Index()
        {
            UsuarioModel model = new UsuarioModel();
            
            return View();
        }
        
    
    [HttpPost]
    public ActionResult IniciarSeccion(UsuarioEnt entidad)
    {
        UsuarioModel model = new UsuarioModel();
        model.ValidarUsuario();
        return View();
        }
    }

}
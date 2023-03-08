using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectowebB.Entities
{
    public class UsuarioEnt
    {
        //Todos los atributos
      
            public int IdUsuario { get; set; }
            public string CorreoElectronico { get; set; }
            public string Contrasenna { get; set; }
            public bool Estado { get; set; }

    }
}
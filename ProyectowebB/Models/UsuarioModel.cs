using ProyectowebB.Entities;
using ProyectowebB.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectowebB.Models
{
    public class UsuarioModel
    {
      
        // Metodo para validar que el usuario exista
        public UsuarioEnt ValidarUsuario(UsuarioEnt entidad)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var resultado = (from x in conexion.User_tb
                                 where x.email == entidad.CorreoElectronico
                                 && x.password == entidad.Contrasenna
                                 && x.active == true
                                 select x).FirstOrDefault();

                UsuarioEnt entidadResultado = new UsuarioEnt();
                if (resultado != null)
                {
                    entidadResultado.IdUsuario = resultado.id_user;
                    entidadResultado.CorreoElectronico = resultado.email;
                    return entidadResultado;
                }

                return null;
            }
        }


        // Registrar errores de usuarios NO logueados en el sistema
        public void RegistrarBitacora(string origen, string descripcion)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                Binnacle bitacora = new Binnacle();
                bitacora.date_time = DateTime.Now;
                bitacora.origin = origen;
                bitacora.description = descripcion;

                conexion.Binnacle.Add(bitacora);
                conexion.SaveChanges();
            }
        }

        // Registrar errores de usuarios logueados en el sistema
        public void RegistrarErrores(object usuario, string origen, string descripcion)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                Errors error = new Errors();
                error.id_user = int.Parse(usuario.ToString());
                error.date_time = DateTime.Now;
                error.origin = origen;
                error.description = descripcion;

                conexion.Errors.Add(error);
                conexion.SaveChanges();
            }
        }

    }

}

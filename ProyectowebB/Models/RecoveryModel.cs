using ProyectowebB.ModeloBD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ProyectowebB.Models
{
    public class RecoveryModel
    {
        // ------------------- RELLENAR URL-----------------------------//
        string urlDomain = "https://localhost:44307/Access/Recovery";

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string BuscarCorreo(string correoElectronico)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var resultado = (from x in conexion.User_tb
                                 where x.email == correoElectronico
                                 select x).FirstOrDefault();

                if (resultado == null)
                    return string.Empty;
                else
                {
                    if (resultado.active)
                        return "";
                    else
                        return "Esta cuenta de correo se encuentra inactiva";
                }
            }
        }

        #region HELPERS

        // Metodo de Encriptacion del Token
        public string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        // Metodo para enviar correo por SMTP
        public void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = "dcardenas90058@ufide.ac.cr";
            string Contrasenna = "Machito2014";
            string url = urlDomain + "?token=" + token;

            MailMessage vMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de Contraseña",
                "<p>Correo para recuperación de contraseña </p><br/>" +
                "<a href ='" + url + "'>Click para recuperar </a>"); ;

            vMailMessage.IsBodyHtml = true;
            SmtpClient vSmtpClient = new SmtpClient("smtp.office365.com");
            vSmtpClient.EnableSsl = true;
            vSmtpClient.UseDefaultCredentials = true;
            vSmtpClient.Port = 587;
            vSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contrasenna);

            vSmtpClient.Send(vMailMessage);
            vSmtpClient.Dispose();
        }

        #endregion

    }
}
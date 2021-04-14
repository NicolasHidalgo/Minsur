using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Usuario es requerido."), AllowHtml]
        public string Username { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida.")]
        [AllowHtml]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsPersistent { get; set; }

        public string GoogleSiteKey { get; set; }
        public string GoogleSecretKey { get; set; }
        public string FullUrl { get; set; }
    }
}
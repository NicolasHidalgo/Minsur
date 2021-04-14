using Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.ViewModels
{
    public class UsuarioLogin
    {
        //[Required]
        //[Display(Name = "Nombre de Usuario")]
        //public string NombreUsuario { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Contraseña")]
        //public string Contrasenia { get; set; }
    }
    public class UsuarioEdit
    {
        public SEG_UsuarioBean Usuario { get; set; }
        public IEnumerable<ExtendedSelectListItem> Roles { get; set; } = new HashSet<ExtendedSelectListItem>();
    }
}
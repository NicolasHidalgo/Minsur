using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class SEG_UsuarioBean
    {
        public int? ide_usuario { get; set; }
        public string cod_usuario { get; set; }
        public string nom_usuario { get; set; }
        public string cod_personal { get; set; }
        public string est_usuario { get; set; }
        public string correo_electronico { get; set; }
        public DateTime? fec_creacion { get; set; }
        public DateTime? fec_cese { get; set; }
        public string direccion_usuario { get; set; }
        public string cod_aplicacion { get; set; }
        public string des_usuario { get; set; }
        public string cod_unidad_negocio { get; set; }

        public string mensaje { get; set; }
        public string perfil { get; set; }
        public string search { get; set; }
    }
    public class UsuarioFilter
    {
        public string cod_usuario { get; set; }
        public string nom_usuario { get; set; }
        public string correo_electronico { get; set; }
    }
}

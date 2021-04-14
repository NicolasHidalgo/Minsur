using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class KCS_CecoBean
    {
        public string cod_unidad_negocio { get; set; }
        public long cod_centro_costo { get; set; }
        public string nom_centro_costo { get; set; }
        public string ceco_detallado { get; set; }
        public string nivel1 { get; set; }
        public string nivel2 { get; set; }
        public string nivel3 { get; set; }
        public string cod_grupo_costo { get; set; }
        public string nom_grupo_costo { get; set; }
        public string ubicacion { get; set; }
        public string cod_grupo_mantenimiento { get; set; }
        public string des_grupo_mantenimiento { get; set; }
        public int estado { get; set; }
        public string grupo1 { get; set; }
        public string grupo2 { get; set; }
        public string grupo3 { get; set; }
        public string grupo4 { get; set; }
        public string grupo5 { get; set; }
        public string observaciones { get; set; }
        public int periodo { get; set; }
        public string accion { get; set; }
    }
}

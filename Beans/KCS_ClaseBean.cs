using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class KCS_ClaseBean
    {
        public string cod_unidad_negocio { get; set; }
        public string nom_unidad_negocio { get; set; }
        public string cod_cuenta { get; set; }
        public string nom_cuenta { get; set; }
        public string cod_categoria { get; set; }
        public string nom_categoria { get; set; }
        public string tip_cuenta { get; set; }
        public string tip_gasto { get; set; }
        public string nom_tip_gasto { get; set; }
        public int periodo { get; set; }
        public string grupo_costo { get; set; }
        public string accion { get; set; }
    }
}

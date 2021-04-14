using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class KCS_PartidaBean
    {
        public string cod_unidad_negocio { get; set; }
        public int periodo { get; set; }
        public int? secuencia { get; set; }
        public string cod_area { get; set; }
        public string area { get; set; }
        public string proceso_01 { get; set; }
        public string proceso_02 { get; set; }
        public string proceso_03 { get; set; }
        public long cod_centro_costo { get; set; }
        public string nom_centro_costo { get; set; }
        public string cod_partida { get; set; }
        public string nom_partida { get; set; }
        public string cod_material { get; set; }
        public string nom_material { get; set; }

        public string accion { get; set; }
        public string search { get; set; }

    }
}

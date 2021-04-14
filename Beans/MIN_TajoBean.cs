using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class MIN_TajoBean
    {
        public string cod_tajo { get; set; }
        public string cod_tajo_old { get; set; }
        public string cod_tajo_tipo { get; set; }
        public string cod_tajo_interno { get; set; }
        public string nom_tajo_tipo { get; set; }
        public int cod_tajo_estructura { get; set; }
        public string nom_tajo_estructura { get; set; }
        public string nom_tajo { get; set; }
        public DateTime? fec_inicio { get; set; }
        public DateTime? fec_fin { get; set; }
        public string fase { get; set; }
        public string tip_mineral { get; set; }

        public string search { get; set; }
        public string accion { get; set; }
        public string cod_unidad_negocio { get; set; }
    }

    public class MIN_TipoTajoBean
    {
        public string cod_tajo_tipo { get; set; }
        public string nom_tajo_tipo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class MIN_EstructuraBean
    {
        public string cod_unidad_negocio { get; set; }
        public int? cod_tajo_estructura { get; set; }
        public int? cod_tajo_estructura_old { get; set; }
        public string nom_tajo_estructura { get; set; }
        public string tip_mineral { get; set; }
        public string equivalencia { get; set; }
        public string cod_interno { get; set; }
        public string vacio { get; set; }
        public string search { get; set; }
        public string accion { get; set; }
    }

    public class MIN_TipoMineralBean
    {
        public MIN_TipoMineralBean()
        {

        }

        public int idMineral { get; set; }
        public string mineral { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class GEN_UnidadNegocioBean
    {
        public string cod_unidad_negocio { get; set; }
        public string nom_unidad_negocio { get; set; }
        public string cod_sociedad { get; set; }
        public string cod_sociedad_costo { get; set; }
        public string cod_centro_beneficio { get; set; }
        public string identificador_RRHH { get; set; }
        public short notas { get; set; }
        public short tip_cierre { get; set; }
        public string nom_operativo { get; set; }
        public int niveles { get; set; }
    }

    public class GEN_FrecuenciaBean
    {
        public string cod_frecuencia { get; set; }
        public string frecuencia { get; set; }
    }
}

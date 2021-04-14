using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class KOP_PivotParamBean
    {
        public string cod_unidad_negocio { get; set; }
        public string nom_unidad_negocio { get; set; }

        public int anio { get; set; }
        public int mes { get; set; }
        public string nom_mes { get; set; }
        public int semana { get; set; }
        public string dia { get; set; }

    }
    public class KOP_PivotReporteBean
    {
        public string fecha { get; set; }
        public Int64 cod_indicador { get; set; }
        public string etiqueta { get; set; }
        public string unidad { get; set; }
        public decimal? Real { get; set; }
        public decimal? Ppto { get; set; }
        public decimal? Maximo { get; set; }
        public decimal? mReal { get; set; }
        public decimal? mPpto { get; set; }
        public decimal? step { get; set; }
        public string titulo { get; set; }
        public Byte? decimales { get; set; }

    }
}

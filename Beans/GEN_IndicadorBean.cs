using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class GEN_IndicadorBean
    {
        public string cod_unidad_negocio { get; set; }
        public long? cod_indicador { get; set; }
        public string nom_indicador { get; set; }
        public string unidad { get; set; }
        public byte? num_decimales { get; set; }
        public string frecuencia_mes { get; set; }
        public string frec_mes { get; set; }
        public string frecuencia_sem { get; set; }
        public string frec_sem { get; set; }
        public string frecuencia_dia { get; set; }
        public string frec_dia { get; set; }
        public string tip_indicador { get; set; }
        public string tip_acumulado { get; set; }
        public long? ponderado_indicador { get; set; }
        public string ponderador { get; set; }
        public long? enlace_indicador { get; set; }
        public string formula { get; set; }
        public string formula_presupuesto { get; set; }
        public string perfil_autorizado { get; set; }
        public long? cod_grupo_indicador { get; set; }
        public string nom_grupo_indicador { get; set; }
        public long? cod_subgrupo_indicador { get; set; }
        public string nom_subgrupo_indicador { get; set; }
        public DateTime? fec_vigencia { get; set; }
        public DateTime? fec_fin { get; set; }
        public byte? importante { get; set; }
        public byte? tip_variacion { get; set; }
        public string sql_comando { get; set; }
        public string sql_presupuesto { get; set; }
        public string documentacion { get; set; }
        public long? orden { get; set; }
        public long? cod_indicador_operativo { get; set; }
        public string nom_indicador_operativo { get; set; }
        public string origen { get; set; }
        public string equivalencia1 { get; set; }
        public string equivalencia2 { get; set; }
        public string grupos_costo { get; set; }
        public string agrupador { get; set; }
        public string cod_precio { get; set; }
        public long? ordenS { get; set; }
        public long? ordenD { get; set; }
        public long? ordenE { get; set; }
        public long? enlace_comentario { get; set; }
        public bool vis_acumulado { get; set; }
        public bool excluye_parada { get; set; }
        public long? cod_indicador_presupuesto { get; set; }
        public string nom_indicador_presupuesto { get; set; }
        public bool ver_acumulado { get; set; }
        public bool edicion_presupuesto { get; set; }

        public string accion { get; set; }
        public string cod_usuario { get; set; }

        //public bool? carga_operativa { get; set; }

    }

    public class GEN_TipoBean
    {
        public string valor { get; set; }
        public string texto { get; set; }
    }
}

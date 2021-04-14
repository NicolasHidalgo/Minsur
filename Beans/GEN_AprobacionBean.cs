using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class GEN_AprobacionBean
    {
        public string cod_unidad_negocio { get; set; }
        public string nom_unidad_negocio { get; set; }
        public string cod_frecuencia { get; set; }
        public string frecuencia { get; set; }
        public int? cod_rango_fecha { get; set; }
        public string rango_fecha { get; set; }
        public int? cod_carga_auxiliar { get; set; }
        public int? est_aprobacion { get; set; }
        public string cod_usuario { get; set; }
        public DateTime? fec_ultimo_acceso { get; set; }
        public DateTime? fec_envio_email { get; set; }
        public DateTime? fec_desde { get; set; }
        public DateTime? fec_hasta { get; set; }
        public DateTime? fec_actualizacion { get; set; }
        public int cod_presupuesto { get; set; }

        public string periodo { get; set; }
        public string estado { get; set; }
        public string accion { get; set; }

        public int cod_periodo { get; set; }
        public int? cod_mes { get; set; }
        public string cod_mes_txt { get; set; }
        public int mes { get; set; }
        public string cod_rango_fecha_txt { get; set; }
        public DateTime? fec_ult_cierre { get; set; }
        public string comentario_per { get; set; }
        public string comentario_bra { get; set; }
        public string glosa { get; set; }

        public string cod_modulo { get; set; }
        public string estado_operativo { get; set; }
        public string estado_publicacion { get; set; }
        public string search { get; set; }

    }
}

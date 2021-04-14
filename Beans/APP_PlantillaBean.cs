using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class APP_PlantillaBean
    {
        public Int64 ide_configuracion { get; set; }
        public string cod_frecuencia { get; set; }
        public long? ide_plantilla { get; set; }
        public string pais { get; set; }
        public string cod_unidad_negocio { get; set; }
        public string nom_unidad_negocio { get; set; }
        public long? cod_interno { get; set; }
        public string descripcion { get; set; }
        public string unidad { get; set; }
        public long? cod_indicador { get; set; }
        public bool requerido { get; set; }
        public bool oculto { get; set; }
        public string estilo_linea { get; set; }
        public string estilo_color { get; set; }
        public string estilo_fondo { get; set; }
        public string formato { get; set; }
        public long? agrupado { get; set; }
        public string estado { get; set; }

        public string unidad_negocio { get; set; }
        public string tipo { get; set; }
        public long? cod_indicador_base { get; set; }

        public string nom_indicador { get; set; }
        public string und_indicador { get; set; }
        public string tip_indicador { get; set; }
        public Int16? tip_variacion { get; set; }
        public string frecuencia { get; set; }

        public string tip_carga { get; set; }
        public long? cod_indicador_operativo { get; set; }
        public string cod_unidad_negocio_operativo { get; set; }
        public bool val_real { get; set; }
        public bool val_acm { get; set; }
        public long? cod_indicador_referencia1_operativo { get; set; }
        public long? cod_indicador_referencia2_operativo { get; set; }
        public long? cod_indicador_referencia3_operativo { get; set; }
        public long? cod_indicador_referencia4_operativo { get; set; }
        public string sp_carga { get; set; }
        public string accion_conf { get; set; }
        public string texto_ref1 { get; set; }
        public string texto_ref2 { get; set; }
        public string texto_ref3 { get; set; }
        public string texto_ref4 { get; set; }
        public string formato_ref1 { get; set; }
        public string formato_ref2 { get; set; }
        public string formato_ref3 { get; set; }
        public string formato_ref4 { get; set; }

        public string search { get; set; }
        public string accion { get; set; }

        public int cod_mes { get; set; }
        public int cod_rango_fecha { get; set; }
        public double? real { get; set; }
        public double? ppto { get; set; }
        public double? frct { get; set; }
        public double? var_frct { get; set; }
        public string sem_frct { get; set; }

        public double? real_acm { get; set; }
        public double? ppto_acm { get; set; }
        public double? frct_acm { get; set; }

        public double? var_frct_acm { get; set; }
        public string sem_frct_acm { get; set; }

        public double? ppto_esp { get; set; }
        public double? frct_esp { get; set; }

        public string nom_indicador_app { get; set; }
        public string nom_indicador_operativo { get; set; }

        public bool? carga_operativa { get; set; }
        public string grupo { get; set; }

    }
}

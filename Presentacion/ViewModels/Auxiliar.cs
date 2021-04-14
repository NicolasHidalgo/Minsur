using Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.ViewModels
{
    public class AuxiliarEdit
    {
        public IEnumerable<ExtendedSelectListItem> Aplicaciones { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> Unidades { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> Perfiles { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> Usuarios { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> Frecuencias { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> Formatos { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> Periodos { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> Meses { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> Fechas { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> Grupos { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> DropDownList { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlCeco { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlGrupo { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlPeriodo { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlPresupuesto { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlNivel1 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlNivel2 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlNivel3 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlArea { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlProceso01 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlProceso02 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlProceso03 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlCategoria { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlTipo { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlTipoFilter { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlTipoIndicador { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlTipoAcumulado { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlIndicador { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlIndicador1 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlIndicador2 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlIndicador3 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlIndicador4 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlPonderadoIndicador { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlGrupoIndicador { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlSubGrupoIndicador { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlIndicadorPresupuesto { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlUnidad { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlUnidadFilter { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlFrecuencia { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlFrecuenciaMes { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlFrecuenciaSem { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlFrecuenciaDia { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlIndicadorApp { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlFormato { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlFormatoRef1 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlFormatoRef2 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlFormatoRef3 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlFormatoRef4 { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlMaterial { get; set; } = new HashSet<ExtendedSelectListItem>();

        public APP_PlantillaBean APP_PlantillaBean { get; set; }
        public GEN_IndicadorBean GEN_IndicadorBean { get; set; }

        public string cod_usuario { get; set; }
        public string cod_aplicacion { get; set; }
        public string cod_unidad_negocio { get; set; }
        public string cod_frecuencia { get; set; }
        public int? cod_rango_fecha { get; set; }
        public int cod_perfil { get; set; }
        public string string_perfil { get; set; }
        public string accion { get; set; }
        public string estado { get; set; }

        public string search { get; set; }
        public int ide_grupo_correo { get; set; }
        public string cod_directorio_activo { get; set; }
        public string titulo { get; set; }
        public int cod_carga_auxiliar { get; set; }
        public string mensajeError { get; set; }
        public string tipoError { get; set; }
        public string cod_usuario_accion { get; set; }

        public string nivel1 { get; set; }
        public string nivel2 { get; set; }
        public string nivel3 { get; set; }
        public string cod_area { get; set; }
        public int periodo { get; set; }
        public int numNivel { get; set; }
        public string cod_partida { get; set; }
        public string periodo_txt { get; set; }

        public string tipo_gasto { get; set; }
        public string categoria { get; set; }

        public string fec_informe { get; set; }

        public DateTime fec_desde { get; set; }
        public DateTime fec_hasta { get; set; }
        public string fec_desde_txt { get; set; }
        public string fec_hasta_txt { get; set; }

        public int? cod_mes { get; set; }
        public string cod_mes_txt { get; set; }
        public string cod_rango_fecha_txt { get; set; }
        public string comentario_per { get; set; }
        public string comentario_bra { get; set; }
        public string glosa { get; set; }
        public int? est_aprobacion { get; set; }

        public IEnumerable<ExtendedSelectListItem> ddlAnio { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlMes { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlSemana { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlDia { get; set; } = new HashSet<ExtendedSelectListItem>();

    }
}
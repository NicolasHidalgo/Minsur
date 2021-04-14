using Beans;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class KOP_Negocio
    {
        KOP_Datos datos = new KOP_Datos();
        public List<GEN_UnidadNegocioBean> fn_kop_sel_unidad(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion)
        {
            return datos.fn_kop_sel_unidad(cod_usuario, cod_unidad_negocio, cod_frecuencia, cod_rango_fecha, accion);
        }
        public List<GEN_FrecuenciaBean> fn_kop_sel_frecuencia(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion)
        {
            return datos.fn_kop_sel_frecuencia(cod_usuario, cod_unidad_negocio, cod_frecuencia, cod_rango_fecha, accion);
        }
        public List<GEN_AprobacionBean> fn_kop_sel_aprobacion(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion)
        {
            return datos.fn_kop_sel_aprobacion(cod_usuario, cod_unidad_negocio, cod_frecuencia, cod_rango_fecha, accion);
        }
        public GEN_AprobacionBean fn_kop_get_aprobacion(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion)
        {
            return datos.fn_kop_get_aprobacion(cod_usuario,cod_unidad_negocio,cod_frecuencia,cod_rango_fecha,accion);
        }
        public GEN_MensajeBean fn_kop_update_aprobacion(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion, int cod_carga_auxiliar)
        {
            return datos.fn_kop_update_aprobacion(cod_usuario, cod_unidad_negocio, cod_frecuencia, cod_rango_fecha, accion, cod_carga_auxiliar);
        }
        public Tuple<List<GEN_TiempoBean>,GEN_MensajeBean> fn_kop_sel_rangoFecha(string cod_unidad_negocio, string accion, string cod_frecuencia, int periodo, int mes, string cod_usuario, int? cod_carga_auxiliar, int? cod_rango_fecha)
        {
            return datos.fn_kop_sel_rangoFecha(cod_unidad_negocio, accion, cod_frecuencia, periodo, mes, cod_usuario,cod_carga_auxiliar,cod_rango_fecha);
        }
        public List<KOP_CargaAuxiliarBean> fn_kop_sel_cargaAuxiliar(string accion, string cod_usuario, string cod_unidad_negocio)
        {
            return datos.fn_kop_sel_cargaAuxiliar(accion, cod_usuario, cod_unidad_negocio);
        }
        public GEN_MensajeBean fn_kop_movimientoIndicador(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha)
        {
            return datos.fn_kop_movimientoIndicador(cod_usuario, cod_unidad_negocio, cod_frecuencia, cod_rango_fecha);
        }
        public GEN_MensajeBean fn_kop_pro_envioCorreo(GEN_AprobacionBean bean)
        {
            return datos.fn_kop_pro_envioCorreo(bean);
        }
        public List<GEN_IndicadorBean> fn_kop_sel_indicador(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            return datos.fn_kop_sel_indicador(cod_usuario, cod_unidad_negocio, accion);
        }
        public GEN_IndicadorBean fn_kop_get_indicador(string cod_usuario, string cod_unidad_negocio, string accion, long cod_indicador)
        {
            return datos.fn_kop_get_indicador(cod_usuario, cod_unidad_negocio, accion, cod_indicador);
        }
        public List<GEN_TipoBean> fn_kop_sel_tipo(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            return datos.fn_kop_sel_tipo(cod_usuario, cod_unidad_negocio, accion);
        }
        public GEN_MensajeBean fn_kop_pro_reconstruye(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, string tipo)
        {
            return datos.fn_kop_pro_reconstruye(cod_usuario, cod_unidad_negocio, cod_frecuencia, tipo);
        }

        public List<KOP_PivotParamBean> fn_kop_sel_pivotParam(int accion, string anio, string mes, string semana, string dia)
        {
            return datos.fn_kop_sel_pivotParam(accion, anio, mes, semana, dia);
        }
        public List<KOP_PivotReporteBean> fn_kop_sel_configPivot(int cod_informe, string cod_unidad_negocio, string anio, string mes, string semana, string dia)
        {
            return datos.fn_kop_sel_configPivot(cod_informe, cod_unidad_negocio, anio, mes, semana, dia);
        }
    }
}

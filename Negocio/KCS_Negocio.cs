using Beans;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class KCS_Negocio
    {
        KCS_Datos datos = new KCS_Datos();
        public List<KCS_CecoBean> fn_kcs_sel_ceco(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            return datos.fn_kcs_sel_ceco(cod_usuario, cod_unidad_negocio, periodo, accion);
        }
        public List<KCS_ClaseBean> fn_kcs_sel_clase(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            return datos.fn_kcs_sel_clase(cod_usuario, cod_unidad_negocio, periodo, accion);
        }
        public List<KCS_CecoBean> fn_kcs_sel_ddlCeco(string cod_usuario, string cod_unidad_negocio, int periodo, string accion, string cod_grupo_costo)
        {
            return datos.fn_kcs_sel_ddlCeco(cod_usuario, cod_unidad_negocio, periodo, accion, cod_grupo_costo);
        }
        public List<GEN_UnidadNegocioBean> fn_kcs_sel_ddlUnidad(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            return datos.fn_kcs_sel_ddlUnidad(cod_usuario, cod_unidad_negocio, periodo, accion);
        }
        public List<KCS_ClaseBean> fn_kcs_sel_ddlClase(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            return datos.fn_kcs_sel_ddlClase(cod_usuario, cod_unidad_negocio, periodo, accion);
        }
        public GEN_MensajeBean fn_kcs_pro_ceco(string cod_usuario, string cod_unidad_negocio, int periodo, string accion, KCS_CecoBean obj)
        {
            return datos.fn_kcs_pro_ceco(cod_usuario, cod_unidad_negocio, periodo, accion, obj);
        }
        public GEN_MensajeBean fn_kcs_pro_clase(string cod_usuario, string cod_unidad_negocio, int periodo, string accion, KCS_ClaseBean obj)
        {
            return datos.fn_kcs_pro_clase(cod_usuario, cod_unidad_negocio, periodo, accion, obj);
        }
        public GEN_MensajeBean fn_kcs_pro_cierre(string cod_usuario, string cod_unidad_negocio, int periodo, int mes, string accion)
        {
            return datos.fn_kcs_pro_cierre(cod_usuario, cod_unidad_negocio, periodo, mes, accion);
        }
        public List<GEN_AprobacionBean> fn_kcs_sel_ddlCierre(string cod_usuario, string cod_unidad_negocio, int periodo, int mes, string accion)
        {
            return datos.fn_kcs_sel_ddlCierre(cod_usuario, cod_unidad_negocio, periodo,mes, accion);
        }
        public List<KCS_PartidaBean> fn_kcs_sel_partida(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            return datos.fn_kcs_sel_partida(cod_usuario, cod_unidad_negocio, periodo, accion);
        }
        public List<KCS_AreaBean> fn_kcs_sel_ddlArea(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            return datos.fn_kcs_sel_ddlArea(cod_usuario, cod_unidad_negocio, periodo, accion);
        }
        public GEN_MensajeBean up_kcs_cud_partida(string cod_usuario, KCS_PartidaBean obj)
        {
            return datos.up_kcs_cud_partida(cod_usuario, obj);
        }
        public List<KCS_MaterialBean> fn_kcs_sel_material(string cod_usuario, string cod_unidad_negocio, int periodo, string accion, long term1,string term2)
        {
            return datos.fn_kcs_sel_material(cod_usuario, cod_unidad_negocio, periodo, accion, term1, term2);
        }
        public List<GEN_IndicadorBean> fn_kcs_sel_indicador(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            return datos.fn_kcs_sel_indicador(cod_usuario, cod_unidad_negocio, accion);
        }
        public GEN_IndicadorBean fn_kcs_get_indicador(string cod_usuario, string cod_unidad_negocio, string accion, long cod_indicador)
        {
            return datos.fn_kcs_get_indicador(cod_usuario, cod_unidad_negocio, accion, cod_indicador);
        }
        public GEN_AprobacionBean fn_kcs_get_aprobacion(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion)
        {
            return datos.fn_kcs_get_aprobacion(cod_usuario, cod_unidad_negocio, cod_frecuencia, cod_rango_fecha, accion);
        }
        public GEN_MensajeBean fn_kcs_update_aprobacion(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_mes ,int cod_rango_fecha, string accion)
        {
            return datos.fn_kcs_update_aprobacion(cod_usuario, cod_unidad_negocio, cod_frecuencia, cod_mes ,cod_rango_fecha, accion);
        }
        public List<GEN_FrecuenciaBean> fn_kcs_sel_frecuencia(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            return datos.fn_kcs_sel_frecuencia(cod_usuario, cod_unidad_negocio, accion);
        }
        public Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> fn_kcs_sel_rangoFecha(string cod_unidad_negocio, string accion, string cod_frecuencia, int periodo, int mes, string cod_usuario, int? cod_carga_auxiliar, int? cod_rango_fecha)
        {
            return datos.fn_kcs_sel_rangoFecha(cod_unidad_negocio, accion, cod_frecuencia, periodo, mes, cod_usuario, cod_carga_auxiliar, cod_rango_fecha);
        }
    }
}

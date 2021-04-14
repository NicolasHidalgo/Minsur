using Beans;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class APP_Negocio
    {
        APP_Datos datos = new APP_Datos();
        public List<GEN_UnidadNegocioBean> fn_app_sel_unidad(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            return datos.fn_app_sel_unidad(cod_usuario, cod_unidad_negocio,accion);
        }
        public List<APP_PlantillaBean> fn_app_sel_plantilla(string accion, string cod_unidad_negocio, string cod_usuario)
        {
            return datos.fn_app_sel_plantilla(accion, cod_unidad_negocio, cod_usuario);
        }
        public List<APP_PlantillaBean> fn_app_rep_carga(string accion, string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, int cod_rango_fecha, string tipo)
        {
            return datos.fn_app_rep_carga(accion, cod_unidad_negocio, cod_usuario, cod_frecuencia, cod_rango_fecha, tipo);
        }
        public GEN_MensajeBean up_app_pro_cargaXLS(string cod_unidad_negocio, string cod_usuario, string archivo_fisico, string archivo_logico, string accion, string cod_frecuencia, int cod_rango_fecha, string tipo)
        {
            return datos.up_app_pro_cargaXLS(cod_unidad_negocio, cod_usuario, archivo_fisico, archivo_logico, accion, cod_frecuencia, cod_rango_fecha,tipo);
        }
        public List<GEN_IndicadorBean> fn_app_sel_indicador(string accion, string cod_unidad_negocio, string cod_usuario, string tip_indicador)
        {
            return datos.fn_app_sel_indicador(accion, cod_unidad_negocio, cod_usuario, tip_indicador);
        }
        public APP_PlantillaBean fn_app_get_plantilla(string accion, string cod_unidad_negocio, string cod_usuario, int ide_plantilla, string cod_frecuencia)
        {
            return datos.fn_app_get_plantilla(accion, cod_unidad_negocio, cod_usuario, ide_plantilla, cod_frecuencia);
        }
        public APP_PlantillaBean fn_app_get_indicador_config(string accion, long ide_configuracion, string cod_usuario)
        {
            return datos.fn_app_get_indicador_config(accion, ide_configuracion, cod_usuario);
        }
        public GEN_MensajeBean up_app_cud_plantilla(string accion, string cod_usuario, APP_PlantillaBean obj)
        {
            return datos.up_app_cud_plantilla(accion, cod_usuario, obj);
        }
        public List<GEN_TipoBean> fn_app_sel_tipo(string accion, string cod_unidad_negocio, string cod_usuario, string tipo)
        {
            return datos.fn_app_sel_tipo(accion, cod_unidad_negocio, cod_usuario, tipo);
        }
        public List<GEN_AprobacionBean> fn_app_sel_publicacion(string accion, string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, int cod_rango_fecha, string cod_modulo)
        {
            return datos.fn_app_sel_publicacion(accion, cod_unidad_negocio, cod_usuario, cod_frecuencia, cod_rango_fecha, cod_modulo);
        }
        public Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> fn_app_sel_rangoFecha(string accion, string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, string cod_modulo, int periodo, int mes, int? cod_rango_fecha)
        {
            return datos.fn_app_sel_rangoFecha(accion, cod_unidad_negocio, cod_usuario, cod_frecuencia, cod_modulo, periodo, mes,cod_rango_fecha);
        }
        public GEN_MensajeBean fn_app_pro_publicacion(string cod_usuario, GEN_AprobacionBean obj)
        {
            return datos.fn_app_pro_publicacion(cod_usuario, obj);
        }
    }
}

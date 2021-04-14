using Beans;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MIN_Negocio
    {
        MIN_Datos datos = new MIN_Datos();
        public GEN_MensajeBean fn_min_cud_tajoEstructura(string cod_unidad_negocio, string cod_usuario, string accion, MIN_EstructuraBean bean)
        {
            return datos.fn_min_cud_tajoEstructura(cod_unidad_negocio, cod_usuario, accion, bean);
        }
        public MIN_EstructuraBean fn_min_get_estructura(string cod_usuario, string accion, int? cod_tajo_estructura)
        {
            return datos.fn_min_get_estructura(cod_usuario, accion, cod_tajo_estructura);
        }
        public List<MIN_EstructuraBean> fn_min_sel_estructura(string cod_usuario, string accion, string cod_unidad_negocio)
        {
            return datos.fn_min_sel_estructura(cod_usuario, accion, cod_unidad_negocio);
        }
        public List<MIN_TipoMineralBean> fn_min_sel_tipMineral(string accion, string cod_unidad_negocio ,string cod_usuario)
        {
            return datos.fn_min_sel_tipMineral(accion, cod_unidad_negocio, cod_usuario);
        }
        public List<MIN_TajoBean> fn_min_sel_tajo(string cod_usuario, string accion, string cod_unidad_negocio)
        {
            return datos.fn_min_sel_tajo(cod_usuario, accion, cod_unidad_negocio);
        }
        public List<MIN_TipoTajoBean> fn_min_sel_tipoTajo(string cod_usuario, string accion, string cod_unidad_negocio)
        {
            return datos.fn_min_sel_tipoTajo(cod_usuario, accion, cod_unidad_negocio);
        }
        public GEN_MensajeBean fn_min_cud_tajo(string cod_unidad_negocio, string cod_usuario, string accion, MIN_TajoBean bean)
        {
            return datos.fn_min_cud_tajo(cod_unidad_negocio,cod_usuario, accion, bean);
        }
    }
}

using Beans;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CPX_Negocio
    {
        CPX_Datos datos = new CPX_Datos();

        public List<CPX_MaestraBean> fn_cpx_sel_maestra(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            return datos.fn_cpx_sel_maestra(cod_usuario, cod_unidad_negocio, accion);
        }
        public GEN_MensajeBean up_cpx_cud_maestra(string cod_usuario, CPX_MaestraBean obj)
        {
            return datos.up_cpx_cud_maestra(cod_usuario, obj);
        }
    }
}

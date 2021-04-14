using Beans;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class OX_Negocio
    {
        OX_Datos datos = new OX_Datos();

        public List<OX_MovimientoBean> fn_ox_sel_movimiento(string accion)
        {
            return datos.fn_ox_sel_movimiento(accion);
        }
        public OX_MovimientoBean fn_ox_get_movimiento(string accion, string cod_unidad_negocio, int ide_movimiento)
        {
            return datos.fn_ox_get_movimiento(accion, cod_unidad_negocio, ide_movimiento);
        }
    }
}

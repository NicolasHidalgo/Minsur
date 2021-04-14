using Beans;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TV_Negocio
    {
        TV_Datos datos = new TV_Datos();
        public GEN_MensajeBean up_tv_cargaKPI_taboca(string cod_usuario, string accion, string archivo_fisico)
        {
            return datos.up_tv_cargaKPI_taboca(cod_usuario, accion, archivo_fisico);
        }
    }
}

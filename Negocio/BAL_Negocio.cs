using Beans;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BAL_Negocio
    {
        BAL_Datos datos = new BAL_Datos();
        public List<BAL_CodificacionBean> fn_bal_sel_codificacion(string accion, string cod_unidad_negocio, string cod_usuario)
        {
            return datos.fn_bal_sel_codificacion(accion, cod_unidad_negocio, cod_usuario);
        }
        public List<BAL_PlantillaBean> fn_bal_sel_plantilla(string accion, string cod_unidad_negocio, string cod_usuario)
        {
            return datos.fn_bal_sel_plantilla(accion, cod_unidad_negocio, cod_usuario);
        }
        public GEN_MensajeBean up_bal_cud_codificacion(string accion, string cod_usuario, BAL_CodificacionBean obj)
        {
            return datos.up_bal_cud_codificacion(accion, cod_usuario, obj);
        }
        public GEN_MensajeBean up_bal_cud_plantilla(string accion, string cod_usuario, BAL_PlantillaBean obj)
        {
            return datos.up_bal_cud_plantilla(accion, cod_usuario, obj);
        }
        public List<BAL_CodificacionBean> fn_bal_sel_balmet(string accion, string cod_unidad_negocio, string cod_usuario)
        {
            return datos.fn_bal_sel_balmet(accion, cod_unidad_negocio, cod_usuario);
        }
        public BAL_CodificacionBean fn_bal_get_balmet(string accion, string cod_unidad_negocio, string cod_usuario, string cod_balmet)
        {
            return datos.fn_bal_get_balmet(accion, cod_unidad_negocio, cod_usuario,cod_balmet);
        }
        public GEN_MensajeBean up_bal_pro_cargaXLS(string cod_unidad_negocio, string cod_usuario, string archivo_fisico, string archivo_logico,string accion, Int64 ide_carga, DateTime? fec_informe, DateTime? fec_informe_hasta)
        {
            return datos.up_bal_pro_cargaXLS(cod_unidad_negocio, cod_usuario, archivo_fisico, archivo_logico,accion, ide_carga, fec_informe, fec_informe_hasta);
        }
        public Tuple<List<BAL_ProduccionDiaBean>, GEN_MensajeBean> fn_bal_sel_repProduccion(string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, string accion, DateTime? fec_informe)
        {
            return datos.fn_bal_sel_repProduccion(cod_unidad_negocio,cod_usuario,cod_frecuencia,accion,fec_informe);
        }
        public List<BAL_CodificacionBean> fn_bal_sel_indicador(string accion, string cod_unidad_negocio, string cod_usuario)
        {
            return datos.fn_bal_sel_indicador(accion, cod_unidad_negocio, cod_usuario);
        }
        public GEN_MensajeBean up_bal_pro_reporte(string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, string accion, DateTime? fec_informe, List<BAL_ProduccionDiaBean> dataListFromTable)
        {
            return datos.up_bal_pro_reporte(cod_unidad_negocio, cod_usuario, cod_frecuencia, accion, fec_informe, dataListFromTable);
        }
        public List<BAL_Handsontable> fn_bal_sel_headerRep(string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, string accion, DateTime? fec_informe)
        {
            return datos.fn_bal_sel_headerRep(cod_unidad_negocio, cod_usuario, cod_frecuencia, accion, fec_informe);
        }
        public List<GEN_AuxiliarBean> fn_bal_sel_campanias(string cod_unidad_negocio, string cod_usuario, string accion, string cod_campana, DateTime? fec_informe)
        {
            return datos.fn_bal_sel_campanias(cod_unidad_negocio, cod_usuario, accion, cod_campana, fec_informe);
        }
        public List<BAL_ProduccionDiaBean> fn_bal_sel_marca(string cod_unidad_negocio, string cod_usuario, string archivo_fisico, string archivo_logico, string accion, Int64? ide_carga, DateTime? fec_informe, DateTime? fec_informe_hasta)
        {
            return datos.fn_bal_sel_marca(cod_unidad_negocio, cod_usuario, archivo_fisico, archivo_logico, accion, ide_carga, fec_informe, fec_informe_hasta);
        }
        public GEN_MensajeBean fn_bal_pro_marca(string cod_unidad_negocio, string cod_usuario, string archivo_fisico, string archivo_logico, string accion, Int64? ide_carga, DateTime? fec_informe, List<BAL_ProduccionDiaBean> dataListFromTable)
        {
            return datos.fn_bal_pro_marca(cod_unidad_negocio, cod_usuario, archivo_fisico, archivo_logico, accion, ide_carga, fec_informe, dataListFromTable);
        }
    }
}

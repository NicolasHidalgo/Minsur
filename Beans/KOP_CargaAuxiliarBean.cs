using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class KOP_CargaAuxiliarBean
    {
        public string cod_unidad_negocio { get; set; }
        public int cod_carga_auxiliar { get; set; }
        public string nom_carga_auxiliar { get; set; }
        public string cod_frecuencia { get; set; }
        public string hoja_xls { get; set; }
        public string lin_periodo { get; set; }
        public string etiqueta_periodo { get; set; }
        public string lin_mes { get; set; }
        public string etiqueta_mes { get; set; }
        public string lin_semana { get; set; }
        public string etiqueta_semana { get; set; }
        public string lin_dia { get; set; }
        public string etiqueta_dia { get; set; }
        public string lin_dato { get; set; }
        public string sp_cargaAuxiliar { get; set; }
        public string perfil_autorizado { get; set; }
        public float version { get; set; }
        public string filtro_indicador { get; set; }
        public string tabla { get; set; }
        public string filtro_lectura { get; set; }
        public string filtro_comentario { get; set; }
        public string perfil_tca { get; set; }
        public string path { get; set; }
        public string descripcion_excel { get; set; }
    }
}

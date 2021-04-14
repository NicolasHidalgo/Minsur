using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class SEG_AplicacionBean
    {
        public string cod_aplicacion { get; set; }
        public string nom_aplicacion { get; set; }
        public int cod_grupo_aplicacion { get; set; }
        public string navegacion_url { get; set; }
        public Byte activacion_log { get; set; }
        public int est_aplicacion { get; set; }
        public string icono_aplicacion { get; set; }
    }
}

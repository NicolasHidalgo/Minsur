using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class CPX_MaestraBean
    {
        public int? id { get; set; }
        public string tipo { get; set; }
        public string nom_tipo { get; set; }
        public string nom_corto { get; set; }
        public string nom_anterior { get; set; }
        public long cod_indicador_app { get; set; }

        public string accion { get; set; }
        public string search { get; set; }
    }
}

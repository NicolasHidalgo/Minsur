using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class KCS_MaterialBean
    {
        public string cod_material { get; set; }
        public string nom_material { get; set; }
        public string und_medida { get; set; }
        public double factor1 { get; set; }
        public double factor2 { get; set; }
        public string und_medida_req { get; set; }
        public Int16 tip_conversion { get; set; }
        public double factor { get; set; }
        public long cod_centro_costo { get; set; }

        public string accion { get; set; }
    }
}

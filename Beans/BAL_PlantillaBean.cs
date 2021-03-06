using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class BAL_PlantillaBean
    {
        public string cod_unidad_negocio { get; set; }
        public int cod_mes { get; set; }
        public long ide_plantilla_origen { get; set; }
        public long ide_plantilla { get; set; }
        public string cod_balmet { get; set; }
        public Int16 estilo { get; set; }
        public long cod_indicador { get; set; }
        public DateTime fec_modificacion { get; set; }
        public int ide_usuario { get; set; }

        public string cod_sap { get; set; }
        public string nom_balmet { get; set; }
        public string ide_rep_produccion { get; set; }
        public string unidad { get; set; }
        public string cod_padre_balmet { get; set; }
        public string nom_padre_balmet { get; set; }
        public string nom_indicador { get; set; }

        public string search { get; set; }
        public string empty { get; set; }
        public string accion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class GEN_CorreoBean
    {
        public int? ide_grupo_correo { get; set; }
        public string nom_grupo_correo { get; set; }
        public int? orden { get; set; }
        public string cod_aplicacion { get; set; }
        public string cod_unidad_negocio { get; set; }

        public string correo_remitente { get; set; }
        public string nom_remitente { get; set; }
        public bool envio_correos { get; set; }

        public string cod_directorio_activo { get; set; }
        public Int16? est_remitente { get; set; }
        public string estado { get; set; }
        public string cod_usuario { get; set; }
    }
}

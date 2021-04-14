using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class OX_MovimientoBean
    {
        public string cod_unidad_negocio { get; set; }
        public string accion { get; set; }

        public int ide_movimiento { get; set; }
        public string imei { get; set; }
        public int ide_operador { get; set; }
        public string nom_operador { get; set; }
        public int ide_vehiculo { get; set; }
        public string nom_vehiculo { get; set; }
        public int ide_secuencia_movil { get; set; }
        public string turno { get; set; }
        public string guardia { get; set; }
        public string material { get; set; }
        public DateTime? fec_operacion { get; set; }
        public DateTime? fec_descarga { get; set; }
        public int ide_mineral { get; set; }
        public string nom_mineral { get; set; }
        public double? peso_mineral { get; set; }
        public double? ley_mineral { get; set; }
        public string tip_accion { get; set; }
        public int ide_vehiculo_carguio { get; set; }
        public string carguio_ini { get; set; }
        public string carguio_fin { get; set; }
        public int est_movimiento { get; set; }
        public int est_sincronizacion { get; set; }
        public DateTime? fec_actualizacion { get; set; }
        public DateTime? fec_movil_ini { get; set; }
        public DateTime? fec_movil_fin { get; set; }
        public string ubicacion_ini { get; set; }
        public string ubicacion_fin { get; set; }

    }
}

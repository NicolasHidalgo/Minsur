using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    public class SEG_MenuBean
    {
        public SEG_MenuBean()
        {
            // constructor
        }

        /*public int IdOpcion { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }
        public string Alias { get; set; }*/

        public int cod_menu { get; set; }
        public int cod_menu_padre { get; set; }
        public SEG_MenuBean MenuPadre { get; set; }
        public string nom_menu { get; set; }
        public string navegacion_url { get; set; }
        public string target_menu { get; set; }
        
        public string action { get; set; }
        public string controller { get; set; }
        public string icono { get; set; }
        public string tip_menu { get; set; }
        public string parametro { get; set; }

        public string cod_aplicacion { get; set; }
        public int ide_aplicacion { get; set; }
        public string nom_aplicacion { get; set; }

        public IEnumerable<SEG_MenuBean> MenuHijos { get; set; } = new HashSet<SEG_MenuBean>();

        //Campos del Carousel
        public int ide_carousel { get; set; }
        public string imagen { get; set; }
        public string cod_unidad_negocio { get; set; }
    }

}

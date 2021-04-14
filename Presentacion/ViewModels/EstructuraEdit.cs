using Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.ViewModels
{
    public class EstructuraEdit
    {
        public MIN_EstructuraBean Estructura { get; set; }
        public IEnumerable<ExtendedSelectListItem> Minerales { get; set; } = new HashSet<ExtendedSelectListItem>();
    }
}
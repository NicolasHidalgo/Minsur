using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.ViewModels
{
    public class DocumentoInformacion
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Mine { get; set; }
        public string Url { get; set; }
        public double Tamano { get; set; }
        public string Descripcion { get; set; }
        public int TipoDocumento { get; set; }
    }

    public class DocumentoDataTable
    {
        public long? Id { get; set; }
        public SharedModel TipoDocumento { get; set; }
        public string Nombre { get; set; }
    }

    public class DocumentoEdit
    {
        public IEnumerable<ExtendedSelectListItem> TiposDocumentos { get; set; } = new HashSet<ExtendedSelectListItem>();

        public long? Id { get; set; }
        public string Titulo { get; set; }
    }

    public class SharedModel
    {
        public short? Id { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }
        public string Alias { get; set; }
    }
}
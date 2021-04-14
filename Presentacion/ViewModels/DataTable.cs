using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.ViewModels
{
    public class DataTable<T>
    {
        public int? draw { get; set; }
        public int? recordsTotal { get; set; }
        public int? recordsFiltered { get; set; }
        public IEnumerable<T> data { get; set; }
    }

    public class DataTableRequest
    {
        public int? Draw { get; set; }
        public int? Start { get; set; }
        public int? Length { get; set; }
    }
}
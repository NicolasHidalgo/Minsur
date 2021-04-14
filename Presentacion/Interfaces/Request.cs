using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Interfaces
{
    public class ModelRequest<TModel>
    {
        public TModel Filter { get; set; }
        public ICollection<Order> OrderBy { get; set; } = new HashSet<Order>();
    }

    public class DataTableRequest_<TModel> : ModelRequest<TModel>
    {
        public int? Draw { get; set; }
        public int? Start { get; set; }
        public int? Length { get; set; }
    }

    public class Order
    {
        public string Property { get; set; }
        public string Direction { get; set; }
    }
}
using Presentacion.Interfaces;
using Presentacion.ViewModels;
using Beans;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;

namespace Presentacion.Controllers.Api
{
    [RoutePrefix("api/CPX")]
    public class CPXController : ApiController
    {
        private readonly CPX_Negocio db = new CPX_Negocio();
        string cod_usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public CPXController()
        {

        }

        [HttpGet]
        [Route("GetDataTable_Maestra")]
        public DataTable<CPX_MaestraBean> GetDataTable_Maestra([FromUri]DataTableRequest_<CPX_MaestraBean> model)
        {
            IQueryable<CPX_MaestraBean> query = db.fn_cpx_sel_maestra(cod_usuario, "", model.Filter.accion).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {

            }

            if (model.OrderBy.Count() > 0)
            {
                //por implementar
            }
            //else
            //{
            //    query = query.OrderBy(x => x.cod_tajo_estructura);
            //}

            var recordsFiltered = query.Count();

            if (model.Start != null)
            {
                query = query.Skip(model.Start.GetValueOrDefault());
            }

            if (model.Length != null)
            {
                query = query.Take(model.Length.GetValueOrDefault());
            }

            var data = query
                .AsEnumerable()
                .Select(x => new CPX_MaestraBean
                {
                    id = x.id,
                    tipo = x.tipo,
                    nom_tipo = x.nom_tipo,
                    nom_corto = x.nom_corto,
                    nom_anterior = x.nom_anterior
                });

            var dataTable = new DataTable<CPX_MaestraBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }
    }
}
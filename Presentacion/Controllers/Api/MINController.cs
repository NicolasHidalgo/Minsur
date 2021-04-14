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

namespace Presentacion.Controllers.Api
{
    [RoutePrefix("api/MIN")]
    public class MINController : ApiController
    {
        private readonly MIN_Negocio db = new MIN_Negocio();
        string cod_usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public MINController()
        {

        }

        [HttpGet]
        [Route("GetDataTable_Estructura")]
        public DataTable<MIN_EstructuraBean> GetDataTable_Estructura([FromUri]DataTableRequest_<MIN_EstructuraBean> model)
        {
            IQueryable<MIN_EstructuraBean> query = db.fn_min_sel_estructura(cod_usuario,"SELECT", model.Filter.cod_unidad_negocio).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                if (model.Filter.search != null)
                {
                    query = query.Where(x => x.nom_tajo_estructura.ToLower().Contains(model.Filter.search.ToLower()) 
                                        || x.tip_mineral.ToLower().Contains(model.Filter.search.ToLower())
                                         || x.cod_tajo_estructura.ToString().Contains(model.Filter.search.ToLower())
                                         || x.cod_interno.ToString().Contains(model.Filter.search.ToLower()));
                }
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
                .Select(x => new MIN_EstructuraBean
                {
                    cod_tajo_estructura = x.cod_tajo_estructura,
                    nom_tajo_estructura = x.nom_tajo_estructura,
                    tip_mineral = x.tip_mineral,
                    cod_interno = x.cod_interno,
                    vacio = x.vacio
                });

            var dataTable = new DataTable<MIN_EstructuraBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }

        [HttpGet]
        [Route("GetDataTable_Tajo")]
        public DataTable<MIN_TajoBean> GetDataTable_Tajo([FromUri]DataTableRequest_<MIN_TajoBean> model)
        {
            IQueryable<MIN_TajoBean> query = db.fn_min_sel_tajo(cod_usuario, "SELECT", model.Filter.cod_unidad_negocio).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
               query = query.Where(x => x.cod_tajo_estructura == model.Filter.cod_tajo_estructura);
                if (model.Filter.search != null)
                {
                    query = query.Where(x => x.nom_tajo.ToLower().Contains(model.Filter.search.ToLower()));
                }
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
                .Select(x => new MIN_TajoBean
                {
                    cod_tajo = x.cod_tajo,
                    cod_tajo_tipo = x.cod_tajo_tipo,
                    cod_tajo_interno = x.cod_tajo_interno,
                    nom_tajo_tipo = x.nom_tajo_tipo,
                    cod_tajo_estructura = x.cod_tajo_estructura,
                    nom_tajo_estructura = x.nom_tajo_estructura,
                    nom_tajo = x.nom_tajo,
                    fec_inicio = x.fec_inicio,
                    fec_fin = x.fec_fin,
                    fase = x.fase,
                    tip_mineral = x.tip_mineral
                });

            var dataTable = new DataTable<MIN_TajoBean>()
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
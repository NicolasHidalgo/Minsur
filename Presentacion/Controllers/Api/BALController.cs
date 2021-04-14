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
    [RoutePrefix("api/BAL")]
    public class BALController : ApiController
    {
        private readonly BAL_Negocio db = new BAL_Negocio();
        string cod_usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public BALController()
        {

        }
        [HttpGet]
        [Route("GetDataTable_Codificacion")]
        public DataTable<BAL_CodificacionBean> GetDataTable_Codificacion([FromUri]DataTableRequest_<BAL_CodificacionBean> model)
        {
            IQueryable<BAL_CodificacionBean> query = db.fn_bal_sel_codificacion("SELECT", model.Filter.cod_unidad_negocio,cod_usuario).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                if (model.Filter.search != null)
                {
                    query = query.Where(x => x.cod_balmet.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.nom_balmet.ToLower().Contains(model.Filter.search.ToLower()));
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
                .Select(x => new BAL_CodificacionBean
                {
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    cod_balmet = x.cod_balmet,
                    nom_balmet = x.nom_balmet,
                    unidad = x.unidad,
                    ide_rep_produccion = x.ide_rep_produccion,
                    cod_padre_balmet = x.cod_padre_balmet,
                    cod_indicador = x.cod_indicador,
                    fec_modificacion = x.fec_modificacion,
                    ide_usuario = x.ide_usuario,
                    nom_padre_balmet = x.nom_padre_balmet,
                    nom_indicador = x.nom_indicador,
                    tip_balmet = x.tip_balmet,
                    formula = x.formula,
                    empty = x.empty
                });

            var dataTable = new DataTable<BAL_CodificacionBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }

        [HttpGet]
        [Route("GetDataTable_Plantilla")]
        public DataTable<BAL_PlantillaBean> GetDataTable_Plantilla([FromUri]DataTableRequest_<BAL_PlantillaBean> model)
        {
            IQueryable<BAL_PlantillaBean> query = db.fn_bal_sel_plantilla("SELECT", model.Filter.cod_unidad_negocio, cod_usuario).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                if (model.Filter.search != null)
                {
                    query = query.Where(x => x.cod_balmet.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.cod_sap.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.nom_balmet.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.ide_rep_produccion.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.unidad.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.nom_padre_balmet.ToLower().Contains(model.Filter.search.ToLower()));
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
                .Select(x => new BAL_PlantillaBean
                {
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    ide_plantilla = x.ide_plantilla,
                    cod_balmet = x.cod_balmet,
                    cod_sap = x.cod_sap,
                    nom_balmet = x.nom_balmet,
                    ide_rep_produccion = x.ide_rep_produccion,
                    unidad = x.unidad,
                    cod_padre_balmet = x.cod_padre_balmet,
                    nom_padre_balmet = x.nom_padre_balmet,
                    nom_indicador = x.nom_indicador,
                    empty = x.empty
                });

            var dataTable = new DataTable<BAL_PlantillaBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }

        [HttpGet]
        [Route("GetDataTable_Batch")]
        public DataTable<BAL_ProduccionDiaBean> GetDataTable_Batch([FromUri]DataTableRequest_<BAL_ProduccionDiaBean> model)
        {

            DateTime dtfecInforme = DateTime.ParseExact(model.Filter.fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtfecInformeHasta = DateTime.ParseExact(model.Filter.fec_informe_hasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            IQueryable<BAL_ProduccionDiaBean> query = db.fn_bal_sel_marca(model.Filter.cod_unidad_negocio, model.Filter.cod_usuario,model.Filter.archivo_fisico, model.Filter.archivo_logico, "START", model.Filter.ide_carga, dtfecInforme, dtfecInformeHasta).AsQueryable();

            var recordsTotal = query.Count();

            /*if (model.Filter != null)
            {
                if (model.Filter.search != null)
                {
                    query = query.Where(x => x.cod_balmet.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.cod_sap.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.nom_balmet.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.ide_rep_produccion.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.unidad.ToLower().Contains(model.Filter.search.ToLower())
                                    || x.nom_padre_balmet.ToLower().Contains(model.Filter.search.ToLower()));
                }
            }*/

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

            //if (model.Length != null)
            //{
            //    query = query.Take(model.Length.GetValueOrDefault());
            //}

            var data = query
                .AsEnumerable()
                .Select(x => new BAL_ProduccionDiaBean
                {
                    ide_carga = x.ide_carga,
                    fecha = x.fecha,
                    batch = x.batch,
                    cama = x.cama,
                    lanza = x.lanza,
                    marca = x.marca
                });

            var dataTable = new DataTable<BAL_ProduccionDiaBean>()
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
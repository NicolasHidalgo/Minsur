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
using System.Text;
using Newtonsoft.Json.Linq;

namespace Presentacion.Controllers.Api
{
    [RoutePrefix("api/KOP")]
    public class KOPController : ApiController
    {
        private readonly KOP_Negocio db = new KOP_Negocio();
        public KOPController()
        {

        }

        [HttpGet]
        [Route("ConsultaCierre")]
        public DataTable<GEN_AprobacionBean> ConsultaCierre([FromUri]DataTableRequest_<AuxiliarEdit> model)
        {
            IQueryable<GEN_AprobacionBean> query = null;

            if (model.Filter != null)
            {
                query = db.fn_kop_sel_aprobacion(model.Filter.cod_usuario, model.Filter.cod_unidad_negocio, model.Filter.cod_frecuencia ,model.Filter.cod_rango_fecha.Value, "CONSULTA_CIERRES").AsQueryable();

                //if (model.Filter.search != null)
                //{
                //    query = query.Where(x => x.nom_usuario.ToLower().Contains(model.Filter.search.ToLower()));
                //}
            }
            else
            {
                query = db.fn_kop_sel_aprobacion(model.Filter.cod_usuario, model.Filter.cod_unidad_negocio, model.Filter.cod_frecuencia, model.Filter.cod_rango_fecha.Value, "CONSULTA_CIERRES").AsQueryable();
            }

            var recordsTotal = query.Count();

            if (model.OrderBy.Count() > 0)
            {
                //por implementar
            }

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
                .Select(x => new GEN_AprobacionBean
                {
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    cod_frecuencia = x.cod_frecuencia,
                    periodo = x.periodo,
                    cod_usuario = x.cod_usuario,
                    fec_ultimo_acceso = x.fec_ultimo_acceso,
                    fec_desde = x.fec_desde,
                    fec_hasta = x.fec_hasta,
                    estado = x.estado,
                    cod_rango_fecha = x.cod_rango_fecha,
                    est_aprobacion = x.est_aprobacion
                });

            var dataTable = new DataTable<GEN_AprobacionBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }

        [HttpGet]
        [Route("GetDataTable_Indicador")]
        public DataTable<GEN_IndicadorBean> GetDataTable_Indicador([FromUri]DataTableRequest_<GEN_IndicadorBean> model)
        {
            IQueryable<GEN_IndicadorBean> query = db.fn_kop_sel_indicador(model.Filter.cod_usuario, model.Filter.cod_unidad_negocio,model.Filter.accion).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                if (model.Filter.accion == "SELECT")
                {
                    if (model.Filter.cod_indicador != null)
                    {
                        query = query.Where(x => x.cod_indicador.ToString().StartsWith(model.Filter.cod_indicador.ToString()));
                    }
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
                .Select(x => new GEN_IndicadorBean
                {
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    cod_indicador = x.cod_indicador,
                    nom_indicador = x.nom_indicador,
                    unidad = x.unidad,
                    num_decimales = x.num_decimales,
                    frecuencia_mes = x.frecuencia_mes,
                    frecuencia_sem = x.frecuencia_sem,
                    frecuencia_dia = x.frecuencia_dia,
                    tip_indicador = x.tip_indicador,
                    tip_acumulado = x.tip_acumulado,
                    ponderado_indicador = x.ponderado_indicador,
                    enlace_indicador = x.enlace_indicador,
                    formula = x.formula,
                    formula_presupuesto = x.formula_presupuesto,
                    perfil_autorizado = x.perfil_autorizado,
                    cod_grupo_indicador = x.cod_grupo_indicador,
                    cod_subgrupo_indicador = x.cod_subgrupo_indicador,
                    fec_vigencia = x.fec_vigencia,
                    fec_fin = x.fec_fin,
                    importante = x.importante,
                    tip_variacion = x.tip_variacion,
                    sql_comando = x.sql_comando,
                    documentacion = x.documentacion,
                    orden = x.orden,
                    agrupador = x.agrupador,
                    ordenS = x.ordenS,
                    ordenD = x.ordenD,
                    ordenE = x.ordenE,
                    enlace_comentario = x.enlace_comentario,
                    vis_acumulado = x.vis_acumulado,
                    excluye_parada = x.excluye_parada,
                    cod_indicador_presupuesto = x.cod_indicador_presupuesto,
                    ver_acumulado = x.ver_acumulado,
                    nom_grupo_indicador = x.nom_grupo_indicador,
                });

            var dataTable = new DataTable<GEN_IndicadorBean>()
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
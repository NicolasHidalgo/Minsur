using Beans;
using Negocio;
using Presentacion.Interfaces;
using Presentacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Presentacion.Controllers.Api
{
    [RoutePrefix("api/KCS")]
    public class KCSController : ApiController
    {
        private readonly KCS_Negocio db = new KCS_Negocio();
        public KCSController()
        {

        }
        [HttpGet]
        [Route("GetMaterialesPorNombre")]
        public IEnumerable<KCS_MaterialBean> GetMaterialesPorNombre(long term1, string term2)
        {
            return db.fn_kcs_sel_material("","",0, "MATERIAL", term1, term2).ToList();
        }

        [HttpGet]
        [Route("GetDataTable_Ceco")]
        public DataTable<KCS_CecoBean> GetDataTable_Ceco([FromUri]DataTableRequest_<AuxiliarEdit> model)
        {
            IQueryable<KCS_CecoBean> query = null;

            if (model.Filter != null)
            {
                query = db.fn_kcs_sel_ceco(model.Filter.cod_usuario, model.Filter.cod_unidad_negocio, model.Filter.periodo, "SELECT").AsQueryable();

                if (model.Filter.nivel1 != null && model.Filter.nivel1 != string.Empty)
                {
                    query = query.Where(x => x.cod_grupo_costo.StartsWith(model.Filter.nivel1));
                }

                if (model.Filter.nivel2 != null && model.Filter.nivel2 != string.Empty)
                {
                    query = query.Where(x => x.cod_grupo_costo.StartsWith(model.Filter.nivel2));
                }

                if (model.Filter.nivel3 != null && model.Filter.nivel3 != string.Empty)
                {
                    query = query.Where(x => x.cod_grupo_costo == model.Filter.nivel3);
                }

                if (model.Filter.search != null)
                {
                    query = query.Where(x => x.cod_centro_costo.ToString().Contains(model.Filter.search.ToLower()));
                }
            }
            else
            {
                query = db.fn_kcs_sel_ceco(model.Filter.cod_usuario, model.Filter.cod_unidad_negocio, model.Filter.periodo, "SELECT").AsQueryable();
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
                .Select(x => new KCS_CecoBean
                {
                    cod_centro_costo = x.cod_centro_costo,
                    ceco_detallado = x.ceco_detallado,
                    nivel1 = x.nivel1,
                    nivel2 = x.nivel2,
                    nivel3 = x.nivel3,
                    cod_grupo_costo = x.cod_grupo_costo,
                    ubicacion = x.ubicacion,
                    des_grupo_mantenimiento = x.des_grupo_mantenimiento,
                    cod_grupo_mantenimiento = x.cod_grupo_mantenimiento
                });

            var dataTable = new DataTable<KCS_CecoBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }
        [HttpGet]
        [Route("GetDataTable_Clase")]
        public DataTable<KCS_ClaseBean> GetDataTable_Clase([FromUri]DataTableRequest_<AuxiliarEdit> model)
        {
            IQueryable<KCS_ClaseBean> query = null;

            if (model.Filter != null)
            {
                query = db.fn_kcs_sel_clase(model.Filter.cod_usuario, model.Filter.cod_unidad_negocio, model.Filter.periodo, "SELECT").AsQueryable();

                if (model.Filter.tipo_gasto != null)
                {
                    if (model.Filter.tipo_gasto == "[]")
                        query = query.Where(x => x.tip_gasto.ToLower() == "");
                    else
                        query = query.Where(x => x.tip_gasto.ToLower().Contains(model.Filter.tipo_gasto.ToLower()));
                }

                if (model.Filter.categoria != null)
                {
                    query = query.Where(x => x.cod_categoria.ToLower().Contains(model.Filter.categoria.ToLower()));
                }

                if (model.Filter.search != null)
                {
                    query = query.Where(x => x.cod_cuenta.ToLower().Contains(model.Filter.search.ToLower()));
                }
            }
            else
            {
                query = db.fn_kcs_sel_clase(model.Filter.cod_usuario, model.Filter.cod_unidad_negocio, model.Filter.periodo, "SELECT").AsQueryable();
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
                .Select(x => new KCS_ClaseBean
                {
                    cod_cuenta = x.cod_cuenta,
                    nom_cuenta = x.nom_cuenta,
                    tip_gasto = x.tip_gasto,
                    cod_categoria = x.cod_categoria,
                    nom_categoria = x.nom_categoria,
                    tip_cuenta = x.tip_cuenta,
                    grupo_costo = x.grupo_costo
                });

            var dataTable = new DataTable<KCS_ClaseBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }

        [HttpGet]
        [Route("GetDataTable_Partida")]
        public DataTable<KCS_PartidaBean> GetDataTable_Partida([FromUri]DataTableRequest_<AuxiliarEdit> model)
        {
            IQueryable<KCS_PartidaBean> query = null;

            if (model.Filter != null)
            {
                query = db.fn_kcs_sel_partida(model.Filter.cod_usuario, model.Filter.cod_unidad_negocio, model.Filter.periodo, "SELECT").AsQueryable();

                if (model.Filter.search != null && model.Filter.search != string.Empty)
                {
                    query = query.Where(x => x.cod_partida.ToLower().Contains(model.Filter.search.ToLower().Trim()));
                }
                else if (model.Filter.cod_area != null)
                {
                    query = query.Where(x => x.cod_area.StartsWith(model.Filter.cod_area));
                }
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
                .Select(x => new KCS_PartidaBean
                {
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    periodo = x.periodo,
                    secuencia = x.secuencia,
                    cod_area = x.cod_area,
                    area = x.area,
                    proceso_01 = x.proceso_01,
                    proceso_02 = x.proceso_02,
                    proceso_03 = x.proceso_03,
                    cod_centro_costo = x.cod_centro_costo,
                    nom_centro_costo = x.nom_centro_costo,
                    cod_partida = x.cod_partida,
                    nom_partida = x.nom_partida,
                    cod_material = x.cod_material,
                    nom_material = x.nom_material
                });

            var dataTable = new DataTable<KCS_PartidaBean>()
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
            IQueryable<GEN_IndicadorBean> query = db.fn_kcs_sel_indicador(model.Filter.cod_usuario, model.Filter.cod_unidad_negocio, model.Filter.accion).AsQueryable();

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
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
    [RoutePrefix("api/APP")]
    public class APPController : ApiController
    {
        private readonly APP_Negocio db = new APP_Negocio();
        string cod_usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public APPController()
        {

        }

        [HttpGet]
        [Route("GetDataTable_Plantilla")]
        public DataTable<APP_PlantillaBean> GetDataTable_Plantilla([FromUri]DataTableRequest_<APP_PlantillaBean> model)
        {
            IQueryable<APP_PlantillaBean> query = db.fn_app_sel_plantilla(model.Filter.accion, model.Filter.cod_unidad_negocio, cod_usuario).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                if (model.Filter.accion == "SELECT_INDICADOR" || model.Filter.accion == "SELECT_CONFIG")
                {
                    if (model.Filter.tip_indicador == "*"){
                        model.Filter.tip_indicador = "";
                        query = query.Where(x => x.tip_indicador.ToLower().Contains(model.Filter.tip_indicador.ToLower()));
                    }
                    else
                    {
                        query = query.Where(x => x.tip_indicador.ToLower() == model.Filter.tip_indicador.ToLower()); 
                    }

                    if (model.Filter.cod_unidad_negocio == "*"){
                        model.Filter.cod_unidad_negocio = "";
                        query = query.Where(x => x.cod_unidad_negocio.ToLower().Contains(model.Filter.cod_unidad_negocio.ToLower()));
                    }
                    else
                    {
                        query = query.Where(x => x.cod_unidad_negocio.ToLower() == model.Filter.cod_unidad_negocio.ToLower());
                    }

                    if (model.Filter.search != null)
                    {
                        query = query.Where(x => x.cod_indicador.ToString().Contains(model.Filter.search.ToLower())
                                        || x.nom_indicador.ToLower().Contains(model.Filter.search.ToLower()));
                    }
                }

                if (model.Filter.accion == "SELECT_XLS")
                {
                    if (model.Filter.search != null)
                    {
                        query = query.Where(x => x.descripcion.ToLower().Contains(model.Filter.search.ToLower())
                                        || x.unidad.ToLower().Contains(model.Filter.search.ToLower()));
                    }
                }
                if (model.Filter.accion == "SELECT_SEG")
                {
                    if (model.Filter.search != null)
                    {
                        query = query.Where(x => x.descripcion.ToLower().Contains(model.Filter.search.ToLower())
                                        || x.tipo.ToLower().Contains(model.Filter.search.ToLower()));
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
                .Select(x => new APP_PlantillaBean
                {
                    cod_frecuencia = x.cod_frecuencia,
                    ide_plantilla = x.ide_plantilla,
                    pais = x.pais,
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    cod_interno = x.cod_interno,
                    descripcion = x.descripcion,
                    unidad = x.unidad,
                    cod_indicador = x.cod_indicador,
                    requerido = x.requerido,
                    oculto = x.oculto,
                    estilo_linea = x.estilo_linea,
                    estilo_color = x.estilo_color,
                    estilo_fondo = x.estilo_fondo,
                    formato = x.formato,
                    agrupado = x.agrupado,

                    unidad_negocio = x.unidad_negocio,
                    tipo = x.tipo,
                    cod_indicador_base = x.cod_indicador_base,

                    nom_indicador = x.nom_indicador,
                    und_indicador = x.und_indicador,
                    tip_indicador = x.tip_indicador,
                    tip_variacion = x.tip_variacion,
                    frecuencia = x.frecuencia,

                    tip_carga = x.tip_carga,
                    cod_indicador_operativo = x.cod_indicador_operativo,
                    cod_unidad_negocio_operativo = x.cod_unidad_negocio_operativo,

                    nom_indicador_app = x.nom_indicador_app,
                    nom_indicador_operativo = x.nom_indicador_operativo,
                    ide_configuracion = x.ide_configuracion
                });
    
            var dataTable = new DataTable<APP_PlantillaBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }

        [HttpGet]
        [Route("GetDataTable_RepXLS")]
        public DataTable<APP_PlantillaBean> GetDataTable_RepXLS([FromUri]DataTableRequest_<APP_PlantillaBean> model)
        {
            if (model.Filter.cod_unidad_negocio == "*")
                model.Filter.cod_unidad_negocio = "";
            IQueryable<APP_PlantillaBean> query = db.fn_app_rep_carga(model.Filter.accion, model.Filter.cod_unidad_negocio, cod_usuario, model.Filter.cod_frecuencia,model.Filter.cod_rango_fecha, model.Filter.tipo).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                if (model.Filter.accion.Contains("XLS_SEG"))
                {
                    if (model.Filter.search != null)
                    {
                        query = query.Where(x => x.descripcion.ToLower().Contains(model.Filter.search.ToLower())
                                        || x.unidad.ToLower().Contains(model.Filter.search.ToLower())
                                        || x.real.ToString().Contains(model.Filter.search.ToLower())
                                        || x.ppto.ToString().Contains(model.Filter.search.ToLower()));
                    }

                    if (model.Filter.grupo != "*")
                    {
                        query = query.Where(x => x.grupo.ToLower().Contains(model.Filter.grupo.ToLower()));
                    }
                }
                if (model.Filter.accion == "XLS_OPE")
                {
                    if (model.Filter.search != null)
                    {
                        query = query.Where(x => x.nom_indicador.ToLower().Contains(model.Filter.search.ToLower())
                                        || x.und_indicador.ToLower().Contains(model.Filter.search.ToLower()));
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
                .Select(x => new APP_PlantillaBean
                {
                    cod_frecuencia = x.cod_frecuencia,
                    ide_plantilla = x.ide_plantilla,
                    pais = x.pais,
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    cod_rango_fecha = x.cod_rango_fecha,
                    cod_interno = x.cod_interno,
                    descripcion = x.descripcion,
                    unidad = x.unidad,
                    cod_indicador = x.cod_indicador,
                    requerido = x.requerido,
                    oculto = x.oculto,
                    estilo_linea = x.estilo_linea,
                    estilo_color = x.estilo_color,
                    estilo_fondo = x.estilo_fondo,
                    formato = x.formato,
                    agrupado = x.agrupado,
                    estado = x.estado,

                    unidad_negocio = x.unidad_negocio,
                    tipo = x.tipo,
                    cod_indicador_base = x.cod_indicador_base,

                    nom_indicador = x.nom_indicador,
                    und_indicador = x.und_indicador,
                    tip_indicador = x.tip_indicador,
                    tip_variacion = x.tip_variacion,
                    frecuencia = x.frecuencia,

                    tip_carga = x.tip_carga,
                    cod_indicador_operativo = x.cod_indicador_operativo,
                    cod_unidad_negocio_operativo = x.cod_unidad_negocio_operativo,

                    nom_unidad_negocio = x.nom_unidad_negocio,
                    cod_mes = x.cod_mes,
                    real = x.real,
                    ppto = x.ppto,
                    frct = x.frct,
                    var_frct = x.var_frct,
                    sem_frct = x.sem_frct,

                    real_acm = x.real_acm,
                    ppto_acm = x.ppto_acm,
                    frct_acm = x.frct_acm,
                    var_frct_acm = x.var_frct_acm,
                    sem_frct_acm = x.sem_frct_acm,

                    frct_esp = x.frct_esp,
                    grupo = x.grupo,
                });

            var dataTable = new DataTable<APP_PlantillaBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }

        [HttpGet]
        [Route("GetDataTable_Publicacion")]
        public DataTable<GEN_AprobacionBean> GetDataTable_Publicacion([FromUri]DataTableRequest_<GEN_AprobacionBean> model)
        {
            IQueryable<GEN_AprobacionBean> query = db.fn_app_sel_publicacion(model.Filter.accion, model.Filter.cod_unidad_negocio, model.Filter.cod_usuario, model.Filter.cod_frecuencia, model.Filter.cod_rango_fecha.Value, model.Filter.cod_modulo).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                if (model.Filter.accion == "SELECT")
                {
                    if (model.Filter.search != null)
                    {
                        //query = query.Where(x => x.descripcion.ToLower().Contains(model.Filter.search.ToLower())
                        //                || x.unidad.ToLower().Contains(model.Filter.search.ToLower()));
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
                .Select(x => new GEN_AprobacionBean
                {
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    nom_unidad_negocio = x.nom_unidad_negocio,
                    cod_modulo = x.cod_modulo,
                    cod_frecuencia = x.cod_frecuencia,
                    frecuencia = x.frecuencia,
                    cod_rango_fecha = x.cod_rango_fecha,
                    rango_fecha = x.rango_fecha,
                    est_aprobacion = x.est_aprobacion,
                    estado_operativo = x.estado_operativo,
                    estado_publicacion = x.estado_publicacion,
                    fec_actualizacion = x.fec_actualizacion,
                    fec_ult_cierre = x.fec_ult_cierre,
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
    }
}
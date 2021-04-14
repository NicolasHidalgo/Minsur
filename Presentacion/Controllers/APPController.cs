using Beans;
using Negocio;
using Presentacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class APPController : BaseController
    {
        APP_Negocio appNeg = new APP_Negocio();
        TV_Negocio tvNeg = new TV_Negocio();
        static string xlsPathDES = ConfigurationManager.AppSettings["xlsPathDES"];
        static string xlsPathPRO = ConfigurationManager.AppSettings["xlsPathPRO"];

        public ActionResult Indicador()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var dataTipoFilter = appNeg.fn_app_sel_tipo("DDL_TIPO_FILTER", model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_TipoBean>();
            model.ddlTipoFilter = dataTipoFilter.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            var dataTipo = appNeg.fn_app_sel_tipo("DDL_TIPO", model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_TipoBean>();
            model.ddlTipo = dataTipo.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            var dataFrecuencia = appNeg.fn_app_sel_tipo("DDL_FRECUENCIA", model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_TipoBean>();
            model.ddlFrecuencia = dataFrecuencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            var dataUnidadFilter = appNeg.fn_app_sel_unidad(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, "DDL_UNIDAD_FILTER") ?? new List<GEN_UnidadNegocioBean>();
            model.ddlUnidadFilter = dataUnidadFilter.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_operativo,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var data = appNeg.fn_app_sel_unidad(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, "DDL_UNIDAD") ?? new List<GEN_UnidadNegocioBean>();
            model.Unidades = data.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_operativo,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            return View(model);
        }

        public ActionResult IndicadorConfig()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var dataTipoFilter = appNeg.fn_app_sel_tipo("DDL_TIPO_FILTER", model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_TipoBean>();
            model.ddlTipoFilter = dataTipoFilter.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });
            var dataUnidadFilter = appNeg.fn_app_sel_unidad(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, "DDL_UNIDAD_FILTER") ?? new List<GEN_UnidadNegocioBean>();
            model.ddlUnidadFilter = dataUnidadFilter.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_operativo,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var dataUnidad = appNeg.fn_app_sel_unidad(Usuario.Item1.cod_usuario, "", "DDL_UNIDAD") ?? new List<GEN_UnidadNegocioBean>();
            model.ddlUnidad = dataUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_operativo,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            return View(model);
        }

        public ActionResult Edit_IndicadorConfig(long ide_configuracion)
        {
            var model = new AuxiliarEdit();
            model.APP_PlantillaBean = appNeg.fn_app_get_indicador_config("GET_INDICADOR_CONFIG2", ide_configuracion, Usuario.Item1.cod_usuario);
            if (model == null)
            {
                return HttpNotFound();
            }

            var tip_indicador = model.APP_PlantillaBean.tip_indicador;
            var carga_operativa = model.APP_PlantillaBean.carga_operativa;
            long cod_indicador = model.APP_PlantillaBean.cod_indicador.Value;
            string cod_unidad_negocio = model.APP_PlantillaBean.cod_unidad_negocio;
            string frecuencia = model.APP_PlantillaBean.frecuencia;
            string tip_carga = model.APP_PlantillaBean.tip_carga;
            long cod_indicador_operativo = model.APP_PlantillaBean.cod_indicador_operativo.Value;

            var dataAppIndicador = appNeg.fn_app_sel_indicador("DDL_INDICADOR_APP", cod_unidad_negocio, Usuario.Item1.cod_usuario, tip_indicador) ?? new List<GEN_IndicadorBean>();
            var dataAppSelected = dataAppIndicador.Where(a => a.cod_indicador == cod_indicador).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
            model.ddlIndicadorApp = dataAppIndicador.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_indicador.ToString(),
                Text = x.nom_indicador,
                Selected = dataAppSelected.Any(c => c == x.cod_indicador),
                HtmlAttributes = new
                {
                    data_alias = x.cod_indicador
                }
            });

            if (tip_indicador.Contains("OPE"))
            {
                var dataUnidad = appNeg.fn_app_sel_unidad(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, "DDL_UNIDAD") ?? new List<GEN_UnidadNegocioBean>();
                var dataUnidadSelected = dataUnidad.Where(a => a.cod_unidad_negocio == cod_unidad_negocio).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
                model.ddlUnidad = dataUnidad.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_unidad_negocio.ToString(),
                    Text = x.nom_operativo,
                    Selected = dataUnidadSelected.Any(c => c == x.cod_unidad_negocio),
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_unidad_negocio
                    }
                });
            }

            

            List<GEN_FrecuenciaBean> dataFrecuencia = new List<GEN_FrecuenciaBean>();
            dataFrecuencia.Add(new GEN_FrecuenciaBean()
            {
                cod_frecuencia = "D",
                frecuencia = "Diario"
            });
            dataFrecuencia.Add(new GEN_FrecuenciaBean()
            {
                cod_frecuencia = "S",
                frecuencia = "Semanal"
            });
            dataFrecuencia.Add(new GEN_FrecuenciaBean()
            {
                cod_frecuencia = "M",
                frecuencia = "Mensual"
            });
            var frecs = frecuencia.Split(',');
            model.ddlFrecuencia = dataFrecuencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_frecuencia.ToString(),
                Text = x.frecuencia,
                Selected = frecs.Any(c => c == x.cod_frecuencia),
                HtmlAttributes = new
                {
                    data_alias = x.cod_frecuencia
                }
            });

            List<GEN_TipoBean> dataTipoCarga = new List<GEN_TipoBean>();
            dataTipoCarga.Add(new GEN_TipoBean()
            {
                valor = "D(%)",
                texto = "D(%)"
            });
            dataTipoCarga.Add(new GEN_TipoBean()
            {
                valor = "D(H12)",
                texto = "D(H12)"
            });
            dataTipoCarga.Add(new GEN_TipoBean()
            {
                valor = "I",
                texto = "I"
            });
            dataTipoCarga.Add(new GEN_TipoBean()
            {
                valor = "ID",
                texto = "ID"
            });
            dataTipoCarga.Add(new GEN_TipoBean()
            {
                valor = "ID(H12)",
                texto = "ID(H12)"
            });
            var dataTipoCargaSelected = dataTipoCarga.Where(a => a.valor == tip_carga).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.DropDownList = dataTipoCarga.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataTipoCargaSelected.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });


            List<GEN_IndicadorBean> dataIndicadorOpe = null;

            if (tip_indicador.Contains("OPE"))
            {
                if (carga_operativa == true)
                {
                    dataIndicadorOpe = appNeg.fn_app_sel_indicador("DDL_CODINTERNO_XLS", cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_IndicadorBean>();
                    var dataIndicadorOpeSelected = dataIndicadorOpe.Where(a => a.cod_indicador == cod_indicador_operativo).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
                    model.ddlIndicador = dataIndicadorOpe.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_indicador.ToString(),
                        Text = x.nom_indicador,
                        Selected = dataIndicadorOpeSelected.Any(c => c == x.cod_indicador),
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_indicador
                        }
                    });
                }
                else
                {
                    dataIndicadorOpe = appNeg.fn_app_sel_indicador("DDL_INDICADOR_OPE", cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_IndicadorBean>();
                    var dataIndicadorOpeSelected = dataIndicadorOpe.Where(a => a.cod_indicador == cod_indicador_operativo).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
                    model.ddlIndicador = dataIndicadorOpe.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_indicador.ToString(),
                        Text = x.nom_indicador,
                        Selected = dataIndicadorOpeSelected.Any(c => c == x.cod_indicador),
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_indicador
                        }
                    });
                }

                var dataIndicador1OpeSelected = dataIndicadorOpe.Where(a => a.cod_indicador == model.APP_PlantillaBean.cod_indicador_referencia1_operativo).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
                model.ddlIndicador1 = dataIndicadorOpe.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_indicador.ToString(),
                    Text = x.nom_indicador,
                    Selected = dataIndicador1OpeSelected.Any(c => c == x.cod_indicador),
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_indicador
                    }
                });
                var dataIndicador2OpeSelected = dataIndicadorOpe.Where(a => a.cod_indicador == model.APP_PlantillaBean.cod_indicador_referencia2_operativo).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
                model.ddlIndicador2 = dataIndicadorOpe.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_indicador.ToString(),
                    Text = x.nom_indicador,
                    Selected = dataIndicador2OpeSelected.Any(c => c == x.cod_indicador),
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_indicador
                    }
                });
                var dataIndicador3OpeSelected = dataIndicadorOpe.Where(a => a.cod_indicador == model.APP_PlantillaBean.cod_indicador_referencia3_operativo).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
                model.ddlIndicador3 = dataIndicadorOpe.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_indicador.ToString(),
                    Text = x.nom_indicador,
                    Selected = dataIndicador3OpeSelected.Any(c => c == x.cod_indicador),
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_indicador
                    }
                });
                var dataIndicador4OpeSelected = dataIndicadorOpe.Where(a => a.cod_indicador == model.APP_PlantillaBean.cod_indicador_referencia4_operativo).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
                model.ddlIndicador4 = dataIndicadorOpe.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_indicador.ToString(),
                    Text = x.nom_indicador,
                    Selected = dataIndicador4OpeSelected.Any(c => c == x.cod_indicador),
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_indicador
                    }
                });
            }
            else
            {
                dataIndicadorOpe = appNeg.fn_app_sel_indicador("DDL_INDICADOR_APP", cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_IndicadorBean>();
                var dataIndicadorOpeSelected = dataIndicadorOpe.Where(a => a.cod_indicador == cod_indicador).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
                model.ddlIndicador = dataIndicadorOpe.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_indicador.ToString(),
                    Text = x.nom_indicador,
                    Selected = dataIndicadorOpeSelected.Any(c => c == x.cod_indicador),
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_indicador
                    }
                });
            }
            

            List<GEN_TipoBean> dataFormato = new List<GEN_TipoBean>();
            dataFormato.Add(new GEN_TipoBean()
            {
                valor = "",
                texto = ""
            });
            dataFormato.Add(new GEN_TipoBean()
            {
                valor = "#,##0",
                texto = "#,##0"
            });
            dataFormato.Add(new GEN_TipoBean()
            {
                valor = "#,##0.0",
                texto = "#,##0.0"
            });
            dataFormato.Add(new GEN_TipoBean()
            {
                valor = "#,##0.00",
                texto = "#,##0.00"
            });
            dataFormato.Add(new GEN_TipoBean()
            {
                valor = "#,##0.000",
                texto = "#,##0.000"
            });

            dataFormato.Add(new GEN_TipoBean()
            {
                valor = "#,##0%",
                texto = "#,##0%"
            });
            dataFormato.Add(new GEN_TipoBean()
            {
                valor = "#,##0.0%",
                texto = "#,##0.0%"
            });
            dataFormato.Add(new GEN_TipoBean()
            {
                valor = "#,##0.00%",
                texto = "#,##0.00%"
            });
            dataFormato.Add(new GEN_TipoBean()
            {
                valor = "#,##0.000%",
                texto = "#,##0.000%"
            });
            var dataFormatoSelected = dataFormato.Where(a => a.valor == model.APP_PlantillaBean.formato).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.ddlFormato = dataFormato.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataFormatoSelected.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });
            var dataFormatoSelectedRef1 = dataFormato.Where(a => a.valor == model.APP_PlantillaBean.formato_ref1).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.ddlFormatoRef1 = dataFormato.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataFormatoSelectedRef1.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });
            var dataFormatoSelectedRef2 = dataFormato.Where(a => a.valor == model.APP_PlantillaBean.formato_ref2).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.ddlFormatoRef2 = dataFormato.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataFormatoSelectedRef2.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });
            var dataFormatoSelectedRef3 = dataFormato.Where(a => a.valor == model.APP_PlantillaBean.formato_ref3).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.ddlFormatoRef3 = dataFormato.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataFormatoSelectedRef3.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });
            var dataFormatoSelectedRef4 = dataFormato.Where(a => a.valor == model.APP_PlantillaBean.formato_ref4).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.ddlFormatoRef4 = dataFormato.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataFormatoSelectedRef4.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            return View(model);
        }

        public ActionResult PlantillaXLS()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var dataUnidad = appNeg.fn_app_sel_unidad(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, "DDL_UNIDAD") ?? new List<GEN_UnidadNegocioBean>();
            model.Unidades = dataUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_operativo,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var dataFrecuencia = appNeg.fn_app_sel_tipo("DDL_FRECUENCIA", model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_TipoBean>();
            model.ddlFrecuencia = dataFrecuencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            return View(model);
        }

        public ActionResult PlantillaSEG()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var data = appNeg.fn_app_sel_unidad(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, "DDL_UNIDAD") ?? new List<GEN_UnidadNegocioBean>();
            var dataSelected = data.Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.Unidades = data.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_operativo,
                Selected = dataSelected.Any(c => c == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var dataIndicador = appNeg.fn_app_sel_indicador("DDL_INDICADOR_APP", cod_unidad_negocio, Usuario.Item1.cod_usuario, "SEG") ?? new List<GEN_IndicadorBean>();
            model.ddlIndicador = dataIndicador.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_indicador.ToString(),
                Text = x.nom_indicador,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_indicador
                }
            });

            var dataTipo = appNeg.fn_app_sel_tipo("DDL_TIPO_SEG", model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_TipoBean>();
            model.ddlTipo = dataTipo.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            return View(model);
        }

        public ActionResult CargaXLS_OPE()
        {
            string fec_informe = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.fecha = fec_informe;
            return View();
        }
        public ActionResult CargaXLS_OPE_Brasil()
        {
            string fecha = DateTime.Now.AddDays(-1).ToString("yyyy-MM", CultureInfo.InvariantCulture);
            ViewBag.fecha = fecha;
            return View();
        }
        public ActionResult CargaXLS_SEG()
        {
            //string fec_informe = DateTime.Now.AddDays(-1).ToString("yyyy-MM", CultureInfo.InvariantCulture);
            //ViewBag.fecha = fec_informe;
            return View();
        }
        public ActionResult RepXLS_SEG()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var dataUnidad = appNeg.fn_app_sel_unidad(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, "DDL_UNIDAD_FILTER") ?? new List<GEN_UnidadNegocioBean>();
            model.Unidades = dataUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_operativo,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var dataTipo = appNeg.fn_app_sel_tipo("DDL_TIPO_SEG_FILTER", model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_TipoBean>();
            model.ddlTipo = dataTipo.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            //string fecha = DateTime.Now.AddDays(-1).ToString("yyyy-MM", CultureInfo.InvariantCulture);
            //ViewBag.fecha = fecha;

            return View(model);
        }
        public ActionResult RepXLS_OPE()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }


            var dataUnidad = appNeg.fn_app_sel_unidad(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, "DDL_UNIDAD_FILTER") ?? new List<GEN_UnidadNegocioBean>();
            model.Unidades = dataUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_operativo,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            string fec_informe = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.fecha = fec_informe;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit_Plantilla(APP_PlantillaBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                //if (Session["cod_unidad_negocio"] != null)
                //{
                //    cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                //}

                mensajeBean = appNeg.up_app_cud_plantilla(model.accion, Usuario.Item1.cod_usuario, model);

                if (mensajeBean.tipo == "SUCCESS")
                {
                    return Json(
                     new Response
                     {
                         Status = HttpStatusCode.OK,
                         Message = mensajeBean.mensaje
                     },
                     JsonRequestBehavior.AllowGet);
                }
                return Json(
                    new Response
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = mensajeBean.mensaje
                    },
                    JsonRequestBehavior.AllowGet);
            }

            return Json(
                     new Response
                     {
                         Status = HttpStatusCode.BadRequest,
                         Message = "No se puede continuar por errores en el modelo",
                         Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                     },
                     JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetPlantillaById(string cod_unidad_negocio, int? ide_plantilla, string cod_frecuencia)
        {
            if (cod_unidad_negocio == null || cod_unidad_negocio == string.Empty)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var objPlantilla = appNeg.fn_app_get_plantilla("GET_PLANTILLA_XLS", cod_unidad_negocio, Usuario.Item1.cod_usuario, ide_plantilla.Value, cod_frecuencia) ?? new APP_PlantillaBean();
            var model = new AuxiliarEdit();
            model.APP_PlantillaBean = objPlantilla;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetDropDownIndicador(string cod_unidad_negocio)
        {
            if (cod_unidad_negocio == null || cod_unidad_negocio == string.Empty)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var dataIndicador = appNeg.fn_app_sel_indicador("DDL_INDICADOR_OPE", cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_IndicadorBean>();
            var dataSelected = dataIndicador.Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
            var model = new AuxiliarEdit();
            model.DropDownList = dataIndicador.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_indicador.ToString(),
                Text = x.nom_indicador,
                Selected = dataSelected.Any(c => c == x.cod_indicador),
                HtmlAttributes = new
                {
                    data_alias = x.cod_indicador
                }
            });
            return Json(model.DropDownList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFileAjax(string accion, string fec_informe, string cod_frecuencia, string tipo)
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var fileContent = Request.Files[i];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string fileCliente = Path.GetFileName(fileContent.FileName);
                        string fileExtension = Path.GetExtension(fileContent.FileName).ToLower();
                        string fileDestino = string.Empty;
                        string fileDestinoDBS = string.Empty;
                        int cod_rango_fecha = 0;

                        if (fileExtension != ".xlsx" && fileExtension != ".xls")
                        {
                            return Json(
                                    new Response
                                    {
                                        Status = HttpStatusCode.BadRequest,
                                        Message = "El archivo debe ser tipo Excel (.xlsx,.xls)",
                                    },
                                    JsonRequestBehavior.AllowGet);
                        }

                        // get a stream
                        var stream = fileContent.InputStream;
                        var nomenclatura = string.Empty;
                        if (tipo == "OPE")
                            nomenclatura = "APP_OPE_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xlsx";
                        if (tipo == "SEG")
                            nomenclatura = "APP_SEG_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xlsx";
                        if (tipo == "BRASIL")
                            nomenclatura = "APP_BRASIL_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xlsx";

                        if (HttpContext.IsDebuggingEnabled)
                        {
                            fileDestino = Path.Combine(@"M:\MVC\com\xlsd", nomenclatura);
                            fileDestinoDBS = fileDestino.Replace(@"M:\MVC\com\xlsd", xlsPathDES);
                        }
                        else
                        {
                            fileDestino = Path.Combine(Server.MapPath("~/XlsD"), nomenclatura);
                            fileDestinoDBS = fileDestino.Replace(Server.MapPath("~/XlsD"), xlsPathPRO);
                            //fileDestinoDBS = fileDestino;
                        }

                        var mensajeError = string.Empty;
                        if (accion == "CARGA")
                        {
                            using (var fileStream = System.IO.File.Create(fileDestinoDBS))
                            {
                                stream.CopyTo(fileStream);
                            }
                        }

                        //Grabacion de temporal en sql
                        var cod_unidad_negocio = string.Empty;
                        if (Session["cod_unidad_negocio"] != null)
                        {
                            cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                        }

                        try
                        {
                            var excelApp = new Microsoft.Office.Interop.Excel.Application();
                            excelApp.Application.Visible = false;
                            excelApp.DisplayAlerts = false;
                            var excelWorkbook = excelApp.Workbooks.Open(Filename: fileDestinoDBS, ReadOnly: false);

                            try
                            {
                                if (tipo == "OPE")
                                {
                                    if (cod_frecuencia == "D")
                                        excelWorkbook.Sheets["TCD"].Range("A1:Z400").NumberFormat = "0.0000000000000000";
                                    if (cod_frecuencia == "S")
                                        excelWorkbook.Sheets["TCS"].Range("A1:Z400").NumberFormat = "0.0000000000000000";
                                    if (cod_frecuencia == "M")
                                        excelWorkbook.Sheets["TCM"].Range("A1:Z400").NumberFormat = "0.0000000000000000";
                                }

                                if (tipo == "SEG")
                                {
                                    excelWorkbook.Sheets["Seguridad"].Range("A1:Z400").NumberFormat = "0.0000000000000000";
                                }

                                if (tipo == "BRASIL")
                                {
                                    excelWorkbook.Sheets["TABOCA"].Range("E2:R50").NumberFormat = "0.0000000000000000";
                                }

                            }
                            catch (Exception exc)
                            {
                                mensajeError = "ERROR: En la ejecución de Excel " + exc.Message;
                            }

                            //Close Excel
                            excelWorkbook.Close(SaveChanges: true);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
                            excelWorkbook = null;
                            excelApp.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                            excelApp = null;

                            //force a garbage collection 
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                            var archivoDestino = fileDestinoDBS.Replace(@"\", "/");

                            foreach (var item in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                            {
                                if (item.MainWindowTitle == "Microsoft Excel - " + archivoDestino || item.MainWindowTitle == archivoDestino + " - Excel"
                                        || item.MainWindowTitle == fileDestino + " - Excel" || item.MainWindowTitle == "Microsoft Excel - " + fileDestino)
                                {
                                    item.Kill();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Threading.Thread.Sleep(1000);  //Espera un segundo para intentarlo de nuevo
                            mensajeError += " Servicio de Excel en estos momentos se encuentra sin recursos.\r Por favor intentelo mas tarde.";
                        }

                        if (tipo == "OPE")
                        {
                            DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            cod_rango_fecha = int.Parse(dtfecInforme.ToString("yyMMdd"));
                        }
                        if (tipo == "SEG")
                        {
                            fec_informe = fec_informe + "-01";
                            DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            cod_rango_fecha = int.Parse(dtfecInforme.ToString("yyyyMM"));
                        }
                        if (tipo == "BRASIL")
                        {
                            var exec = tvNeg.up_tv_cargaKPI_taboca(Usuario.Item1.cod_usuario, "CARGA", fileDestinoDBS);

                            if (exec.tipo == "ERROR")
                            {
                                return Json(
                                new Response
                                {
                                    Status = HttpStatusCode.BadRequest,
                                    Message = exec.mensaje.Replace("\n", "<br>")
                                },
                                JsonRequestBehavior.AllowGet);
                            }

                            return Json(
                                new Response
                                {
                                    Status = HttpStatusCode.OK,
                                    Message = exec.mensaje,
                                },
                                JsonRequestBehavior.AllowGet);

                        }

                        var execute = appNeg.up_app_pro_cargaXLS(cod_unidad_negocio, Usuario.Item1.cod_usuario, fileDestinoDBS, fileDestino, "CARGA", cod_frecuencia, cod_rango_fecha, tipo);
                        
                        if (execute.tipo == "ERROR")
                        {
                            return Json(
                            new Response
                            {
                                Status = HttpStatusCode.BadRequest,
                                Message = execute.mensaje.Replace("\n", "<br>")
                            },
                            JsonRequestBehavior.AllowGet);
                        }
                        else if (execute.tipo == "SUCCESS")
                        {
                            execute = appNeg.up_app_pro_cargaXLS(cod_unidad_negocio, Usuario.Item1.cod_usuario, fileDestinoDBS, fileDestino, "START", cod_frecuencia, cod_rango_fecha, tipo);

                            if (execute.tipo == "ERROR")
                            {
                                return Json(
                                new Response
                                {
                                    Status = HttpStatusCode.BadRequest,
                                    Message = execute.mensaje,
                                    Kind = execute.tipo,
                                    Id = execute.Id.ToString()
                                },
                                JsonRequestBehavior.AllowGet);
                            }
                            else if (execute.tipo == "WARNING")
                            {
                                return Json(
                                new Response
                                {
                                    Status = HttpStatusCode.BadRequest,
                                    Message = execute.mensaje,
                                    Kind = execute.tipo,
                                    Id = execute.Id.ToString(),
                                    Aux = nomenclatura,
                                    Aux2 = fileDestinoDBS,
                                    Aux3 = fileDestino
                                },
                                JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                execute = appNeg.up_app_pro_cargaXLS(cod_unidad_negocio, Usuario.Item1.cod_usuario, fileDestinoDBS, fileDestino, "DETALLE", cod_frecuencia, cod_rango_fecha, tipo);

                                if (execute.tipo == "ERROR")
                                {
                                    return Json(
                                    new Response
                                    {
                                        Status = HttpStatusCode.BadRequest,
                                        Message = execute.mensaje,
                                        Kind = execute.tipo,
                                        Id = execute.Id.ToString()
                                    },
                                    JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(
                                        new Response
                                        {
                                            Status = HttpStatusCode.OK,
                                            Message = execute.mensaje
                                        },
                                        JsonRequestBehavior.AllowGet);
                                }
                            }

                        }

                    }
                }
            }
            catch (Exception e)
            {
                return Json(
                    new Response
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "El archivo no se cargó." + e.Message, 
                    },
                    JsonRequestBehavior.AllowGet);
            }

            return Json(
                     new Response
                     {
                         Status = HttpStatusCode.BadRequest,
                         Message = "No se puede continuar por errores en el modelo",
                         Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                     },
                     JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProcesarFileAjax(string accion, string nomenclatura, string fec_informe, string cod_frecuencia, string tipo)
        {
            try
            {
                var cod_unidad_negocio = string.Empty;
                int cod_rango_fecha = 0;
                var fileDestino = string.Empty;
                var fileDestinoDBS = string.Empty;

                if (Session["cod_unidad_negocio"] != null)
                {
                    cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                }

                if (HttpContext.IsDebuggingEnabled)
                {
                    fileDestino = Path.Combine(@"M:\MVC\com\xlsd", nomenclatura);
                    fileDestinoDBS = fileDestino.Replace(@"M:\MVC\com\xlsd", xlsPathDES);
                }
                else
                {
                    fileDestino = Path.Combine(Server.MapPath("~/XlsD"), nomenclatura);
                    fileDestinoDBS = fileDestino.Replace(Server.MapPath("~/XlsD"), xlsPathPRO);
                }

                if (tipo == "OPE")
                {
                    DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    cod_rango_fecha = int.Parse(dtfecInforme.ToString("yyMMdd"));
                }
                if (tipo == "SEG")
                {
                    fec_informe = fec_informe + "-01";
                    DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    cod_rango_fecha = int.Parse(dtfecInforme.ToString("yyyyMM"));
                }

                var execute = appNeg.up_app_pro_cargaXLS(cod_unidad_negocio, Usuario.Item1.cod_usuario, fileDestinoDBS, fileDestino, accion, cod_frecuencia, cod_rango_fecha, tipo);

                if (execute.tipo == "ERROR")
                {
                    return Json(
                        new Response
                        {
                            Status = HttpStatusCode.BadRequest,
                            Message = execute.mensaje.Replace("\n", "<br>")
                        },
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(
                        new Response
                        {
                            Status = HttpStatusCode.OK,
                            Message = execute.mensaje.Replace("\n", "<br>")
                        },
                        JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(
                     new Response
                     {
                         Status = HttpStatusCode.BadRequest,
                         Message = ex.Message
                     },
                     JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult RunXls(string cod_unidad_negocio, string cod_frecuencia, int? cod_rango_fecha, string accion)
        {
            var mensajeError = string.Empty;
            var tablero = string.Empty;
            var codFrecuencia = cod_frecuencia;
            var codUnidadNegocio = cod_unidad_negocio;
            var codRangoFecha = cod_rango_fecha;
            int pid = 0;

            var resultado = string.Empty;
            var mensaje = string.Empty;
            var archivoDestino = string.Empty;

            if (codFrecuencia == "D")
                tablero = "TCD";
            else if (codFrecuencia == "S")
                tablero = "TCS";
            else
                tablero = "TCM";

            if (codUnidadNegocio == "" || codUnidadNegocio == " ")
                codUnidadNegocio = "COR";

            //Identificar el archivo a descargar, incluyendo la ruta de acceso.
            var excelFuente = string.Empty;
            var excelDestino = string.Empty;
            var fileDestino = string.Empty;
            //excelFuente = Server.MapPath("~/xlso/COR_tableroControl.xlsm");
            excelFuente = Server.MapPath("~/xlso/KPI_COR_Consolidado.xlsm");

            //Cambiamos el nombre del archivo Destino
            fileDestino = tablero + "_" + codUnidadNegocio + "_" + codRangoFecha + "_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xlsx";
            excelDestino = Server.MapPath("~/xlsd/" + fileDestino);

            try
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Application.Visible = false;
                excelApp.DisplayAlerts = false;

                var excelWorkbook = excelApp.Workbooks.Open(Filename: excelFuente, ReadOnly: true);
                GetWindowThreadProcessId(excelApp.Hwnd, out pid);

                excelWorkbook.Sheets["Registro"].Range("B5").Value = codUnidadNegocio;
                excelWorkbook.Sheets["Registro"].Range("E5").Value = codFrecuencia;
                excelWorkbook.Sheets["Registro"].Range("F5").Value = codRangoFecha;
                excelWorkbook.Sheets["Registro"].Range("H5").Value = excelDestino;

                try
                {
                    mensajeError = excelApp.Run("BtnRegistro_Carga");
                }
                catch (Exception)
                {
                    mensajeError = "ERROR: En la ejecución de Excel";
                }


                if (mensajeError == null)
                    mensajeError = "";

                mensajeError.Replace("'", "´");
                mensajeError.Replace(((char)34).ToString(), "´");
                mensajeError.Replace(((char)13).ToString(), "\r");
                mensajeError.Replace(((char)10).ToString(), "\r");

                //Close Excel
                excelWorkbook.Close(SaveChanges: false);
                excelWorkbook = null;
                excelApp.Quit();
                archivoDestino = excelDestino.Replace(@"\", "/");

                try
                {
                    //procesoExcel.Kill();
                    Process.GetProcessById(pid).Kill();
                }
                catch (Exception)
                {
                }

            }
            catch (Exception ex)
            {
                System.Threading.Thread.Sleep(1000);  //Espera un segundo para intentarlo de nuevo
                mensajeError += " Servicio de Excel en estos momentos se encuentra sin recursos.\r Por favor intentelo mas tarde.";
                try
                {
                    Process.GetProcessById(pid).Kill();
                }
                catch (Exception)
                {
                }
            }

            if (mensajeError == "")
            {
                return Json(
                new Response
                {
                    Status = HttpStatusCode.OK,
                    Message = archivoDestino
                },
                JsonRequestBehavior.AllowGet);
            }

            return Json(
                    new Response
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = mensajeError
                    },
                    JsonRequestBehavior.AllowGet);

        }

        public ActionResult DownloadFile(string archivoDestino)
        {
            /*==========================================================================================================================
            ' Download del archivo Excel
            '===========================================================================================================================*/
            FileInfo fileInfo = new FileInfo(archivoDestino);
            if (fileInfo.Exists)
            {
                int pos = archivoDestino.LastIndexOf("/") + 1;
                var filename = archivoDestino.Substring(pos, archivoDestino.Length - pos);

                return File(archivoDestino, "application/vnd.ms-excel", filename);
            }
            return null;
        }

        public ActionResult Publicacion()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var dataTipo = appNeg.fn_app_sel_tipo("DDL_TIPO_PUBL", model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "") ?? new List<GEN_TipoBean>();
            model.ddlTipo = dataTipo.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });
            var cod_modulo = dataTipo.Select(l => l.valor).First();

            var dataFrecuencia = appNeg.fn_app_sel_tipo("DDL_FRECUENCIA_PUBL", model.cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_modulo) ?? new List<GEN_TipoBean>();
            model.ddlFrecuencia = dataFrecuencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            var cod_frecuencia = dataFrecuencia.Select(l => l.valor).First();

            //Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listPeriodo = appNeg.fn_app_sel_rangoFecha("PERIODO", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, 0, 0, 0);
            //int periodoTop = listPeriodo.Item1.Select(l => l.periodo).First();
            //Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listMes = appNeg.fn_app_sel_rangoFecha("MES", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, periodoTop, 0, 0);
            //int mesTop = listMes.Item1.Select(l => l.mes).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = appNeg.fn_app_sel_rangoFecha("FECHA" ,cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, 0, 0, 0);

            //model.Periodos = listPeriodo.Item1.Select(x => new ExtendedSelectListItem
            //{
            //    Value = x.periodo.ToString(),
            //    Text = x.periodo.ToString(),
            //    Selected = false,
            //    HtmlAttributes = new
            //    {
            //        data_alias = x.periodo
            //    }
            //});
            //model.Meses = listMes.Item1.Select(x => new ExtendedSelectListItem
            //{
            //    Value = x.mes.ToString(),
            //    Text = x.nom_mes.ToString(),
            //    Selected = false,
            //    HtmlAttributes = new
            //    {
            //        data_alias = x.mes
            //    }
            //});
            //model.Fechas = listFecha.Item1.Select(x => new ExtendedSelectListItem
            //{
            //    Value = x.cod_rango_fecha.ToString(),
            //    Text = x.nom_rango_fecha.ToString(),
            //    Selected = false,
            //    HtmlAttributes = new
            //    {
            //        data_alias = x.cod_rango_fecha
            //    }
            //});

            string fecha = listFecha.Item1.Select(x => x.nom_rango_fecha).First();
            int cod_rango_fecha = listFecha.Item1.Select(x => x.cod_rango_fecha).First();

            //string fecha = DateTime.Now.AddDays(-1).ToString("yyyy-MM", CultureInfo.InvariantCulture);
            ViewBag.fecha = fecha;

            List<GEN_AprobacionBean> lstAprobacion = appNeg.fn_app_sel_publicacion("SELECT_COMENTARIOS", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_rango_fecha, cod_modulo);
            ViewBag.comentario = "Está seguro de publicar?";
            ViewBag.glosa = "";
            if (lstAprobacion.Count() != 0)
            {
                GEN_AprobacionBean objAprobacion = lstAprobacion.First();
                ViewBag.glosa = objAprobacion.glosa;
                model.est_aprobacion = objAprobacion.est_aprobacion;
            }
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit_Publicacion(GEN_AprobacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                mensajeBean = appNeg.fn_app_pro_publicacion(Usuario.Item1.cod_usuario, model);

                if (mensajeBean.tipo == "SUCCESS")
                {
                    return Json(
                     new Response
                     {
                         Status = HttpStatusCode.OK,
                         Message = mensajeBean.mensaje
                     },
                     JsonRequestBehavior.AllowGet);
                }
                return Json(
                    new Response
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = mensajeBean.mensaje
                    },
                    JsonRequestBehavior.AllowGet);
            }

            return Json(
                     new Response
                     {
                         Status = HttpStatusCode.BadRequest,
                         Message = "No se puede continuar por errores en el modelo",
                         Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                     },
                     JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetPanelPorTipo(string cod_modulo, string cod_unidad_negocio)
        {
            var dataFrecuencia = appNeg.fn_app_sel_tipo("DDL_FRECUENCIA_PUBL", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_modulo) ?? new List<GEN_TipoBean>();         
            var cod_frecuencia = dataFrecuencia.Select(l => l.valor).First();

            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listPeriodo = appNeg.fn_app_sel_rangoFecha("PERIODO", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, 0, 0, 0);
            int periodoTop = listPeriodo.Item1.Select(l => l.periodo).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listMes = appNeg.fn_app_sel_rangoFecha("MES", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, periodoTop, 0, 0);
            int mesTop = listMes.Item1.Select(l => l.mes).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = appNeg.fn_app_sel_rangoFecha("FECHA", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, periodoTop, mesTop, 0);
            var model = new AuxiliarEdit();


            model.Frecuencias = dataFrecuencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });
            model.Periodos = listPeriodo.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.periodo.ToString(),
                Text = x.periodo.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.periodo
                }
            });
            model.Meses = listMes.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.mes.ToString(),
                Text = x.nom_mes.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.mes
                }
            });
            model.Fechas = listFecha.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_rango_fecha.ToString(),
                Text = x.nom_rango_fecha.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_rango_fecha
                }
            });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetPanelPorFrecuencia(string cod_modulo, string cod_unidad_negocio, string cod_frecuencia)
        {
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listPeriodo = appNeg.fn_app_sel_rangoFecha("PERIODO", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, 0, 0, 0);
            int periodoTop = listPeriodo.Item1.Select(l => l.periodo).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listMes = appNeg.fn_app_sel_rangoFecha("MES", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, periodoTop, 0, 0);
            int mesTop = listMes.Item1.Select(l => l.mes).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = appNeg.fn_app_sel_rangoFecha("FECHA", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, periodoTop, mesTop, 0);
            var model = new AuxiliarEdit();

            model.Periodos = listPeriodo.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.periodo.ToString(),
                Text = x.periodo.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.periodo
                }
            });
            model.Meses = listMes.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.mes.ToString(),
                Text = x.nom_mes.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.mes
                }
            });
            model.Fechas = listFecha.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_rango_fecha.ToString(),
                Text = x.nom_rango_fecha.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_rango_fecha
                }
            });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetPanelPorPeriodo(string cod_modulo, string cod_unidad_negocio, string cod_frecuencia, int periodo)
        {
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listMes = appNeg.fn_app_sel_rangoFecha("MES", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, periodo, 0, 0);
            var lastMonth = listMes.Item1.Select(l => l.mes).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = appNeg.fn_app_sel_rangoFecha("FECHA", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, periodo, lastMonth, 0);
            var model = new AuxiliarEdit();

            model.Meses = listMes.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.mes.ToString(),
                Text = x.nom_mes.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.mes
                }
            });
            model.Fechas = listFecha.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_rango_fecha.ToString(),
                Text = x.nom_rango_fecha.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_rango_fecha
                }
            });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetPanelPorMes(string cod_modulo, string cod_unidad_negocio, string cod_frecuencia, int periodo, int mes)
        {
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = appNeg.fn_app_sel_rangoFecha("FECHA", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, periodo, mes, 0);
            var model = new AuxiliarEdit();

            model.Fechas = listFecha.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_rango_fecha.ToString(),
                Text = x.nom_rango_fecha.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_rango_fecha
                }
            });

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetPanelFecha(string cod_modulo, string cod_unidad_negocio, string cod_frecuencia, int periodo, int mes)
        {
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = appNeg.fn_app_sel_rangoFecha("FECHA", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_modulo, periodo, mes, 0);
            var model = new AuxiliarEdit();

            string fecha = listFecha.Item1.Select(x => x.nom_rango_fecha).First();
            model.fec_informe = fecha;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetComentarios(string cod_modulo, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha)
        {
            List<GEN_AprobacionBean> lstAprobacion = appNeg.fn_app_sel_publicacion("SELECT_COMENTARIOS", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, cod_rango_fecha, cod_modulo);
            var model = new AuxiliarEdit();
            if (lstAprobacion.Count() == 0)
            {
                model.glosa = "";
                model.est_aprobacion = 0;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            GEN_AprobacionBean objAprobacion = lstAprobacion.First();
            model.glosa = objAprobacion.glosa;
            model.est_aprobacion = objAprobacion.est_aprobacion;

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
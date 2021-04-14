using Beans;
using Microsoft.Reporting.WebForms;
using Negocio;
using Presentacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace Presentacion.Controllers
{
    public class KCSController : BaseController
    {
        KCS_Negocio kcsNeg = new KCS_Negocio();
        SEG_Negocio segNeg = new SEG_Negocio();
        //static string xlsPath = ConfigurationManager.AppSettings["xlsBat"];

        public ActionResult Ceco()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();

            if (Session["dataNiveles"] != null)
                Session["dataNiveles"] = null;

            //var dataUnidad = kcsNeg.fn_kcs_sel_ddlUnidad(Usuario.Item1.cod_usuario, "", 0, "UNIDAD") ?? new List<GEN_UnidadNegocioBean>();
            var dataUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();
            var UnidadDefault = dataUnidad.First().cod_unidad_negocio;
            var numNivel = dataUnidad.First().niveles;
            model.numNivel = numNivel;

            var UnidadSelected = dataUnidad.Where(a => a.cod_unidad_negocio == UnidadDefault).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.ddlUnidad = dataUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_unidad_negocio.ToString(),
                Selected = UnidadSelected.Any(c => c == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var dataPeriodo = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, UnidadDefault, 0, "PERIODO", "") ?? new List<KCS_CecoBean>();
            if (dataPeriodo.Count == 0 || dataPeriodo == null)
            {
                return View(model);
            }
            var PeriodoDefault = dataPeriodo.First().periodo;
            var PeriodoSelected = dataPeriodo.Where(a => a.periodo == PeriodoDefault).Select(a => a.periodo).AsEnumerable() ?? new HashSet<int>();
            model.Periodos = dataPeriodo.Select(x => new ExtendedSelectListItem
            {
                Value = x.periodo.ToString(),
                Text = x.periodo.ToString(),
                Selected = PeriodoSelected.Any(c => c == x.periodo),
                HtmlAttributes = new
                {
                    data_alias = x.periodo
                }
            });

            var dataNiveles = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, UnidadDefault, PeriodoDefault, "NIVELES", "") ?? new List<KCS_CecoBean>();
            var uqNivel1 = dataNiveles.Select(i => new { cod_grupo_costo = i.cod_grupo_costo.Substring(0, 2), i.nivel1}).Distinct();
            model.ddlNivel1 = uqNivel1.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_grupo_costo,
                Text = x.nivel1,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_grupo_costo
                }
            });

            var listGrupo = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, UnidadDefault, PeriodoDefault, "GRUPO_MANT_WEB", "");
            model.ddlGrupo = listGrupo.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_grupo_mantenimiento.ToString(),
                Text = x.des_grupo_mantenimiento.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_grupo_mantenimiento
                }
            });

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit_Ceco(KCS_CecoBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                //if (Session["cod_unidad_negocio"] != null)
                //{
                //    model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                //}

                mensajeBean = kcsNeg.fn_kcs_pro_ceco(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.periodo, model.accion ,model);

                if (mensajeBean.tipo == "SUCCESS")
                {
                    mensajeBean = kcsNeg.fn_kcs_pro_ceco(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.periodo, "END", model);
                }


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
        public JsonResult JSON_GetNivel(string cod_unidad_negocio, int periodo, string nivel)
        {
            var dataNiveles = new List<KCS_CecoBean>();
            var anio = periodo;

            if (nivel.Equals("000000"))
            {
                nivel = string.Empty;
            }

            var model = new AuxiliarEdit();

            if (periodo == 0)
            {
                var dataPeriodo = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, cod_unidad_negocio, 0, "PERIODO", "") ?? new List<KCS_CecoBean>();
                if (dataPeriodo.Count == 0 || dataPeriodo == null)
                {
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                var PeriodoDefault = dataPeriodo.First().periodo;
                anio = PeriodoDefault;
                var PeriodoSelected = dataPeriodo.Where(a => a.periodo == PeriodoDefault).Select(a => a.periodo).AsEnumerable() ?? new HashSet<int>();
                model.ddlPeriodo = dataPeriodo.Select(x => new ExtendedSelectListItem
                {
                    Value = x.periodo.ToString(),
                    Text = x.periodo.ToString(),
                    Selected = PeriodoSelected.Any(c => c == x.periodo),
                    HtmlAttributes = new
                    {
                        data_alias = x.periodo
                    }
                });

                dataNiveles = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, cod_unidad_negocio, PeriodoDefault, "NIVELES", "") ?? new List<KCS_CecoBean>();
                var uqNivel1 = dataNiveles.Select(i => new { cod_grupo_costo = i.cod_grupo_costo.Substring(0, 2), i.nivel1 }).Distinct();
                model.ddlNivel1 = uqNivel1.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_grupo_costo.ToString(),
                    Text = x.nivel1.ToString(),
                    Selected = false,
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_grupo_costo
                    }
                });

                var listGrupo = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, cod_unidad_negocio, PeriodoDefault, "GRUPO_MANT_WEB", "");
                model.ddlGrupo = listGrupo.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_grupo_mantenimiento.ToString(),
                    Text = x.des_grupo_mantenimiento.ToString(),
                    Selected = false,
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_grupo_mantenimiento
                    }
                });

                Session["dataNiveles"] = dataNiveles;
            }

            if (Session["dataNiveles"] == null || nivel == "")
            {
                dataNiveles = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, cod_unidad_negocio, anio, "NIVELES", "") ?? new List<KCS_CecoBean>();
                Session["dataNiveles"] = dataNiveles;
            }
            else
                dataNiveles = Session["dataNiveles"] as List<KCS_CecoBean>;

            if (dataNiveles == null || dataNiveles.Count() == 0)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            if (periodo != 0 && (nivel == null || nivel == string.Empty))
            {
                var uqNivel1 = dataNiveles.Select(i => new { cod_grupo_costo = i.cod_grupo_costo.Substring(0, 2), i.nivel1 }).Distinct();
                model.ddlNivel1 = uqNivel1.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_grupo_costo.ToString(),
                    Text = x.nivel1.ToString(),
                    Selected = false,
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_grupo_costo
                    }
                });

            }
            else
            {
                if (nivel.Length == 2)
                {
                    var uqNivel2 = dataNiveles.Where(a => a.cod_grupo_costo.StartsWith(nivel)).Select(i => new { cod_grupo_costo = i.cod_grupo_costo.Substring(0, 4), i.nivel2 }).Distinct();
                    model.ddlNivel2 = uqNivel2.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_grupo_costo.ToString(),
                        Text = x.nivel2.ToString(),
                        Selected = false,
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_grupo_costo
                        }
                    });
                }

                if (nivel.Length == 4)
                {
                    var uqNivel3 = dataNiveles.Where(a => a.cod_grupo_costo.StartsWith(nivel)).Select(i => new { cod_grupo_costo = i.cod_grupo_costo.Substring(0, 6), i.nivel3 }).Distinct();
                    model.ddlNivel3 = uqNivel3.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_grupo_costo.ToString(),
                        Text = x.nivel3.ToString(),
                        Selected = false,
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_grupo_costo
                        }
                    });
                }

                if (nivel.Length == 6)
                {
                    var nivel1 = nivel.Substring(0, 2);
                    var nivel2 = nivel.Substring(0, 4);

                    var uqNivel1 = dataNiveles.Select(i => new { cod_grupo_costo = i.cod_grupo_costo.Substring(0, 2), i.nivel1 }).Distinct();
                    var nivel1Selected = uqNivel1.Where(a => a.cod_grupo_costo.StartsWith(nivel1)).Select(a => a.cod_grupo_costo).AsEnumerable() ?? new HashSet<string>();
                    model.ddlNivel1 = uqNivel1.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_grupo_costo.ToString(),
                        Text = x.nivel1.ToString(),
                        Selected = nivel1Selected.Any(c => c == x.cod_grupo_costo),
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_grupo_costo
                        }
                    });
                    var uqNivel2 = dataNiveles.Select(i => new { cod_grupo_costo = i.cod_grupo_costo.Substring(0, 4), i.nivel2 }).Distinct();
                    var nivel2Selected = uqNivel2.Where(a => a.cod_grupo_costo.StartsWith(nivel2)).Select(a => a.cod_grupo_costo).AsEnumerable() ?? new HashSet<string>();
                    model.ddlNivel2 = uqNivel2.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_grupo_costo.ToString(),
                        Text = x.nivel2.ToString(),
                        Selected = nivel2Selected.Any(c => c == x.cod_grupo_costo),
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_grupo_costo
                        }
                    });
                    var uqNivel3 = dataNiveles.Select(i => new { cod_grupo_costo = i.cod_grupo_costo.Substring(0, 6), i.nivel3 }).Distinct();
                    var nivel3Selected = uqNivel3.Where(a => a.cod_grupo_costo.StartsWith(nivel)).Select(a => a.cod_grupo_costo).AsEnumerable() ?? new HashSet<string>();
                    model.ddlNivel3 = uqNivel3.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_grupo_costo.ToString(),
                        Text = x.nivel3.ToString(),
                        Selected = nivel3Selected.Any(c => c == x.cod_grupo_costo),
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_grupo_costo
                        }
                    });
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetGrupoMant(string cod_unidad_negocio, int periodo, string grupo)
        {
            List<KCS_CecoBean> listGrupo = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, cod_unidad_negocio, periodo, "GRUPO_MANT_WEB", "");
            var model = new AuxiliarEdit();

            var dataSelected = listGrupo.Where(a => a.cod_grupo_mantenimiento == grupo).Select(a => a.cod_grupo_mantenimiento).AsEnumerable() ?? new HashSet<string>();
            model.DropDownList = listGrupo.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_grupo_mantenimiento.ToString(),
                Text = x.des_grupo_mantenimiento.ToString(),
                Selected = dataSelected.Any(c => c == x.cod_grupo_mantenimiento),
                HtmlAttributes = new
                {
                    data_alias = x.cod_grupo_mantenimiento
                }
            });

            return Json(model.DropDownList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Clase()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            //if (Session["cod_unidad_negocio"] != null)
            //{
            //    model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            //}

            //var dataUnidad = kcsNeg.fn_kcs_sel_ddlUnidad(Usuario.Item1.cod_usuario, "", 0, "UNIDAD") ?? new List<GEN_UnidadNegocioBean>();
            var dataUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();
            var UnidadDefault = dataUnidad.First().cod_unidad_negocio;

            var UnidadSelected = dataUnidad.Where(a => a.cod_unidad_negocio == UnidadDefault).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.ddlUnidad = dataUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_unidad_negocio.ToString(),
                Selected = UnidadSelected.Any(c => c == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var dataPeriodo = kcsNeg.fn_kcs_sel_ddlClase(Usuario.Item1.cod_usuario, UnidadDefault, 0, "PERIODO") ?? new List<KCS_ClaseBean>();
            if (dataPeriodo.Count == 0 || dataPeriodo == null)
            {
                return View(model);
            }
            var PeriodoDefault = dataPeriodo.First().periodo;
            var PeriodoSelected = dataPeriodo.Where(a => a.periodo == PeriodoDefault).Select(a => a.periodo).AsEnumerable() ?? new HashSet<int>();
            model.Periodos = dataPeriodo.Select(x => new ExtendedSelectListItem
            {
                Value = x.periodo.ToString(),
                Text = x.periodo.ToString(),
                Selected = PeriodoSelected.Any(c => c == x.periodo),
                HtmlAttributes = new
                {
                    data_alias = x.periodo
                }
            });

            var dataTipoGasto = kcsNeg.fn_kcs_sel_ddlClase(Usuario.Item1.cod_usuario, UnidadDefault, PeriodoDefault, "TIPO_GASTO_WEB") ?? new List<KCS_ClaseBean>();
            model.ddlTipo = dataTipoGasto.Select(x => new ExtendedSelectListItem
            {
                Value = x.tip_gasto.ToString(),
                Text = x.nom_tip_gasto.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.tip_gasto
                }
            });

            var dataCategoria = kcsNeg.fn_kcs_sel_ddlClase(Usuario.Item1.cod_usuario, UnidadDefault, PeriodoDefault, "CATEGORIA_WEB") ?? new List<KCS_ClaseBean>();
            model.ddlCategoria = dataCategoria.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_categoria.ToString(),
                Text = x.nom_categoria.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_categoria
                }
            });

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit_Clase(KCS_ClaseBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                //if (Session["cod_unidad_negocio"] != null)
                //{
                //    model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                //}

                mensajeBean = kcsNeg.fn_kcs_pro_clase(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.periodo, model.accion, model);

                if (mensajeBean.tipo == "SUCCESS")
                {
                    mensajeBean = kcsNeg.fn_kcs_pro_clase(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.periodo, "END", model);
                }

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

        public JsonResult JSON_GetClaseData(string cod_unidad_negocio, int periodo)
        {
            var model = new AuxiliarEdit();

            var dataPeriodo = kcsNeg.fn_kcs_sel_ddlClase(Usuario.Item1.cod_usuario, cod_unidad_negocio, 0, "PERIODO") ?? new List<KCS_ClaseBean>();
            if (dataPeriodo.Count == 0 || dataPeriodo == null)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            var PeriodoDefault = dataPeriodo.First().periodo;
            var PeriodoSelected = dataPeriodo.Where(a => a.periodo == PeriodoDefault).Select(a => a.periodo).AsEnumerable() ?? new HashSet<int>();
            model.ddlPeriodo = dataPeriodo.Select(x => new ExtendedSelectListItem
            {
                Value = x.periodo.ToString(),
                Text = x.periodo.ToString(),
                Selected = PeriodoSelected.Any(c => c == x.periodo),
                HtmlAttributes = new
                {
                    data_alias = x.periodo
                }
            });

            var dataTipoGasto = kcsNeg.fn_kcs_sel_ddlClase(Usuario.Item1.cod_usuario, cod_unidad_negocio, PeriodoDefault, "TIPO_GASTO_WEB") ?? new List<KCS_ClaseBean>();
            model.ddlTipo = dataTipoGasto.Select(x => new ExtendedSelectListItem
            {
                Value = x.tip_gasto.ToString(),
                Text = x.nom_tip_gasto.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.periodo
                }
            });

            var dataCategoria = kcsNeg.fn_kcs_sel_ddlClase(Usuario.Item1.cod_usuario, cod_unidad_negocio, PeriodoDefault, "CATEGORIA_WEB") ?? new List<KCS_ClaseBean>();
            model.ddlCategoria = dataCategoria.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_categoria.ToString(),
                Text = x.nom_categoria.ToString(),
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_categoria
                }
            });

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetTipoGasto(string cod_unidad_negocio, int periodo, string tipGasto)
        {
            var model = new AuxiliarEdit();
            var dataTipoGasto = kcsNeg.fn_kcs_sel_ddlClase(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, 0, "TIPO_GASTO") ?? new List<KCS_ClaseBean>();
            var dataSelected = dataTipoGasto.Where(a => a.tip_gasto == tipGasto).Select(a => a.tip_gasto).AsEnumerable() ?? new HashSet<string>();
            model.DropDownList = dataTipoGasto.Select(x => new ExtendedSelectListItem
            {
                Value = x.tip_gasto.ToString(),
                Text = x.nom_tip_gasto.ToString(),
                Selected = dataSelected.Any(c => c == x.tip_gasto),
                HtmlAttributes = new
                {
                    data_alias = x.periodo
                }
            });
            return Json(model.DropDownList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetCategoria(string cod_unidad_negocio, int periodo, string codCategoria)
        {
            var model = new AuxiliarEdit();
            var dataCategoria = kcsNeg.fn_kcs_sel_ddlClase(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, 0, "CATEGORIA") ?? new List<KCS_ClaseBean>();
            var dataSelected2 = dataCategoria.Where(a => a.cod_categoria == codCategoria).Select(a => a.cod_categoria).AsEnumerable() ?? new HashSet<string>();
            model.DropDownList = dataCategoria.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_categoria.ToString(),
                Text = x.nom_categoria.ToString(),
                Selected = dataSelected2.Any(c => c == x.cod_categoria),
                HtmlAttributes = new
                {
                    data_alias = x.cod_categoria
                }
            });
            return Json(model.DropDownList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cierre()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var dataUnidad = kcsNeg.fn_kcs_sel_ddlCierre(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, 0,0, "UNIDAD") ?? new List<GEN_AprobacionBean>();
            var listaUnidadSelected = dataUnidad.Where(a => a.cod_unidad_negocio == cod_unidad_negocio).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.Unidades = dataUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.cod_unidad_negocio,
                Selected = listaUnidadSelected.Any(c => c == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var dataPeriodo = kcsNeg.fn_kcs_sel_ddlCierre(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, 0,0, "PERIODO") ?? new List<GEN_AprobacionBean>();
            var dataSelected = dataPeriodo.Where(a => a.cod_periodo == dataPeriodo.First().cod_periodo).Select(a => a.cod_periodo).AsEnumerable() ?? new HashSet<int>();
            model.Periodos = dataPeriodo.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_periodo.ToString(),
                Text = x.cod_periodo.ToString(),
                Selected = dataSelected.Any(c => c == x.cod_periodo),
                HtmlAttributes = new
                {
                    data_alias = x.periodo
                }
            });

            var dataMes = kcsNeg.fn_kcs_sel_ddlCierre(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, dataPeriodo.First().cod_periodo,0, "MESES") ?? new List<GEN_AprobacionBean>();
            var dataSelected2 = dataMes.Where(a => a.mes == dataMes.First().mes).Select(a => a.mes).AsEnumerable() ?? new HashSet<int>();
            model.Meses = dataMes.Select(x => new ExtendedSelectListItem
            {
                Value = x.mes.ToString(),
                Text = x.mes.ToString(),
                Selected = dataSelected2.Any(c => c == x.mes),
                HtmlAttributes = new
                {
                    data_alias = x.mes
                }
            });

            var estado = kcsNeg.fn_kcs_sel_ddlCierre(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, dataPeriodo.First().cod_periodo, dataMes.First().mes, "ESTADO").First().estado;
            if (estado == "Activo")
                model.estado = "Cierre de Mes";
            else if (estado == "Cerrado")
                model.estado = "Re-apertura de Mes";
            else
                model.estado = "";

            return View(model);
        }
        [HttpPost]
        public ActionResult Cierre(GEN_AprobacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                mensajeBean = kcsNeg.fn_kcs_pro_cierre(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.cod_periodo,model.mes, "START_2");

                if (mensajeBean.tipo != "ERROR")
                {
                    mensajeBean = kcsNeg.fn_kcs_pro_cierre(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.cod_periodo, model.mes, "CIERRE");

                    if (mensajeBean.tipo != "ERROR")
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
        public JsonResult JSON_GetPeriodo(string cod_unidad_negocio, int periodo, int mes)
        {
            var model = new AuxiliarEdit();
            string estado = string.Empty;

            if(periodo == 0 && mes == 0)
            {
                var dataPeriodo = kcsNeg.fn_kcs_sel_ddlCierre(Usuario.Item1.cod_usuario, cod_unidad_negocio, 0,0, "PERIODO") ?? new List<GEN_AprobacionBean>();
                var dataSelected = dataPeriodo.Where(a => a.cod_periodo == dataPeriodo.First().cod_periodo).Select(a => a.cod_periodo).AsEnumerable() ?? new HashSet<int>();
                model.Periodos = dataPeriodo.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_periodo.ToString(),
                    Text = x.cod_periodo.ToString(),
                    Selected = dataSelected.Any(c => c == x.cod_periodo),
                    HtmlAttributes = new
                    {
                        data_alias = x.periodo
                    }
                });

                var dataMes = kcsNeg.fn_kcs_sel_ddlCierre(Usuario.Item1.cod_usuario, cod_unidad_negocio, dataPeriodo.First().cod_periodo, 0, "MESES") ?? new List<GEN_AprobacionBean>();
                var dataSelected2 = dataMes.Where(a => a.mes == dataMes.First().mes).Select(a => a.mes).AsEnumerable() ?? new HashSet<int>();
                model.Meses = dataMes.Select(x => new ExtendedSelectListItem
                {
                    Value = x.mes.ToString(),
                    Text = x.mes.ToString(),
                    Selected = dataSelected2.Any(c => c == x.mes),
                    HtmlAttributes = new
                    {
                        data_alias = x.mes
                    }
                });

                estado = kcsNeg.fn_kcs_sel_ddlCierre(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, dataPeriodo.First().cod_periodo, dataMes.First().mes, "ESTADO").First().estado;
                if (estado == "Activo")
                    model.estado = "Cierre de Mes";
                else if (estado == "Cerrado")
                    model.estado = "Re-apertura de Mes";
                else
                    model.estado = "";

                return Json(model, JsonRequestBehavior.AllowGet);
            }

            if (mes == 0)
            {
                var dataMes = kcsNeg.fn_kcs_sel_ddlCierre(Usuario.Item1.cod_usuario, cod_unidad_negocio, periodo,0, "MESES") ?? new List<GEN_AprobacionBean>();
                var dataSelected2 = dataMes.Where(a => a.mes == dataMes.First().mes).Select(a => a.mes).AsEnumerable() ?? new HashSet<int>();
                model.Meses = dataMes.Select(x => new ExtendedSelectListItem
                {
                    Value = x.mes.ToString(),
                    Text = x.mes.ToString(),
                    Selected = dataSelected2.Any(c => c == x.mes),
                    HtmlAttributes = new
                    {
                        data_alias = x.mes
                    }
                });

                estado = kcsNeg.fn_kcs_sel_ddlCierre(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, periodo, dataMes.First().mes, "ESTADO").First().estado;
                if (estado == "Activo")
                    model.estado = "Cierre de Mes";
                else if (estado == "Cerrado")
                    model.estado = "Re-apertura de Mes";
                else
                    model.estado = "";

                return Json(model, JsonRequestBehavior.AllowGet);
            }

            estado = kcsNeg.fn_kcs_sel_ddlCierre(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, periodo, mes, "ESTADO").First().estado;
            if (estado == "Activo")
                model.estado = "Cierre de Mes";
            else if (estado == "Cerrado")
                model.estado = "Re-apertura de Mes";
            else
                model.estado = "";

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Partidas()
        {
            var model = new AuxiliarEdit();
            //string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();

            if (Session["dataAreaProceso"] != null)
                Session["dataAreaProceso"] = null;

            var dataUnidad = kcsNeg.fn_kcs_sel_ddlUnidad(Usuario.Item1.cod_usuario, "", 0, "UNIDAD").Where(x => x.niveles > 0) ?? new List<GEN_UnidadNegocioBean>();
            var UnidadDefault = dataUnidad.First().cod_unidad_negocio;
            var numNivel = dataUnidad.First().niveles;
            model.numNivel = numNivel;

            var UnidadSelected = dataUnidad.Where(a => a.cod_unidad_negocio == UnidadDefault).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.ddlUnidad = dataUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_unidad_negocio.ToString(),
                Selected = UnidadSelected.Any(c => c == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var dataPeriodo = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, UnidadDefault, 0, "PERIODO", "") ?? new List<KCS_CecoBean>();
            var PeriodoDefault = dataPeriodo.First().periodo;
            var PeriodoSelected = dataPeriodo.Where(a => a.periodo == PeriodoDefault).Select(a => a.periodo).AsEnumerable() ?? new HashSet<int>();
            model.Periodos = dataPeriodo.Select(x => new ExtendedSelectListItem
            {
                Value = x.periodo.ToString(),
                Text = x.periodo.ToString(),
                Selected = PeriodoSelected.Any(c => c == x.periodo),
                HtmlAttributes = new
                {
                    data_alias = x.periodo
                }
            });

            var dataCeco = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, UnidadDefault, PeriodoDefault, "CECO_GES", "") ?? new List<KCS_CecoBean>();
            model.ddlCeco = dataCeco.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_centro_costo.ToString(),
                Text = x.nom_centro_costo.ToString(),
                Selected = true,
                HtmlAttributes = new
                {
                    data_alias = x.cod_centro_costo
                }
            });

            var dataAreaProceso = kcsNeg.fn_kcs_sel_ddlArea(Usuario.Item1.cod_usuario, UnidadDefault, PeriodoDefault, "AREA") ?? new List<KCS_AreaBean>();
            IEnumerable<KCS_AreaBean> uqArea = null;

            if (numNivel > 0)
            {
                uqArea = dataAreaProceso.Where(i => i.cod_area.Length == 2).Distinct();
                model.ddlArea = uqArea.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_area,
                    Text = x.nom_area,
                    Selected = false,
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_area
                    }
                });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit_Partida(KCS_PartidaBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                mensajeBean = kcsNeg.up_kcs_cud_partida(Usuario.Item1.cod_usuario, model);

                if (mensajeBean.tipo == "SUCCESS")
                {
                    if (Session["dataAreaProceso"] != null)
                        Session["dataAreaProceso"] = null;

                    model.accion = "END";
                    mensajeBean = kcsNeg.up_kcs_cud_partida(Usuario.Item1.cod_usuario, model);

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

        public JsonResult JSON_GetAreaProceso(string cod_unidad_negocio, int periodo, string cod_area)
        {
            var model = new AuxiliarEdit();
            var dataAreaProceso = new List<KCS_AreaBean>();
            var anio = periodo;

            var UnidadNegocio = kcsNeg.fn_kcs_sel_ddlUnidad(Usuario.Item1.cod_usuario, "", 0, "UNIDAD").Where(x => x.cod_unidad_negocio == cod_unidad_negocio).First() ?? new GEN_UnidadNegocioBean();
            var numNivel = UnidadNegocio.niveles;
            model.numNivel = numNivel;

            if (periodo == 0)
            {
                var dataPeriodo = kcsNeg.fn_kcs_sel_ddlCeco(Usuario.Item1.cod_usuario, cod_unidad_negocio, 0, "PERIODO", "") ?? new List<KCS_CecoBean>();
                var PeriodoDefault = dataPeriodo.First().periodo;
                var PeriodoSelected = dataPeriodo.Where(a => a.periodo == PeriodoDefault).Select(a => a.periodo).AsEnumerable() ?? new HashSet<int>();
                model.ddlPeriodo = dataPeriodo.Select(x => new ExtendedSelectListItem
                {
                    Value = x.periodo.ToString(),
                    Text = x.periodo.ToString(),
                    Selected = PeriodoSelected.Any(c => c == x.periodo),
                    HtmlAttributes = new
                    {
                        data_alias = x.periodo
                    }
                });
                anio = PeriodoDefault;

                dataAreaProceso = kcsNeg.fn_kcs_sel_ddlArea(Usuario.Item1.cod_usuario, cod_unidad_negocio, anio, "AREA") ?? new List<KCS_AreaBean>();
                Session["dataAreaProceso"] = dataAreaProceso;
            }

            if (Session["dataAreaProceso"] == null || cod_area == "")
            {
                dataAreaProceso = kcsNeg.fn_kcs_sel_ddlArea(Usuario.Item1.cod_usuario, cod_unidad_negocio, anio, "AREA") ?? new List<KCS_AreaBean>();
                Session["dataAreaProceso"] = dataAreaProceso;
            }
            else
                dataAreaProceso = Session["dataAreaProceso"] as List<KCS_AreaBean>;

            if (dataAreaProceso == null || dataAreaProceso.Count() == 0)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            IEnumerable<KCS_AreaBean> uqArea = null;
            IEnumerable<KCS_AreaBean> uqProceso01 = null;
            IEnumerable<KCS_AreaBean> uqProceso02 = null;
            IEnumerable<KCS_AreaBean> uqProceso03 = null;
            var AreaDefault = "";
            var Proceso01Default= "";
            var Proceso02Default= "";
            var Proceso03Default = "";

            if (numNivel > 0)
            {
                uqArea = dataAreaProceso.Where(i => i.cod_area.Length == 2).Distinct();
            }
            if (numNivel > 1)
            {
                uqProceso01 = dataAreaProceso.Where(i => i.cod_area.Length == 5).Distinct();
            }
            if (numNivel > 2)
            {
                uqProceso02 = dataAreaProceso.Where(i => i.cod_area.Length == 8).Distinct();
            }
            if (numNivel > 3)
            {
                uqProceso03 = dataAreaProceso.Where(i => i.cod_area.Length == 11).Distinct();
            }

            
            if (cod_area == null || cod_area == string.Empty || cod_area == "")
            {
                if (numNivel > 0)
                {
                    AreaDefault = uqArea.First().cod_area;
                    var AreaSelected = uqArea.Where(a => a.cod_area == AreaDefault).Select(a => a.cod_area).AsEnumerable() ?? new HashSet<string>();
                    model.ddlArea = uqArea.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_area,
                        Text = x.nom_area,
                        Selected = AreaSelected.Any(c => c == x.cod_area),
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_area
                        }
                    });
                }
            }
            else
            {
                if (cod_area.Length == 2)
                {
                    if (numNivel > 1)
                    {
                        var filterProceso01 = uqProceso01.Where(a => a.cod_area.StartsWith(cod_area));
                        Proceso01Default = filterProceso01.First().cod_area;
                        var Proceso01Selected = filterProceso01.Where(a => a.cod_area.Substring(0, 5) == Proceso01Default).Select(a => a.cod_area).AsEnumerable() ?? new HashSet<string>();
                        model.ddlProceso01 = filterProceso01.Select(x => new ExtendedSelectListItem
                        {
                            Value = x.cod_area,
                            Text = x.proceso01,
                            Selected = Proceso01Selected.Any(c => c == x.cod_area),
                            HtmlAttributes = new
                            {
                                data_alias = x.cod_area
                            }
                        });
                    }
                }

                if (cod_area.Length == 5)
                {
                    if (numNivel > 2)
                    {
                        var filterProceso02 = uqProceso02.Where(a => a.cod_area.StartsWith(cod_area));
                        Proceso02Default = filterProceso02.First().cod_area;
                        var Proceso02Selected = filterProceso02.Where(a => a.cod_area.Substring(0, 8) == Proceso02Default).Select(a => a.cod_area).AsEnumerable() ?? new HashSet<string>();
                        model.ddlProceso02 = filterProceso02.Select(x => new ExtendedSelectListItem
                        {
                            Value = x.cod_area,
                            Text = x.proceso02,
                            Selected = Proceso02Selected.Any(c => c == x.cod_area),
                            HtmlAttributes = new
                            {
                                data_alias = x.cod_area
                            }
                        });
                    }
                }

                if (cod_area.Length == 8)
                {
                    if (numNivel > 3)
                    {
                        var filterProceso03 = uqProceso03.Where(a => a.cod_area.StartsWith(cod_area));
                        Proceso03Default = filterProceso03.First().cod_area;
                        var Proceso03Selected = filterProceso03.Where(a => a.cod_area.Substring(0, 11) == Proceso03Default).Select(a => a.cod_area).AsEnumerable() ?? new HashSet<string>();
                        model.ddlProceso03 = filterProceso03.Select(x => new ExtendedSelectListItem
                        {
                            Value = x.cod_area,
                            Text = x.proceso03,
                            Selected = Proceso03Selected.Any(c => c == x.cod_area),
                            HtmlAttributes = new
                            {
                                data_alias = x.cod_area
                            }
                        });
                    } 
                }
                if (cod_area.Length == 11)
                {
                    var Area = cod_area.Substring(0,2);
                    var Proceso01 = cod_area.Substring(0, 5);
                    var Proceso02 = cod_area.Substring(0, 8);
                    var Proceso03 = cod_area.Substring(0, 11);

                    var AreaSelected = uqArea.Where(a => a.cod_area.Substring(0,2) == Area).Select(a => a.cod_area).AsEnumerable() ?? new HashSet<string>();
                    model.ddlArea = uqArea.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_area,
                        Text = x.nom_area,
                        Selected = AreaSelected.Any(c => c == x.cod_area),
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_area
                        }
                    });

                    var filterProceso01 = uqProceso01.Where(a => a.cod_area.StartsWith(Area));
                    var Proceso01Selected = filterProceso01.Where(a => a.cod_area.Substring(0, 5) == Proceso01).Select(a => a.cod_area).AsEnumerable() ?? new HashSet<string>();
                    model.ddlProceso01 = filterProceso01.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_area,
                        Text = x.proceso01,
                        Selected = Proceso01Selected.Any(c => c == x.cod_area),
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_area
                        }
                    });

                    var filterProceso02 = uqProceso02.Where(a => a.cod_area.StartsWith(Proceso01));
                    var Proceso02Selected = filterProceso02.Where(a => a.cod_area.Substring(0, 8) == Proceso02).Select(a => a.cod_area).AsEnumerable() ?? new HashSet<string>();
                    model.ddlProceso02 = filterProceso02.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_area,
                        Text = x.proceso02,
                        Selected = Proceso02Selected.Any(c => c == x.cod_area),
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_area
                        }
                    });

                    var filterProceso03 = uqProceso03.Where(a => a.cod_area.StartsWith(Proceso02));
                    var Proceso03Selected = filterProceso03.Where(a => a.cod_area.Substring(0, 11) == Proceso03).Select(a => a.cod_area).AsEnumerable() ?? new HashSet<string>();
                    model.ddlProceso03 = filterProceso03.Select(x => new ExtendedSelectListItem
                    {
                        Value = x.cod_area,
                        Text = x.proceso03,
                        Selected = Proceso03Selected.Any(c => c == x.cod_area),
                        HtmlAttributes = new
                        {
                            data_alias = x.cod_area
                        }
                    });
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetCecoMaterial(string cod_unidad_negocio, int periodo, long cod_centro_costo)
        {
            var data = kcsNeg.fn_kcs_sel_material(Usuario.Item1.cod_usuario, cod_unidad_negocio, periodo, "CECO_MATERIAL", cod_centro_costo, "") ?? new List<KCS_MaterialBean>();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult CargaExcelPptoValor()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult UploadFileAjax(string accion, string cod_unidad_negocio, int periodo, int cod_presupuesto)
        //{
        //    try
        //    {
        //        for (int i = 0; i < Request.Files.Count; i++)
        //        {
        //            var fileContent = Request.Files[i];
        //            if (fileContent != null && fileContent.ContentLength > 0)
        //            {
        //                string fileCliente = Path.GetFileName(fileContent.FileName);
        //                string fileExtension = Path.GetExtension(fileContent.FileName);
        //                string fileDestino = string.Empty;
        //                string fileDestinoDBS = string.Empty;
        //                int pid = 0;

        //                if (fileExtension != ".xlsx" && fileExtension != ".xls")
        //                {
        //                    return Json(
        //                            new Response
        //                            {
        //                                Status = HttpStatusCode.BadRequest,
        //                                Message = "El archivo debe ser tipo Excel (.xlsx,.xls)",
        //                            },
        //                            JsonRequestBehavior.AllowGet);
        //                }

        //                // get a stream
        //                var stream = fileContent.InputStream;

        //                var nomenclatura = "KCS_PresupuestoValor_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xlsx";
        //                if (HttpContext.IsDebuggingEnabled)
        //                {
        //                    fileDestino = Path.Combine(@"M:\BAT", nomenclatura);
        //                    fileDestinoDBS = fileDestino.Replace(@"M:\BAT", xlsPath);
        //                }
        //                else
        //                {
        //                    fileDestino = Path.Combine(Server.MapPath("~/XlsD"), nomenclatura);
        //                    fileDestinoDBS = fileDestino.Replace(Server.MapPath("~/XlsD"), xlsPath);
        //                }
        //                var mensajeError = string.Empty;
        //                if (accion == "CARGA")
        //                {
        //                    using (var fileStream = System.IO.File.Create(fileDestinoDBS))
        //                    {
        //                        stream.CopyTo(fileStream);
        //                    }
        //                }

        //                // Formateando excel a 2 decimales

        //                //Grabacion de temporal en sql
        //                if (Session["cod_unidad_negocio"] != null)
        //                {
        //                    cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
        //                }

        //                try
        //                {
        //                    var excelApp = new Microsoft.Office.Interop.Excel.Application(); //CreateObject("Excel.Application")
        //                    excelApp.Application.Visible = false;
        //                    excelApp.DisplayAlerts = false;
        //                    var excelWorkbook = excelApp.Workbooks.Open(Filename: fileDestinoDBS, ReadOnly: false);
        //                    GetWindowThreadProcessId(excelApp.Hwnd, out pid);

        //                    try
        //                    {
        //                        excelWorkbook.Sheets["Data"].Range("J2:J35000").NumberFormat = "0.0000000000000000";
        //                    }
        //                    catch (Exception exc)
        //                    {
        //                        mensajeError = "ERROR: En la ejecución de Excel " + exc.Message;
        //                    }

        //                    //Close Excel
        //                    excelWorkbook.Close(SaveChanges: true);
        //                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
        //                    excelWorkbook = null;
        //                    excelApp.Quit();
        //                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        //                    excelApp = null;
        //                    var archivoDestino = fileDestinoDBS.Replace(@"\", "/");

        //                    try
        //                    {
        //                        Process.GetProcessById(pid).Kill();
        //                    }
        //                    catch (Exception)
        //                    {
        //                    }

        //                }
        //                catch (Exception ex)
        //                {
        //                    System.Threading.Thread.Sleep(1000);  //Espera un segundo para intentarlo de nuevo
        //                    mensajeError += " Servicio de Excel en estos momentos se encuentra sin recursos.\r Por favor intentelo mas tarde.";
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(
        //            new Response
        //            {
        //                Status = HttpStatusCode.BadRequest,
        //                Message = "El archivo no se cargó"
        //            },
        //            JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(
        //             new Response
        //             {
        //                 Status = HttpStatusCode.BadRequest,
        //                 Message = "No se puede continuar por errores en el modelo",
        //                 Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
        //             },
        //             JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Indicador()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();
            var model = new AuxiliarEdit();

            var listaUnidadSelected = listUnidad.Where(a => a.cod_unidad_negocio == cod_unidad_negocio).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.Unidades = listUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_unidad_negocio,
                Selected = listaUnidadSelected.Any(c => c == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            return View(model);
        }
        public JsonResult JSON_GetIndicador(string cod_unidad_negocio, long cod_indicador)
        {
            GEN_IndicadorBean bean = kcsNeg.fn_kcs_get_indicador(Usuario.Item1.cod_usuario, cod_unidad_negocio, "GET_INDICADOR", cod_indicador);
            return Json(bean, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CierreCosto()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();
            var unidadTop = listUnidad.Select(l => l.cod_unidad_negocio).First();
            var model = new AuxiliarEdit();

            var listaUnidadSelected = listUnidad.Where(a => a.cod_unidad_negocio == unidadTop).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.Unidades = listUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_unidad_negocio,
                Selected = listaUnidadSelected.Any(c => c == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var listFrecuencia = kcsNeg.fn_kcs_sel_frecuencia(Usuario.Item1.cod_usuario, unidadTop, "Frecuencias") ?? new List<GEN_FrecuenciaBean>();
            var frecuenciaTop = listFrecuencia.Select(l => l.cod_frecuencia).First();
            var listFrecuenciaSelected = listFrecuencia.Where(a => a.cod_frecuencia == frecuenciaTop).Select(a => a.cod_frecuencia).AsEnumerable() ?? new HashSet<string>();
            model.ddlFrecuencia = listFrecuencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_frecuencia.ToString(),
                Text = x.frecuencia,
                Selected = listFrecuenciaSelected.Any(c => c == x.cod_frecuencia),
                HtmlAttributes = new
                {
                    data_alias = x.cod_frecuencia
                }
            });

            var aprobacion = kcsNeg.fn_kcs_get_aprobacion(Usuario.Item1.cod_usuario, unidadTop, frecuenciaTop, 0, "PANEL_CERRAR2") ?? new GEN_AprobacionBean();
            model.estado = aprobacion.estado;
            model.cod_mes = aprobacion.cod_mes;
            model.cod_mes_txt = aprobacion.cod_mes_txt;
            model.cod_rango_fecha = aprobacion.cod_rango_fecha;
            model.cod_rango_fecha_txt = aprobacion.cod_rango_fecha_txt;
            model.fec_desde_txt = String.Format("{0:dd/MM/yyyy}", aprobacion.fec_desde);
            model.fec_hasta_txt = String.Format("{0:dd/MM/yyyy}", aprobacion.fec_hasta);

            return View(model);
        }
        [HttpPost]
        public ActionResult CierreCosto(GEN_AprobacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                mensajeBean = kcsNeg.fn_kcs_update_aprobacion(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.cod_frecuencia, model.cod_mes.Value ,model.cod_rango_fecha.Value, "CIERRE");

                if (mensajeBean.tipo != "ERROR")
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

        public JsonResult JSON_GetFrecuenciasPorUnidad(string cod_unidad_negocio)
        {
            var model = new AuxiliarEdit();
            var listFrecuencia = kcsNeg.fn_kcs_sel_frecuencia(Usuario.Item1.cod_usuario, cod_unidad_negocio, "Frecuencias") ?? new List<GEN_FrecuenciaBean>();
            var frecuenciaTop = listFrecuencia.Select(l => l.cod_frecuencia).First();
            var listFrecuenciaSelected = listFrecuencia.Where(a => a.cod_frecuencia == frecuenciaTop).Select(a => a.cod_frecuencia).AsEnumerable() ?? new HashSet<string>();
            model.ddlFrecuencia = listFrecuencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_frecuencia.ToString(),
                Text = x.frecuencia,
                Selected = listFrecuenciaSelected.Any(c => c == x.cod_frecuencia),
                HtmlAttributes = new
                {
                    data_alias = x.cod_frecuencia
                }
            });

            var aprobacion = kcsNeg.fn_kcs_get_aprobacion(Usuario.Item1.cod_usuario, cod_unidad_negocio, frecuenciaTop, 0, "PANEL_CERRAR2") ?? new GEN_AprobacionBean();
            model.estado = aprobacion.estado;
            model.cod_mes = aprobacion.cod_mes;
            model.cod_mes_txt = aprobacion.cod_mes_txt;
            model.cod_rango_fecha = aprobacion.cod_rango_fecha;
            model.cod_rango_fecha_txt = aprobacion.cod_rango_fecha_txt;
            model.fec_desde_txt = String.Format("{0:dd/MM/yyyy}", aprobacion.fec_desde);
            model.fec_hasta_txt = String.Format("{0:dd/MM/yyyy}", aprobacion.fec_hasta);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReaperturarCosto()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();
            var unidadTop = listUnidad.Select(l => l.cod_unidad_negocio).First();
            var model = new AuxiliarEdit();

            var listaUnidadSelected = listUnidad.Where(a => a.cod_unidad_negocio == unidadTop).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.Unidades = listUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_unidad_negocio,
                Selected = listaUnidadSelected.Any(c => c == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var listFrecuencia = kcsNeg.fn_kcs_sel_frecuencia(Usuario.Item1.cod_usuario, unidadTop, "Frecuencias") ?? new List<GEN_FrecuenciaBean>();
            var frecuenciaTop = listFrecuencia.Select(l => l.cod_frecuencia).First();
            var listFrecuenciaSelected = listFrecuencia.Where(a => a.cod_frecuencia == frecuenciaTop).Select(a => a.cod_frecuencia).AsEnumerable() ?? new HashSet<string>();
            model.ddlFrecuencia = listFrecuencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_frecuencia.ToString(),
                Text = x.frecuencia,
                Selected = listFrecuenciaSelected.Any(c => c == x.cod_frecuencia),
                HtmlAttributes = new
                {
                    data_alias = x.cod_frecuencia
                }
            });

            var aprobacion = kcsNeg.fn_kcs_get_aprobacion(Usuario.Item1.cod_usuario, unidadTop, frecuenciaTop, 0, "PANEL_REAPERTURAR2") ?? new GEN_AprobacionBean();
            model.estado = aprobacion.estado;
            model.cod_mes = aprobacion.cod_mes;
            model.cod_mes_txt = aprobacion.cod_mes_txt;
            model.cod_rango_fecha = aprobacion.cod_rango_fecha;
            model.cod_rango_fecha_txt = aprobacion.cod_rango_fecha_txt;
            model.fec_desde_txt = String.Format("{0:dd/MM/yyyy}", aprobacion.fec_desde);
            model.fec_hasta_txt = String.Format("{0:dd/MM/yyyy}", aprobacion.fec_hasta);

            return View(model);
        }

        [HttpPost]
        public ActionResult ReaperturarCosto(GEN_AprobacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                mensajeBean = kcsNeg.fn_kcs_update_aprobacion(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.cod_frecuencia, model.cod_mes.Value, model.cod_rango_fecha.Value, "ABRIR");

                if (mensajeBean.tipo != "ERROR")
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

        public JsonResult JSON_GetAprobacion(string cod_unidad_negocio, string cod_frecuencia, string accion)
        {
            var model = new AuxiliarEdit();
            var aprobacion = kcsNeg.fn_kcs_get_aprobacion(Usuario.Item1.cod_usuario, cod_unidad_negocio, cod_frecuencia, 0, accion) ?? new GEN_AprobacionBean();
            model.estado = aprobacion.estado;
            model.cod_mes = aprobacion.cod_mes;
            model.cod_mes_txt = aprobacion.cod_mes_txt;
            model.cod_rango_fecha = aprobacion.cod_rango_fecha;
            model.cod_rango_fecha_txt = aprobacion.cod_rango_fecha_txt;
            model.fec_desde_txt = String.Format("{0:dd/MM/yyyy}", aprobacion.fec_desde);
            model.fec_hasta_txt = String.Format("{0:dd/MM/yyyy}", aprobacion.fec_hasta);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Proceso01()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();
            var model = new AuxiliarEdit();

            var listaUnidadSelected = listUnidad.Where(a => a.cod_unidad_negocio == cod_unidad_negocio).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.Unidades = listUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_unidad_negocio,
                Selected = listaUnidadSelected.Any(c => c == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            return View(model);
        }

        public ActionResult InformeCorporativo()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();
            var model = new AuxiliarEdit();

            var listaUnidadSelected = listUnidad.Where(a => a.cod_unidad_negocio == cod_unidad_negocio).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.Unidades = listUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_unidad_negocio,
                Selected = listaUnidadSelected.Any(c => c == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            var listFrecuencia = new List<GEN_FrecuenciaBean>(); //kopNeg.fn_kop_sel_frecuencia(Usuario.Item1.cod_usuario, cod_unidad_negocio, "", 0, "Frecuencias_Formato") ?? new List<GEN_FrecuenciaBean>();
            //var objFre1 = new GEN_FrecuenciaBean();
            //objFre1.cod_frecuencia = "S";
            //objFre1.frecuencia = "Semanal";
            //listFrecuencia.Add(objFre1);
            var objFre2 = new GEN_FrecuenciaBean();
            objFre2.cod_frecuencia = "M";
            objFre2.frecuencia = "Mensual";
            listFrecuencia.Add(objFre2);

            var frecTop = listFrecuencia.First().cod_frecuencia;
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listPeriodo = kcsNeg.fn_kcs_sel_rangoFecha(cod_unidad_negocio, "PERIODO", frecTop, 0, 0, Usuario.Item1.cod_usuario, 0, 0);
            int periodoTop = listPeriodo.Item1.Select(l => l.periodo).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = kcsNeg.fn_kcs_sel_rangoFecha(cod_unidad_negocio, "FECHA", frecTop, periodoTop, 0, Usuario.Item1.cod_usuario, 0, 0);


            model.Frecuencias = listFrecuencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_frecuencia.ToString(),
                Text = x.frecuencia,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_frecuencia
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

            return View(model);
        }

        /*public JsonResult JSON_GetPanelPorUnidad(string cod_unidad_negocio)
        {
            var listFormato = kopNeg.fn_kop_sel_cargaAuxiliar("FORMATO", Usuario.Item1.cod_usuario, cod_unidad_negocio) ?? new List<KOP_CargaAuxiliarBean>();
            var listFrecuencia = kopNeg.fn_kop_sel_frecuencia(Usuario.Item1.cod_usuario, cod_unidad_negocio, "", 0, "Frecuencias_Formato") ?? new List<GEN_FrecuenciaBean>();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listPeriodo = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "PERIODO", "D", 0, 0, Usuario.Item1.cod_usuario, 0, 0);
            int periodoTop = listPeriodo.Item1.Select(l => l.periodo).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listMes = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "MES", "D", periodoTop, 0, Usuario.Item1.cod_usuario, 0, 0);
            int mesTop = listMes.Item1.Select(l => l.mes).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "FECHA", "D", periodoTop, mesTop, Usuario.Item1.cod_usuario, 0, 0);
            var model = new AuxiliarEdit();

            model.Formatos = listFormato.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_carga_auxiliar.ToString(),
                Text = x.nom_carga_auxiliar,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_carga_auxiliar
                }
            });
            model.Frecuencias = listFrecuencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_frecuencia.ToString(),
                Text = x.frecuencia,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_frecuencia
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
        */

        public JsonResult JSON_GetPanelPorFrecuencia(string cod_unidad_negocio, string cod_frecuencia)
        {
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listPeriodo = kcsNeg.fn_kcs_sel_rangoFecha(cod_unidad_negocio, "PERIODO", cod_frecuencia, 0, 0, Usuario.Item1.cod_usuario, 0, 0);
            int periodoTop = listPeriodo.Item1.Select(l => l.periodo).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = kcsNeg.fn_kcs_sel_rangoFecha(cod_unidad_negocio, "FECHA", cod_frecuencia, periodoTop, 0, Usuario.Item1.cod_usuario, 0, 0);
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

        public JsonResult JSON_GetPanelPorPeriodo(string cod_unidad_negocio, string cod_frecuencia, int periodo)
        {
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = kcsNeg.fn_kcs_sel_rangoFecha(cod_unidad_negocio, "FECHA", cod_frecuencia, periodo, 0, Usuario.Item1.cod_usuario, 0, 0);
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

        public ActionResult _TableroControl(string cod_unidad_negocio, string cod_frecuencia, string cod_rango_fecha, string cod_carga_auxiliar)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true,
                ShowParameterPrompts = false,
                ShowPromptAreaButton = false,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                AsyncRendering = false,
                BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
            };

            List<ReportParameter> reportParameters = new List<ReportParameter>();

            var cod_informe = 1;
            var cod_periodo = cod_rango_fecha.Substring(cod_rango_fecha.Length - 4);

            reportParameters.Add(new ReportParameter("cod_usuario", Usuario.Item1.cod_usuario, false));
            reportParameters.Add(new ReportParameter("cod_informe", cod_informe.ToString(), false));
            reportParameters.Add(new ReportParameter("cod_periodo", cod_periodo.ToString(), false));
            //reportParameters.Add(new ReportParameter("cod_frecuencia", cod_frecuencia, false));
            //reportParameters.Add(new ReportParameter("cod_unidad_negocio", cod_unidad_negocio, false));
            //reportParameters.Add(new ReportParameter("cod_rango_fecha", cod_rango_fecha, false));
            //reportParameters.Add(new ReportParameter("cod_carga_auxiliar", cod_carga_auxiliar, false));

            var nomArchivo = string.Empty;

            switch (cod_frecuencia)
            {
                case "D":
                    reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["Report_Path"] + "/rp_kcs_informeCosto";
                    break;
                case "S":
                    reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["Report_Path"] + "/rp_kcs_informeCosto";
                    break;
                case "M":
                    reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["Report_Path"] + "/rp_kcs_informeCosto";
                    break;
            }

            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["Report_Server"]);
            reportViewer.ServerReport.SetParameters(reportParameters);

            reportViewer.ServerReport.Refresh();
            ViewBag.ReportViewer = reportViewer;

            return PartialView("_TableroControl");
        }


    }
}
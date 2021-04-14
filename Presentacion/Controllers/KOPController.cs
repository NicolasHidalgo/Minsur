using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.Mvc.Html;
using System.Configuration;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using Presentacion.ViewModels;
using Beans;
using Negocio;
using System.Web.UI;
using System.Web.Routing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Globalization;

namespace Presentacion.Controllers
{
    public class KOPController : BaseController
    {
        KOP_Negocio kopNeg = new KOP_Negocio();
        SEG_Negocio segNeg = new SEG_Negocio();

        public ActionResult CierreTablero()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = kopNeg.fn_kop_sel_unidad(Usuario.Item1.cod_usuario,"","",0, "Unidad") ?? new List<GEN_UnidadNegocioBean>();
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
        [HttpPost]
        public ActionResult CierreTablero(GEN_AprobacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                var cod_carga_auxiliar = 0;
                if (model.cod_carga_auxiliar != null)
                    cod_carga_auxiliar = model.cod_carga_auxiliar.Value;

                mensajeBean = kopNeg.fn_kop_update_aprobacion(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.cod_frecuencia, model.cod_rango_fecha.Value, "CERRAR", cod_carga_auxiliar);

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

        [HttpPost]
        public ActionResult CierreTableroCorreo(GEN_AprobacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                var cod_carga_auxiliar = 0;
                if (model.cod_carga_auxiliar != null)
                    cod_carga_auxiliar = model.cod_carga_auxiliar.Value;

                mensajeBean = kopNeg.fn_kop_update_aprobacion(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.cod_frecuencia, model.cod_rango_fecha.Value, model.accion, cod_carga_auxiliar);

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


        [HttpPost]
        public ActionResult ReenviarCorreo(GEN_AprobacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                var cod_carga_auxiliar = 0;
                if (model.cod_carga_auxiliar != null)
                    cod_carga_auxiliar = model.cod_carga_auxiliar.Value;

                model.cod_usuario = Usuario.Item1.cod_usuario;
                mensajeBean = kopNeg.fn_kop_pro_envioCorreo(model);

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

        public ActionResult ConsultaCierre()
        {
            var cod_unidad_negocio = HttpContext.Request["cod_unidad_negocio"];
            var cod_frecuencia = HttpContext.Request["cod_frecuencia"];

            var listUnidad = kopNeg.fn_kop_sel_unidad(Usuario.Item1.cod_usuario, "", "", 0, "Unidad") ?? new List<GEN_UnidadNegocioBean>();
            
            var model = new AuxiliarEdit();
            if (cod_unidad_negocio != null && cod_unidad_negocio != string.Empty)
            {
                var lista = listUnidad.Where(a => a.cod_unidad_negocio == cod_unidad_negocio).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
                model.Unidades = listUnidad.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_unidad_negocio.ToString(),
                    Text = x.nom_unidad_negocio,
                    Selected = lista.Any(m => m == x.cod_unidad_negocio),
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_unidad_negocio
                    }
                });
            }
            else
            {
                model.Unidades = listUnidad.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_unidad_negocio.ToString(),
                    Text = x.nom_unidad_negocio,
                    Selected = false,
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_unidad_negocio
                    }
                });
            }

            if (cod_frecuencia != null && cod_frecuencia != string.Empty)
            {
                var listFrecuencia = kopNeg.fn_kop_sel_frecuencia(Usuario.Item1.cod_usuario, cod_unidad_negocio, "", 0, "Frecuencias") ?? new List<GEN_FrecuenciaBean>();
                var lista = listFrecuencia.Where(a => a.cod_frecuencia == cod_frecuencia).Select(a => a.cod_frecuencia).AsEnumerable() ?? new HashSet<string>();
                model.Frecuencias = listFrecuencia.Select(x => new ExtendedSelectListItem
                {
                    Value = x.cod_frecuencia.ToString(),
                    Text = x.frecuencia,
                    Selected = lista.Any(m => m == x.cod_frecuencia),
                    HtmlAttributes = new
                    {
                        data_alias = x.cod_frecuencia
                    }
                });
            }

            return View(model);
        }

        public ActionResult ReaperturarTablero()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = kopNeg.fn_kop_sel_unidad(Usuario.Item1.cod_usuario, "", "", 0, "Unidad") ?? new List<GEN_UnidadNegocioBean>();
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

        [HttpPost]
        public ActionResult ReaperturarTablero(GEN_AprobacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                var cod_carga_auxiliar = 0;
                if (model.cod_carga_auxiliar != null)
                    cod_carga_auxiliar = model.cod_carga_auxiliar.Value;

                mensajeBean = kopNeg.fn_kop_update_aprobacion(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.cod_frecuencia, model.cod_rango_fecha.Value, "ABRIR", cod_carga_auxiliar);

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
            var listFrecuencia = kopNeg.fn_kop_sel_frecuencia(Usuario.Item1.cod_usuario, cod_unidad_negocio, "", 0, "Frecuencias") ?? new List<GEN_FrecuenciaBean>();
            var model = new AuxiliarEdit();
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
            return Json(model.Frecuencias, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetAprobacion(string cod_unidad_negocio, string cod_frecuencia, string accion)
        {
            var aprobacion = kopNeg.fn_kop_get_aprobacion(Usuario.Item1.cod_usuario, cod_unidad_negocio, cod_frecuencia, 0, accion) ?? new GEN_AprobacionBean();
            return Json(aprobacion, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TableroControl()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = kopNeg.fn_kop_sel_unidad(Usuario.Item1.cod_usuario, "", "", 0, "Unidad") ?? new List<GEN_UnidadNegocioBean>();
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

        public ActionResult TableroControlCorreo(string cod_unidad_negocio, string cod_frecuencia, int? cod_rango_fecha, int? cod_carga_auxiliar)
        {
            var model = new AuxiliarEdit();

            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> defecto = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "DEFAULT", cod_frecuencia, 0, 0, Usuario.Item1.cod_usuario,cod_carga_auxiliar,cod_rango_fecha);

            if (defecto.Item2.tipo == "ERROR")
            {
                model.mensajeError = defecto.Item2.mensaje;
                model.tipoError = defecto.Item2.tipo;

                HttpContext.Response.Clear();
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "Error");
                routeData.Values.Add("exception", defecto.Item2.mensaje);
                routeData.Values.Add("statusCode", 999);

                Response.TrySkipIisCustomErrors = true;
                IController controller = new ErrorController();
                controller.Execute(new RequestContext(HttpContext, routeData));
                Response.End();
                return View(model);
            }

            var defectoItem1 = defecto.Item1.First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listPeriodo = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "PERIODO", cod_frecuencia, 0, 0, Usuario.Item1.cod_usuario, 0, 0);
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listMes = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "MES", cod_frecuencia, defectoItem1.periodo, 0, Usuario.Item1.cod_usuario, 0, 0);
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "FECHA", cod_frecuencia, defectoItem1.periodo, defectoItem1.mes, Usuario.Item1.cod_usuario, 0, 0);

            model.cod_unidad_negocio = cod_unidad_negocio;
            model.cod_frecuencia = cod_frecuencia;
            model.cod_rango_fecha = cod_rango_fecha.Value;
            model.cod_carga_auxiliar = cod_carga_auxiliar.Value;
            model.titulo = defectoItem1.titulo;

            if (defectoItem1.est_aprobacion == 0)
                model.estado = "Activo";
            else if(defectoItem1.est_aprobacion == 1)
                model.estado = "Pre-Cierre";
            else
                model.estado = "Cerrado";


            var lista1 = listPeriodo.Item1.Where(a => a.periodo == defectoItem1.periodo).Select(a => a.periodo).AsEnumerable() ?? new HashSet<int>();
            model.Periodos = listPeriodo.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.periodo.ToString(),
                Text = x.periodo.ToString(),
                Selected = lista1.Any(m => m == x.periodo),
                HtmlAttributes = new
                {
                    data_alias = x.periodo
                }
            });
            var lista2 = listMes.Item1.Where(a => a.mes == defectoItem1.mes).Select(a => a.mes).AsEnumerable() ?? new HashSet<int>();
            model.Meses = listMes.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.mes.ToString(),
                Text = x.nom_mes.ToString(),
                Selected = lista2.Any(m => m == x.mes),
                HtmlAttributes = new
                {
                    data_alias = x.mes
                }
            });
            var lista3 = listFecha.Item1.Where(a => a.cod_rango_fecha == defectoItem1.cod_rango_fecha).Select(a => a.cod_rango_fecha).AsEnumerable() ?? new HashSet<int>();
            model.Fechas = listFecha.Item1.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_rango_fecha.ToString(),
                Text = x.nom_rango_fecha.ToString(),
                Selected = lista3.Any(m => m == x.cod_rango_fecha),
                HtmlAttributes = new
                {
                    data_alias = x.cod_rango_fecha
                }
            });

            model.mensajeError = defecto.Item2.mensaje ?? "";
            model.tipoError = defecto.Item2.tipo ?? "";
            return View(model);
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
            reportParameters.Add(new ReportParameter("cod_frecuencia", cod_frecuencia, false));
            reportParameters.Add(new ReportParameter("cod_unidad_negocio", cod_unidad_negocio, false));
            reportParameters.Add(new ReportParameter("cod_rango_fecha", cod_rango_fecha, false));
            reportParameters.Add(new ReportParameter("cod_carga_auxiliar", cod_carga_auxiliar, false));

            var nomArchivo = string.Empty;

            if (cod_carga_auxiliar == "500")
            {
                switch (cod_frecuencia)
                {
                    case "D":
                        reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["Report_Path"] + "/rp_kop_gerencialIndicadorDia";
                        break;
                    case "S":
                        reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["Report_Path"] + "/rp_kop_gerencialIndicadorSem";
                        break;
                    case "M":
                        reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["Report_Path"] + "/rp_kop_gerencialIndicadorMes";
                        break;
                }
            }
            else
            {
                switch (cod_frecuencia)
                {
                    case "D":
                        reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["Report_Path"] + "/rp_kop_movimientoIndicadorDia";
                        break;
                    case "S":
                        reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["Report_Path"] + "/rp_kop_movimientoIndicadorSem";
                        break;
                    case "M":
                        reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["Report_Path"] + "/rp_kop_movimientoIndicadorMes";
                        break;
                }
            }
            
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["Report_Server"]);
            reportViewer.ServerReport.SetParameters(reportParameters);

            reportViewer.ServerReport.Refresh();
            ViewBag.ReportViewer = reportViewer;

            return PartialView("_TableroControl");
        }

        public JsonResult JSON_GetPanelPorUnidad(string cod_unidad_negocio)
        {
            var listFormato = kopNeg.fn_kop_sel_cargaAuxiliar("FORMATO",Usuario.Item1.cod_usuario, cod_unidad_negocio) ?? new List<KOP_CargaAuxiliarBean>();
            var listFrecuencia = kopNeg.fn_kop_sel_frecuencia(Usuario.Item1.cod_usuario, cod_unidad_negocio, "", 0, "Frecuencias_Formato") ?? new List<GEN_FrecuenciaBean>();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listPeriodo = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "PERIODO", "D", 0, 0,Usuario.Item1.cod_usuario,0,0);
            int periodoTop = listPeriodo.Item1.Select(l => l.periodo).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listMes = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "MES", "D", periodoTop, 0, Usuario.Item1.cod_usuario,0,0);
            int mesTop = listMes.Item1.Select(l => l.mes).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "FECHA", "D", periodoTop, mesTop, Usuario.Item1.cod_usuario,0,0);
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

        public JsonResult JSON_GetPanelPorFrecuencia(string cod_unidad_negocio, string cod_frecuencia)
        {
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listPeriodo = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "PERIODO", cod_frecuencia, 0, 0, Usuario.Item1.cod_usuario,0,0);
            int periodoTop = listPeriodo.Item1.Select(l => l.periodo).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listMes = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "MES", cod_frecuencia, periodoTop, 0, Usuario.Item1.cod_usuario,0,0);
            int mesTop = listMes.Item1.Select(l => l.mes).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "FECHA", cod_frecuencia, periodoTop, mesTop, Usuario.Item1.cod_usuario,0,0);
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

        public JsonResult JSON_GetPanelPorPeriodo(string cod_unidad_negocio, string cod_frecuencia, int periodo)
        {
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listMes = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "MES", cod_frecuencia, periodo, 0, Usuario.Item1.cod_usuario,0,0);
            var lastMonth = listMes.Item1.Select(l => l.mes).First();
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "FECHA", cod_frecuencia, periodo, lastMonth, Usuario.Item1.cod_usuario,0,0);
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

        public JsonResult JSON_GetPanelPorMes(string cod_unidad_negocio, string cod_frecuencia, int periodo, int mes)
        {
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> listFecha = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "FECHA", cod_frecuencia, periodo, mes, Usuario.Item1.cod_usuario, 0, 0);
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

        public JsonResult JSON_GetEstado(string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, int cod_carga_auxiliar)
        {
            Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> defecto = kopNeg.fn_kop_sel_rangoFecha(cod_unidad_negocio, "DEFAULT", cod_frecuencia, 0, 0, Usuario.Item1.cod_usuario, cod_carga_auxiliar, cod_rango_fecha);
            var defectoItem1 = defecto.Item1.First();

            var model = new AuxiliarEdit();
            if (defectoItem1.est_aprobacion == 0)
                model.estado = "Activo";
            else if (defectoItem1.est_aprobacion == 1)
                model.estado = "Pre-Cierre";
            else
                model.estado = "Cerrado";

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RunXls(string cod_unidad_negocio, string cod_frecuencia, int? cod_rango_fecha, int? cod_carga_auxiliar)
        {
            var mensajeError = string.Empty;
            var tablero = string.Empty;
            var codFrecuencia = cod_frecuencia;
            var codUnidadNegocio = cod_unidad_negocio;
            var codCargaAuxiliar = cod_carga_auxiliar;
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

            //Identificar el archivo a descargar, incluyendo la ruta de acceso.
            var excelFuente = string.Empty;
            var excelDestino = string.Empty;
            var fileDestino = string.Empty;
            excelFuente = Server.MapPath("~/xlso/KPI_tableroControl.xlsm");

            //Cambiamos el nombre del archivo Destino
            fileDestino = "kpi_" + tablero + "_" + codUnidadNegocio + "_" + codRangoFecha + "_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xlsx";
            excelDestino = Server.MapPath("~/xlsd/" + fileDestino);

            try
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application(); //CreateObject("Excel.Application")
                excelApp.Application.Visible = false;
                excelApp.DisplayAlerts = false;

                var excelWorkbook = excelApp.Workbooks.Open(Filename: excelFuente,ReadOnly:true);
                GetWindowThreadProcessId(excelApp.Hwnd, out pid);

                excelWorkbook.Sheets["Registro"].Range("B5").Value = codUnidadNegocio;
                excelWorkbook.Sheets["Registro"].Range("C5").Value = codCargaAuxiliar;
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


                if(mensajeError == null)
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

        public ActionResult GenerarMovimiento()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = kopNeg.fn_kop_sel_unidad(Usuario.Item1.cod_usuario, "", "", 0, "Unidad") ?? new List<GEN_UnidadNegocioBean>();
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
            var listFrecuencia = kopNeg.fn_kop_sel_frecuencia(Usuario.Item1.cod_usuario, cod_unidad_negocio, "", 0, "Frecuencias_Formato") ?? new List<GEN_FrecuenciaBean>();
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

            return View(model);
        }
        [HttpPost]
        public ActionResult GenerarMovimiento(GEN_AprobacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                mensajeBean = kopNeg.fn_kop_movimientoIndicador(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.cod_frecuencia, model.cod_rango_fecha.Value);

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
            GEN_IndicadorBean bean = kopNeg.fn_kop_get_indicador(Usuario.Item1.cod_usuario, cod_unidad_negocio, "GET_INDICADOR", cod_indicador);
            return Json(bean, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit_Indicador(string cod_unidad_negocio, long cod_indicador)
        {
            var model = new AuxiliarEdit();
            model.GEN_IndicadorBean = kopNeg.fn_kop_get_indicador(Usuario.Item1.cod_usuario, cod_unidad_negocio,"GET_INDICADOR", cod_indicador);
            if (model == null)
            {
                return HttpNotFound();
            }

            List<GEN_TipoBean> dataFrecMes = new List<GEN_TipoBean>();
            dataFrecMes.Add(new GEN_TipoBean()
            {
                valor = "",
                texto = ""
            });
            dataFrecMes.Add(new GEN_TipoBean()
            {
                valor = "M",
                texto = "(M) Mensual"
            });
            dataFrecMes.Add(new GEN_TipoBean()
            {
                valor = "V",
                texto = "(V) Virtual"
            });
            dataFrecMes.Add(new GEN_TipoBean()
            {
                valor = "E",
                texto = "(E) Especial"
            });
            dataFrecMes.Add(new GEN_TipoBean()
            {
                valor = "O",
                texto = "(O) Oculto"
            });

            var dataFrecMesSelected = dataFrecMes.Where(a => a.valor == model.GEN_IndicadorBean.frecuencia_mes).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.ddlFrecuenciaMes = dataFrecMes.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataFrecMesSelected.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            List<GEN_TipoBean> dataFrecSem = new List<GEN_TipoBean>();
            dataFrecSem.Add(new GEN_TipoBean()
            {
                valor = "",
                texto = ""
            });
            dataFrecSem.Add(new GEN_TipoBean()
            {
                valor = "S",
                texto = "(S) Semanal"
            });
            dataFrecSem.Add(new GEN_TipoBean()
            {
                valor = "V",
                texto = "(V) Virtual"
            });
            dataFrecSem.Add(new GEN_TipoBean()
            {
                valor = "E",
                texto = "(E) Especial"
            });
            dataFrecSem.Add(new GEN_TipoBean()
            {
                valor = "O",
                texto = "(O) Oculto"
            });

            var dataFrecSemSelected = dataFrecSem.Where(a => a.valor == model.GEN_IndicadorBean.frecuencia_sem).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.ddlFrecuenciaSem = dataFrecSem.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataFrecSemSelected.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            List<GEN_TipoBean> dataFrecDia = new List<GEN_TipoBean>();
            dataFrecDia.Add(new GEN_TipoBean()
            {
                valor = "",
                texto = ""
            });
            dataFrecDia.Add(new GEN_TipoBean()
            {
                valor = "D",
                texto = "(D) Diario"
            });
            dataFrecDia.Add(new GEN_TipoBean()
            {
                valor = "V",
                texto = "(V) Virtual"
            });
            dataFrecDia.Add(new GEN_TipoBean()
            {
                valor = "E",
                texto = "(E) Especial"
            });
            dataFrecDia.Add(new GEN_TipoBean()
            {
                valor = "O",
                texto = "(O) Oculto"
            });

            var dataFrecDiaSelected = dataFrecDia.Where(a => a.valor == model.GEN_IndicadorBean.frecuencia_dia).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.ddlFrecuenciaDia = dataFrecDia.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataFrecDiaSelected.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            var dataTipoIndicador = kopNeg.fn_kop_sel_tipo(Usuario.Item1.cod_usuario, "", "DDL_TIP_INDICADOR") ?? new List<GEN_TipoBean>();
            var dataTipoIndicadorSelected = dataTipoIndicador.Where(a => a.valor == model.GEN_IndicadorBean.tip_indicador).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.ddlTipoIndicador = dataTipoIndicador.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataTipoIndicadorSelected.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });
            var dataTipoAcumulado = kopNeg.fn_kop_sel_tipo(Usuario.Item1.cod_usuario, "", "DDL_TIP_ACUMULADO") ?? new List<GEN_TipoBean>();
            var dataTipoAcumuladoSelected = dataTipoAcumulado.Where(a => a.valor == model.GEN_IndicadorBean.tip_acumulado).Select(a => a.valor).AsEnumerable() ?? new HashSet<string>();
            model.ddlTipoAcumulado = dataTipoAcumulado.Select(x => new ExtendedSelectListItem
            {
                Value = x.valor.ToString(),
                Text = x.texto,
                Selected = dataTipoAcumuladoSelected.Any(c => c == x.valor),
                HtmlAttributes = new
                {
                    data_alias = x.valor
                }
            });

            var dataGrupoIndicador = kopNeg.fn_kop_sel_indicador(Usuario.Item1.cod_usuario, cod_unidad_negocio, "DDL_GRUPO_INDICADOR") ?? new List<GEN_IndicadorBean>();
            var dataGrupoIndicadorSelected = dataGrupoIndicador.Where(a => a.cod_indicador == model.GEN_IndicadorBean.cod_grupo_indicador).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
            model.ddlGrupoIndicador = dataGrupoIndicador.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_indicador.ToString(),
                Text = x.nom_indicador,
                Selected = dataGrupoIndicadorSelected.Any(c => c == x.cod_indicador),
                HtmlAttributes = new
                {
                    data_alias = x.cod_indicador
                }
            });


            var dataIndicador = kopNeg.fn_kop_sel_indicador(Usuario.Item1.cod_usuario, cod_unidad_negocio, "DDL_INDICADOR") ?? new List<GEN_IndicadorBean>();
            var dataIndicadorSelected = dataIndicador.Where(a => a.cod_indicador == model.GEN_IndicadorBean.ponderado_indicador).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
            model.ddlPonderadoIndicador = dataIndicador.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_indicador.ToString(),
                Text = x.nom_indicador,
                Selected = dataIndicadorSelected.Any(c => c == x.cod_indicador),
                HtmlAttributes = new
                {
                    data_alias = x.cod_indicador
                }
            });

            var dataSubGrupoIndicador = kopNeg.fn_kop_sel_indicador(Usuario.Item1.cod_usuario, cod_unidad_negocio, "DDL_SUBGRUPO_INDICADOR") ?? new List<GEN_IndicadorBean>();
            var dataSubGrupoIndicadorSelected = dataSubGrupoIndicador.Where(a => a.cod_indicador == model.GEN_IndicadorBean.cod_subgrupo_indicador).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
            model.ddlSubGrupoIndicador = dataSubGrupoIndicador.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_indicador.ToString(),
                Text = x.nom_indicador,
                Selected = dataSubGrupoIndicadorSelected.Any(c => c == x.cod_indicador),
                HtmlAttributes = new
                {
                    data_alias = x.cod_indicador
                }
            });

            var dataIndicadorPrespuestoSelected = dataIndicador.Where(a => a.cod_indicador == model.GEN_IndicadorBean.cod_indicador_presupuesto).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long?>();
            model.ddlIndicadorPresupuesto = dataIndicador.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_indicador.ToString(),
                Text = x.nom_indicador,
                Selected = dataIndicadorPrespuestoSelected.Any(c => c == x.cod_indicador),
                HtmlAttributes = new
                {
                    data_alias = x.cod_indicador
                }
            });

            return View(model);
        }

        public ActionResult Reconstruye()
        {
            var cod_unidad_negocio = string.Empty;
            var cod_aplicacion = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();

            if (listUnidad == null)
            {
                return HttpNotFound();
            }

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

        [HttpPost]
        public ActionResult Reconstruye(GEN_AprobacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                mensajeBean = kopNeg.fn_kop_pro_reconstruye(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, model.cod_frecuencia, "");

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

        public ActionResult ReporteOperativo()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = kopNeg.fn_kop_sel_pivotParam(0, "", "", "", "") ?? new List<KOP_PivotParamBean>();
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

            
            var listaAnioPivot = kopNeg.fn_kop_sel_pivotParam(1, "", "", "", "") ?? new List<KOP_PivotParamBean>();
            var anioSelected = listaAnioPivot.Select(x => x.anio).First();
            //var listaAnioPivotSelected = listaAnioPivot.Where(a => a.anio == anioSelected).Select(a => a.anio).AsEnumerable() ?? new HashSet<int>();
            model.ddlAnio = listaAnioPivot.Select(x => new ExtendedSelectListItem
            {
                Value = x.anio.ToString(),
                Text = x.anio.ToString(),
                //Selected = listaAnioPivotSelected.Any(c => c == x.anio),
                HtmlAttributes = new
                {
                    data_alias = x.anio
                }
            });

            /*
            var listaMesPivot = kopNeg.fn_kop_sel_pivotParam(2, anioSelected.ToString(), "", "", "") ?? new List<KOP_PivotParamBean>();
            var mesSelected = listaMesPivot.Select(x => x.mes).First();
            var listaMesPivotSelected = listaMesPivot.Where(a => a.mes == mesSelected).Select(a => a.mes).AsEnumerable() ?? new HashSet<int>();
            model.ddlMes = listaMesPivot.Select(x => new ExtendedSelectListItem
            {
                Value = x.mes.ToString(),
                Text = x.nom_mes.ToString(),
                Selected = listaMesPivotSelected.Any(c => c == x.mes),
                HtmlAttributes = new
                {
                    data_alias = x.mes
                }
            });

            var listaSemanaPivot = kopNeg.fn_kop_sel_pivotParam(3, anioSelected.ToString(), mesSelected.ToString(), "", "") ?? new List<KOP_PivotParamBean>();
            var semanaSelected = listaSemanaPivot.Select(x => x.semana).First();
            var listaSemanaPivotSelected = listaSemanaPivot.Where(a => a.semana == semanaSelected).Select(a => a.semana).AsEnumerable() ?? new HashSet<int>();
            model.ddlSemana = listaSemanaPivot.Select(x => new ExtendedSelectListItem
            {
                Value = x.semana.ToString(),
                Text = x.semana.ToString(),
                //Selected = listaSemanaPivotSelected.Any(c => c == x.semana),
                HtmlAttributes = new
                {
                    data_alias = x.semana
                }
            });


            var listaDiaPivot = kopNeg.fn_kop_sel_pivotParam(4, anioSelected.ToString(), mesSelected.ToString(), "", "") ?? new List<KOP_PivotParamBean>();
            var diaSelected = listaDiaPivot.Select(x => x.dia).First();
            var listaDiaPivotSelected = listaDiaPivot.Where(a => a.dia == diaSelected).Select(a => a.dia).AsEnumerable() ?? new HashSet<String>();
            model.ddlDia = listaDiaPivot.Select(x => new ExtendedSelectListItem
            {
                Value = x.dia.ToString(),
                Text = x.dia.ToString(),
                //Selected = listaDiaPivotSelected.Any(c => c == x.dia),
                HtmlAttributes = new
                {
                    data_alias = x.dia
                }
            });
            */

            return View(model);
        }

        public JsonResult JSON_GetParamReporteOpe(string cod_unidad_negocio, string anio, string mes, string semana)
        {
            var model = new AuxiliarEdit();

            if (mes == string.Empty || mes == null)
            {
                var listaMesPivot = kopNeg.fn_kop_sel_pivotParam(2, anio.ToString(), "", "", "") ?? new List<KOP_PivotParamBean>();
                var mesSelected = listaMesPivot.Select(x => x.mes).First();
                mes = "";//mesSelected.ToString();
                var listaMesPivotSelected = listaMesPivot.Where(a => a.mes == mesSelected).Select(a => a.mes).AsEnumerable() ?? new HashSet<int>();
                model.ddlMes = listaMesPivot.Select(x => new ExtendedSelectListItem
                {
                    Value = x.mes.ToString(),
                    Text = x.nom_mes.ToString(),
                    //Selected = listaMesPivotSelected.Any(c => c == x.mes),
                    HtmlAttributes = new
                    {
                        data_alias = x.mes
                    }
                });
            }

            if (semana == string.Empty || semana == null)
            {
                var listaSemanaPivot = kopNeg.fn_kop_sel_pivotParam(3, anio.ToString(), mes.ToString(), "", "") ?? new List<KOP_PivotParamBean>();
                var semanaSelected = listaSemanaPivot.Select(x => x.semana).First();
                semana = semanaSelected.ToString();
                var listaSemanaPivotSelected = listaSemanaPivot.Where(a => a.semana == semanaSelected).Select(a => a.semana).AsEnumerable() ?? new HashSet<int>();
                model.ddlSemana = listaSemanaPivot.Select(x => new ExtendedSelectListItem
                {
                    Value = x.semana.ToString(),
                    Text = x.semana.ToString(),
                    //Selected = listaSemanaPivotSelected.Any(c => c == x.semana),
                    HtmlAttributes = new
                    {
                        data_alias = x.semana
                    }
                });
                semana = "";
            }
            
            var listaDiaPivot = kopNeg.fn_kop_sel_pivotParam(4, anio.ToString(), mes.ToString(), semana.ToString(), "") ?? new List<KOP_PivotParamBean>();
            var diaSelected = listaDiaPivot.Select(x => x.dia).First();
            var listaDiaPivotSelected = listaDiaPivot.Where(a => a.dia == diaSelected).Select(a => a.dia).AsEnumerable() ?? new HashSet<String>();
            model.ddlDia = listaDiaPivot.Select(x => new ExtendedSelectListItem
            {
                Value = x.dia.ToString(),
                Text = x.dia.ToString(),
                //Selected = listaDiaPivotSelected.Any(c => c == x.dia),
                HtmlAttributes = new
                {
                    data_alias = x.dia
                }
            });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetReporteOperativo(string cod_unidad_negocio, string anio, string mes, string semana, string dia)
        {
            string dias = "";
            if (dia != null && dia !=string.Empty)
            {
                string[] fechas = dia.Split(',');
                List<string> listaFecha = new List<string>();
                foreach (var item in fechas)
                {
                    DateTime dt = Convert.ToDateTime(item); //DateTime.ParseExact(dia, "ddMMyyyy", CultureInfo.InvariantCulture);
                    dia = dt.ToString("yyMMdd");
                    listaFecha.Add(dia);
                }
                dias = String.Join(",", listaFecha);

                
            }
            

            List<KOP_PivotReporteBean> lista = kopNeg.fn_kop_sel_configPivot(101, cod_unidad_negocio, anio, mes, semana, dia);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

    }
}
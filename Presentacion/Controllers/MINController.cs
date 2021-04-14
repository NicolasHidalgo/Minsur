using Beans;
using Negocio;
using Presentacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class MINController : BaseController
    {
        MIN_Negocio minNeg = new MIN_Negocio();

        public ActionResult Consulta_Estructura()
        {
            return View();
        }

        public JsonResult JSON_GetDropDownTipoMineral(string cod_unidad_negocio, string tipMineral)
        {
            if (cod_unidad_negocio == null || cod_unidad_negocio == string.Empty)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var data = minNeg.fn_min_sel_tipMineral("TIP_MINERAL", cod_unidad_negocio, Usuario.Item1.cod_usuario) ?? new List<MIN_TipoMineralBean>();
            var dataSelected = data.Where(a => a.mineral == tipMineral).Select(a => a.mineral).AsEnumerable() ?? new HashSet<string>();
            var model = new AuxiliarEdit();
            model.DropDownList = data.Select(x => new ExtendedSelectListItem
            {
                Value = x.idMineral.ToString(),
                Text = x.mineral,
                Selected = dataSelected.Any(c => c == x.mineral),
                HtmlAttributes = new
                {
                    data_alias = x.mineral
                }
            });
            return Json(model.DropDownList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetDropDownTipoTajo(string cod_unidad_negocio, string cod_tajo_tipo)
        {
            if (cod_unidad_negocio == null || cod_unidad_negocio == string.Empty)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var data = minNeg.fn_min_sel_tipoTajo(Usuario.Item1.cod_usuario,"DDL_TIPO", cod_unidad_negocio) ?? new List<MIN_TipoTajoBean>();
            var dataSelected = data.Where(a => a.cod_tajo_tipo == cod_tajo_tipo).Select(a => a.cod_tajo_tipo).AsEnumerable() ?? new HashSet<string>();
            var model = new AuxiliarEdit();
            model.DropDownList = data.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_tajo_tipo.ToString(),
                Text = x.nom_tajo_tipo,
                Selected = dataSelected.Any(c => c == x.cod_tajo_tipo),
                HtmlAttributes = new
                {
                    data_alias = x.cod_tajo_tipo
                }
            });
            return Json(model.DropDownList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetDropDownEstructura(string cod_unidad_negocio, int? cod_tajo_estructura)
        {
            if (cod_unidad_negocio == null || cod_unidad_negocio == string.Empty)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var data = minNeg.fn_min_sel_estructura(Usuario.Item1.cod_usuario, "DDL_ESTRUCTURA", cod_unidad_negocio) ?? new List<MIN_EstructuraBean>();
            var dataSelected = data.Where(a => a.cod_tajo_estructura == cod_tajo_estructura).Select(a => a.cod_tajo_estructura).AsEnumerable() ?? new HashSet<int?>();
            var model = new AuxiliarEdit();
            model.DropDownList = data.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_tajo_estructura.ToString(),
                Text = x.nom_tajo_estructura,
                Selected = dataSelected.Any(c => c == x.cod_tajo_estructura),
                HtmlAttributes = new
                {
                    data_alias = x.cod_tajo_estructura
                }
            });
            return Json(model.DropDownList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit_Estructura(MIN_EstructuraBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                string cod_unidad_negocio = string.Empty;
                if (Session["cod_unidad_negocio"] != null)
                {
                    model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                }

                if (model.accion == "CREATE")
                {
                    mensajeBean = minNeg.fn_min_cud_tajoEstructura(model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "INSERT", model);
                }
                else
                {
                    mensajeBean = minNeg.fn_min_cud_tajoEstructura(model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "UPDATE", model);
                }

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
        public ActionResult Edit_Tajo(MIN_TajoBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                string cod_unidad_negocio = string.Empty;
                if (Session["cod_unidad_negocio"] != null)
                {
                    model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                }

                if (model.accion == "CREATE")
                {
                    mensajeBean = minNeg.fn_min_cud_tajo(model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "INSERT", model);
                }
                else
                {
                    mensajeBean = minNeg.fn_min_cud_tajo(model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "UPDATE", model);
                }

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
        public ActionResult Delete_Estructura(MIN_EstructuraBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                string cod_unidad_negocio = string.Empty;
                if (Session["cod_unidad_negocio"] != null)
                {
                    model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                }

                mensajeBean = minNeg.fn_min_cud_tajoEstructura(model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "DELETE", model);

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
        public ActionResult Delete_Tajo(MIN_TajoBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                string cod_unidad_negocio = string.Empty;
                if (Session["cod_unidad_negocio"] != null)
                {
                    model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                }

                mensajeBean = minNeg.fn_min_cud_tajo(model.cod_unidad_negocio, Usuario.Item1.cod_usuario, "DELETE", model);

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

        public ActionResult Consulta_Tajo()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var data = minNeg.fn_min_sel_estructura(Usuario.Item1.cod_usuario, "DDL_ESTRUCTURA", model.cod_unidad_negocio) ?? new List<MIN_EstructuraBean>();
            var dataSelected = data.Select(a => a.cod_tajo_estructura).AsEnumerable() ?? new HashSet<int?>();
            model.DropDownList = data.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_tajo_estructura.ToString(),
                Text = x.nom_tajo_estructura,
                Selected = dataSelected.Any(c => c == x.cod_tajo_estructura),
                HtmlAttributes = new
                {
                    data_alias = x.cod_tajo_estructura
                }
            });

            return View(model);
        }
    }
}
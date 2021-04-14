using Beans;
using Negocio;
using Presentacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class CPXController : BaseController
    {
        CPX_Negocio cpxNeg = new CPX_Negocio();
        static string xlsPathDES = ConfigurationManager.AppSettings["xlsPathDES"];
        static string xlsPathPRO = ConfigurationManager.AppSettings["xlsPathPRO"];

        public ActionResult Maestras()
        {
            var model = new AuxiliarEdit();
            string cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var dataMaestra = cpxNeg.fn_cpx_sel_maestra(Usuario.Item1.cod_usuario, model.cod_unidad_negocio, "DDL_MAESTRA") ?? new List<CPX_MaestraBean>();
            model.ddlTipo = dataMaestra.Select(x => new ExtendedSelectListItem
            {
                Value = x.tipo.ToString(),
                Text = x.tipo,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.tipo
                }
            });

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit_Maestra(CPX_MaestraBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                mensajeBean = cpxNeg.up_cpx_cud_maestra(Usuario.Item1.cod_usuario, model);

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
    }
}
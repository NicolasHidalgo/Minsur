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
    public class SEGController : BaseController
    {
        SEG_Negocio segNeg = new SEG_Negocio();

        public ActionResult Consulta()
        {
            var cod_unidad_negocio = HttpContext.Request["cod_unidad_negocio"] ?? string.Empty;

            var listAplicacion = segNeg.fn_seg_listAplicacion("Aplicacion", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<SEG_AplicacionBean>();
            var listUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();
            var listPerfil = segNeg.fn_seg_listPerfil("Select", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<SEG_PerfilBean>();

            if (listAplicacion == null)
            {
                return HttpNotFound();
            }

            var model = new AuxiliarEdit();

            model.Aplicaciones = listAplicacion.OrderBy(x => x.cod_aplicacion).Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_aplicacion.ToString(),
                Text = x.nom_aplicacion,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_aplicacion
                }
            });

            var listaUnidadSelected = listUnidad.Where(a => a.cod_unidad_negocio == cod_unidad_negocio).Select(a => a.cod_unidad_negocio).AsEnumerable() ?? new HashSet<string>();
            model.Unidades = listUnidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_unidad_negocio.ToString(),
                Text = x.nom_unidad_negocio,
                Selected = listaUnidadSelected.Any(m => m == x.cod_unidad_negocio),
                HtmlAttributes = new
                {
                    data_alias = x.cod_unidad_negocio
                }
            });

            model.Perfiles = listPerfil.OrderBy(x => x.nom_perfil).Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_perfil.ToString(),
                Text = x.nom_perfil,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.nom_perfil
                }
            });

            return View(model);
        }

        public ActionResult Acceso()
        {
            var cod_unidad_negocio = string.Empty;
            var cod_aplicacion = string.Empty;
            if(Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listAplicacion = segNeg.fn_seg_listAplicacion("Aplicacion", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<SEG_AplicacionBean>();
            var listUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();
            var listPerfil = segNeg.fn_seg_listPerfil("Select", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<SEG_PerfilBean>();
            var listUsuario = segNeg.fn_seg_listUsuario("Select") ?? new List<SEG_UsuarioBean>();

            if (listAplicacion == null)
            {
                return HttpNotFound();
            }

            var model = new AuxiliarEdit();

            model.Aplicaciones = listAplicacion.OrderBy(x => x.cod_aplicacion).Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_aplicacion.ToString(),
                Text = x.nom_aplicacion,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_aplicacion
                }
            });

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

            model.Usuarios = listUsuario.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_usuario.ToString(),
                Text = x.nom_usuario + new string(' ', 10) + '(' + x.cod_usuario + ')',
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_usuario
                }
            });

            model.Perfiles = listPerfil.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_perfil.ToString(),
                Text = x.nom_perfil,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.nom_perfil
                }
            });

            return View(model);
        }

        [HttpPost]
        public ActionResult Acceso(AuxiliarEdit model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                var cod_perfil = "";
                foreach (var item in model.Perfiles)
                {
                    if(item != null)
                        cod_perfil += item.Value + ',';
                }

                mensajeBean = segNeg.updatePermiso(model.accion, Usuario.Item1.cod_usuario, model.cod_aplicacion, model.cod_unidad_negocio, cod_perfil,model.cod_usuario_accion);

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

        public JsonResult JSON_GetPerfilesPorUsuario(string cod_aplicacion, string cod_unidad_negocio, string cod_usuario)
        {
            var listPerfil = segNeg.fn_seg_listPerfil("Select", cod_usuario, cod_aplicacion, cod_unidad_negocio, "") ?? new List<SEG_PerfilBean>();
            var lista = listPerfil.Where(a => a.nom_perfil.Contains("(*)")).Select(a => a.cod_perfil).AsEnumerable() ?? new HashSet<int>();

            var model = new AuxiliarEdit();
            model.Perfiles = listPerfil.OrderBy(x => x.nom_perfil).Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_perfil.ToString(),
                Text = x.nom_perfil,
                Selected = lista.Any(m => m == x.cod_perfil),
                HtmlAttributes = new
                {
                    data_alias = x.nom_perfil
                }
            });
            return Json(model.Perfiles, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetPerfilesPorAplicacion(string cod_aplicacion, string cod_unidad_negocio)
        {
            var listPerfil = segNeg.fn_seg_listPerfil("Select", Usuario.Item1.cod_usuario, cod_aplicacion, cod_unidad_negocio, "") ?? new List<SEG_PerfilBean>();

            var model = new AuxiliarEdit();
            model.Perfiles = listPerfil.OrderBy(x => x.nom_perfil).Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_perfil.ToString(),
                Text = x.nom_perfil,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.nom_perfil
                }
            });
            return Json(model.Perfiles, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Correo()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listAplicacion = segNeg.fn_seg_listAplicacion("Aplicacion", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<SEG_AplicacionBean>();
            var listUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, Usuario.Item1.cod_aplicacion, "", "") ?? new List<GEN_UnidadNegocioBean>();
            
            if (listAplicacion == null)
            {
                return HttpNotFound();
            }

            var model = new AuxiliarEdit();

            model.Aplicaciones = listAplicacion.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_aplicacion.ToString(),
                Text = x.nom_aplicacion,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.cod_aplicacion
                }
            });

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
        public ActionResult Correo(AuxiliarEdit model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                var cod_perfil = "";
                foreach (var item in model.Perfiles)
                {
                    cod_perfil += item.Value + ',';
                }

                mensajeBean = segNeg.updateCorreo(model.cod_aplicacion, model.cod_unidad_negocio,model.ide_grupo_correo, Usuario.Item1.cod_usuario, model.accion, string.Empty,model.cod_directorio_activo);

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
        public JsonResult JSON_GetGrupoCorreoPorUnidad(string cod_aplicacion, string cod_unidad_negocio)
        {
            var listGrupo = segNeg.fn_seg_sel_grupoCorreo(cod_aplicacion, cod_unidad_negocio, 0, Usuario.Item1.cod_usuario, "Grupos_Correo") ?? new List<GEN_CorreoBean>();

            var model = new AuxiliarEdit();
            model.Grupos = listGrupo.Select(x => new ExtendedSelectListItem
            {
                Value = x.ide_grupo_correo.ToString(),
                Text = x.nom_grupo_correo,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.ide_grupo_correo
                }
            });
            return Json(model.Grupos, JsonRequestBehavior.AllowGet);
        }


    }
}
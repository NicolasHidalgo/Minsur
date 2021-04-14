using Beans;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Presentacion.Controllers
{
    public class BaseController : Controller
    {
        /*.EDMX Entities*/
        protected SEG_Negocio neg_seguridad;

        /* DATOS DEL SISTEMA */
        //protected tb_sistema Sistema;

        /* DATOS DEL USUARIO */
        protected Tuple<SEG_UsuarioBean,GEN_MensajeBean> Usuario;
        protected string mensaje = "";
        static string dominio = ConfigurationManager.AppSettings["dominio"];

        [DllImport("user32.dll")]
        static public extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);
        Process GetExcelProcess(Microsoft.Office.Interop.Excel.Application excelApp)
        {
            int id;
            GetWindowThreadProcessId(excelApp.Hwnd, out id);
            return Process.GetProcessById(id);
        }


        public BaseController()
        {
            neg_seguridad = new SEG_Negocio();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var cod_aplicacion = "";
            int number = 0;

            var LoginName = string.Empty;

            if(Session["LoginName"] != null)
            {
                LoginName = Session["LoginName"].ToString();
            }
            else
            {
                if (System.Diagnostics.Debugger.IsAttached)
                    LoginName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                else
                    LoginName = HttpContext.User.Identity.Name.ToString();
            }

            var usuario = HttpContext.User.Identity.Name.ToString();
            var cod_usuario = string.Empty;

            if (LoginName.Contains("\\"))
                cod_usuario = LoginName;
            else if (!(usuario.Contains(dominio)))
                cod_usuario = dominio + usuario;
            else
                cod_usuario = usuario;

            Session["clientId"] = cod_usuario;
            var hostname = Dns.GetHostName();
            var session = Session.SessionID;
            Session["session"] = session;
            cod_aplicacion = "";
            if (requestContext.RouteData.Values["id"] != null)
            {
                cod_aplicacion = requestContext.RouteData.Values["id"].ToString();
                if (int.TryParse(cod_aplicacion, out number))
                    cod_aplicacion = Session["cod_aplicacion"].ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(Session["cod_aplicacion"] as string))
                    cod_aplicacion = Session["cod_aplicacion"].ToString();
            }

            Usuario = neg_seguridad.up_seg_pro_usuario(cod_usuario, cod_aplicacion, "ACCESO", hostname, session);

            GEN_MensajeBean log = null;
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();

            //VALIDANDO ERRORES DE USUARIO
            var errorUsuario = false;
            if (Usuario.Item2.tipo == "ERROR")
            {
                requestContext.HttpContext.Response.Clear();
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "Error");
                routeData.Values.Add("exception", Usuario.Item2.mensaje);
                routeData.Values.Add("statusCode", 999);

                Response.TrySkipIisCustomErrors = true;
                IController controller = new ErrorController();
                controller.Execute(new RequestContext(requestContext.HttpContext, routeData));
                Response.End();
                errorUsuario = true;
            }

            if (errorUsuario == false)
            {
                //VALIDANDO ERRORES DE SESSION
                var errorSession = false;
                var idx = requestContext.HttpContext.Request["idx"];
                var isAjax = requestContext.HttpContext.Request.IsAjaxRequest();
                if (controllerName != "Home" && isAjax == false)
                {
                    if (idx == null || idx != Session.SessionID)
                    {
                        GEN_MensajeBean execute = null;
                        if(cod_aplicacion == string.Empty)
                        {
                            if (requestContext.RouteData.Values["controller"] != null)
                            {
                                cod_aplicacion = requestContext.RouteData.Values["controller"].ToString();
                                if (int.TryParse(cod_aplicacion, out number))
                                    cod_aplicacion = Session["cod_aplicacion"].ToString();
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Session["cod_aplicacion"] as string))
                                    cod_aplicacion = Session["cod_aplicacion"].ToString();
                            }
                        }
                        

                        if (idx.StartsWith("qwe"))
                        {
                            execute = neg_seguridad.Update(Usuario.Item1.cod_usuario, cod_aplicacion, "SESSION", actionName, controllerName, idx);
                            if (!(execute.mensaje.StartsWith("OK")))
                            {
                                requestContext.HttpContext.Response.Clear();
                                var routeData = new RouteData();
                                routeData.Values.Add("controller", "Error");
                                routeData.Values.Add("action", "Error");
                                routeData.Values.Add("exception", execute.mensaje);
                                routeData.Values.Add("statusCode", 999);

                                Response.TrySkipIisCustomErrors = true;
                                IController controller = new ErrorController();
                                controller.Execute(new RequestContext(requestContext.HttpContext, routeData));
                                Response.End();
                                errorSession = true;
                            }
                        }
                        else
                        {
                            requestContext.HttpContext.Response.Clear();
                            var routeData = new RouteData();
                            routeData.Values.Add("controller", "Error");
                            routeData.Values.Add("action", "Error");
                            routeData.Values.Add("exception", "Error: Sesión expirada.");
                            routeData.Values.Add("statusCode", 999);

                            Response.TrySkipIisCustomErrors = true;
                            IController controller = new ErrorController();
                            controller.Execute(new RequestContext(requestContext.HttpContext, routeData));
                            Response.End();
                            errorSession = true;
                        }                        
                    }
                }

                if (errorSession == false)
                {
                    if(cod_aplicacion == null || cod_aplicacion == string.Empty)
                        cod_aplicacion = Usuario.Item1.cod_aplicacion;

                    if (Usuario.Item1 != null)
                    {
                        Session["cod_unidad_negocio"] = Usuario.Item1.cod_unidad_negocio;

                        //Verificando cambios de aplicación
                        if (!string.IsNullOrEmpty(Session["cod_aplicacion"] as string))
                        {
                            if (Session["cod_aplicacion"].ToString() == cod_aplicacion)
                            {
                                if (Session["aplicaciones"] == null || (Session["aplicaciones"] as IEnumerable<SEG_MenuBean>)?.Count() == 0) { }
                                    Session["aplicaciones"] = neg_seguridad.fn_seg_aplicaciones(Usuario.Item1.cod_usuario, 0, "APLICACIONES").AsEnumerable<SEG_MenuBean>();

                                if (Session["Opciones"] == null || (Session["Opciones"] as IEnumerable<SEG_MenuBean>)?.Count() == 0)
                                    Session["Opciones"] = neg_seguridad.fn_seg_menu(Usuario.Item1.cod_usuario, 0, "", cod_aplicacion).AsEnumerable<SEG_MenuBean>();

                                if (Session["Carousel"] == null || (Session["Carousel"] as IEnumerable<SEG_MenuBean>)?.Count() == 0)
                                    Session["Carousel"] = neg_seguridad.fn_seg_sel_carousel(Usuario.Item1.cod_usuario, "Carousel", cod_aplicacion,string.Empty).AsEnumerable<SEG_MenuBean>();
                            }
                            else
                            {
                                Session["aplicaciones"] = neg_seguridad.fn_seg_aplicaciones(Usuario.Item1.cod_usuario, 0, "APLICACIONES").AsEnumerable<SEG_MenuBean>();
                                Session["Opciones"] = neg_seguridad.fn_seg_menu(Usuario.Item1.cod_usuario, 0, "", cod_aplicacion).AsEnumerable<SEG_MenuBean>();
                                Session["Carousel"] = neg_seguridad.fn_seg_sel_carousel(Usuario.Item1.cod_usuario, "Carousel", cod_aplicacion, string.Empty).AsEnumerable<SEG_MenuBean>();
                            }
                        }
                        else
                        {
                            Session["aplicaciones"] = neg_seguridad.fn_seg_aplicaciones(Usuario.Item1.cod_usuario, 0, "APLICACIONES").AsEnumerable<SEG_MenuBean>();
                            Session["Opciones"] = neg_seguridad.fn_seg_menu(Usuario.Item1.cod_usuario, 0, "", cod_aplicacion).AsEnumerable<SEG_MenuBean>();
                            Session["Carousel"] = neg_seguridad.fn_seg_sel_carousel(Usuario.Item1.cod_usuario, "Carousel", cod_aplicacion, string.Empty).AsEnumerable<SEG_MenuBean>();
                        }
                            
                        Session["cod_aplicacion"] = cod_aplicacion;
                        Session["Usuario"] = Usuario.Item1;

                         log = neg_seguridad.Update(Usuario.Item1.cod_usuario, cod_aplicacion, "LOG", hostname, controllerName, session);
                    }
                }
                
            }

        }

    }
}
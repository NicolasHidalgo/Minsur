using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Presentacion.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error(int statusCode, string exception)
        {
            var mensajeError = string.Empty;
            if (statusCode == 403)
            {
                mensajeError += mensajeError + "No tienen los permisos para ver está página.";
            }
            else if(statusCode == 404 )
            {
                mensajeError += mensajeError + "La página requerida no ha sido encontrada.";
            }
            else if (statusCode == 408)
            {
                mensajeError += mensajeError + "El requerimiento a sobrepasdo el tiempo de espera.";
            }
            else if (statusCode == 503)
            {
                mensajeError += mensajeError + "Servidor de Reportes no se encuentra activo";
            }
            else if (statusCode == 999)
            {
                mensajeError += mensajeError + exception;
            }
            else
            {
                mensajeError += mensajeError + "El servidor ha experimentado un error (" + statusCode + ") <br>" + exception;
            }
            //Response.StatusCode = statusCode;
            ViewBag.Mensaje = mensajeError;
            return View();
        }
    }
}
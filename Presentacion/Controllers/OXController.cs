using Beans;
using Negocio;
using Presentacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class OXController : BaseController
    {
        OX_Negocio oxNeg = new OX_Negocio();
        static string xlsPathDES = ConfigurationManager.AppSettings["xlsPathDES"];
        static string xlsPathPRO = ConfigurationManager.AppSettings["xlsPathPRO"];

        public ActionResult Transporte()
        {
            return View();
        }

        public JsonResult JSON_GetMovimiento(string cod_unidad_negocio, int ide_movimiento)
        {
            OX_MovimientoBean movimiento = oxNeg.fn_ox_get_movimiento("GET_MOVIMIENTO", cod_unidad_negocio, ide_movimiento);
            return Json(movimiento, JsonRequestBehavior.AllowGet);
        }
    }
}
using Beans;
using Microsoft.Office.Interop.PowerPoint;
using Negocio;
using Presentacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class TVController : BaseController
    {
        static string pptPathDES = @"\\svlimdpd0\APLICACION\MVC\com\";
        static string pptPathPRO = @"\\svdcxdpd0\APLICACION\MVC\com\";

        static string videoPathDES = @"\\svlimdpd0\APLICACION\TV\com\pages\";
        static string videoPathPRO = @"\\svdcxdpd0\APLICACION\TV\com\pages\";

        SEG_Negocio segNeg = new SEG_Negocio();

        public ActionResult CargaPPTX()
        {
            var cod_unidad_negocio = string.Empty;
            var cod_aplicacion = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var listUnidad = segNeg.fn_seg_listUnidad("Unidad", Usuario.Item1.cod_usuario, "TV", "", "") ?? new List<GEN_UnidadNegocioBean>();

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
        public ActionResult UploadPowerPoint(string cod_unidad_negocio)
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
                        string filePpt = string.Empty;
                        string filePptDBS = string.Empty;
                        string fileVideo = string.Empty;
                        string fileVideoDBS = string.Empty;

                        if (fileExtension != ".ppt" && fileExtension != ".pptx")
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
                        var nomenclaturaPPT = cod_unidad_negocio + "_Presentacion.pptx";
                        var nomenclaturaVideo = cod_unidad_negocio + "_Presentacion.mp4";

                        if (HttpContext.IsDebuggingEnabled)
                        {
                            filePpt = Path.Combine(@"M:\MVC\com\ppt", nomenclaturaPPT);
                            //filePptDBS = filePpt.Replace(@"M:\MVC\com\ppt", pptPathDES);
                            fileVideo = Path.Combine(@"M:\TV\com\pages\" + cod_unidad_negocio + @"\ppt", nomenclaturaVideo);
                            //fileVideoDBS = filePpt.Replace(@"M:\TV\com\pages\" + cod_unidad_negocio + @"\ppt", videoPathDES + cod_unidad_negocio + @"\ppt");
                        }
                        else
                        {
                            filePpt = Path.Combine(Server.MapPath("~/ppt"), nomenclaturaPPT);
                            //filePptDBS = filePpt.Replace(Server.MapPath("~/ppt"), pptPathPRO);
                            fileVideo = Path.Combine(videoPathPRO + cod_unidad_negocio + @"\ppt", nomenclaturaVideo);
                            //fileVideoDBS = filePpt.Replace(videoPathPRO + cod_unidad_negocio + @"\ppt", videoPathPRO + cod_unidad_negocio + @"\ppt");
                        }

                        fileContent.SaveAs(filePpt);
                        Microsoft.Office.Interop.PowerPoint.Application appPpt = new Microsoft.Office.Interop.PowerPoint.Application();
                        Microsoft.Office.Interop.PowerPoint.Presentation objTempPresentation = appPpt.Presentations.Open(FileName: filePpt, ReadOnly: Microsoft.Office.Core.MsoTriState.msoFalse, Untitled: Microsoft.Office.Core.MsoTriState.msoFalse, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                        objTempPresentation.CreateVideo(FileName: fileVideo, UseTimingsAndNarrations: false, DefaultSlideDuration: 2, VertResolution: 1080, FramesPerSecond: 60, Quality: 90);
                        while (objTempPresentation.CreateVideoStatus == PpMediaTaskStatus.ppMediaTaskStatusInProgress)
                        {
                            Thread.Sleep(100);
                        }

                        //Close PowerPoint
                        objTempPresentation.Close();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(objTempPresentation);
                        objTempPresentation = null;
                        appPpt.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(appPpt);
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(appPpt);
                        appPpt = null;

                        //force a garbage collection 
                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        foreach (var item in System.Diagnostics.Process.GetProcessesByName("powerpnt"))
                        {
                            item.Kill();
                        }

                        return Json(
                            new Response
                            {
                                Status = HttpStatusCode.OK,
                                Message = "SUCCESS: Carga exitosa."
                            },
                            JsonRequestBehavior.AllowGet);


                    }
                }
            }
            catch (Exception e)
            {
                return Json(
                    new Response
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "El archivo no se cargó. " + e.Message,
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
using Beans;
using Negocio;
using Presentacion.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class BALController : BaseController
    {
        BAL_Negocio balNeg = new BAL_Negocio();
        static string xlsPathDES = ConfigurationManager.AppSettings["xlsPathDES"];
        static string xlsPathPRO = ConfigurationManager.AppSettings["xlsPathPRO"];

        public ActionResult Codificacion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit_Codificacion(BAL_CodificacionBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                string cod_unidad_negocio = string.Empty;
                if(Session["cod_unidad_negocio"] != null)
                {
                    model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                }
                mensajeBean = balNeg.up_bal_cud_codificacion(model.accion, Usuario.Item1.cod_usuario,model);

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
        public JsonResult JSON_GetDropDownIndicador(string cod_unidad_negocio, string cod_balmet)
        {
            if (cod_unidad_negocio == null || cod_unidad_negocio == string.Empty)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var data = balNeg.fn_bal_sel_indicador("INDICADOR", cod_unidad_negocio, Usuario.Item1.cod_usuario) ?? new List<BAL_CodificacionBean>();
            var dataSelected = data.Where(a => a.cod_balmet == cod_balmet).Select(a => a.cod_indicador).AsEnumerable() ?? new HashSet<long>();
            var model = new AuxiliarEdit();
            model.DropDownList = data.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_indicador.ToString(),
                Text = x.nom_indicador,
                Selected = dataSelected.Any(c => c == x.cod_indicador),
                HtmlAttributes = new
                {
                    data_alias = x.cod_balmet
                }
            });
            return Json(model.DropDownList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Plantilla()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit_Plantilla(BAL_PlantillaBean model)
        {
            GEN_MensajeBean mensajeBean = null;
            if (ModelState.IsValid)
            {
                if (Session["cod_unidad_negocio"] != null)
                {
                    model.cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                }

                mensajeBean = balNeg.up_bal_cud_plantilla(model.accion, Usuario.Item1.cod_usuario, model);

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
        public JsonResult JSON_GetDropDownBalmet(string cod_unidad_negocio, string cod_balmet)
        {
            if (cod_unidad_negocio == null || cod_unidad_negocio == string.Empty)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }
            
            var data = balNeg.fn_bal_sel_balmet("SELECT_BALMET", cod_unidad_negocio, Usuario.Item1.cod_usuario) ?? new List<BAL_CodificacionBean>();
            var dataSelected = data.Where(a => a.cod_balmet == cod_balmet).Select(a => a.cod_balmet).AsEnumerable() ?? new HashSet<string>();
            var model = new AuxiliarEdit();
            model.DropDownList = data.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_balmet.ToString(),
                Text = x.nom_balmet,
                Selected = dataSelected.Any(c => c == x.cod_balmet),
                HtmlAttributes = new
                {
                    data_alias = x.cod_balmet
                }
            });
            return Json(model.DropDownList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetBalmet(string cod_unidad_negocio, string cod_balmet)
        {
            if (cod_unidad_negocio == null || cod_unidad_negocio == string.Empty)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }

            var data = balNeg.fn_bal_get_balmet("GET_BALMET", cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_balmet) ?? new BAL_CodificacionBean();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Carga()
        {
            string fec_informe = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.fecha = fec_informe;
            return View();
        }
        [HttpPost]
        public ActionResult UploadFileAjax(string accion, string fec_informe, string fec_informe_hasta)
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var fileContent = Request.Files[i];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string fileCliente = Path.GetFileName(fileContent.FileName);
                        string fileExtension = Path.GetExtension(fileContent.FileName);
                        string fileDestino = string.Empty;
                        string fileDestinoDBS = string.Empty;
                        int pid = 0;

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

                        var nomenclatura = "BAL_Car_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xlsx";
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
                        var mensajeError = string.Empty;
                        if (accion == "CARGA")
                        {
                            using (var fileStream = System.IO.File.Create(fileDestinoDBS))
                            {
                                stream.CopyTo(fileStream);
                            }
                        }

                        // Formateando excel a 2 decimales
                        
                        //Grabacion de temporal en sql
                        var cod_unidad_negocio = string.Empty;
                        if (Session["cod_unidad_negocio"] != null)
                        {
                            cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                        }

                        try
                        {
                            var excelApp = new Microsoft.Office.Interop.Excel.Application(); //CreateObject("Excel.Application")
                            excelApp.Application.Visible = false;
                            excelApp.DisplayAlerts = false;
                            var excelWorkbook = excelApp.Workbooks.Open(Filename: fileDestinoDBS, ReadOnly: false);
                            GetWindowThreadProcessId(excelApp.Hwnd, out pid);

                            try
                            {
                                excelWorkbook.Sheets["BD Balmet"].Range("F6:EO500").NumberFormat = "0.0000000000000000";
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
                            var archivoDestino = fileDestinoDBS.Replace(@"\", "/");

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
                        }

                        DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime dtfecInformeHasta = DateTime.ParseExact(fec_informe_hasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var execute = balNeg.up_bal_pro_cargaXLS(cod_unidad_negocio, Usuario.Item1.cod_usuario, fileDestinoDBS, fileDestino, "CARGA",0, dtfecInforme, dtfecInformeHasta);

                        if(execute.tipo == "ERROR")
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
                            execute = balNeg.up_bal_pro_cargaXLS(cod_unidad_negocio, Usuario.Item1.cod_usuario, fileDestinoDBS, fileDestino, "START", 0, dtfecInforme, dtfecInformeHasta);

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
                                execute = balNeg.up_bal_pro_cargaXLS(cod_unidad_negocio, Usuario.Item1.cod_usuario, fileDestinoDBS, fileDestino, "DETALLE",execute.Id, dtfecInforme, dtfecInformeHasta);

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
        [HttpPost]
        public ActionResult UpdateMarca(string accion, Int64? ide_carga, string archivo_fisico, string archivo_logico, string fec_informe, List<BAL_ProduccionDiaBean> dataListFromTable)
        {
            try
            {
                var cod_unidad_negocio = string.Empty;
                if (Session["cod_unidad_negocio"] != null)
                {
                    cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
                }

                DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var execute = balNeg.fn_bal_pro_marca(cod_unidad_negocio, Usuario.Item1.cod_usuario, archivo_fisico, archivo_logico, accion, ide_carga.Value, dtfecInforme, dataListFromTable);

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
                            Message = "OK"
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
        public ActionResult ProcesarFileAjax(string accion, Int64? ide_carga,string nomenclatura, string fec_informe, string fec_informe_hasta)
        {
            try
            {
                var cod_unidad_negocio = string.Empty;
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

                DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtfecInformeHasta = DateTime.ParseExact(fec_informe_hasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var execute = balNeg.up_bal_pro_cargaXLS(cod_unidad_negocio, Usuario.Item1.cod_usuario, fileDestinoDBS, fileDestino, accion, ide_carga.Value, dtfecInforme, dtfecInformeHasta);

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
        public ActionResult ProduccionDia()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }
            var data = balNeg.fn_bal_sel_headerRep(cod_unidad_negocio, Usuario.Item1.cod_usuario, "D", "FECHA", null).First();
            string fec_informe = data.fec_informe.Value.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture);
            ViewBag.fecha = fec_informe;
            return View();
        }
        public ActionResult RepProduccionDia()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }
            var data = balNeg.fn_bal_sel_headerRep(cod_unidad_negocio, Usuario.Item1.cod_usuario, "D", "FECHA", null).First();
            string fec_informe = data.fec_informe.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.fecha = fec_informe;
            return View();
        }
        public JsonResult Get_ProduccionDia(string cod_unidad_negocio, string cod_frecuencia, string fec_informe)
        {
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }
            DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Tuple<List<BAL_ProduccionDiaBean>, GEN_MensajeBean> data = balNeg.fn_bal_sel_repProduccion(cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, "SELECT", dtfecInforme);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_ProduccionHeader(string cod_unidad_negocio, string cod_frecuencia, string fec_informe)
        {
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }
            DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<BAL_Handsontable> data = balNeg.fn_bal_sel_headerRep(cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, "HEADER", dtfecInforme);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Save_Produccion(string cod_unidad_negocio, string cod_frecuencia, string fec_informe, List<BAL_ProduccionDiaBean> dataListFromTable) //List<string[]> dataListFromTable)
        {
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }
            DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var mensajeBean = balNeg.up_bal_pro_reporte(cod_unidad_negocio, Usuario.Item1.cod_usuario, cod_frecuencia, "START", dtfecInforme, dataListFromTable);
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
        public ActionResult RunXls(string cod_unidad_negocio, string cod_frecuencia, string fec_informe)
        {
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }
            var mensajeError = string.Empty;
            var tablero = string.Empty;
            var codFrecuencia = cod_frecuencia;
            var codUnidadNegocio = cod_unidad_negocio;
            var fecInforme = fec_informe;

            var resultado = string.Empty;
            var mensaje = string.Empty;
            var archivoDestino = string.Empty;
            int pid = 0;

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
            excelFuente = Server.MapPath("~/xlso/KPI_PI_ProduccionDia.xlsm");
            DateTime dtfecInforme = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //Cambiamos el nombre del archivo Destino
            fileDestino = "ProduccionDia" + "_" + codUnidadNegocio + "_" + dtfecInforme.ToString("yyMMdd") + DateTime.Now.ToString("hhmmsss") + ".xlsx";
            excelDestino = Server.MapPath("~/xlsd/" + fileDestino);

            try
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Application.Visible = false;
                excelApp.DisplayAlerts = false;
                var excelWorkbook = excelApp.Workbooks.Open(Filename: excelFuente, ReadOnly: true);
                GetWindowThreadProcessId(excelApp.Hwnd, out pid);

                excelWorkbook.Sheets["Registro"].Range("A5").Value = codUnidadNegocio;
                excelWorkbook.Sheets["Registro"].Range("B5").Value = codFrecuencia;
                
                var fecExcel = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                excelWorkbook.Sheets["Registro"].Range("C5").Value = fecExcel;
                excelWorkbook.Sheets["Registro"].Range("E5").Value = excelDestino;

                try
                {
                    mensajeError = excelApp.Run("BtnRegistro_Carga");
                }
                catch (Exception)
                {
                    mensajeError = "ERROR: En la ejecución de Excel";
                    try
                    {
                        //procesoExcel.Kill();
                        Process.GetProcessById(pid).Kill();
                    }
                    catch (Exception)
                    {
                    }
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
                    //procesoExcel.Kill();
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
        public ActionResult RepCampania()
        {
            var cod_unidad_negocio = string.Empty;
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }
            var model = new AuxiliarEdit();
            var listCampania = balNeg.fn_bal_sel_campanias(cod_unidad_negocio,Usuario.Item1.cod_usuario, "SELECT","",null) ?? new List<GEN_AuxiliarBean>();
            model.DropDownList = listCampania.Select(x => new ExtendedSelectListItem
            {
                Value = x.codigo,
                Text = x.descripcion,
                Selected = false,
                HtmlAttributes = new
                {
                    data_alias = x.codigo
                }
            });

            model.fec_informe = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return View(model);
        }
        public ActionResult RunXlsCampania(string cod_unidad_negocio, string fec_informe, string campania, int batch_desde, int batch_hasta)
        {
            if (Session["cod_unidad_negocio"] != null)
            {
                cod_unidad_negocio = Session["cod_unidad_negocio"].ToString();
            }
            var mensajeError = string.Empty;
            var tablero = string.Empty;
            var codUnidadNegocio = cod_unidad_negocio;
            var fecInforme = fec_informe;
            var batchDesde = batch_desde;
            var batchHasta = batch_hasta;

            var resultado = string.Empty;
            var mensaje = string.Empty;
            var archivoDestino = string.Empty;
            int pid = 0;

            //Identificar el archivo a descargar, incluyendo la ruta de acceso.
            var excelFuente = string.Empty;
            var excelDestino = string.Empty;
            var fileDestino = string.Empty;
            excelFuente = Server.MapPath("~/xlso/KPI_PI_Campañas.xlsm");

            DateTime? dtfecInforme = null;
            if (!(fec_informe == "" || fec_informe == string.Empty))
            {
                dtfecInforme = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //Cambiamos el nombre del archivo Destino
                fileDestino = "Campaña" + "_" + campania + "_" + codUnidadNegocio + "_" + dtfecInforme.Value.ToString("yyMMdd") + DateTime.Now.ToString("hhmmsss") + ".xlsx";

            }
            else
            {
                fileDestino = "Campaña" + "_" + campania + "_" + codUnidadNegocio + "_" + batchDesde + "_" + batch_hasta + "_" + DateTime.Now.ToString("hhmmsss") + ".xlsx";
            }
                
            excelDestino = Server.MapPath("~/xlsd/" + fileDestino);

            try
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Application.Visible = false;
                excelApp.DisplayAlerts = false;

                var excelWorkbook = excelApp.Workbooks.Open(Filename: excelFuente, ReadOnly: true);
                GetWindowThreadProcessId(excelApp.Hwnd, out pid);

                excelWorkbook.Sheets["Registro"].Range("A2").Value = codUnidadNegocio;
                excelWorkbook.Sheets["Registro"].Range("B2").Value = campania;

                var fecExcel = DateTime.ParseExact(fec_informe, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                excelWorkbook.Sheets["Registro"].Range("C2").Value = fecExcel;
                excelWorkbook.Sheets["Registro"].Range("D2").Value = excelDestino;
                excelWorkbook.Sheets["Registro"].Range("E2").Value = batchDesde;
                excelWorkbook.Sheets["Registro"].Range("F2").Value = batchHasta;

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
                    //procesoExcel.Kill();
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
    }
}
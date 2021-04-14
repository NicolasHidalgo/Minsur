using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Presentacion.ViewModels;
using Microsoft.Owin.Security;
using Presentacion.Helpers;
using System.Net;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace Presentacion.Controllers
{
    public class LoginController : Controller
    {
        static string dominio = ConfigurationManager.AppSettings["dominio"];
        // for debbuging app
        static string siteKey_localhost = ConfigurationManager.AppSettings["GoogleSiteKey1"];
        static string secretKey_localhost = ConfigurationManager.AppSettings["GoogleSecretKey1"];
        // for connecting outside minsur network
        static string siteKey_mineriabreca = ConfigurationManager.AppSettings["GoogleSiteKey2"];
        static string secretKey_mineriabreca = ConfigurationManager.AppSettings["GoogleSecretKey2"];
        // for connecting inside minsur network (produccion)
        static string siteKey_svdcxdpd0 = ConfigurationManager.AppSettings["GoogleSiteKey3"];
        static string secretKey_svdcxdpd0 = ConfigurationManager.AppSettings["GoogleSecretKey3"];
        // for connecting inside minsur network (desarrollo)
        static string siteKey_svlimdpd0 = ConfigurationManager.AppSettings["GoogleSiteKey4"];
        static string secretKey_svlimdpd0 = ConfigurationManager.AppSettings["GoogleSecretKey4"];

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var fullUrl = Request.Url.ToString();
            var model = new LoginViewModel();

            if (fullUrl.Contains("localhost"))
            {
                model.GoogleSiteKey = siteKey_localhost; 
                model.GoogleSecretKey = secretKey_localhost;
            }
            if (fullUrl.Contains("mineriabreca"))
            {
                model.GoogleSiteKey = siteKey_mineriabreca;
                model.GoogleSecretKey = secretKey_mineriabreca;
            }
            if (fullUrl.Contains("svdcxdpd0"))
            {
                model.GoogleSiteKey = siteKey_svdcxdpd0;
                model.GoogleSecretKey = secretKey_svdcxdpd0;
            }
            if (fullUrl.Contains("svlimdpd0"))
            {
                model.GoogleSiteKey = siteKey_svlimdpd0;
                model.GoogleSecretKey = secretKey_svlimdpd0;
            }
            model.FullUrl = fullUrl;

            model.IsPersistent = true;
            ViewBag.ReturnUrl = returnUrl;

            if (model.GoogleSiteKey == "" || model.GoogleSiteKey == null)
            {
                ModelState.AddModelError("", "Inválido acceso al sistema.");
                ModelState.AddModelError("", "Ingrese usando el dominio correcto.");
            }
          
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginViewModel model, string returnUrl)
        {
            var fullUrl = Request.Url.ToString();
            if (fullUrl.Contains("localhost"))
            {
                model.GoogleSiteKey = siteKey_localhost;
                model.GoogleSecretKey = secretKey_localhost;
            }
            if (fullUrl.Contains("mineriabreca"))
            {
                model.GoogleSiteKey = siteKey_mineriabreca;
                model.GoogleSecretKey = secretKey_mineriabreca;
            }
            if (fullUrl.Contains("svdcxdpd0"))
            {
                model.GoogleSiteKey = siteKey_svdcxdpd0;
                model.GoogleSecretKey = secretKey_svdcxdpd0;
            }
            if (fullUrl.Contains("svlimdpd0"))
            {
                model.GoogleSiteKey = siteKey_svlimdpd0;
                model.GoogleSecretKey = secretKey_svlimdpd0;
            }

            if (model.GoogleSiteKey == "" || model.GoogleSiteKey == null)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Inválido acceso al sistema");
                ModelState.AddModelError("", "Ingrese usando el dominio correcto.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //CAPCHA
            var response = Request["g-recaptcha-response"];
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", model.GoogleSecretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            //ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";
            if (!status)
            {
                ModelState.AddModelError("", "Google reCaptcha validation failed");
                return View(model);
            }
            
            // usually this will be injected via DI. but creating this manually now for brevity
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);

            model.Username =  model.Username.ToLower();
            if (model.Username.Contains(dominio))
            {
                model.Username = model.Username.Replace(dominio, "");
            }

            if (model.Username.Contains("dg\\"))
            {
                //Auth BD PENDIENTE DE INVESTIGAR
                // we are in!
                Session["LoginName"] = model.Username;
                return RedirectToLocal(returnUrl);
            }
            else
            {
                var authenticationResult = authService.SignIn(model.Username, model.Password, model.IsPersistent);

                if (authenticationResult.IsSuccess)
                {
                    // we are in!
                    Session["LoginName"] = model.Username;
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError("", authenticationResult.ErrorMessage);
                return View(model);
            }

        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public virtual ActionResult Logoff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}
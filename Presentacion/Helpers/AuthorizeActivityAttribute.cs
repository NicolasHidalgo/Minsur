using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Helpers
{
    public class AuthorizeActivityAttribute : AuthorizeAttribute
    {
        //private DGMINEntities db = new DGMINEntities();

        public AuthorizeActivityAttribute(string Activity)
        {

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }
    }
}
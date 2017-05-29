using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace CommonAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        internal void SetAllowOrigin(string url)
        {
            //get the allow origin url from appsetting
            string allowOrigin = System.Configuration.ConfigurationManager.AppSettings["AllowWebApiCallURL"];

            if (allowOrigin.Split(';').Select(s=>s.Trim().ToLower()).Contains(url.ToLower()))
            {
                HttpContext.Current.Response.Headers.Remove("Access-Control-Allow-Origin");
                //http://domain.com or * to allow all caller
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", url.ToLower());
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            SetAllowOrigin(HttpContext.Current.Request.Headers["Origin"] == null ?
                string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority)
                : HttpContext.Current.Request.Headers["Origin"]);
            //HttpContext.Current.Response.Headers.Remove("Access-Control-Allow-Origin");
            // HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, PUT, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, content-type, Accept, requestverificationtoken");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }

        }
    }
}

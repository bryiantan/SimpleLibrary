using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WarningBanner.Attribute
{
    public class RequireAcceptPolicy : AuthorizeAttribute, IAuthorizationFilter
    {
        internal static bool PageToSkipPolicyNotification(HttpContext ctx)
        {
            string[] pagesToExclude = { "/home/testwarningpage", "/home/warningpage", "/home/warningpageng" };

            string pageToCheck = ctx.Request.Path.ToString().ToLower().TrimEnd('/');

            return pagesToExclude.Contains(pageToCheck) ? true : false;
        }

        internal static bool PageToShowPolicyNotification(HttpContext ctx)
        {
            string[] pagesToExclude = { "/home/testwarningpage" };

            string pageToCheck = ctx.Request.Path.ToString().ToLower().TrimEnd('/');

            return pagesToExclude.Contains(pageToCheck) ? true : false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //here are the section for the warning page
            //don't prompt if the action is acceptpolicy, and pages to exclude and SessionOKClick != null (user already accepted policy)
            if (!filterContext.ActionDescriptor.ActionName.ToLower().Contains("acceptpolicy") && !PageToSkipPolicyNotification(HttpContext.Current) &&
                               HttpContext.Current.Session["SessionOKClick"] == null)
            {
                //track the request url, include the query string
                HttpContext.Current.Session["SessionReturnUrl"] = filterContext.HttpContext.Request.Url.PathAndQuery;

                //redirect to policy page
                if (System.Configuration.ConfigurationManager.AppSettings["WaningBannerAngular"] == "1")
                {
                    filterContext.Result = new RedirectResult("~/home/WarningPageNg");
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/home/WarningPage");
                }
                
            }
        }
    }
}
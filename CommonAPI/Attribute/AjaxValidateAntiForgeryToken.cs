using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Filters;

namespace CommonApp.Attribute
{
    //http://blog.novanet.no/anti-forgery-tokens-using-mvc-web-api-and-angularjs/
    public class AjaxValidateAntiForgeryToken : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string cookieToken = String.Empty;
            string formToken = String.Empty;
            IEnumerable<string> tokenHeaders;
            if (actionContext.Request.Headers.TryGetValues("RequestVerificationToken", out tokenHeaders))
                if (tokenHeaders != null)
                {
                    //somehow the : replaced by , in the hidden textbox
                    string[] tokens = tokenHeaders.First().Replace(",", ":").Split(':');
                    // string[] tokens = tokenHeaders.Replace(",", ":").Split(':');
                    if (tokens.Length == 2)
                    {
                        cookieToken = tokens[0].Trim();
                        formToken = tokens[1].Trim();
                    }
                }
            System.Web.Helpers.AntiForgery.Validate(cookieToken, formToken);

            base.OnActionExecuting(actionContext);
        }

    }
}
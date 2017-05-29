using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WarningBanner.Attribute;

namespace WarningBanner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestModalDialog()
        {
            return View();
        }

        public ActionResult TestModalDialogNg()
        {
            ViewBag.Something = "Text from ViewBag";
            return View();
        }

        public ActionResult TestWarningPage()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult AcceptPolicy(string rURL)
        {
            Session["SessionOKClick"] = "1";

            string decodedUrl = string.Empty;
            //return url to redirect after user accepted the policy
            if (Session["SessionReturnUrl"] != null)
            {
                decodedUrl = Server.UrlDecode(Session["SessionReturnUrl"].ToString());
            }

            if (Url.IsLocalUrl(decodedUrl))
            {
                return Json(decodedUrl);
            }
            else
            {
                return Json(Url.Action("Index", "Home"));
            }
        }

        internal void SetWarningBanner()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();


            //this should come from the database, web api, or you can hardcode it on the view

            //web api call
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:47503");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("api/warningbanner/1").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    JObject json = JObject.Parse(responseString);

                    ViewBag.SystemWarningMessage = json["Content"].ToString();
                }
            }

//            ViewBag.SystemWarningMessage = "<p style='text-align: CENTER'><span style='font-size: 10pt'>**WARNING**WARNING**WARNING**</span></p>" +
//"<p><span style = 'font-size: 10pt' > This is a (</span><span style = 'font-size: 10pt' ><span style = 'text-decoration: underline' > XYZ </span>) computer system. (</span><span style = 'font-size: 10pt' ><span style = 'text-decoration: underline' > XYZ </span>) computer" +
//                                  " systems are provided for the processing of Official abc" +
//                                  " gov information only.All data contained on" +
//                                  " (</span><span style = 'font-size: 10pt' ><span style = 'text-decoration: underline' > XYZ </span>) computer systems is owned by the(</span><span style = 'font-size: 10pt' ><span style = 'text-decoration: underline' > XYZ </span>)" +
//                                  " </span><span style = 'font-size: 10pt' ><em> may be monitored, intercepted, recorded, read, copied," +
//                                  " or captured in any manner and disclosed in any manner," +
//                                  " </ em > by authorized personnel.THERE IS NO RIGHT OF" +
//                                   " PRIVACY IN THIS SYSTEM.System personnel may" +
//                                  " give to law enforcement officials any potential evidence" +
//                                  " of crime found on(</span><span style = 'font-size: 10pt' ><span style = 'text-decoration: underline'> XYZ </span>) computer systems. </span><span style = 'font-size: 10pt' ><em><span style = 'text-decoration: underline'> USE OF" +
//                                 "  THIS SYSTEM BY ANY USER, AUTHORIZED OR" +
//                                  " UNAUTHORIZED, CONSTITUTES CONSENT TO THIS" +
//                                  " MONITORING, INTERCEPTION, RECORDING," +
//                                  " READING, COPYING, OR CAPTURING and" +
//                                   " DISCLOSURE.</span></ em ></span><span style = 'font-size: 10pt' ><span style = 'text-decoration: underline' ></span></span></p>" +
//                                   "<p style = 'text-align: CENTER' ><span style = 'font-size: 10pt' > **WARNING * *WARNING * *WARNING * *</span></p> ";

        }

        public ActionResult WarningPage()
        {
            if (Session["SessionOKClick"] != null)
            {
                return RedirectToAction("Index");
            }

            SetWarningBanner();

            return View();
        }

        public ActionResult WarningPageNg()
        {
            if (Session["SessionOKClick"] != null)
            {
                return RedirectToAction("Index");
            }

            SetWarningBanner();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
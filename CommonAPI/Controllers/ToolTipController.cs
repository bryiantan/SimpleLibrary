using CommonAPI.Attribute;
using CommonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommonAPI.Controllers
{
    [RoutePrefix("api/tooltip")]
    public class ToolTipController : BaseApiController
    {
        //this is just a sample, in reality, the data should be originated from a repository
        IList<ToolTip> toolTips = new List<ToolTip>
        {
            new ToolTip { Id = 1, Key ="registerEmail", Title = "Email", Description ="Enter your Email Address", LastUpdatedDate = DateTime.Now },
            new ToolTip { Id = 2, Key ="password", Title = "Password", Description ="Password must contains Uppercase characters (A-Z), \r\n Lowercase characters (a-z)", LastUpdatedDate = DateTime.Now},
            new ToolTip { Id = 3, Key ="confirmpassword", Title = "Confirm Password", Description ="Enter Password again ....", LastUpdatedDate = DateTime.Now},
            new ToolTip { Id = 4, Key ="firstName", Title = "First Name", Description ="Enter first name", LastUpdatedDate = DateTime.Now}
        };

        //[Route("{id}")]
        //public IHttpActionResult Get(int id)
        //{
        //    var toolTip = toolTips.FirstOrDefault((p) => p.Id == id);
        //    if (toolTip == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(toolTip);
        //}

        [HttpGet]
        [Route("")]
        public IEnumerable<ToolTip> Get()
        {
            return toolTips;
        }

        [Route("{key}")]
        public IHttpActionResult Get(string key)
        {
            var toolTip = toolTips.FirstOrDefault((p) => p.Key.ToLower() == key.ToLower());
            if (toolTip == null)
            {
                return NotFound();
            }
            return Ok(toolTip);
        }

        /// <summary>
        /// Get content using POST method
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWithPost")]
        [AjaxValidateAntiForgeryToken]
        public IHttpActionResult GetWithPost(Dummy dummy)
        {
            var toolTip = toolTips.FirstOrDefault((p) => p.Key == dummy.Key);
            if (toolTip == null)
            {
                return NotFound();
            }
            return Ok(toolTip);
        }

    }
}

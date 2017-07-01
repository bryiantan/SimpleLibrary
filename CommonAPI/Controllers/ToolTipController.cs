using CommonApp.Attribute;
using CommonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommonApp.Controllers
{
    [RoutePrefix("api/tooltip")]
    public class ToolTipController : BaseApiController
    {
        //this is just a sample, in reality, the data should be originated from a repository
        IList<ToolTip> toolTips = new List<ToolTip>
        {
            new ToolTip { Id = 1, Key ="registerEmail", Title = "Email", Description ="Enter your Email Address", LastUpdatedDate = DateTime.Now },
            new ToolTip { Id = 2, Key ="registerPassword", Title = "Password policy",
                Description ="Password must contains atleast 1 Uppercase characters (A-Z) <br/>Password must contains any Lowercase characters (a-z)<br/>Password must contains 1 number of Base 10 digits (0 through 9)<br/>Password minimum length is 12", LastUpdatedDate = DateTime.Now},
            new ToolTip { Id = 3, Key ="registerConfirmpassword", Title = "Confirm Password", Description ="Enter Password again ....", LastUpdatedDate = DateTime.Now},
            new ToolTip { Id = 4, Key ="registerFirstName", Title = "First Name", Description ="Display tooltip from HTML element", LastUpdatedDate = DateTime.Now},
            new ToolTip { Id = 4, Key ="registerSubmit", Title = "Submit", Description ="Click to Submit the registration", LastUpdatedDate = DateTime.Now},
             new ToolTip { Id = 4, Key ="registerDummyParagraph", Title = "Dummy paragraph", Description ="display tooltip within paragraph element using Bootstrap popover", LastUpdatedDate = DateTime.Now}
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


        /// <summary>
        /// Get content using POST method
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWithPostNoToken")]
        public IHttpActionResult GetWithPostNoToken(Dummy dummy)
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

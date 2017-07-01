using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommonApp.Controllers
{
    public class BaseApiController : ApiController
    {
        public struct Dummy
        {
           // public int Id { get; set; }
            public string Key { get; set; }
            public string Other { get; set; }
        }

    }
}

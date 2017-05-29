using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommonAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        public class Dummy
        {
           // public int Id { get; set; }
            public string Key { get; set; }
        }

    }
}

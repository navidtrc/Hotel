using InternshipHMSWeb.JsonWebTokenConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InternshipHMSWeb.Controllers
{
    public class JwtTestApiController : ApiController
    {
        [HttpPost]
        [JwtAuthorize]
        public IHttpActionResult FindName()
        {
            return Ok();
        }
    }
}

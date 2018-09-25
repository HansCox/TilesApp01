using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Drawing;
using TilesApp01.Models;

namespace TilesApp01.Controllers
{
    public class TilesController : ApiController
    {
        public HttpResponseMessage Post(Triangle t)
        {
            Triangle triangle = Triangle.GetTriangle(t);
            var response = Request.CreateResponse<Triangle>(
                   System.Net.HttpStatusCode.OK, triangle);
            return response;
        }

        
    }
}

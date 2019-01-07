using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VagbidragGranskning.Models;

namespace VagbidragGranskning.Controllers
{
    public class RaindanceController : ApiController
    {
        // GET: api/Raindance
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.NotImplemented);
        }

        // GET: api/Raindance/5
        public HttpResponseMessage Get(int id)
        {
            return new HttpResponseMessage(HttpStatusCode.NotImplemented);
        }

        // POST: api/Raindance
        public HttpResponseMessage Post([FromBody]ReportQuery value)
        {
            HttpResponseMessage response = new HttpResponseMessage();




            return response;
        }

        // PUT: api/Raindance/5
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            return new HttpResponseMessage(HttpStatusCode.NotImplemented);
        }

        // DELETE: api/Raindance/5
        public HttpResponseMessage Delete(int id)
        {
            return new HttpResponseMessage(HttpStatusCode.NotImplemented);
        }
    }
}

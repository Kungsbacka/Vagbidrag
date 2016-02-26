using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KBA.TE.Models;

namespace VagbidragGranskning.Controllers
{
    public class AnsokanController : ApiController
    {
        // GET: api/Ansokan
        public IEnumerable<Vagforening> Get()
        {
            VFXMLPopulator xmlpop = new VFXMLPopulator();
            return xmlpop.list();
        }

        // GET: api/Ansokan/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Ansokan
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Ansokan/5
        // Moves to "Approved" folder.
        public string Put(string file)
        {
            try
            {
                VFXMLPopulator.approve(file);
            } catch (System.IO.FileNotFoundException e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return "200 OK";
        }

        // DELETE: api/Ansokan/5
        // Moves to "Denied" folder
        public string Delete(string file)
        {
            try
            {
                VFXMLPopulator.deny(file);
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return "200 OK";
        }
    }
}

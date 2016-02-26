using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KBA.TE.Models;

namespace VagbidragGranskning.Controllers
{
    public class VagforeningController : ApiController
    {
        // GET: api/Vagforening
        public IEnumerable<Vagforening> Get()
        {
            VFSQLPopulator pop = new VFSQLPopulator();
            return pop.list();
        }

        // GET: api/Vagforening/5
        public Vagforening Get(string id)
        {
            VFSQLPopulator pop = new VFSQLPopulator();
            return pop.getById(id);
        }

        // POST: api/Vagforening
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Vagforening/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Vagforening/5
        public void Delete(int id)
        {
        }
    }
}

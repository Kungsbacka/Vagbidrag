using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Configuration;
using System.Web;
using System.Text;

namespace VagbidragGranskning.Controllers
{
    public class PDFController : ApiController
    {
        public static string pdfDirSetting = "KBA:pdfdir";

        // GET: api/PDF
        public IEnumerable<string> Get()
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotImplemented));
        }

        // GET: api/PDF/5
        public HttpResponseMessage Get(string path, string fileName)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            string pdfDir = ConfigurationManager.AppSettings[pdfDirSetting];
            string filePath = pdfDir + "\\" + path + "\\" + fileName;
            if(File.Exists(filePath))
            {
                result.Content = new StreamContent(File.OpenRead(filePath));
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MimeMapping.GetMimeMapping(filePath));
            } else
            {
                result.Content = new StringContent("404 " + filePath, Encoding.UTF8, "text/plain");
            }
            return result;
        }

        // POST: api/PDF
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PDF/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PDF/5
        public void Delete(int id)
        {
        }
    }
}

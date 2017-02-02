using KBA.TE.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;
using KBA.TE.Models;

namespace VagbidragGranskning.Controllers
{
    public class ExcelController : ApiController
    {
        // GET: api/Excel
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Excel/5
        public HttpResponseMessage Get(int id)
        {     

            return createExcel();
        }

        // POST: api/Excel
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Excel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Excel/5
        public void Delete(int id)
        {
        }

        private void TestDb()
        {
            //Använd VFSQLPopulator
            VFSQLPopulator db = new VFSQLPopulator();


        }

        private HttpResponseMessage createExcel()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            MemoryStream ms = new MemoryStream();
            response.Content = new StreamContent(ms);
            //Skapa ett tillfälligt excel
            using (ExcelPackage doc = new ExcelPackage(ms))
            {
                //Create the worksheet
                ExcelWorksheet workSheet = doc.Workbook.Worksheets.Add("Vagbidrag");
                
                //Headers
                /*workSheet.Cells["A1"].Value = "nr";
                workSheet.Cells["B1"].Value = "vaghallare";
                workSheet.Cells["C1"].Value = "bank_post";
                workSheet.Cells["D1"].Value = "orgnr";
                workSheet.Cells["E1"].Value = "total_vaglangd";
                workSheet.Cells["F1"].Value = "statligt_bidrag";
                workSheet.Cells["G1"].Value = "statligt_bidragsgrundande";
                workSheet.Cells["H1"].Value = "vaglangd_gc";
                workSheet.Cells["A1:H1"].Style.Font.Bold = true;*/

                VagbidragSQLPopulator bidragPop = new VagbidragSQLPopulator();
                //Load the datatable into the sheet, starting from cell A1.
                workSheet.Cells["A1"].LoadFromDataTable(bidragPop.getRapportTable(), true);
                
                //Format
                workSheet.Cells.AutoFitColumns();
                workSheet.Row(1).Style.Font.Bold = true;

                string filePath = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.Content.Headers.ContentType =  new System.Net.Http.Headers.MediaTypeHeaderValue(MimeMapping.GetMimeMapping(filePath));
                response.Content.Headers.ContentDisposition =  new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "Vagbidrag.xlsx";

                doc.Save();
                ms.Seek(0,0);
                   
                
            }
            return response;
        }
    }
}

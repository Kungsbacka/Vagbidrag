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
using System.Data;

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
                DataTable table = bidragPop.getRapportTable();
                workSheet.Cells["A1"].LoadFromDataTable(table, true);
                
                
                
                //I sista kolumnen lägg in formel
                int columns = table.Columns.Count;
                int rows = table.Rows.Count;

                workSheet.Cells[1,columns+1].Value = "Kommunalt bidrag";

                //Format
                workSheet.Cells.AutoFitColumns();
                workSheet.Row(1).Style.Font.Bold = true;

                const string vagBidrag = "11.0";//Excel vill ha . inte ,
                const string cykelBidrag = "5.5";
                
                for (int i = 2; i <= rows; i++)
                {
                    //Formel
                    //"OM(J2 - I2 > H2 * 11; G2 * 11; J2 - I2 + (11 * (G2 - H2))) +K2 * 5,5";
                    //total_vaglangd = G
                    //statlig_vaglangd = H
                    //statligt_bidrag = I
                    //statligt_bidragsgrundande = J
                    //vaglangd_gc = K
                    //string test = cykelBidrag.ToString();
                    string row = i.ToString();
                    string formel1 = "J" + row + "-I" + row;
                    string formel2 = "H" + row + " * " + vagBidrag;
                    string formel3 = "G"+row+"*"+vagBidrag;
                    string formel4 = "J"+row+ "-I"+row+ "+(" + vagBidrag + " * (G" +row 
                        + " - H" + row + "))";
                    string formel5 = "K" + row + " * " + cykelBidrag;
                    //workSheet.Cells[i, columns + 1].Formula = "OM("+formel1+";"+formel2+";"+formel3+")"+formel4;
                    //workSheet.Cells[i, columns + 1].Formula = "OM(2 > 1, 3,4)+5";
                    workSheet.Cells[i, columns + 1].Formula = "IF("+formel1+" > "+formel2+", "+formel3+","+formel4+")+"+formel5;
                    workSheet.Cells[i, columns + 2].Formula = formel1;
                    workSheet.Cells[i, columns + 3].Formula = formel2;
                    workSheet.Cells[i, columns + 4].Formula = formel3;
                    workSheet.Cells[i, columns + 5].Formula = formel4;
                    workSheet.Cells[i, columns + 6].Formula = formel5;
                }

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

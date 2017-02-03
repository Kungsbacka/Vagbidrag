using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace KBA.TE.Models
{
    public class VagbidragSQLPopulator
    {

        public IEnumerable<Vagforening> list()
        {
            List<Vagforening> result = new List<Vagforening>();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["kbakarta"].ConnectionString;
                connection.Open();
                command.Connection = connection;
                DataTable resultTable = new DataTable();
                command.CommandText = "SELECT * FROM vagforeningar_rapport";
                resultTable.Load(command.ExecuteReader());
                connection.Close();
                foreach (DataRow row in resultTable.Rows)
                {
                    Vagforening vf = load(row);
                    result.Add(vf);
                }
            }
            return result;
        }
        public DataTable getRapportTable()
        {
            DataTable result = new DataTable();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["kbakarta"].ConnectionString;
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM vagforeningar_rapport";
                //command.CommandText = "SELECT nr, vaghallare, bank_post, orgnr, total_vaglangd, statlig_vaglangd, statligt_bidrag, statligt_bidragsgrundande, vaglangd_gc FROM vagforeningar_rapport";
                result.Load(command.ExecuteReader());
                connection.Close();
            }
            return result;
        }
        private Vagforening load(DataRow row)
        {
            Vagforening result = new Vagforening();

            
            result.nr = row["nr"]?.ToString();
            

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Text.RegularExpressions;


namespace KBA.TE.Models
{
    public class VFSQLPopulator
    {


        public IEnumerable<Vagforening> list()
        {
            List<Vagforening> result = new List<Vagforening>();

            using (NpgsqlConnection conn = new NpgsqlConnection())
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["kbakarta"].ConnectionString;
                conn.Open();
                cmd.Connection = conn;
                DataTable resultTable = new DataTable();
                cmd.CommandText = "SELECT vf1.id, vf1.nr, '' orgnr, vf1.typ, vf1.vaghallare, vf1.ordforande, vf1.gatuadress, vf1.postadress, vf1.postnummer, vf1.telefon_he, vf1.telefon_mo, vf1.fd_k_nr, vf1.bank__post, vf1.fnr, vf1.\"löpnr\", vf1.belag, vf1.nr1, vf1.url, vf1.link, vf1.leverantors_id, vf1.bidragsberattigad_vaglangd, vf1.statligt_vagnr, vf1.ordf_epost, vf1.kassor_namn, vf1.kassor_gatuadress, vf1.kassor_postnummer, vf1.kassor_ort, vf1.kassor_telefon, vf1.kassor_epost, vf1.antal_medlemmar, vf1.antal_permanentboende, vf1.ga_beteckning, vf1.statligt_bidrag FROM vagforeningar vf1";
                resultTable.Load(cmd.ExecuteReader());
                conn.Close();
                foreach (DataRow row in resultTable.Rows)
                {
                    Vagforening vf = load(row);
                    result.Add(vf);
                }
            }
            return result;
        }

        public Vagforening getById(string nr)
        {
            Vagforening result = null;
            using (NpgsqlConnection conn = new NpgsqlConnection())
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["kbakarta"].ConnectionString;
                conn.Open();
                cmd.Connection = conn;
                DataTable resultTable = new DataTable();
                cmd.CommandText = "SELECT vf1.id, vf1.nr, '' orgnr, vf1.typ, vf1.vaghallare, vf1.ordforande, vf1.gatuadress, vf1.postadress, vf1.postnummer, vf1.telefon_he, vf1.telefon_mo, vf1.fd_k_nr, vf1.bank__post, vf1.fnr, vf1.\"löpnr\", vf1.belag, vf1.nr1, vf1.url, vf1.link, vf1.leverantors_id, vf1.bidragsberattigad_vaglangd, vf1.statligt_vagnr, vf1.ordf_epost, vf1.kassor_namn, vf1.kassor_gatuadress, vf1.kassor_postnummer, vf1.kassor_ort, vf1.kassor_telefon, vf1.kassor_epost, vf1.antal_medlemmar, vf1.antal_permanentboende, vf1.ga_beteckning, vf1.statligt_bidrag FROM vagforeningar vf1 WHERE nr = @nr AND vf1.typ NOT LIKE '%nskild GC'";
                cmd.Parameters.AddWithValue("@nr", nr);
                resultTable.Load(cmd.ExecuteReader());
                conn.Close();
                if(resultTable.Rows.Count > 0)
                    result = load(resultTable.Rows[0]);
            }
            return result;
        }

        public Vagforening load(DataRow row)
        {

            Vagforening result = new Vagforening();

            result.leverantors_id = row["leverantors_id"]?.ToString();
            result.vaghallare = row["vaghallare"].ToString();
            result.orgnr = row["orgnr"]?.ToString();
            result.nr = row["nr"]?.ToString();
            result.ordforande = row["ordforande"]?.ToString();
            result.gatuadress = row["gatuadress"]?.ToString();
            result.postadress = row["postadress"]?.ToString();
            result.postnummer = row["postnummer"]?.ToString();
            result.telefon_he = row["telefon_he"]?.ToString();
            result.telefon_mo = row["telefon_mo"]?.ToString();
            result.bank__post = row["bank__post"]?.ToString();
            result.ordf_epost = row["ordf_epost"]?.ToString();
            result.kassor_namn = row["kassor_namn"]?.ToString();
            result.kassor_gatuadress = row["kassor_gatuadress"]?.ToString();
            result.kassor_postnummer = row["kassor_postnummer"]?.ToString();
            result.kassor_ort = row["kassor_ort"]?.ToString();
            result.kassor_telefon = row["kassor_telefon"]?.ToString();
            result.kassor_epost = row["kassor_epost"]?.ToString();
            if(row["antal_medlemmar"] != null && row["antal_medlemmar"].ToString() != "")
                result.antal_medlemmar = int.Parse(row["antal_medlemmar"].ToString());
            if(row["antal_permanentboende"] != null && row["antal_permanentboende"].ToString() != "")
                result.antal_permanentboende = int.Parse(row["antal_permanentboende"].ToString());

            return result;
        }

    }
}
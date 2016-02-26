using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using System.Web;
using System.IO;
using System.Configuration;

namespace KBA.TE.Models
{
    public class VFXMLPopulator
    {
        public static string xmlDirSetting = "KBA:xmldir";

        public static void approve(string file)
        {
            string xmlDir = ConfigurationManager.AppSettings[xmlDirSetting];
            if (File.Exists(xmlDir + "\\" + file)) {
                string dest = xmlDir + "\\approved\\" + file;
                File.Move(xmlDir + "\\" + file, dest);
            } else
            {
                throw new FileNotFoundException(xmlDir + "\\" + file + " does not exist.");
            }
        }

        public static void deny(string file)
        {
            string xmlDir = ConfigurationManager.AppSettings[xmlDirSetting];
            if (File.Exists(xmlDir + "\\" + file)) {
                string dest = xmlDir + "\\denied\\" + file;
                File.Move(xmlDir + "\\" + file, dest);
            }
            else
            {
                throw new FileNotFoundException(xmlDir + "\\" + file + " does not exist.");
            }
        }

        public IEnumerable<Vagforening> list()
        {
            List<Vagforening> result = new List<Vagforening>();

            string xmlDir = ConfigurationManager.AppSettings[xmlDirSetting];

            if (Directory.Exists(xmlDir))
            {
                string[] xmlFiles = Directory.GetFiles(xmlDir, "*.xml");
                foreach (string xmlFile in xmlFiles)
                {
                    Vagforening vf = load(xmlFile);
                    result.Add(vf);
                }
            }
            else
            {
                throw new Exception("Directory " + xmlDir + " does not exist. Check the \"" + xmlDirSetting + "\" setting.");
            }

            return result;
        }

        public Vagforening load(string path)
        {
            XPathDocument doc = new XPathDocument(path);
            XPathNavigator xn = doc.CreateNavigator();

            Vagforening result = new Vagforening();

            result.leverantors_id = xn.SelectSingleNode("/eformular/variabel[@namn='leverantors_id']")?.Value;
            result.vaghallare = xn.SelectSingleNode("/eformular/variabel[@namn='vagforening']").Value;
            result.orgnr = xn.SelectSingleNode("/eformular/variabel[@namn='org_nr']")?.Value;
            result.nr = xn.SelectSingleNode("/eformular/forminfo/integrationsvariabel[@namn='nr']")?.Value;
            result.ordforande = xn.SelectSingleNode("/eformular/variabel[@namn='ordforande']")?.Value;
            result.gatuadress = xn.SelectSingleNode("/eformular/variabel[@namn='gatuadress']")?.Value;
            result.postadress = xn.SelectSingleNode("/eformular/variabel[@namn='postadress']")?.Value;
            result.postnummer = xn.SelectSingleNode("/eformular/variabel[@namn='postnummer']")?.Value;
            result.telefon_he = xn.SelectSingleNode("/eformular/variabel[@namn='telefon_he']")?.Value;
            result.telefon_mo = xn.SelectSingleNode("/eformular/variabel[@namn='telefon_mo']")?.Value;
            result.bank__post = xn.SelectSingleNode("/eformular/variabel[@namn='bank_post']")?.Value;
            result.ordf_epost = xn.SelectSingleNode("/eformular/variabel[@namn='ordf_epost']")?.Value;
            result.kassor_namn = xn.SelectSingleNode("/eformular/variabel[@namn='kassor_namn']")?.Value;
            result.kassor_gatuadress = xn.SelectSingleNode("/eformular/variabel[@namn='kassor_gatuadress']")?.Value;
            result.kassor_postnummer = xn.SelectSingleNode("/eformular/variabel[@namn='kassor_postnummer']")?.Value;
            result.kassor_ort = xn.SelectSingleNode("/eformular/variabel[@namn='kassor_ort']")?.Value;
            result.kassor_telefon = xn.SelectSingleNode("/eformular/variabel[@namn='kassor_telefon']")?.Value;
            result.kassor_epost = xn.SelectSingleNode("/eformular/variabel[@namn='kassor_epost']")?.Value;
            if(xn.SelectSingleNode("/eformular/variabel[@namn='antal_medlemmar']") != null)
                result.antal_medlemmar = int.Parse(xn.SelectSingleNode("/eformular/variabel[@namn='antal_medlemmar']")?.Value);
            if (xn.SelectSingleNode("/eformular/variabel[@namn='antal_permanentboende']") != null)
                result.antal_permanentboende = int.Parse(xn.SelectSingleNode("/eformular/variabel[@namn='antal_permanentboende']")?.Value);
            result.modified = DateTime.Parse(xn.SelectSingleNode("/eformular/forminfo/tidsstampel")?.Value);

            result.uppgiftslamnare = xn.SelectSingleNode("/eformular/variabel[@namn='uppgNamn']")?.Value;
            result.uppgiftslamnare_epost = xn.SelectSingleNode("/eformular/variabel[@namn='uppgEmailAddress']")?.Value;
            result.uppgiftslamnare_telefon = xn.SelectSingleNode("/eformular/variabel[@namn='uppgPhoneNumber']")?.Value;

            result.path = path.Substring(path.LastIndexOf("\\") + 1);

            XPathNodeIterator files = xn.Select("/eformular/bifogadfil");
            Dictionary<string, string> docs = new Dictionary<string, string>();
            foreach (XPathNavigator f in files)
            {
                string fileName = f.SelectSingleNode("filnamn").Value;
                string desc = f.SelectSingleNode("originalfilnamn").Value;
                docs.Add(desc, fileName);
            }
            result.documents = docs;

            return result;
        }
    }
}
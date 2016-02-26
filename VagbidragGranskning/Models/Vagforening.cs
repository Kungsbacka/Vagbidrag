using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace KBA.TE.Models
{
    [DataContract]
    public class Vagforening
    {
        [DataMember]
        public DateTime modified { get; set; } = DateTime.Now;

        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string geom { get; set; }
        [DataMember]
        public string bbox { get; set; }
        [DataMember]
        public string nr { get; set; }
        [DataMember]
        public string typ { get; set; }
        [DataMember]
        public string vaghallare { get; set; }
        [DataMember]
        public string orgnr { get; set; }
        [DataMember]
        public DateTime? approved_date { get; set; }
        [DataMember]
        public DateTime? export_date { get; set; }
        [DataMember]
        public string ordforande { get; set; }
        [DataMember]
        public string gatuadress { get; set; }
        [DataMember]
        public string postadress { get; set; }
        [DataMember]
        public string postnummer { get; set; }
        [DataMember]
        public string telefon_he { get; set; }
        [DataMember]
        public string telefon_mo { get; set; }
        [DataMember]
        public string fd_k_nr { get; set; }
        [DataMember]
        public string bank__post { get; set; }
        [DataMember]
        public string fnr { get; set; }
        [DataMember]
        public string lopnr { get; set; }
        [DataMember]
        public string belag { get; set; }
        [DataMember]
        public string nr1 { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string link { get; set; }
        [DataMember]
        public string leverantors_id { get; set; }
        [DataMember]
        public double bidragsberattigad_vaglangd { get; set; }
        [DataMember]
        public double bidragsberattigad_vaglangd_gc { get; set; }
        [DataMember]
        public string statligt_vagnr { get; set; }
        [DataMember]
        public string ordf_epost { get; set; }
        [DataMember]
        public string kassor_namn { get; set; }
        [DataMember]
        public string kassor_gatuadress { get; set; }
        [DataMember]
        public string kassor_postnummer { get; set; }
        [DataMember]
        public string kassor_ort { get; set; }
        [DataMember]
        public string kassor_telefon { get; set; }
        [DataMember]
        public string kassor_epost { get; set; }
        [DataMember]
        public int antal_medlemmar { get; set; }
        [DataMember]
        public int antal_permanentboende { get; set; }
        [DataMember]
        public string ga_beteckning { get; set; }
        [DataMember]
        public long statligt_bidrag { get; set; }
        [DataMember]
        public string path { get; set; }
        [DataMember]    
        public string uppgiftslamnare { get; set; }
        [DataMember]
        public string uppgiftslamnare_epost { get; set; }
        [DataMember]
        public string uppgiftslamnare_telefon { get; set; }
        [DataMember]
        public IDictionary<string, string> documents { get; set; }

    }
}
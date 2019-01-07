using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VagbidragGranskning.Models
{
    public class VagbidragAnsokan
    {
        public int vf_id { get; set; }
        public int ar { get; set; }
        public DateTime inkom { get; set; }
        public DateTime godkand { get; set; }
        public DateTime exporterad { get; set; }
        public string filnamn { get; set; }
        public byte[] fildata { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VagbidragGranskning.Models
{
    public class ReportQuery
    {
        public int year { get; set; }

        public bool all { get; set; } = false;

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTopCovidCases.Models
{
    public class reports
    {
        public string date { get; set; }
        public double confirmed { get; set; }

        public double deaths { get; set; }

        public double recovered { get; set; }

        public provinces region { get; set; }

    }

    public class reportslists
    {
        public List<reports> data { get; set; }
    }

    public class reportSum
    {
        public string date { get; set; }

        public string region { get; set; }
        public double confirmed { get; set; }

        public double deaths { get; set; }

        public double recovered { get; set; }

        
    }
    public class reportSumlist
    {
        public List<reportSum> data { get; set; }

    }
}
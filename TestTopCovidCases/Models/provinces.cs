using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTopCovidCases.Models
{
    public class provinces
    {
        public string iso { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public string lat { get; set; }
        public string longi { get; set; }

    }

    public class provinceslists
    {
        public List<provinces> data { get; set; }

    }
}
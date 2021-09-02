using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTopCovidCases.Models
{
    public class regions { 
        
        public string iso { get; set; }
        public string name { get; set; }
        
    }
    public class regionslists
    {

        public List<regions> data { get; set; }

    }
}